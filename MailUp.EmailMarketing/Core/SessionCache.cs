using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using RestSharp;
using System;
using System.Collections.Generic;

namespace MailUp.EmailMarketing.Core
{
    public class SessionCache : ISessionCache, IDisposable
    {
        private string _name { get; set; }
        private readonly RestRequest _request;
        private readonly RestResponse _response;

        public AuthorizationModel CacheModel { get; set; }
        private IList<RestResponseCookie> cookies;
        private CookieOptions option;

        public SessionCache(string name)
        {
            this._name = name;

            //option = new CookieOptions();
            //option.HttpOnly = true;
            //option.IsEssential = true;

            this._request = new RestRequest();
            this._response = new RestResponse();

            /// Model over Application for authorizzation return by MailUp. Manager Session to cookies.
            CacheModel = new AuthorizationModel();

        }
        private String ExtractJsonValue(string json, string name)
        {
            String delim = "\"" + name + "\":";
            int start = json.IndexOf(delim) + delim.Length;
            int end1 = json.IndexOf("\"", start + 1);
            if (end1 < 0) end1 = 100000;
            int end2 = json.IndexOf(",", start + 1);
            if (end2 < 0) end2 = 100000;
            int end3 = json.IndexOf("}", start + 1);

            int end = Math.Min(Math.Min(end1, end2), end3);

            if (end > start && start > -1 && end > -1)
            {
                String result = json.Substring(start, end - start);
                if (result.StartsWith("\""))
                {
                    return json.Substring(start + 1, end - start - 1);
                }
                else
                {
                    return result;
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>  
        /// Get the cookie  
        /// </summary>  
        /// <param name="key">Key </param>  
        /// <returns>string value</returns>  
        private string Get(string key)
        {
            return this._request.Parameters.Find(a => a.Name == key).ToString();
        }

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        private void Set(string key, string value, double expireTime)
        {
            DateTime expires;
            try
            {
                if (expireTime > 0)
                    expires = DateTime.Now.AddMinutes(expireTime);
                else
                    expires = DateTime.Now.AddMilliseconds(10);
                
                this._response.Cookies.Add(
                    new RestResponseCookie() { Name = key, Value = value, Expires = expires }
                );
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        private void Remove(string key)
        {
            cookies = this._response.Cookies;
            cookies.Remove(new RestResponseCookie() { Name = key });
        }

        public virtual void LoadCache()
        {
            //string cookie = coockies["MailUpCookie"];
            CacheModel.Access_Token = Get(nameof(CacheModel.Access_Token));
            CacheModel.Refresh_Token = Get(nameof(CacheModel.Refresh_Token));
            CacheModel.Token_type = Get(nameof(CacheModel.Token_type));
            CacheModel.Expires_In = Get(nameof(CacheModel.Expires_In));
            //Model.ExpirationTime = Convert.ToDateTime(Get(nameof(Model.ExpirationTime)));
        }
        public virtual void SaveCache()
        {
            //cookie.Expires = DateTime.Now.AddDays(30);


            Set(nameof(CacheModel.Access_Token), CacheModel.Access_Token, Convert.ToDouble(CacheModel.Expires_In));
            Set(nameof(CacheModel.Refresh_Token), CacheModel.Refresh_Token, Convert.ToDouble(CacheModel.Expires_In));
            Set(nameof(CacheModel.Token_type), CacheModel.Token_type ?? "Bearer", Convert.ToDouble(CacheModel.Token_type));
            Set(nameof(CacheModel.Expires_In), CacheModel.Expires_In, Convert.ToDouble(CacheModel.Expires_In));
            //Set(nameof(Model.ExpirationTime), SetTokenExpirationTime(Model.Expires_In), Convert.ToInt32(Model.Expires_In));
            //Convert.ToInt32(DateTime.Now.AddDays(30))) ; // ;

        }
        public virtual void DeleteCache()
        {
            Remove(nameof(CacheModel.Access_Token));
            Remove(nameof(CacheModel.Refresh_Token));
            Remove(nameof(CacheModel.Token_type));
            Remove(nameof(CacheModel.Expires_In));
            //Remove(nameof(Model.ExpirationTime));
        }
        private Int64 ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return Convert.ToInt64((date.ToUniversalTime() - epoch).TotalSeconds);
        }
        private string SetTokenExpirationTime(string expirationTime)
        {
            int exp = Convert.ToInt32(expirationTime);
            return ToUnixTime(DateTime.Now.AddSeconds(exp)).ToString();
        }

        public void Dispose()
        {
            DeleteCache();

            CacheModel = null;
            cookies = null;
            option = null;
        }

    }
}

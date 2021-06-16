using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Core;
using MailUp.EmailMarketing.Model;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Core
{
    public class TokenBase : SessionCache, ITokenBase
    {
        private readonly MailUpConfigurations.MailUpApiv1 _config;

        public TokenBase()
            : base("MailUp.EmailMarketing.Service.Cache")
        {
            this._config = new MailUpConfigurations.MailUpApiv1();
        }
        public TokenBase(MailUpConfigurations.MailUpApiv1 config)
            : base("MailUp.EmailMarketing.Service.Cache")
        {
            this._config = config;
        }

        public async Task<TokenResponse> GetToken(string username, string password)
        {
            string url = $"{this._config.MailUpToken}";

            //create RestSharp client and POST request object
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.POST);

            //add GetToken() API method parameters
            request.Parameters.Clear();

            string client_id = this._config.MailUpClientId;
            string client_secret = this._config.MailUpClientSecret;

            string ContentType = "application/x-www-form-urlencoded";
            request.AddHeader("Content-Type", ContentType);

            string authToken = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{client_id}:{client_secret}"));
            request.AddHeader("Authorization", $"Basic {authToken}");
            
            string body = $"client_id={client_id }&client_secret={client_secret}" +
                          $"&grant_type=password&username={Uri.EscapeDataString(username)}" +
                          $"&password={Uri.EscapeDataString(password)}";
            
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            //make the API request and get the response
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                TokenResponse res = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(response.Content);
                
                CacheModel.Access_Token = res.access_token;
                CacheModel.Refresh_Token = res.refresh_token;
                CacheModel.Expires_In = res.expires_in.ToString();

                SaveCache();
                
                return res;
            }
            else
            {
                throw new ExceptionToken(response.StatusCode.ToString(), response.Content ?? response.ErrorMessage );
            }
        }

        public string  GetTokenInfo(string callbackUri)
        {
            string url = "";
            if (callbackUri == "")
                url = $"{this._config.MailUpToken}?client_id={this._config.MailUpClientId}&client_secret={this._config.MailUpClientSecret}&response_type=code";
            else
                url = $"{this._config.MailUpToken}?client_id={this._config.MailUpClientId}&client_secret={this._config.MailUpClientSecret}&response_type=code&redirect_uri={callbackUri}";

            return url;
        }

        public async Task<TokenResponse> GetTokenWithCode(string code)
        {
            string url = $"{this._config.MailUpToken}?code={code}&grant_type=authorization_code";

            //create RestSharp client and GET request object
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.GET);

            //make the API request and get the response
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                TokenResponse res = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(response.Content);

                CacheModel.Access_Token = res.access_token;
                CacheModel.Refresh_Token = res.refresh_token;
                CacheModel.Expires_In = res.expires_in.ToString();

                SaveCache();
                
                return res;
            }
            else
            {
                throw new ExceptionToken(response.StatusCode.ToString(), response.Content ?? response.ErrorMessage);
            }
            ////catch (HttpRequestException hre)
            ////{
            ////    throw new MailUpException(hre.StatusCode, hre.Message);
            ////}
            ////catch (Exception ex)
            ////{
            ////    throw new Exception($"StatusCode: {response.error} - Error: {ex.Message} | {response.error_description}");
            ////}
        }

        public async Task<TokenResponse> RenewToken(string refreshcode)
        {
            string url = $"{this._config.MailUpToken}";

            //create RestSharp client and POST request object
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.POST);

            //add GetToken() API method parameters
            request.Parameters.Clear();

            string client_id = this._config.MailUpClientId;
            string client_secret = this._config.MailUpClientSecret;

            string ContentType = "application/x-www-form-urlencoded";
            request.AddHeader("Content-Type", ContentType);

            string authToken = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{client_id}:{client_secret}"));
            request.AddHeader("Authorization", $"Basic {authToken}");

            string body = $"client_id={client_id }&client_secret={client_secret}" +
                          $"&grant_type=refresh_token" +
                          $"&refresh_token={refreshcode}";

            request.AddParameter("application/json", body, ParameterType.RequestBody);

            //make the API request and get the response
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                TokenResponse res = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(response.Content);

                CacheModel.Access_Token = res.access_token;
                CacheModel.Refresh_Token = res.refresh_token;
                CacheModel.Expires_In = res.expires_in.ToString();

                SaveCache();

                return res;
            }
            else
            {
                throw new ExceptionToken(response.StatusCode.ToString(), response.Content ?? response.ErrorMessage);
            }
        }

    }
}
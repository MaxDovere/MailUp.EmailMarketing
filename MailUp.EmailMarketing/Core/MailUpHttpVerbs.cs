using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Error;
using MailUp.EmailMarketing.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestSharp.Extensions;
using System.Linq;
using System.Collections.Specialized;
using System.Web;
using System.Net;
using System.Net.Mime;
using ContentType = RestSharp.Serialization.ContentType;

namespace MailUp.EmailMarketing.Core
{
    internal class MailUpHttpVerbs: IMailUpHttpVerbs
    {
        private readonly RestClient _client;

        public MailUpHttpVerbs()
        {
            this._client = new RestClient();
        }

        private string FormatQueryString(string uri, string sortExpression, string filterExpression, int? pageSize, int? pageNumber)
        {
            Uri uri2 = new Uri(uri);
            NameValueCollection queryString = HttpUtility.ParseQueryString(uri2.Query);
            if (!string.IsNullOrWhiteSpace(filterExpression))
                queryString["filterby"] = "\"" + filterExpression + "\"";
            if (!string.IsNullOrWhiteSpace(sortExpression))
                queryString["orderby"] = "\"" + sortExpression + "\"";
            if (pageSize.HasValue)
                queryString[nameof(pageSize)] = pageSize.ToString();
            if (pageNumber.HasValue)
                queryString[nameof(pageNumber)] = pageNumber.ToString();
            if (queryString.Count > 0)
                uri = uri + "?" + queryString.ToString();
            else
                uri = uri2.AbsoluteUri;
            
            return uri;
        }
        private string CreateUriComplete(string url, bool consoleEndpoint = true)
        {
            string uri = url;
            string main_path = url.StartsWith("http:") ? url : (consoleEndpoint ? MailUpUriConstants.ConsoleEndpoint : MailUpUriConstants.MailStatisticsEndpoint);
            if (!uri.StartsWith(main_path))
            {
                string str2 = main_path;
                if (!uri.StartsWith("/"))
                    str2 += "/";
                uri = str2 + uri;
            }
            return uri;
        }
        public async Task<T> CallMethodAsync<T>(Method verb, string url, object requestData, string scheme, string authorizedToken, bool consoleEndpoint = true) where T : class
        {
            string uri = CreateUriComplete(url, consoleEndpoint);

            RestClient client = new RestClient(uri);

            var request = new RestRequest(uri, verb);

            if (scheme.HasValue() && authorizedToken.HasValue())
            {
                AuthenticationHeaderValue header = new AuthenticationHeaderValue(scheme: scheme, authorizedToken);
                request.AddHeader("Authorization", header.ToString());
            }

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json;utf-8");
            
            switch (verb)
            {
                case Method.GET:
                case Method.HEAD:
                    request.AddHeader("Content-Type", "application/json;utf-8");
                    break;
                case Method.POST:
                    if (requestData != null)
                    {
                        request.AddHeader("content-type", "application/x-www-form-urlencoded");
                        request.AddJsonBody(requestData);
                    }
                    break;
                case Method.PUT:
                    break;
                case Method.DELETE:
                    break;
                case Method.OPTIONS:
                    throw new NotImplementedException();
                case Method.PATCH:
                    throw new NotImplementedException();
                default:
                    break;
            }

            IRestResponse<T> response= new RestResponse<T>();
            
            response.ContentType = ContentType.Json;
            response.ContentEncoding = "utf-8";
            
            response = await client.ExecuteAsync<T>(request).ConfigureAwait(false);

            string contect = response.Content;

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    T result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(contect);
                    return result;
                case HttpStatusCode.NoContent:
                case HttpStatusCode.NotFound:
                    break;
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.InternalServerError:
                    throw new Exception(contect);
                default:
                    break;
            }

            switch (response)
            {
                case null:
                    return default(T);
                case T _:
                    return response as T;
                case Exception _:
                    throw new MailUpException(0, (response as Exception).Message);
                case Dictionary<string, object> _:
                    throw new MailUpException(0, string.Join(",", (response as Dictionary<string, object>).Select<KeyValuePair<string, object>, string>((Func<KeyValuePair<string, object>, string>)(kvp => string.Format("{0}:{1}", (object)kvp.Key, kvp.Value)))));
                default:
                    throw new MailUpException(0, response.Content.ToString());
            }
        }
        public async Task<T> CallMethodAsync<T>(Method verb, string url, object requestData, string scheme, string authorizedToken, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null, bool consoleEndpoint = true) where T : class
        {
            string uri = CreateUriComplete(url, consoleEndpoint);

            uri = FormatQueryString(uri, sortExpression, filterExpression, pageSize, pageNumber);

            return this.CallMethodAsync<T>(verb, uri, requestData, scheme, authorizedToken, consoleEndpoint).Result;
        }
        public long CallMethodWithNumericResponseAsync(Method verb, string url, object requestData, string scheme, string authorizedToken, bool consoleEndpoint = true)
        {
            string uri = CreateUriComplete(url, consoleEndpoint);
            
            var request = new RestRequest(uri, verb);

            if (scheme.HasValue() && authorizedToken.HasValue())
            {
                AuthenticationHeaderValue header = new AuthenticationHeaderValue(scheme: scheme, authorizedToken);
                request.AddHeader("Authorization", header.ToString());
            }

            request.RequestFormat = DataFormat.Json;

            switch (verb)
            {
                case Method.GET:
                case Method.HEAD:
                    break;
                case Method.POST:
                    if (requestData != null)
                        request.AddJsonBody(requestData);
                    break;
                case Method.PUT:
                    break;
                case Method.DELETE:
                    break;
                case Method.OPTIONS:
                    throw new NotImplementedException();
                case Method.PATCH:
                    throw new NotImplementedException();
                default:
                    break;
            }

            object response = this._client.ExecuteAsync(request).Result;

            if (!((IRestResponse)response).IsSuccessful)
            {
                throw new Exception(((IRestResponse)response).Content);
            }

            switch (response)
            {
                case null:
                    return 0;
                case long num:
                    return num;
                case Exception _:
                    throw new MailUpException(0, (response as Exception).Message);
                case Dictionary<string, object> _:
                    throw new MailUpException(0, string.Join(",", (response as Dictionary<string, object>).Select<KeyValuePair<string, object>, string>((Func<KeyValuePair<string, object>, string>)(kvp => string.Format("{0}:{1}", (object)kvp.Key, kvp.Value)))));
                default:
                    throw new MailUpException(0, ((IRestResponse)response).Content.ToString());
            }
        }
        public long CallMethodWithNumericResponseAsync(Method verb, string url, object requestData, string scheme, string authorizedToken, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null, bool consoleEndpoint = true)
        {
            string uri = CreateUriComplete(url, consoleEndpoint);

            uri = FormatQueryString(uri, sortExpression, filterExpression, pageSize, pageNumber);

            return CallMethodWithNumericResponseAsync(verb, uri, requestData, scheme, authorizedToken, consoleEndpoint);
        }
        public async Task<T> CallDirectMethodAsync<T>(Method verb, string url, object requestData, string scheme, string authorizedToken, bool consoleEndpoint = true) where T : class
        {
            RestRequest restRequest = new RestRequest(url, verb);
            restRequest.RequestFormat = DataFormat.Json;
            if(requestData != null) restRequest.AddJsonBody(requestData);
            IRestResponse restResponse;
            try
            {
                restResponse = await this._client.ExecuteAsync(restRequest).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new MailUpException(0, ex.Message);
            }
            string json = restResponse.StatusCode == HttpStatusCode.OK ? restResponse.Content : throw new MailUpException(restResponse.StatusCode, restResponse.ErrorMessage);
            if (string.IsNullOrEmpty(json))
                return default(T);
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw new MailUpException(0, ex.Message);
            }
        }
    }
}

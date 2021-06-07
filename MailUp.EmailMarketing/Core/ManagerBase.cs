using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Entities;
using MailUp.EmailMarketing.Error;
using MailUp.EmailMarketing.Model;
using RestSharp;

namespace MailUp.EmailMarketing.Core
{
    public class ManagerBase : IManagerBase
    {
        private readonly AuthorizationModel _authorizedToken;
        private readonly ConfigurationsMailUp.MailUpApiv1 _config;
        public ManagerBase(AuthorizationModel authorizedToken, ConfigurationsMailUp.MailUpApiv1 config)
        {
            _config = config;
            _authorizedToken = authorizedToken;
        }
        public AuthorizationModel AuthorizedUser { get => _authorizedToken; init => _authorizedToken = value; }

        public T CallMethod<T>(Method verb, string scheme, string url, object requestData, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null, bool consoleEndpoint = true) where T : class
        {
            string uri = url;

            string new_uri = consoleEndpoint ? Constants.ConsoleEndpoint : Constants.MailStatisticsEndpoint;

            IMailUpHttpVerbs _http = new MailUpHttpVerbs(new Uri(new_uri));
            object obj = new Object();

            var request = new RestRequest(url, verb);
            AuthenticationHeaderValue header = new AuthenticationHeaderValue(scheme: scheme, _authorizedToken.Access_Token);
            
            //request.AddHeader("x-api-key", _apiToken);
            //request.AddHeader("x-api-user", _apiUser);
            //var jsonObj = JsonConvert.SerializeObject(addedTask);            

            request.RequestFormat = DataFormat.Json;

            switch (verb)
            {
                case Method.GET:
                case Method.HEAD:
                    obj = _http.GetDataAsync(header, request);
                    break;
                case Method.POST:
                    if (requestData != null)
                        request.AddJsonBody(requestData);

                    obj = _http.PostDataAsync(header, request);
                    break;
                case Method.PUT:
                    obj = _http.PutDataAsync(header, request);
                    break;
                case Method.DELETE:
                    obj = _http.DeleteDataAsync(header, request);
                    break;
                case Method.OPTIONS:
                    throw new NotImplementedException();
                    break;
                case Method.PATCH:
                    throw new NotImplementedException();
                default:
                    break;
            }

            //object obj;// = this._oauth2Client.RequestResource<T>(uri, verb.ToString(), requestData, filterExpression, sortExpression, pageSize, pageNumber);
            switch (obj)
            {
                case null:
                    return default(T);
                case T _:
                    return obj as T;
                case Exception _:
                    throw new MailUpException(0, (obj as Exception).Message);
                case Dictionary<string, object> _:
                    throw new MailUpException(0, string.Join(",", (obj as Dictionary<string, object>).Select<KeyValuePair<string, object>, string>((Func<KeyValuePair<string, object>, string>)(kvp => string.Format("{0}:{1}", (object)kvp.Key, kvp.Value)))));
                default:
                    throw new MailUpException(0, obj.ToString());
            }
        }

        public long CallMethodWithNumericResponse(Method verb, string scheme, string url, object requestData, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null, bool consoleEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public List<EntityUserInfo> ListUserInfo(string username, string password, string userToRead = "")
        {
            throw new NotImplementedException();
        }

        public string PrepareURLToSendEmail(string action, string senderName, string senderAddress, DateTime? scheduledTime)
        {
            throw new NotImplementedException();
        }

        T IManagerBase.CallDirectMethod<T>(Method verb, string scheme, string url, object requestData)
        {
            throw new NotImplementedException();
        }
    }
}

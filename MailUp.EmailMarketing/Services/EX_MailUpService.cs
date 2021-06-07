using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Core;
using MailUp.EmailMarketing.Entities;
using MailUp.EmailMarketing.Error;
using MailUp.EmailMarketing.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Services
{

    public class EX_MailUpService : SessionCache //, IMailUpService
    {
        private readonly ConfigurationsMailUp.MailUpApiv1 _configMailUpApiv1;

        public EX_MailUpService(ConfigurationsMailUp.MailUpApiv1 configMailUpApiv1)
            : base("MailUpSessionCache")
        {
            {
                this._configMailUpApiv1 = configMailUpApiv1;
            }

        }


        //private async Task<HttpResponseMessage> RequestAsync(HttpMethod method, string baseUri, AuthenticationHeaderValue authorization, string body, HeaderDictionary headers)
        //{
        //    try
        //    {
        //        //Informational responses(100–199)
        //        //Successful responses(200–299)
        //        //Redirects(300–399)
        //        //Client errors(400–499)
        //        //Server errors(500–599)

        //        string ContentType = method == HttpMethod.Post ? "application/x-www-form-urlencoded" : "application/json";

        //        int _TimeoutSec = 90;
        //        this._client.BaseAddress = new Uri(baseUri);
        //        this._client.Timeout = new TimeSpan(0, 0, _TimeoutSec);

        //        this._client.DefaultRequestHeaders.Add("User-Agent", "MailUp.EmailMarketing_Library");
        //        this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
        //        if (authorization != null) this._client.DefaultRequestHeaders.Add("Authorization", $"{authorization}");
        //        if (headers != null)
        //            foreach (var h in headers)
        //                this._client.DefaultRequestHeaders.Add(h.Key, $"{h.Value}");

        //        HttpResponseMessage response;

        //        switch (method.ToString().ToUpper())
        //        {
        //            case "GET":
        //            case "HEAD":
        //                // synchronous request without the need for .ContinueWith() or await
        //                response = await this._client.GetAsync(baseUri).ConfigureAwait(false);
        //                break;
        //            case "POST":
        //                {
        //                    // Construct an HttpContent from a StringContent
        //                    HttpContent _Body = new StringContent(body);
        //                    // and add the header to this object instance
        //                    // optional: add a formatter option to it as well
        //                    _Body.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
        //                    // synchronous request without the need for .ContinueWith() or await
        //                    response = await this._client.PostAsync(baseUri, _Body).ConfigureAwait(false);
        //                }
        //                break;
        //            case "PUT":
        //                {
        //                    // Construct an HttpContent from a StringContent
        //                    HttpContent _Body = new StringContent(body);
        //                    // and add the header to this object instance
        //                    // optional: add a formatter option to it as well
        //                    _Body.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
        //                    // synchronous request without the need for .ContinueWith() or await
        //                    response = await this._client.PutAsync(baseUri, _Body).ConfigureAwait(false);
        //                }
        //                break;
        //            case "DELETE":
        //                response = await this._client.DeleteAsync(baseUri).ConfigureAwait(false);
        //                break;
        //            default:
        //                throw new NotImplementedException();
        //        }

        //        response.EnsureSuccessStatusCode();

        //        return await Task.FromResult(response);
        //    }
        //    catch (MailUpException mex)
        //    {
        //        switch (mex.StatusCode.ToString())
        //        {
        //            case "201":
        //            case "401":
        //            case "403":
        //            case "500":
        //            default:
        //                throw new MailUpException(mex.StatusCode, mex.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        //private T Deserialize<T>(Stream s)
        //{
        //    try
        //    {
        //        using (StreamReader reader = new StreamReader(s))
        //        using (JsonTextReader jsonReader = new JsonTextReader(reader))
        //        {

        //            JsonSerializer ser = new JsonSerializer();
        //            ser.NullValueHandling = NullValueHandling.Ignore;
        //            ser.DefaultValueHandling = DefaultValueHandling.Include;
        //            ser.MissingMemberHandling = MissingMemberHandling.Ignore;
        //            ser.Formatting = Formatting.None;

        //            return ser.Deserialize<T>(jsonReader);
        //        }
        //    }
        //    catch (JsonSerializationException jex)
        //    {
        //        throw new JsonSerializationException(jex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}
        //private T Deserialize<T>(string s)
        //{
        //    try
        //    {
        //        JsonSerializerSettings setting = new JsonSerializerSettings();
        //        setting.NullValueHandling = NullValueHandling.Ignore;
        //        setting.DefaultValueHandling = DefaultValueHandling.Include;
        //        setting.MissingMemberHandling = MissingMemberHandling.Ignore;
        //        setting.Formatting = Formatting.None;

        //        return JsonConvert.DeserializeObject<T>(s, setting);
        //    }
        //    catch (JsonSerializationException jex)
        //    {
        //        throw new JsonSerializationException(jex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        //private string Serialize<T>(T obj)
        //{
        //    try
        //    {
        //        JsonSerializerSettings formatting = new JsonSerializerSettings();
        //        formatting.NullValueHandling = NullValueHandling.Ignore;
        //        formatting.DefaultValueHandling = DefaultValueHandling.Include;
        //        formatting.MissingMemberHandling = MissingMemberHandling.Ignore;
        //        formatting.Formatting = Formatting.None;

        //        return JsonConvert.SerializeObject(obj, formatting);
        //    }
        //    catch (JsonSerializationException jex)
        //    {
        //        throw new JsonSerializationException(jex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}

       

        public async Task<ResponseLists> ConsoleReadListAll()
        {
            //  https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/List

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List";

            IRestResponse response = new RestResponse();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                response = await new HttpVerbs(new Uri(url)).GetDataAsync(authorization);
                
                if (response.IsSuccessful)
                {
                    ResponseLists entities = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseLists>(response.Content);
                    return await Task.FromResult(entities);
                }
                else
                {
                    throw new MailUpException(response.StatusCode, response.ErrorMessage);
                }
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.HResult} - Error: {ex.Message}");
            }
        }
        public async Task<ResponseList> ConsoleReadDetailList(int listid)
        {
            // https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/List/{id_List}

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List/{listid}";

            IRestResponse response = new RestResponse();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                response = await new HttpVerbs(new Uri(url)).GetDataAsync(authorization);

                if (response.IsSuccessful)
                {
                    ResponseList entity = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseList>(response.Content);
                    return await Task.FromResult(entity);
                }
                else
                {
                    throw new MailUpException(response.StatusCode, response.ErrorMessage);
                }
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.HResult} - Error: {ex.Message}");
            }
        }
        public async Task<ResponseCreateList> ConsoleCreateList(RequestList list)
        {
            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List";

            IRestResponse response = new RestResponse();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                ListRequest request = new ListRequest();

                string body = Newtonsoft.Json.JsonConvert.SerializeObject(list);

                request.Collection = body;

                response = await new HttpVerbs(new Uri(url)).GetDataAsync(authorization, request);

                if (response.IsSuccessful)
                {
                    ResponseCreateList entity = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseCreateList>(response.Content);
                    return await Task.FromResult(entity);
                }
                else
                {
                    throw new MailUpException(response.StatusCode, response.ErrorMessage);
                }
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.HResult} - Error: {ex.Message}");
            }

         }
        public async Task<ResponseList> ConsoleUpdateList(RequestList list)
        {
            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                string body = Serialize<RequestList>(list);

                response = await RequestAsync(HttpMethod.Put, url, authorization, body, null);

                string content = await response.Content.ReadAsStringAsync();

                ResponseList entity = Deserialize<ResponseList>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<ResponseList> ConsoleDeleteList(int listid)
        {
            //  https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/List/{id_List}

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List/{listid}";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                response = await RequestAsync(HttpMethod.Delete, url, authorization, null, null);

                string content = await response.Content.ReadAsStringAsync();

                ResponseList entity = Deserialize<ResponseList>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<bool> ConsoleDeleteListIfExists(int listid, string listGuid)
        {
            //  https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/List/{id_List}

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List/{listid}";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                KeyValuePair<string, StringValues> header = new KeyValuePair<string, StringValues>("if-match", $"{listGuid}");
                HeaderDictionary headers = new HeaderDictionary();
                headers.Add(header);

                response = await RequestAsync(HttpMethod.Delete, url, authorization, null, headers);

                response.EnsureSuccessStatusCode();

                switch (response.StatusCode.ToString())
                {
                    case "403":
                        ////if IF - MATCH header missing
                        ////    403 Forbidden


                        ////    {
                        ////                    "ErrorCode": "403",
                        ////        "ErrorDescription": "Missing If-Match header.",
                        ////        "ErrorName": "Forbidden",
                        ////        "ErrorStack": null
                        ////    }
                        throw new Exception("403 Forbidden");
                    case "412":
                        ////if IF - MATCH contains a wrong value(i.e., if-match: aaaa)
                        ////    412 PreconditionFailed


                        ////    {
                        ////                    "ErrorCode": "412",
                        ////        "ErrorDescription": "If-Match header content type must be: System.Guid.",
                        ////        "ErrorName": "PreconditionFailed",
                        ////        "ErrorStack": null
                        ////    }

                        ////if IF - MATCH contains a value that does not match to provided id_list
                        ////    412 PreconditionFailed


                        ////    {
                        ////                    "ErrorCode": "412",
                        ////        "ErrorDescription": "Invalid [listGuid] from If-Match header field.",
                        ////        "ErrorName": "PreconditionFailed",
                        ////        "ErrorStack": null
                        ////    }
                        break;
                    default:
                        break;
                }

                return await Task.FromResult(true);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }

        public async Task<ResponseGroup> ConsoleCreateGroup(int listid, RequestGroup group)
        {
            //  https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/List/{ID_LIST}/Group

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List/{listid}/Group";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                string body = Serialize<RequestGroup>(group);

                response = await RequestAsync(HttpMethod.Post, url, authorization, body, null);

                string content = await response.Content.ReadAsStringAsync();

                ResponseGroup entity = Deserialize<ResponseGroup>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<ResponseLists> ConsoleReadGroupsInList(int listid)
        {
            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List/{listid}/Groups";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                response = await RequestAsync(HttpMethod.Get, url, authorization, null, null);

                string content = await response.Content.ReadAsStringAsync();

                ResponseLists entity = Deserialize<ResponseLists>(content);

                entity.BaseUri = url;

                return await Task.FromResult(entity);
                //    new ResponseMessage() 
                //{
                //    Content = content,
                //    Type = "json"
                //});
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<ResponseGroups> ConsoleReadGroupsInListPagined(int listid, int pageNumer = 0, int pageSize = 20)
        {
            //  https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/List/{ID_LIST}/Groups

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List/{listid}/Groups?PageNumber={pageNumer}&PageSize={pageSize}";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                response = await RequestAsync(HttpMethod.Get, url, authorization, null, null);

                string content = await response.Content.ReadAsStringAsync();

                ResponseGroups entities = Deserialize<ResponseGroups>(content);

                return await Task.FromResult(entities);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<ResponseGroup> ConsoleUpdateGroup(int listid, int groupid, RequestGroup group)
        {
            //  https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/List/{ID_LIST}/Group/{ID_GROUP}
            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List/{listid}/Group/{groupid}";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                string body = Serialize<RequestGroup>(group);

                response = await RequestAsync(HttpMethod.Put, url, authorization, body, null);

                string content = await response.Content.ReadAsStringAsync();

                ResponseGroup entity = Deserialize<ResponseGroup>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<bool> ConsoleDeleteGroup(int listid, int groupid)
        {
            //  https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/List/{ID_LIST}/Group/{ID_GROUP}

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List/{listid}/Group/{groupid}";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                response = await RequestAsync(HttpMethod.Delete, url, authorization, null, null);

                return await Task.FromResult(true);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }

        public async Task<ResponseItemEmail> ConsoleTrustedSendersEmail(string email)
        {
            // 	https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/TrustedSenders

            RequestEmail requestEmail = new RequestEmail();
            requestEmail.Email = email;

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/TrustedSenders";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                string body = Serialize<RequestEmail>(requestEmail);

                response = await RequestAsync(HttpMethod.Post, url, authorization, body, null);

                string content = await response.Content.ReadAsStringAsync();

                ResponseItemEmail entity = Deserialize<ResponseItemEmail>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<ResponseEmail> ConsoleTrustedSendersEmail()
        {
            //  The table below shows that you have to consider in your integration.
            //  0   Not confirmed   You have just requested to trust an email address
            //  1   Confirmed Sender email address validated
            //  2   Blocked You disable a trusted sender
            //  The table above does not display statuses that means Ok, your sender's email address is trusted!

            // 	https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/TrustedSenders

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/TrustedSenders";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                response = await RequestAsync(HttpMethod.Get, url, authorization, null, null);

                string content = await response.Content.ReadAsStringAsync();

                ResponseEmail entity = Deserialize<ResponseEmail>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<ResponseItemEmail> ConsoleTrustedSendersEmail(int idTrustedSender)
        {
            //  The table below shows that you have to consider in your integration.
            //  0   Not confirmed   You have just requested to trust an email address
            //  1   Confirmed Sender email address validated
            //  2   Blocked You disable a trusted sender
            //  The table above does not display statuses that means Ok, your sender's email address is trusted!

            // 	https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/TrustedSenders/{IdTrustedSender}

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/TrustedSenders/{idTrustedSender}";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                response = await RequestAsync(HttpMethod.Post, url, authorization, null, null);

                string content = await response.Content.ReadAsStringAsync();

                ResponseItemEmail entity = Deserialize<ResponseItemEmail>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<ResponseItemEmail> ConsoleDeleteTrustedSendersEmail(int idTrustedSender, string email)
        {
            //If the EmailAddress matches the IdTrustedSender
            //200 OK
            //{
            //      "AuthorizationDate": "",
            //      "CreationDate": "2016-11-28 12:34:36Z",
            //      "EmailAddress": "youremailaddress@yourdomain.eu",
            //      "IdTrustedSender": "ror11zuGscWcjl7m",
            //      "StatusCode": 2,
            //      "StatusDescription": "Blocked (status: Blocked)",
            //      "UpdateDate": "2016-11-28 12:57:30Z"
            //}

            //If the If - Match header misses
            //403 Forbidden
            //{
            //      "ErrorCode": "403",
            //      "ErrorDescription": "Missing If-Match header.",
            //      "ErrorName": "Forbidden",
            //      "ErrorStack": ""
            //}

            //If the If - Match header conatins an email address that does not match with the address identified by IdTrustedSender
            //412 Precondition failed
            //{
            //      "ErrorCode": "412",
            //      "ErrorDescription": "Invalid [EmailAddress] from If-Match header field.",
            //      "ErrorName": "PreconditionFailed",
            //      "ErrorStack": ""
            //}

            // 	https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/TrustedSenders/{IdTrustedSender}

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/TrustedSenders/{idTrustedSender}";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                KeyValuePair<string, StringValues> header = new KeyValuePair<string, StringValues>("if-match", $"{email}");
                HeaderDictionary headers = new HeaderDictionary();
                headers.Add(header);

                response = await RequestAsync(HttpMethod.Delete, url, authorization, null, headers);

                string content = await response.Content.ReadAsStringAsync();

                ResponseItemEmail entity = Deserialize<ResponseItemEmail>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }

        public async Task<int> ConsoleCreateRecipientOnList(int listid, RequestEmail email)
        {
            //  https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/List/{id_List}/Recipient

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List/{listid}/Recipient";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                string body = Serialize<RequestEmail>(email);

                response = await RequestAsync(HttpMethod.Post, url, authorization, body, null);

                string content = await response.Content.ReadAsStringAsync();

                int entity = Deserialize<int>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<int> ConsoleCreateRecipientOnListConfirm(int listid, RequestEmail email)
        {
            /*  You can choose to perform a double optin procedure for the given recipient 
             *  by adding the querystring parameter "ConfirmEmail=true": in this case the 
             *  recipient will be added to the given list / group in "Pending" status, and a 
             *  confirmation email will be sent to that address, waiting for his / her confirmation.
             *  You can also update the information and status of an existing recipient by 
             *  specifying his / her email address or mobile number as unique identifier of the 
             *  existing recipient. */

            //  POST https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/List/{id_List}/Recipient?ConfirmEmail=true

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List/{listid}/Recipient?ConfirmEmail=true";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                string body = Serialize<RequestEmail>(email);

                response = await RequestAsync(HttpMethod.Post, url, authorization, body, null);

                string content = await response.Content.ReadAsStringAsync();

                int entity = Deserialize<int>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<int> ConsoleCreateRecipientOnGroup(int groupid, RequestEmail email)
        {
            //  POST https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/Group/{id_Group}/Recipient

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/Group/{groupid}/Recipient";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                string body = Serialize<RequestEmail>(email);

                response = await RequestAsync(HttpMethod.Post, url, authorization, body, null);

                string content = await response.Content.ReadAsStringAsync();

                int entity = Deserialize<int>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<int> ConsoleCreateRecipientOnGroupConfirm(int groupid, RequestEmail email)
        {
            /*  You can choose to perform a double optin procedure for the given recipient 
             *  by adding the querystring parameter "ConfirmEmail=true": in this case the 
             *  recipient will be added to the given list / group in "Pending" status, and a 
             *  confirmation email will be sent to that address, waiting for his / her confirmation.
             *  You can also update the information and status of an existing recipient by 
             *  specifying his / her email address or mobile number as unique identifier of the 
             *  existing recipient. */

            //  POST https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/Group/{id_Group}/Recipient?ConfirmEmail=true

            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/Group/{groupid}/Recipient?ConfirmEmail=true";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                string body = Serialize<RequestEmail>(email);

                response = await RequestAsync(HttpMethod.Post, url, authorization, body, null);

                string content = await response.Content.ReadAsStringAsync();

                int entity = Deserialize<int>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }
        }
        public async Task<ResponseRecipient> ConsoleDeleteRecipientsOnGroup(int listid, int groupid)
        {
            //  https://services.mailup.com/API/v1.1/Rest/ConsoleService.svc/Console/List/{ID_LIST}/Group/{ID_GROUP}/Recipients
            LoadCache();

            var url = this._configMailUpApiv1.MailUpConsole + $"/List/{listid}/Group/{groupid}/Recipients";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Bearer", Model.Access_Token);

                response = await RequestAsync(HttpMethod.Post, url, authorization, null, null);

                string content = await response.Content.ReadAsStringAsync();

                ResponseRecipient entity = Deserialize<ResponseRecipient>(content);

                return await Task.FromResult(entity);
            }
            catch (HttpRequestException hre)
            {
                throw new MailUpException(hre.StatusCode, hre.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"StatusCode: {response.StatusCode.ToString()} - Error: {ex.Message}");
            }

        }
    }
}

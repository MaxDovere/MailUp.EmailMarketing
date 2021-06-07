using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Model;
using RestSharp;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Core
{
    internal class MailUpHttpVerbs: IMailUpHttpVerbs
    {
        private readonly Uri _url;

        public MailUpHttpVerbs(Uri url)
        {
            this._url = url;
        }

        public async Task<IRestResponse> GetDataAsync(AuthenticationHeaderValue authorization, RestRequest req)
        {
            string urlRequest = this._url.AbsoluteUri;

            //if (req != null)
            //    urlRequest = req.Url.Length == 0 ? this._url.AbsoluteUri : req.Url;

            RestClient client = new RestClient(urlRequest);
            RestRequest request = new RestRequest(Method.GET)
            {
                //request headers
                RequestFormat = DataFormat.Json
            };
            string ContentType =  "application/json"; // : "application/x-www-form-urlencoded";

            request.AddHeader("Content-Type", ContentType);

            //add parameters and token to request
            request.Parameters.Clear();

            //if (req != null)
            //{
            //    if (!string.IsNullOrEmpty(req.RawQuery))
            //    {
            //        request.AddParameter("rawQuery", req.RawQuery);
            //    }
            //    if (!string.IsNullOrEmpty(req.Collection))
            //    {
            //        request.AddParameter("Collection", req.Collection);
            //    }
            //    if (req.PageSize > 0)
            //    {
            //        request.AddParameter("pageNumber", req.PageNumber);
            //        request.AddParameter("pageSize", req.PageSize);
            //    }
            //}

            if (authorization != null && (authorization.Parameter ?? string.Empty) != string.Empty) //+ req.Token
                request.AddParameter("Authorization", authorization.Parameter, ParameterType.HttpHeader);

            
            //if (req != null && (req.Collection ?? string.Empty) != string.Empty)
            //{

            //    HttpContent body = new StringContent(req.Collection ?? string.Empty);
            //    // and add the header to this object instance
            //    // optional: add a formatter option to it as well
            //    body.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
            //    request.AddJsonBody(body);
            //}
            //make the API request and get a response
            IRestResponse response = await client.ExecuteAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }

            return response;
        }

        public async Task<IRestResponse> PostDataAsync(AuthenticationHeaderValue authorization, RestRequest req)
        {
            string urlRequest = this._url.AbsoluteUri;

            //if (req != null)
            //    urlRequest = req.Url.Length == 0 ? this._url.AbsoluteUri : req.Url;

            RestClient client = new RestClient(urlRequest);

            RestRequest request = new RestRequest(Method.POST)
            {
                //request headers
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json");

            request.Parameters.Clear();

            //if (!string.IsNullOrEmpty(req.RawQuery))
            //{
            //    request.AddParameter("rawQuery", req.RawQuery);
            //}
            //if(req.PageSize > 0) 
            //{
            //    request.AddParameter("pageNumber", req.PageNumber);
            //    request.AddParameter("pageSize", req.PageSize);
            //}
            if (authorization != null)
                request.AddParameter("Authorization", authorization.Parameter, ParameterType.HttpHeader);

            //make the API request and get a response
            IRestResponse response = await client.ExecuteAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }

            return response;
        }

        public Task<IRestResponse> PutDataAsync(AuthenticationHeaderValue authorization, RestRequest req)
        {
            throw new NotImplementedException();
        }
        public Task<IRestResponse> DeleteDataAsync(AuthenticationHeaderValue authorization, RestRequest req)
        {
            throw new NotImplementedException();
        }

    }
}

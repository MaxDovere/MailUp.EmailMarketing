using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryMailUp.Abstractions
{
    public abstract class Response : IResponse
    {
        #region Fields
        private readonly byte[] responseData;
        #endregion

        #region Public Properties
        public bool IsSuccess => StatusCode is >= 200 and <= 299;
        public virtual int StatusCode { get; }
        public virtual IHeadersCollection Headers { get; }
        public virtual HttpRequestMethod HttpRequestMethod { get; }
        public virtual string RequestUri { get; }
        #endregion

        #region Constructor
        protected Response
        (
        IHeadersCollection headersCollection,
        int statusCode,
        HttpRequestMethod httpRequestMethod,
        byte[] responseData,
        string requestUri
        )
        {
            StatusCode = statusCode;
            Headers = headersCollection;
            HttpRequestMethod = httpRequestMethod;
            RequestUri = requestUri;
            this.responseData = responseData;
        }
        #endregion

        #region Public Methods
        public virtual byte[] GetResponseData() => responseData;
        #endregion
    }
}

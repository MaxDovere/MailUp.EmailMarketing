using System;
using System.Net;

namespace MailUp.EmailMarketing.Error
{
    public class MailUpException : Exception
    {
        private HttpStatusCode? statusCode;
        public HttpStatusCode? StatusCode
        {
            set { statusCode = value; }
            get { return statusCode; }
        }

        public MailUpException(HttpStatusCode? statusCode, String message) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}

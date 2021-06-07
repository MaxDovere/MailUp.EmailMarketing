using System;

namespace MailUp.EmailMarketing.Model
{
    public class ExceptionToken : Exception
    {
        public string Code { get; set; }
        public string OriginalCode { get; set; }
        public new string Message { get; set; }

        public ExceptionToken(string Code, string message) : this(Code, message, null)
        {
            this.Code = Code;
            Message = message;
        }

        public ExceptionToken(string Code, string message, Exception inner) : base(message, inner)
        {
            this.Code = OriginalCode = Code;
            Message = message;
        }
    }
}
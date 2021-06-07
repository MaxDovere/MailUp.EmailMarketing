namespace MailUp.EmailMarketing.Model
{
    public class BaseRequest
    {
        public string Token { get; set; }
        public string Collection { get; set; }
        public string RawQuery { get; set; }
        public string Url { get; set; }
    }
}

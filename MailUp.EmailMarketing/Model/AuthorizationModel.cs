using System;

namespace MailUp.EmailMarketing.Model
{
    public class AuthorizationModel
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public string Expires_In { get; set; }
        public string Token_type { get; set; }
        //public DateTime ExpirationTime { get; set; }
    }
}

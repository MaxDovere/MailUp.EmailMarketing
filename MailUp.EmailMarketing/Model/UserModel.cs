namespace DeliveryMailUp.Model
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RefreshCode { get; set; }
        public string UrlCallback { get; set; }
    }
}

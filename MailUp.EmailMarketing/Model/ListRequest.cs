namespace MailUp.EmailMarketing.Model
{
    public class ListRequest : BaseRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Id { get; set; }
    }
}

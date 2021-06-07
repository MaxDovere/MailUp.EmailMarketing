using MailUp.EmailMarketing.Model;
using RestSharp;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Abstractions
{
    internal interface IMailUpHttpVerbs
    {
        Task<IRestResponse> GetDataAsync(AuthenticationHeaderValue authorization, RestRequest req);
        Task<IRestResponse> PostDataAsync(AuthenticationHeaderValue authorization, RestRequest req);
        Task<IRestResponse> PutDataAsync(AuthenticationHeaderValue authorization, RestRequest req);
        Task<IRestResponse> DeleteDataAsync(AuthenticationHeaderValue authorization, RestRequest req);
    }
}

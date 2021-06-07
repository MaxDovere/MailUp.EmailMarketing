using MailUp.EmailMarketing.Model;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Abstractions
{
    public interface ITokenBase
    {
        string GetTokenInfo(string callbackUri);
        Task<TokenResponse> GetToken(string username, string password);
        Task<TokenResponse> GetTokenWithCode(string code);
        Task<TokenResponse> RenewToken(string refreshcode);
    }
}

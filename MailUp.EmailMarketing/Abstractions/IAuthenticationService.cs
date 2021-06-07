using MailUp.EmailMarketing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Abstractions
{
    public interface IAuthenticationService
    {
        string LogOnUri(string callbackUri);
        Task<AuthorizationModel> LoginWithPassword(string username, string password);
        Task<AuthorizationModel> LoginWithCode(string code);
        Task<AuthorizationModel> LoginRenewToken(string refreshCode);

    }
}

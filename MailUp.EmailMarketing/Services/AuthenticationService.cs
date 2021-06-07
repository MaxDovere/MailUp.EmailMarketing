using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Core;
using MailUp.EmailMarketing.Model;
using System;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Services
{
    public class AuthenticationService: TokenBase, IAuthenticationService
    {
        public AuthenticationService(ConfigurationsMailUp.MailUpApiv1 config)
            :base (config)
        {
        }
        public string LogOnUri(string callbackUri)
        {
            return GetTokenInfo(callbackUri);
        }

        public async Task<AuthorizationModel> LoginWithPassword(string username, string password)
        {
            TokenResponse response = await GetToken(username, password);

            return new AuthorizationModel()
            {
                Access_Token = response.access_token,
                Refresh_Token = response.refresh_token,
                Expires_In = response.expires_in.ToString()
            };

        }
        public async Task<AuthorizationModel> LoginWithCode(string code)
        {
            TokenResponse response = await GetTokenWithCode(code);
            return new AuthorizationModel()
            {
                Access_Token = response.access_token,
                Refresh_Token = response.refresh_token,
                Expires_In = response.expires_in.ToString()
            };
        }

        public async Task<AuthorizationModel> LoginRenewToken(string refreshCode)
        {
            TokenResponse response = await RenewToken(refreshCode);
            return new AuthorizationModel()
            {
                Access_Token = response.access_token,
                Refresh_Token = response.refresh_token,
                Expires_In = response.expires_in.ToString()
            };
        }
    }
}

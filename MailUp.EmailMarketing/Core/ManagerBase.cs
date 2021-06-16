using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Entities;
using MailUp.EmailMarketing.Error;
using MailUp.EmailMarketing.Model;
using RestSharp;

namespace MailUp.EmailMarketing.Core
{
    public class ManagerBase : IManagerBase
    {
        private readonly AuthorizationModel _authorizedToken;
        private readonly MailUpConfigurations.MailUpApiv1 _config;
        public ManagerBase(AuthorizationModel authorizedToken, MailUpConfigurations.MailUpApiv1 config)
        {
            _config = config;
            _authorizedToken = authorizedToken;
        }
        public AuthorizationModel AuthorizedUser { get => _authorizedToken; init => _authorizedToken = value; }

        public List<EntityUserInfo> ListUserInfo(string username, string password, string userToRead = "")
        {
            throw new NotImplementedException();
        }

        public string PrepareURLToSendEmail(string action, string senderName, string senderAddress, DateTime? scheduledTime)
        {
            throw new NotImplementedException();
        }

    }
}

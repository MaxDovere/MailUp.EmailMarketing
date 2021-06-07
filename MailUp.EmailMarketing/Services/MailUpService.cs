using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Core;
using MailUp.EmailMarketing.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using MailUp.EmailMarketing.Implementation;
using MailUp.EmailMarketing.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MailUp.EmailMarketing.Services
{
    public class MailUpService: IMailUpService 
    {
        private readonly ConfigurationsMailUp.MailUpApiv1 _config;

        private readonly AuthenticationService authorization;

        public IManagerContainer ManagerContainer;
        public IManagerDelivery ManagerDelivery;
        public IManagerSMSMessage ManagerSMSMessage;
        public MailUpService()
        {
            
            this._config = new ConfigurationsMailUp.MailUpApiv1();

            authorization = new AuthenticationService(_config);

        }
        public MailUpService(ConfigurationsMailUp.MailUpApiv1 config)
        {
            if(config == null)
                this._config = new ConfigurationsMailUp.MailUpApiv1();
            else
                this._config = config;

            authorization = new AuthenticationService(config);

        }
        public async Task<AuthorizationModel> LoginRenewToken()
        {
            authorization.LoadCache();

            AuthorizationModel model = await authorization.LoginRenewToken(authorization.CacheModel.Refresh_Token);

            ManagerContainer = new ManagerContainer(model, _config);
            ManagerDelivery = new ManagerDelivery(model, _config);
            ManagerSMSMessage = new ManagerSMSMessage(model, _config);

            return model;
        }

        public async Task<AuthorizationModel> LoginWithCode()
        {
            authorization.LoadCache();

            AuthorizationModel model = await authorization.LoginWithCode(authorization.CacheModel.Access_Token);

            ManagerContainer = new ManagerContainer(model, _config);
            ManagerDelivery = new ManagerDelivery(model, _config);
            ManagerSMSMessage = new ManagerSMSMessage(model, _config);

            return model;
        }
        public async Task<AuthorizationModel> LoginWithPassword(string username, string password)
        {
            AuthorizationModel model = await authorization.LoginWithPassword(username, password);

            ManagerContainer = new ManagerContainer(model, _config);
            ManagerDelivery = new ManagerDelivery(model, _config);
            ManagerSMSMessage = new ManagerSMSMessage(model, _config);

            return model;

        }
        public string LogOnUri(string callbackUri)
        {
            return authorization.LogOnUri(callbackUri);
        }
    }
}

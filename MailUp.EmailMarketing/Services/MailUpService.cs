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
using Microsoft.Extensions.Configuration;

namespace MailUp.EmailMarketing.Services
{
    public class MailUpService: IMailUpService
    {
        private readonly MailUpConfigurations.MailUpApiv1 _config;
        private readonly AuthenticationService authorization;

        public IManagerContainer Container { get; set; }
        public IManagerDelivery Delivery { get; set ; }
        public IManagerSMSMessage SMSMessage { get; set; }
        public MailUpConfigurations.MailUpApiv1 MailUpConfiguration => this._config;
        public MailUpService(MailUpConfigurations.MailUpApiv1 config)
        {
            if(config == null)
                this._config = new MailUpConfigurations.MailUpApiv1();
            else
                this._config = config;

            authorization = new AuthenticationService(this._config);

        }

        public async Task<AuthorizationModel> LoginRenewToken()
        {
            authorization.LoadCache();

            AuthorizationModel model = await authorization.LoginRenewToken(authorization.CacheModel.Refresh_Token);

            Container = new ManagerContainer(model, this._config);
            Delivery = new ManagerDelivery(model, this._config);
            SMSMessage = new ManagerSMSMessage(model, this._config);

            return model;
        }
        public async Task<AuthorizationModel> LoginWithCode()
        {
            authorization.LoadCache();

            AuthorizationModel model = await authorization.LoginWithCode(authorization.CacheModel.Access_Token);

            Container = new ManagerContainer(model, _config);
            Delivery = new ManagerDelivery(model, _config);
            SMSMessage = new ManagerSMSMessage(model, _config);

            return model;
        }
        public async Task<AuthorizationModel> LoginWithPassword(string username, string password)
        {
            AuthorizationModel model = await authorization.LoginWithPassword(username, password);

            Container = new ManagerContainer(model, _config);
            Delivery = new ManagerDelivery(model, _config);
            SMSMessage = new ManagerSMSMessage(model, _config);

            return model;

        }
        public string LogOnUri(string callbackUri)
        {
            return authorization.LogOnUri(callbackUri);
        }
    }
}

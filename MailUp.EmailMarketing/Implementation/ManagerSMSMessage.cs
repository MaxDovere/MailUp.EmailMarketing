using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailUp.EmailMarketing.Model;

namespace MailUp.EmailMarketing.Implementation
{
    public class ManagerSMSMessage : ManagerBase, IManagerSMSMessage
    {
        public ManagerSMSMessage(AuthorizationModel model, MailUpConfigurations.MailUpApiv1 config)
            : base(model, config)
        {
        }

    }
}
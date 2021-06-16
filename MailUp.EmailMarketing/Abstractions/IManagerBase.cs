using MailUp.EmailMarketing.Entities;
using MailUp.EmailMarketing.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Abstractions
{
    public interface IManagerBase
    {
        AuthorizationModel AuthorizedUser { get; init; }

         string PrepareURLToSendEmail(
            string action,
            string senderName,
            string senderAddress,
            DateTime? scheduledTime);
         List<EntityUserInfo> ListUserInfo(
            string username,
            string password,
            string userToRead = "");

    }
}

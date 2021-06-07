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

        T CallMethod<T>(
            Method verb,
            string scheme,
            string url,
            object requestData,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null,
            bool consoleEndpoint = true)
            where T : class;

         long CallMethodWithNumericResponse(
            Method verb,
            string scheme,
            string url,
            object requestData,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null,
            bool consoleEndpoint = true);

         T CallDirectMethod<T>(Method verb, string scheme, string url, object requestData) where T : class;

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

using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Core;
using MailUp.EmailMarketing.Entities;
using MailUp.EmailMarketing.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Implementation
{
    public class ManagerContainer : ManagerBase, IManagerContainer
    {
        private readonly IMailUpHttpVerbs _http;
        public ManagerContainer(AuthorizationModel model, MailUpConfigurations.MailUpApiv1 config)
        : base(model, config)
        {
            _http = new MailUpHttpVerbs();
        }

        /// <summary>
        /// POST AddRecipientToGroup
        /// </summary>
        /// <url>
        /// "Console/Group/{0}/Recipient"
        /// </url>
        /// <param name="idGroup"></param>
        /// <param name="recipient"></param>
        /// <returns>int</returns>
        public int AddRecipientToGroup(int idGroup, EntityRecipient recipient) =>
            (int)this._http.CallMethodWithNumericResponseAsync(Method.POST, 
                string.Format(MailUpUriConstants.ADD_RECIPIENT_TO_GROUP, idGroup), recipient, 
                MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true);

        /// <summary>
        /// POST CreateList
        /// <url>
        /// "Console/User/Lists"
        /// </url>
        /// </summary>
        /// <param name="list"></param>
        /// <returns>int</returns>
        public int CreateList(EntityList list) => (int) this._http.CallMethodWithNumericResponseAsync(Method.POST,
            MailUpUriConstants.CREATE_LIST, list, 
            MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true);
        //ConsoleListUpdateDetails
        /// <summary>
        /// PUT UpdateList
        /// </summary>
        /// <url>
        /// "Console/User/List/{0}"
        /// </url>
        /// <param name="list"></param>
        /// <returns>EntityList</returns>
        public EntityList UpdateList(EntityList list)
        {
            EntityList _list = list == null || list.IdList <= 0
                    ? (EntityList)null
                    : this._http.CallMethodAsync<EntityList>(Method.PUT,
                        String.Format(MailUpUriConstants.UPDATE_LIST, list.IdList), list, 
                        MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true).Result;
            return _list;
        }

        //ConsoleListItem - IEnumerable<ConsoleListItem>
        /// <summary>
        /// GET GetList (int)
        /// </summary>
        /// <url>
        /// "Console/User/Lists"
        /// </url>
        /// <param name="idList"></param>
        /// <returns>EntityList</returns>
        public EntityList GetList(int idList)
            => this._http.CallMethodAsync<EntityList>(Method.GET, MailUpUriConstants.GET_LISTS, null,
                "idList == " + idList, MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token).Result;
        //ConsoleListItem - IEnumerable<ConsoleListItem>
        /// <summary>
        /// GET GetList (string)
        /// </summary>
        /// <url>
        /// "Console/User/Lists"
        /// </url>
        /// <param name="listName"></param>
        /// <returns>EntityList</returns>
        public EntityList GetList(string listName)
        {
            EntityList _list = (string.IsNullOrEmpty(listName)
                ? (EntityList) null
                : this._http.CallMethodAsync<EntityList>(Method.GET, 
                    MailUpUriConstants.GET_LISTS, null, 
                    MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token,
                    "Name.Trim().Equals('" + listName.Trim() + "')").Result);
            return _list;
        }
        //IEnumerable<ConsoleListItem> - 
        /// <summary>
        /// GET GetLists
        /// </summary>
        /// <url>
        /// "Console/User/Lists"
        /// </url>
        /// <param name="filterExpression"></param>
        /// <param name="sortExpression"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns>EntitiesLists</returns>
        public EntitiesLists GetLists(
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this._http.CallMethodAsync<EntitiesLists>(Method.GET, 
                MailUpUriConstants.GET_LISTS, null, 
                MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, 
                filterExpression, sortExpression, pageSize, pageNumber).Result;
        }
        //ConsoleGroupItem
        /// <summary>
        /// POST CreateGroup
        /// </summary>
        /// <url>
        /// "Console/List/{0}/Group"
        /// </url>
        /// <param name="group"></param>
        /// <returns>EntityGroup</returns>
        public EntityGroup CreateGroup(EntityGroup group)
        {
            if (group != null)
            {
                int? idList = group.IdList;
                if ((idList.GetValueOrDefault() > 0 ? 0 : (idList.HasValue ? 1 : 0)) == 0)
                    return this._http.CallMethodAsync<EntityGroup>(Method.POST, 
                        string.Format(MailUpUriConstants.CREATE_GROUP, group.IdList), group, 
                        MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true).Result;
            }
            return default;
        }
        /// <summary>
        /// PUT UpdateGroup
        /// </summary>
        /// <url>
        /// "Console/List/{0}/Group/{1}"
        /// </url>
        /// <param name="group"></param>
        /// <returns>EntityGroup</returns>
        public EntityGroup UpdateGroup(EntityGroup group)
        {
            if (group != null)
            {
                int? idList = group.IdList;
                if ((idList.GetValueOrDefault() > 0 ? 0 : (idList.HasValue ? 1 : 0)) == 0)
                {
                    int? idGroup = group.IdGroup;
                    if ((idGroup.GetValueOrDefault() > 0 ? 0 : (idGroup.HasValue ? 1 : 0)) == 0)
                        return this._http.CallMethodAsync<EntityGroup>(Method.PUT, 
                            string.Format(MailUpUriConstants.UPDATE_GROUP, (object) group.IdList, group.IdGroup),
                            group, MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true).Result;
                }
            }
            return null;
        }
        /// <summary>
        /// DELETE DeleteGroup
        /// </summary>
        /// <url>
        /// "Console/List/{0}/Group/{1}"
        /// </url>
        /// <param name="idList"></param>
        /// <param name="idGroup"></param>
        public void DeleteGroup(int idList, int idGroup) => this._http.CallMethodWithNumericResponseAsync(Method.DELETE,
            string.Format(MailUpUriConstants.DELETE_GROUP, idList, idGroup), null, 
            MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true);
        //IEnumerable<ConsoleGroupItem>
        /// <summary>
        /// GET GetGroup /(int, int)
        /// </summary>
        /// <url>
        /// "Console/List/{0}/Groups"
        /// </url>
        /// <param name="idList"></param>
        /// <param name="idGroup"></param>
        /// <returns>EntityGroup</returns>
        public EntityGroup GetGroup(int idList, int idGroup) =>
            (EntityGroup) this._http.CallMethodAsync<EntityGroup>(Method.GET, 
                string.Format(MailUpUriConstants.GET_GROUPS, idList), null, 
                MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, "idGroup == " + idGroup).Result;
        // IEnumerable<ConsoleGroupItem>
        /// <summary>
        /// GET GetGroup (int, string)
        /// </summary>
        /// <url>
        /// "Console/List/{0}/Groups"
        /// </url>
        /// <param name="idList"></param>
        /// <param name="groupName"></param>
        /// <returns>EntityGroup</returns>
        public EntityGroup GetGroup(int idList, string groupName) => string.IsNullOrEmpty(groupName)
            ? null
            : this._http.CallMethodAsync<EntityGroup>(Method.GET,
                string.Format(MailUpUriConstants.GET_GROUPS, (object) idList), null,
                MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token,
                "Name.Trim().Equals('" + groupName.Trim() + "')").Result;
        //IEnumerable<ConsoleGroupItem>
        /// <summary>
        /// GET GetGroups
        /// </summary>
        /// <url>
        /// "Console/List/{0}/Groups"
        /// </url>
        /// <param name="idList"></param>
        /// <param name="filterExpression"></param>
        /// <param name="sortExpression"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns>EntitiesGroups</returns>
        public EntitiesGroups GetGroups(
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this._http.CallMethodAsync<EntitiesGroups>(Method.GET, 
                string.Format(MailUpUriConstants.GET_GROUPS, (object) idList), (object) null, 
                MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, filterExpression,
                sortExpression, pageSize, pageNumber).Result;
        }
        /// <summary>
        /// POST AddRecipientsToGroup
        /// </summary>
        /// <url>
        /// "Console/Group/{0}/Recipients"
        /// </url>
        /// <param name="idGroup"></param>
        /// <param name="recipients"></param>
        /// <returns></returns>
        public int AddRecipientsToGroup(int idGroup, List<EntityRecipient> recipients) =>
            (int) this._http.CallMethodWithNumericResponseAsync(Method.POST,
                string.Format(MailUpUriConstants.ADD_RECIPIENTS_TO_GROUP, idGroup), recipients,
                    MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true);
        /// <summary>
        /// POST AddRecipientToList
        /// </summary>
        /// <url>
        /// "Console/List/{0}/Recipient"
        /// </url>
        /// <param name="idList"></param>
        /// <param name="recipient"></param>
        /// <param name="withCOI"></param>
        /// <returns>int</returns>
        public int AddRecipientToList(int idList, EntityRecipient recipient, bool withCOI = false)
        {
            string url = string.Format(MailUpUriConstants.IMPORT_RECIPIENT, idList);
            if (withCOI)
                url += "?ConfirmEmail=true";
            return (int)this._http.CallMethodWithNumericResponseAsync(Method.POST, url, recipient, 
                MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true);
        }
        //EntityRecipient
        /// <summary>
        /// POST AddRecipientsToList
        /// </summary>
        /// <url>
        /// "Console/List/{0}/Recipients"
        /// </url>
        /// <param name="idList"></param>
        /// <param name="recipients"></param>
        /// <param name="withCOI"></param>
        /// <returns>int</returns>
        public int AddRecipientsToList(int idList, List<EntityRecipient> recipients, bool withCOI = false)
        {
            string url = string.Format(MailUpUriConstants.IMPORT_RECIPIENTS, idList);
            if (withCOI)
                url += "?confirmMessage=true";
            return (int)this._http.CallMethodWithNumericResponseAsync(Method.POST, url, recipients, 
                MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true);
        }
        //ConsoleImportStatus
        /// <summary>
        /// GET CheckImportStatus
        /// </summary>
        /// <url>
        /// "Console/Import/{0}"
        /// </url>
        /// <param name="idImport"></param>
        /// <returns>EntityImportStatus</returns>
        public EntityImportStatus CheckImportStatus(int idImport) => this._http.CallMethodAsync<EntityImportStatus>(Method.GET,
            string.Format(MailUpUriConstants.CHECK_IMPORT_STATUS, idImport), null, 
            MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true).Result;
        //EntityRecipient
        /// <summary>
        /// POST ForceOptinOnUnsubscribedRecipient
        /// </summary>
        /// <url>
        /// "Console/List/{0}/Recipients?importType=asOptin"
        /// </url>
        /// <param name="idList"></param>
        /// <param name="recipientEmail"></param>
        /// <returns>bool</returns>
        public bool ForceOptinOnUnsubscribedRecipient(int idList, string recipientEmail)
        {
            int idImport = (int)this._http.CallMethodWithNumericResponseAsync(Method.POST, 
                string.Format(MailUpUriConstants.FORCE_OPTIN_UNSUB_REC, idList), 
                new List<EntityRecipient>()
                            {
                                new EntityRecipient() { Email = recipientEmail }
                            }, 
                MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true
            );
            int num = 0;
            do
            {
                EntityImportStatus consoleImportStatus = this.CheckImportStatus(idImport);
                if (consoleImportStatus == null)
                    return false;
                if (consoleImportStatus.Completed)
                    return true;
                ++num;
                Thread.Sleep(500);
            }
            while (num < 20);
            return false;
        }
        /// <summary>
        /// DELETE UnsubscribeRecipient
        /// </summary>
        /// <url>
        /// "Console/List/{0}/Unsubscribe/{1}"
        /// </url>
        /// 
        /// <param name="idList"></param>
        /// <param name="idRecipient"></param>
        public void UnsubscribeRecipient(int idList, int idRecipient) => this._http.CallMethodAsync<EntityRecipient>(Method.DELETE,
            string.Format(MailUpUriConstants.UNSUBSCRIBE_RECIPIENT, idList, idRecipient),  null,
            MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true);
        /// <summary>
        /// POST UnsubscribeRecipients
        /// </summary>
        /// <url>
        /// "Console/List/{0}/Recipients?importType=asOptout"
        /// </url>
        /// <param name="idList"></param>
        /// <param name="emailRecipients"></param>
        /// <returns>int</returns>
        public int UnsubscribeRecipients(int idList, List<string> emailRecipients)
        {
            if (emailRecipients == null || emailRecipients.Count <= 0)
                return -1;
            List<EntityRecipient> EntityRecipientList = new List<EntityRecipient>();
            foreach (string emailRecipient in emailRecipients)
                EntityRecipientList.Add(new EntityRecipient()
                {
                    Email = emailRecipient
                });
            return (int) this._http.CallMethodWithNumericResponseAsync(Method.POST, 
                string.Format(MailUpUriConstants.UNSUBSCRIBE_RECIPIENTS, idList),
                EntityRecipientList, MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true);
        }
        //IEnumerable<ConsoleDynamicFieldItem>
        /// <summary>
        /// GET GetPersonalDataFields
        /// </summary>
        /// <url>
        /// "Console/Recipient/DynamicFields"
        /// </url>
        /// <returns>object</returns>
        public object GetPersonalDataFields() => this._http.CallMethodAsync<object>(Method.GET,
            MailUpUriConstants.READ_DATA_FIELDS, null,
            MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true).Result;
        /// <summary>
        /// PUT UpdateRecipient
        /// </summary>
        /// <url>
        /// "Console/Recipient/Detail"
        /// </url>
        /// <param name="recipient"></param>
        /// <returns>EntityRecipient</returns>
        public EntityRecipient UpdateRecipient(EntityRecipient recipient) => this._http.CallMethodAsync<EntityRecipient>(
            Method.PUT, MailUpUriConstants.UPDATE_DATA_FIELDS, recipient,
            MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true).Result;
        /// <summary>
        /// GetEnumSubscriptionStatus
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="idRecipient"></param>
        /// <returns>EnumSubscriptionStatus</returns>
        public EnumSubscriptionStatus GetEnumSubscriptionStatus(int idList, int idRecipient)
        {
            string filter = "idRecipient==" + (object)idRecipient;
            return this.RetrieveEnumSubscriptionStatus(idList, filter);
        }
        /// <summary>
        /// GetEnumSubscriptionStatus
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="emailRecipient"></param>
        /// <returns>EnumSubscriptionStatus</returns>
        public EnumSubscriptionStatus GetEnumSubscriptionStatus(
          int idList,
          string emailRecipient)
        {
            if (string.IsNullOrEmpty(emailRecipient))
                return EnumSubscriptionStatus.Unknown;
            string filter = "Email.Trim().Equals('" + emailRecipient.Trim() + "')";
            return this.RetrieveEnumSubscriptionStatus(idList, filter);
        }
        /// <summary>
        /// RetrieveEnumSubscriptionStatus
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="filter"></param>
        /// <returns>EnumSubscriptionStatus</returns>
        private EnumSubscriptionStatus RetrieveEnumSubscriptionStatus(
          int idList,
          string filter)
        {
            if (this.GetRecipientInStatus(EnumSubscriptionStatus.Subscribed, idList, filter) != null)
                return EnumSubscriptionStatus.Subscribed;
            if (this.GetRecipientInStatus(EnumSubscriptionStatus.Pending, idList, filter) != null)
                return EnumSubscriptionStatus.Pending;
            return this.GetRecipientInStatus(EnumSubscriptionStatus.Unsubscribed, idList, filter) != null ? EnumSubscriptionStatus.Unsubscribed : EnumSubscriptionStatus.Unknown;
        }
        /// <summary>
        /// GetRecipientInStatus
        /// </summary>
        /// <param name="status"></param>
        /// <param name="idList"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private EntityRecipient GetRecipientInStatus(
          EnumSubscriptionStatus status,
          int idList,
          string filter)
        {
            return (EntityRecipient)this.GetRecipientsInStatus(status, idList, filter,
                pageSize: new int?(1)); //.SafeFirstOrDefault<EntityRecipient>();
        }
        /// <summary>
        /// GET GetRecipientsInStatus
        /// </summary>
        /// <param name="status"></param>
        /// <param name="idList"></param>
        /// <param name="filterExpression"></param>
        /// <param name="sortExpression"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns>IEnumerable<EntityRecipient></returns>
        public IEnumerable<EntityRecipient> GetRecipientsInStatus(
          EnumSubscriptionStatus status,
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            string format;
            switch (status)
            {
                case EnumSubscriptionStatus.Pending:
                    format = MailUpUriConstants.GET_PENDING_RECIPIENTS; //"Console/List/{0}/Recipients/Pending";
                    break;
                case EnumSubscriptionStatus.Subscribed:
                    format = MailUpUriConstants.GET_SUBSCRIBED_RECIPIENTS; //"Console/List/{0}/Recipients/Subscribed";
                    break;
                case EnumSubscriptionStatus.Unsubscribed:
                    format = MailUpUriConstants.GET_UNSUBSCRIBED_RECIPIENTS; //"Console/List/{0}/Recipients/Unsubscribed";
                    break;
                default:
                    return (IEnumerable<EntityRecipient>)null;
            }

            return this._http.CallMethodAsync<IEnumerable<EntityRecipient>>(Method.GET,
                string.Format(format, idList), null, MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token,
                filterExpression, sortExpression, pageSize, pageNumber).Result;
        }

        /// <summary>
        /// RemoveRecipientFromGroup
        /// </summary>
        /// <url>
        /// "Console/Group/{0}/Unsubscribe/{1}"
        /// </url>
        /// <param name="idGroup"></param>
        /// <param name="idRecipient"></param>
        public void RemoveRecipientFromGroup(int idGroup, int idRecipient) =>
            this._http.CallMethodAsync<EntityRecipient>(Method.DELETE,
                string.Format(MailUpUriConstants.REMOVE_RECIPIENT_FROM_GROUP, idGroup, idRecipient), null,
                MailUpUriConstants.SCHEME_AUTH, AuthorizedUser.Access_Token, true);
    }
}

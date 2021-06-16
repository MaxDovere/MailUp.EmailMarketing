//using fastJSON;
//using MailUp.Framework.Contracts.API.Base;
//using MailUp.Framework.Contracts.API.DataContracts.Console;
//using MailUp.Framework.Contracts.API.DataContracts.Console.MailSend;
//using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Bounces;
//using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Clicks;
//using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Deliveries;
//using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Recipients;
//using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Unsubscriptions;
//using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Views;

using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Entities;
using MailUp.EmailMarketing.Error;
using MailUp.EmailMarketing.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;

namespace MailUp.EmailMarketing.Core
{
    public class EX_ReflectCallApi 
    {
        private readonly RestClient _client = new RestClient();

        //public T CallMethod<T>(
        //  Method verb,
        //  string url,
        //  object requestData,
        //  string filterExpression = "",
        //  string sortExpression = "",
        //  int? pageSize = null,
        //  int? pageNumber = null,
        //  bool consoleEndpoint = true)
        //  where T : class
        //{
        //    string uri = url;
        //    string str1 = consoleEndpoint ? Constants.ConsoleEndpoint : Constants.MailStatisticsEndpoint;
        //    if (!url.StartsWith(str1))
        //    {
        //        string str2 = str1;
        //        if (!url.StartsWith("/"))
        //            str2 += "/";
        //        uri = str2 + url;
        //    }
        //    object obj;// = this._oauth2Client.RequestResource<T>(uri, verb.ToString(), requestData, filterExpression, sortExpression, pageSize, pageNumber);
        //    switch (obj)
        //    {
        //        case null:
        //            return default(T);
        //        case T _:
        //            return obj as T;
        //        case Exception _:
        //            throw new MailUpException(0, (obj as Exception).Message);
        //        case Dictionary<string, object> _:
        //            throw new MailUpException(0, string.Join(",", (obj as Dictionary<string, object>).Select<KeyValuePair<string, object>, string>((Func<KeyValuePair<string, object>, string>)(kvp => string.Format("{0}:{1}", (object)kvp.Key, kvp.Value)))));
        //        default:
        //            throw new MailUpException(0, obj.ToString());
        //    }
        //}

        //public long CallMethodWithNumericResponse(
        //  Method verb,
        //  string url,
        //  object requestData,
        //  string filterExpression = "",
        //  string sortExpression = "",
        //  int? pageSize = null,
        //  int? pageNumber = null,
        //  bool consoleEndpoint = true)
        //{
        //    string uri = url;
        //    string str1 = consoleEndpoint ? Constants.ConsoleEndpoint : Constants.MailStatisticsEndpoint;
        //    if (!url.StartsWith(str1))
        //    {
        //        string str2 = str1;
        //        if (!url.StartsWith("/"))
        //            str2 += "/";
        //        uri = str2 + url;
        //    }
        //    object obj;// = this._oauth2Client.RequestResource<long>(uri, verb.ToString(), requestData, filterExpression, sortExpression, pageSize, pageNumber);
        //    switch (obj)
        //    {
        //        case null:
        //            return 0;
        //        case long num:
        //            return num;
        //        case Exception _:
        //            throw new MailUpException(0, (obj as Exception).Message);
        //        case Dictionary<string, object> _:
        //            throw new MailUpException(0, string.Join(",", (obj as Dictionary<string, object>).Select<KeyValuePair<string, object>, string>((Func<KeyValuePair<string, object>, string>)(kvp => string.Format("{0}:{1}", (object)kvp.Key, kvp.Value)))));
        //        default:
        //            throw new MailUpException(0, obj.ToString());
        //    }
        //}

        //public T CallDirectMethod<T>(Method verb, string url, object requestData) where T : class
        //{
        //    RestRequest restRequest = new RestRequest(url, verb); //.GetRestMethod());
        //    restRequest.RequestFormat = DataFormat.Json;
        //    restRequest.AddJsonBody(requestData);
        //    IRestResponse restResponse;
        //    try
        //    {
        //        restResponse = this._client.Execute((IRestRequest)restRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new MailUpException(0, ex.Message);
        //    }
        //    string json = restResponse.StatusCode == HttpStatusCode.OK ? restResponse.Content : throw new MailUpException((int)restResponse.StatusCode, restResponse.ErrorMessage);
        //    if (string.IsNullOrEmpty(json))
        //        return default(T);
        //    try
        //    {
        //        return JsonConvert.DeserializeObject<T>(json);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new MailUpException(0, ex.Message);
        //    }
        //}

        //public string PrepareURLToSendEmail(
        //    string action,
        //    string senderName,
        //    string senderAddress,
        //    DateTime? scheduledTime)
        //{
        //    action = ClientUtils.AddQuerystringParam(action, "SenderName", senderName);
        //    action = ClientUtils.AddQuerystringParam(action, "SenderAddress", senderAddress);
        //    if (scheduledTime.HasValue)
        //        action = ClientUtils.AddQuerystringParam(action, "datetime", scheduledTime.Value.ToString("yyyy-MM-dd HH:mm:ssZ"));
        //    return action;
        //}
        //ConsoleListCreateDetails
        //public int CreateList(EntityList list) => (int)this.CallMethodWithNumericResponse(Method.POST, "Console/User/Lists", (EntityList)list);
        ////ConsoleListUpdateDetails
        //public EntityList UpdateList(EntityList list) => (EntityList)(list == null || list.IdList <= 0 ? (EntityList)null : this.CallMethod<EntityList>(Method.PUT, string.Format("Console/User/List/{0}", (int)list.IdList), (EntityList)list));
        ////ConsoleListItem - IEnumerable<ConsoleListItem>
        //public EntityList GetList(int idList) =>
        //    this.CallMethod<EntityList>(Method.GET, "Console/User/Lists", (object) null, "idList == " + (object) idList); //-> SafeFirstOrDefault<ConsoleListItem>();
        ////ConsoleListItem - IEnumerable<ConsoleListItem>
        //public EntityList GetList(string listName) =>
        //    (EntityList)(string.IsNullOrEmpty(listName)
        //        ? (EntityList) null
        //        : this.CallMethod<EntityList>(Method.GET, "Console/User/Lists", (object) null,
        //            "Name.Trim().Equals('" + listName.Trim() + "')")); // -> SafeFirstOrDefault<ConsoleListItem>();
        ////IEnumerable<ConsoleListItem> - 
        //public EntitiesLists GetLists(
        //  string filterExpression = "",
        //  string sortExpression = "",
        //  int? pageSize = null,
        //  int? pageNumber = null)
        //{
        //    return this.CallMethod<EntitiesLists>(Method.GET, "Console/User/Lists", (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        //}
        ////ConsoleGroupItem
        //public EntityGroup  CreateGroup(EntityGroup group)
        //{
        //    if (group != null)
        //    {
        //        int? idList = group.IdList;
        //        if ((idList.GetValueOrDefault() > 0 ? 0 : (idList.HasValue ? 1 : 0)) == 0)
        //            return this.CallMethod<EntityGroup>(Method.POST, string.Format("Console/List/{0}/Group", group.IdList), (EntityGroup)group);
        //    }
        //    return default;
        //}
        //public EntityGroup UpdateGroup(EntityGroup group)
        //{
        //    if (group != null)
        //    {
        //        int? idList = group.idList;
        //        if ((idList.GetValueOrDefault() > 0 ? 0 : (idList.HasValue ? 1 : 0)) == 0)
        //        {
        //            int? idGroup = group.idGroup;
        //            if ((idGroup.GetValueOrDefault() > 0 ? 0 : (idGroup.HasValue ? 1 : 0)) == 0)
        //                return this.CallMethod<ConsoleGroupItem>(Method.PUT, string.Format("Console/List/{0}/Group/{1}", (object)group.idList, (object)group.idGroup), (object)group);
        //        }
        //    }
        //    return (ConsoleGroupItem)null;
        //}
        //public void DeleteGroup(int idList, int idGroup) => this.CallMethodWithNumericResponse(Method.DELETE, string.Format("Console/List/{0}/Group/{1}", (object)idList, (object)idGroup), (object)null);
        ////IEnumerable<ConsoleGroupItem>
        //public EntityGroup GetGroup(int idList, int idGroup) => (EntityGroup)this.CallMethod<EntityGroup>(Method.GET, string.Format("Console/List/{0}/Groups", (object)idList), (object)null, "idGroup == " + (object)idGroup); // -> .SafeFirstOrDefault<ConsoleGroupItem>();
        //// IEnumerable<ConsoleGroupItem>
        //public EntityGroup GetGroup(int idList, string groupName) => string.IsNullOrEmpty(groupName) ? (object)null : this.CallMethod<EntityGroup>(Method.GET, string.Format("Console/List/{0}/Groups", (object)idList), (object)null, "Name.Trim().Equals('" + groupName.Trim() + "')"); // -> .SafeFirstOrDefault<ConsoleGroupItem>();
        ////IEnumerable<ConsoleGroupItem>
        //public EntitiesGroups GetGroups(
        //  int idList,
        //  string filterExpression = "",
        //  string sortExpression = "",
        //  int? pageSize = null,
        //  int? pageNumber = null)
        //{
        //    return this.CallMethod<EntitiesGroups>(Method.GET, string.Format("Console/List/{0}/Groups", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        //}
        //EntityRecipient
        //public int AddRecipientToList(int idList, EntityRecipient recipient, bool withCOI = false)
        //{
        //    string url = string.Format("Console/List/{0}/Recipient", (object)idList);
        //    if (withCOI)
        //        url += "?ConfirmEmail=true";
        //    return (int)this.CallMethodWithNumericResponse(Method.POST, url, recipient);
        //}
        ////EntityRecipient
        //public int AddRecipientsToList(int idList, List<EntityRecipient> recipients, bool withCOI = false)
        //{
        //    string url = string.Format("Console/List/{0}/Recipients", (object)idList);
        //    if (withCOI)
        //        url += "?confirmMessage=true";
        //    return (int)this.CallMethodWithNumericResponse(Method.POST, url, recipients);
        //}
        ////ConsoleImportStatus
        //public EntityImportStatus CheckImportStatus(int idImport) => this.CallMethod<EntityImportStatus>(Method.GET, string.Format("Console/Import/{0}", (object)idImport), (object)null);
        ////EntityRecipient
        //public bool ForceOptinOnUnsubscribedRecipient(int idList, string recipientEmail)
        //{
        //    int idImport = (int)this.CallMethodWithNumericResponse(Method.POST, string.Format("Console/List/{0}/Recipients?importType=asOptin", (object)idList), (object)new List<EntityRecipient>()
        //    {
        //        new EntityRecipient() { Email = recipientEmail }
        //    });
        //    int num = 0;
        //    do
        //    {
        //        EntityImportStatus consoleImportStatus = this.CheckImportStatus(idImport);
        //        if (consoleImportStatus == null)
        //            return false;
        //        if (consoleImportStatus.Completed)
        //            return true;
        //        ++num;
        //        Thread.Sleep(500);
        //    }
        //    while (num < 20);
        //    return false;
        //}
        //public void UnsubscribeRecipient(int idList, int idRecipient) => this.CallMethod<EntityRecipient>(Method.DELETE, string.Format("Console/List/{0}/Unsubscribe/{1}", (object)idList, (object)idRecipient), (object)null);
        //public int UnsubscribeRecipients(int idList, List<string> emailRecipients)
        //{
        //    if (emailRecipients == null || emailRecipients.Count <= 0)
        //        return -1;
        //    List<EntityRecipient> EntityRecipientList = new List<EntityRecipient>();
        //    foreach (string emailRecipient in emailRecipients)
        //        EntityRecipientList.Add(new EntityRecipient()
        //        {
        //            Email = emailRecipient
        //        });
        //    return (int)this.CallMethodWithNumericResponse(Method.POST, string.Format("Console/List/{0}/Recipients?importType=asOptout", (object)idList), (object)EntityRecipientList);
        //}
        ////IEnumerable<ConsoleDynamicFieldItem>
        //public object GetPersonalDataFields() => this.CallMethod<object>(Method.GET, "Console/Recipient/DynamicFields", (object)null);
        //public EntityRecipient UpdateRecipient(EntityRecipient recipient) => this.CallMethod<EntityRecipient>(Method.PUT, "Console/Recipient/Detail", (object)recipient);
        //public EnumSubscriptionStatus GetEnumSubscriptionStatus(int idList, int idRecipient)
        //{
        //    string filter = "idRecipient==" + (object)idRecipient;
        //    return this.RetrieveEnumSubscriptionStatus(idList, filter);
        //}
        //public EnumSubscriptionStatus GetEnumSubscriptionStatus(
        //  int idList,
        //  string emailRecipient)
        //{
        //    if (string.IsNullOrEmpty(emailRecipient))
        //        return EnumSubscriptionStatus.Unknown;
        //    string filter = "Email.Trim().Equals('" + emailRecipient.Trim() + "')";
        //    return this.RetrieveEnumSubscriptionStatus(idList, filter);
        //}
        //private EnumSubscriptionStatus RetrieveEnumSubscriptionStatus(
        //  int idList,
        //  string filter)
        //{
        //    if (this.GetRecipientInStatus(EnumSubscriptionStatus.Subscribed, idList, filter) != null)
        //        return EnumSubscriptionStatus.Subscribed;
        //    if (this.GetRecipientInStatus(EnumSubscriptionStatus.Pending, idList, filter) != null)
        //        return EnumSubscriptionStatus.Pending;
        //    return this.GetRecipientInStatus(EnumSubscriptionStatus.Unsubscribed, idList, filter) != null ? EnumSubscriptionStatus.Unsubscribed : EnumSubscriptionStatus.Unknown;
        //}
        //private EntityRecipient GetRecipientInStatus(
        //  EnumSubscriptionStatus status,
        //  int idList,
        //  string filter)
        //{
        //    return this.GetRecipientsInStatus(status, idList, filter, pageSize: new int?(1)).SafeFirstOrDefault<EntityRecipient>();
        //}
        //public IEnumerable<EntityRecipient> GetRecipientsInStatus(
        //  EnumSubscriptionStatus status,
        //  int idList,
        //  string filterExpression = "",
        //  string sortExpression = "",
        //  int? pageSize = null,
        //  int? pageNumber = null)
        //{
        //    string format;
        //    switch (status)
        //    {
        //        case EnumSubscriptionStatus.Pending:
        //            format = "Console/List/{0}/Recipients/Pending";
        //            break;
        //        case EnumSubscriptionStatus.Subscribed:
        //            format = "Console/List/{0}/Recipients/Subscribed";
        //            break;
        //        case EnumSubscriptionStatus.Unsubscribed:
        //            format = "Console/List/{0}/Recipients/Unsubscribed";
        //            break;
        //        default:
        //            return (IEnumerable<EntityRecipient>)null;
        //    }
        //    return this.CallMethod<IEnumerable<EntityRecipient>>(Method.GET, string.Format(format, (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        //}
        //public int AddRecipientToGroup(int idGroup, EntityRecipient recipient) => (int)this.CallMethodWithNumericResponse(Method.POST, string.Format("Console/Group/{0}/Recipient", (object)idGroup), (object)recipient);
        //public int AddRecipientsToGroup(int idGroup, List<EntityRecipient> recipients) => (int)this.CallMethodWithNumericResponse(Method.POST, string.Format("Console/Group/{0}/Recipients", (object)idGroup), (object)recipients);
        //public void RemoveRecipientFromGroup(int idGroup, int idRecipient) => this.CallMethod<object>(Method.DELETE, string.Format("Console/Group/{0}/Unsubscribe/{1}", (object)idGroup, (object)idRecipient), (object)null);
        public EntityMessage CreateMessage(
          int idList,
          EntityMessage message)
        {
            return this.CallMethod<EntityMessage>(Method.POST, string.Format("Console/List/{0}/Email", (object)idList), (object)message);
        }
        public IEnumerable<EntityMessage> GetMessages(
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<EntityMessage>>(Method.GET, string.Format("Console/List/{0}/Emails", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }
        public IEnumerable<ConsolePublishedMessageItem> GetPublishedMessages(
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<ConsolePublishedMessageItem>>(Method.GET, string.Format("Console/List/{0}/Online/Emails", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }
        public IEnumerable<ConsoleArchivedMessageItem> GetArchivedMessages(
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<ConsoleArchivedMessageItem>>(Method.GET, string.Format("Console/List/{0}/Archived/Emails", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }
        public ConsoleMessageDetails GetMessageDetails(int idList, int idMessage) => this.CallMethod<ConsoleMessageDetails>(Method.GET, string.Format("Console/List/{0}/Email/{1}", (object)idList, (object)idMessage), (object)null);
        public List<ConsoleEmailAttachmentItem> GetMessageAttachments(
          int idList,
          int idMessage)
        {
            return this.CallMethod<List<ConsoleEmailAttachmentItem>>(Method.GET, string.Format("Console/List/{0}/Email/{1}/Attachment", (object)idList, (object)idMessage), (object)null);
        }
        public IEnumerable<EmailSendingItem> GetEmailSendingStatus(
          SendingStatus status)
        {
            string url;
            switch (status)
            {
                case SendingStatus.Immediate:
                    url = "Console/Email/Sendings/Immediate";
                    break;
                case SendingStatus.Deferred:
                    url = "Console/Email/Sendings/Deferred";
                    break;
                case SendingStatus.Undefined:
                    url = "Console/Email/Sendings/Undefined";
                    break;
                default:
                    throw new MailUpException(-1, "Unknown Sending Status");
            }
            return this.CallMethod<IEnumerable<EmailSendingItem>>(Method.GET, url, (object)null);
        }
        public DateTime GetDateForDeferredDispatch(int idSending)
        {
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
            string s = this.CallMethod<string>(Method.GET, string.Format("Console/Email/Sending/{0}/Deferred", (object)idSending), (object)null);
            try
            {
                return DateTime.ParseExact(s, "u", (IFormatProvider)invariantCulture);
            }
            catch
            {
                return DateTime.MaxValue;
            }
        }
        public int GetSendingIDForImport(int idImport)
        {
            if (idImport <= 0)
                return -1;
            ImportSendDetails importSendDetails = this.CallMethod<ImportSendDetails>(Method.GET, string.Format("Console/Import/{0}/Sending", (object)idImport), (object)null);
            return importSendDetails == null ? -1 : importSendDetails.idSending;
        }
        public EntityMessage UpdateMessage(
          int idList,
          EntityEmail message)
        {
            if (message != null)
            {
                int? idMessage = message.IdMessage;
                if ((idMessage.GetValueOrDefault() > 0 ? 0 : (idMessage.HasValue ? 1 : 0)) == 0)
                    return this.CallMethod<EntityMessage>(Method.PUT, string.Format("Console/List/{0}/Email/{1}", (object)idList, message.IdMessage), (object)message);
            }
            return (EntityMessage)null;
        }
        public IEnumerable<ConsoleTagItem> GetTags(
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<ConsoleTagItem>>(Method.GET, string.Format("Console/List/{0}/Tags", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }
        public ConsoleTagItem CreateTag(int idList, string name) => this.CallMethod<ConsoleTagItem>(Method.POST, string.Format("Console/List/{0}/Tag", (object)idList), (object)name);
        public ConsoleTagItem UpdateTag(int idList, ConsoleTagItem tag) => tag == null || tag.Id <= 0 ? (ConsoleTagItem)null : this.CallMethod<ConsoleTagItem>(Method.PUT, string.Format("Console/List/{0}/Tag/{1}", (object)idList, (object)tag.Id), (object)tag);
        public void DeleteTag(int idList, int idTag) => this.CallMethodWithNumericResponse(Method.DELETE, string.Format("Console/List/{0}/Tag/{1}", (object)idList, (object)idTag), (object)null);
        public List<ConsoleEmailAttachmentItem> AddMessageAttachment(
          ConsoleAddAttachmentRequest attachRequest)
        {
            if (attachRequest == null || attachRequest.idList <= 0 || (attachRequest.idMessage <= 0 || attachRequest.Slot <= 0))
                return (List<ConsoleEmailAttachmentItem>)null;
            Regex regex = new Regex("[^a-zA-Z0-9 -._]");
            attachRequest.Name = attachRequest.Name == null ? "" : regex.Replace(attachRequest.Name, "");
            return this.CallMethod<List<ConsoleEmailAttachmentItem>>(Method.POST, string.Format("Console/List/{0}/Email/{1}/Attachment/{2}", (object)attachRequest.idList, (object)attachRequest.idMessage, (object)attachRequest.Slot), (object)attachRequest);
        }
        public List<ConsoleEmailAttachmentItem> DeleteMessageAttachment(
          int idList,
          int idMessage,
          int slot)
        {
            return this.CallMethod<List<ConsoleEmailAttachmentItem>>(Method.DELETE, string.Format("Console/List/{0}/Email/{1}/Attachment/{2}", (object)idList, (object)idMessage, (object)slot), (object)null);
        }
        public MailMessageSendResponse SendEmailMessageToList(
          int idList,
          int idMessage,
          string senderName = "",
          string senderAddress = "",
          DateTime? scheduledTime = null)
        {
            return this.CallMethod<MailMessageSendResponse>(Method.POST, this.PrepareURLToSendEmail(string.Format("Console/List/{0}/Email/{1}/Send", (object)idList, (object)idMessage), senderName, senderAddress, scheduledTime), (object)null);
        }
        public MailMessageSendResponse SendEmailMessageToGroup(
          int idGroup,
          int idMessage,
          string senderName = "",
          string senderAddress = "",
          DateTime? scheduledTime = null)
        {
            return this.CallMethod<MailMessageSendResponse>(Method.POST, this.PrepareURLToSendEmail(string.Format("Console/Group/{0}/Email/{1}/Send", (object)idGroup, (object)idMessage), senderName, senderAddress, scheduledTime), (object)null);
        }
        public MailMessageSendResponse SendEmailMessage(
          EmailSendToRecipientRequest request,
          string senderName = "",
          string senderAddress = "",
          DateTime? scheduledTime = null)
        {
            return this.CallMethod<MailMessageSendResponse>(Method.POST, this.PrepareURLToSendEmail("Console/Email/Send", senderName, senderAddress, scheduledTime), (object)request);
        }
        public EmailSendingItem SendEmailMessage(int idSending, DateTime? scheduledTime = null)
        {
            string url = string.Format("Console/Email/Sendings/{0}/Immediate", (object)idSending);
            ConsoleDeferredDispatchInfo deferredDispatchInfo = (ConsoleDeferredDispatchInfo)null;
            if (scheduledTime.HasValue)
            {
                url = string.Format("Console/Email/Sendings/{0}/Deferred", (object)idSending);
                deferredDispatchInfo = new ConsoleDeferredDispatchInfo()
                {
                    Date = scheduledTime.Value.ToString("u")
                };
            }
            return this.CallMethod<EmailSendingItem>(Method.POST, url, (object)deferredDispatchInfo);
        }
        public IEnumerable<ConsoleMessageSendHistoryItem> GetEmailMessageSendHistory(
          int idList,
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<ConsoleMessageSendHistoryItem>>(Method.GET, string.Format("Console/List/{0}/Email/{1}/SendHistory", (object)idList, (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }
        public IEnumerable<MessageRecipientItem> GetMessageRecipients(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<MessageRecipientItem>>(Method.GET, string.Format("Message/{0}/List/Recipients", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public int CountMessageRecipients(int idMessage, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(Method.GET, string.Format("Message/{0}/Count/Recipients", (object)idMessage), (object)null, filterExpression, consoleEndpoint: false);
        public IEnumerable<MessageViewItem> GetMessageViews(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<MessageViewItem>>(Method.GET, string.Format("Message/{0}/List/Views", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public int CountMessageViews(int idMessage, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(Method.GET, string.Format("Message/{0}/Count/Views", (object)idMessage), (object)null, filterExpression, consoleEndpoint: false);
        public IEnumerable<MessageRecipientClickDetailItem> GetMessageClicks(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<MessageRecipientClickDetailItem>>(Method.GET, string.Format("Message/{0}/List/Clicks", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public int CountMessageClicks(int idMessage, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(Method.GET, string.Format("Message/{0}/Count/Clicks", (object)idMessage), (object)null, filterExpression, consoleEndpoint: false);
        public IEnumerable<MessageUrlClickItem> GetMessageUrlClicks(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<MessageUrlClickItem>>(Method.GET, string.Format("Message/{0}/List/UrlClicks", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public IEnumerable<MessageUrlClickDetailItem> GetMessageUrlClicksDetails(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<MessageUrlClickDetailItem>>(Method.GET, string.Format("Message/{0}/List/UrlClickDetails", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public IEnumerable<MessageBounceItem> GetMessageBounces(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<MessageBounceItem>>(Method.GET, string.Format("Message/{0}/List/Bounces", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public int CountMessageBounces(int idMessage, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(Method.GET, string.Format("Message/{0}/Count/Bounces", (object)idMessage), (object)null, filterExpression, consoleEndpoint: false);
        public IEnumerable<MessageUnsubscriptionItem> GetMessageUnsubscriptions(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<MessageUnsubscriptionItem>>(Method.GET, string.Format("Message/{0}/List/Unsubscriptions", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public int CountMessageUnsubscriptions(int idMessage, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(Method.GET, string.Format("Message/{0}/Count/Unsubscriptions", (object)idMessage), (object)null, filterExpression, consoleEndpoint: false);
        public IEnumerable<MessageDeliveryItem> GetMessageDeliveries(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<MessageDeliveryItem>>(Method.GET, string.Format("Recipient/{0}/List/Deliveries", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public int CountMessageDeliveries(int idRecipient, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(Method.GET, string.Format("Recipient/{0}/Count/Deliveries", (object)idRecipient), (object)null, filterExpression, consoleEndpoint: false);
        public IEnumerable<RecipientMessageViewItem> GetMessageViewsByRecipient(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<RecipientMessageViewItem>>(Method.GET, string.Format("Recipient/{0}/List/Views", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public int CountMessageViewsByRecipient(int idRecipient, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(Method.GET, string.Format("Recipient/{0}/Count/Views", (object)idRecipient), (object)null, filterExpression, consoleEndpoint: false);
        public IEnumerable<RecipientClickDetailItem> GetMessageClicksByRecipient(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<RecipientClickDetailItem>>(Method.GET, string.Format("Recipient/{0}/List/Clicks", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public int CountMessageClicksByRecipient(int idRecipient, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(Method.GET, string.Format("Recipient/{0}/Count/Clicks", (object)idRecipient), (object)null, filterExpression, consoleEndpoint: false);
        public IEnumerable<RecipientUrlClickDetailItem> GetMessageClicksDetailsByRecipient(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<RecipientUrlClickDetailItem>>(Method.GET, string.Format("Recipient/{0}/List/ClicksDetails", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public IEnumerable<MessageBounceItem> GetMessageBouncesByRecipient(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<MessageBounceItem>>(Method.GET, string.Format("Recipient/{0}/List/Bounces", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public int CountMessageBouncesByRecipient(int idRecipient, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(Method.GET, string.Format("Recipient/{0}/Count/Bounces", (object)idRecipient), (object)null, filterExpression, consoleEndpoint: false);
        public IEnumerable<MessageBounceDetailItem> GetMessageBouncesDetails(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<MessageBounceDetailItem>>(Method.GET, string.Format("Recipient/{0}/List/BouncesDetails", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public IEnumerable<RecipientUnsubscriptionItem> GetMessageUnsubscriptionsByRecipient(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<IEnumerable<RecipientUnsubscriptionItem>>(Method.GET, string.Format("Recipient/{0}/List/Unsubscriptions", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }
        public int CountMessageUnsubscriptionsByRecipient(int idRecipient, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(Method.GET, string.Format("Recipient/{0}/Count/Unsubscriptions", (object)idRecipient), (object)null, filterExpression, consoleEndpoint: false);

        public EntitySendResponse SendMessage(EntityMessage messageInfo) => this.CallDirectMethod<EntitySendResponse>(Method.POST, "messages/sendmessage", (object)messageInfo);
        public EntitySendResponse SendTemplate(TemplateDTO messageInfo) => this.CallDirectMethod<EntitySendResponse>(Method.POST, "messages/sendtemplate", (object)messageInfo);
        public List<EntityUserInfo> ListUserInfo(
            string username,
            string password,
            string userToRead = "")
        {
            UserResponseDTO userResponseDto = this.CallDirectMethod<UserResponseDTO>(Method.POST, "users/listuserinfo ", (object)new ListUsersDTO()
            {
                User = new SmtpUserDTO()
                {
                    Username = username,
                    Secret = password
                },
                Username = userToRead
            });
            return userResponseDto == null ? new List<EntityUserInfo>() : userResponseDto.UserList;
        }

        #region SMS message
        ////public EntityMessage CreateSmsMessage(
        ////  int idList,
        ////  ConsoleSmsMessageDetailItem message)
        ////{
        ////    return this.CallMethod<EntityMessage>(Method.POST, string.Format("Console/Sms/List/{0}/Message", (object)idList), (object)message);
        ////}
        ////public IEnumerable<EntityMessage> GetSmsMessages(
        ////  int idList,
        ////  string filterExpression = "",
        ////  string sortExpression = "",
        ////  int? pageSize = null,
        ////  int? pageNumber = null)
        ////{
        ////    return this.CallMethod<IEnumerable<EntityMessage>>(Method.GET, string.Format("Console/Sms/List/{0}/Messages", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        ////}
        ////public ConsoleSmsMessageDetailItem GetSmsMessageDetails(
        ////  int idList,
        ////  int idMessage)
        ////{
        ////    return this.CallMethod<ConsoleSmsMessageDetailItem>(Method.GET, string.Format("Console/Sms/List/{0}/Message/{1}", (object)idList, (object)idMessage), (object)null);
        ////}
        ////public IEnumerable<SmsSendingItem> GetSmsSendingStatus(
        ////  SendingStatus status)
        ////{
        ////    string url;
        ////    switch (status)
        ////    {
        ////        case SendingStatus.Immediate:
        ////            url = "Console/Sms/Sendings/Immediate";
        ////            break;
        ////        case SendingStatus.Deferred:
        ////            url = "Console/Sms/Sendings/Deferred";
        ////            break;
        ////        case SendingStatus.Undefined:
        ////            url = "Console/Sms/Sendings/Undefined";
        ////            break;
        ////        default:
        ////            throw new MailUpException(-1, "Unknown Sending Status");
        ////    }
        ////    return this.CallMethod<IEnumerable<SmsSendingItem>>(Method.GET, url, (object)null);
        ////}
        ////public DateTime GetSmsDateForDeferredDispatch(int idSending)
        ////{
        ////    CultureInfo invariantCulture = CultureInfo.InvariantCulture;
        ////    string s = this.CallMethod<string>(Method.GET, string.Format("Console/Sms/Sending/{0}/Deferred", (object)idSending), (object)null);
        ////    try
        ////    {
        ////        return DateTime.ParseExact(s, "u", (IFormatProvider)invariantCulture);
        ////    }
        ////    catch
        ////    {
        ////        return DateTime.MaxValue;
        ////    }
        ////}
        ////public EntityMessage UpdateSmsMessage(
        ////  int idList,
        ////  ConsoleSmsMessageDetailItem message)
        ////{
        ////    if (message != null)
        ////    {
        ////        int? idMessage = message.idMessage;
        ////        if ((idMessage.GetValueOrDefault() > 0 ? 0 : (idMessage.HasValue ? 1 : 0)) == 0)
        ////            return this.CallMethod<EntityMessage>(Method.PUT, string.Format("Console/Sms/List/{0}/Message/{1}", (object)idList, (object)message.idMessage), (object)message);
        ////    }
        ////    return (EntityMessage)null;
        ////}
        ////public SmsMessageSendResponse SendSmsMessageToList(
        ////  int idList,
        ////  int idMessage,
        ////  string senderPrefix = "",
        ////  string senderNumber = "",
        ////  DateTime? scheduledTime = null)
        ////{
        ////    return this.CallMethod<SmsMessageSendResponse>(Method.POST, this.PrepareURLToSendSms(string.Format("Console/Sms/List/{0}/Message/{1}/Send", (object)idList, (object)idMessage), senderPrefix, senderNumber, scheduledTime), (object)null);
        ////}
        ////public SmsMessageSendResponse SendSmsMessageToGroup(
        ////  int idGroup,
        ////  int idMessage,
        ////  string senderPrefix = "",
        ////  string senderNumber = "",
        ////  DateTime? scheduledTime = null)
        ////{
        ////    return this.CallMethod<SmsMessageSendResponse>(Method.POST, this.PrepareURLToSendSms(string.Format("Console/Sms/Group/{0}/Message/{1}/Send", (object)idGroup, (object)idMessage), senderPrefix, senderNumber, scheduledTime), (object)null);
        ////}
        ////public SmsMessageSendResponse SendSmsMessage(
        ////  SmsSendToRecipientRequest request,
        ////  string senderPrefix = "",
        ////  string senderNumber = "",
        ////  DateTime? scheduledTime = null)
        ////{
        ////    return this.CallMethod<SmsMessageSendResponse>(Method.POST, this.PrepareURLToSendSms("Console/Sms/Send", senderPrefix, senderNumber, scheduledTime), (object)request);
        ////}
        ////private string PrepareURLToSendSms(
        ////  string action,
        ////  string senderPrefix,
        ////  string senderNumber,
        ////  DateTime? scheduledTime)
        ////{
        ////    action = ClientUtils.AddQuerystringParam(action, nameof(senderPrefix), senderPrefix);
        ////    action = ClientUtils.AddQuerystringParam(action, nameof(senderNumber), senderNumber);
        ////    if (scheduledTime.HasValue)
        ////        action = ClientUtils.AddQuerystringParam(action, "datetime", scheduledTime.Value.ToString("yyyy-MM-dd HH:mm:ssZ"));
        ////    return action;
        ////}
        ////public SmsMessageSendingReport GetSmsSendingReport(
        ////  int idMessage,
        ////  string filterExpression = "",
        ////  string sortExpression = "",
        ////  int? pageSize = null,
        ////  int? pageNumber = null)
        ////{
        ////    return this.CallMethod<SmsMessageSendingReport>(Method.GET, string.Format("Console/Sms/{0}/Sendings/Report", (object)idMessage), (object)null);
        ////}
        ////public IEnumerable<SmsMessageSendingReportDetail> GetSmsSendingReportDetails(
        ////  int idMessage,
        ////  string filterExpression = "",
        ////  string sortExpression = "",
        ////  int? pageSize = null,
        ////  int? pageNumber = null)
        ////{
        ////    return this.CallMethod<IEnumerable<SmsMessageSendingReportDetail>>(Method.GET, string.Format("Console/Sms/{0}/Sendings/ReportDetails", (object)idMessage), (object)null);
        ////}
#endregion
    }
}

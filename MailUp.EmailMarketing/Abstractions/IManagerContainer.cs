using MailUp.EmailMarketing.Entities;
using MailUp.EmailMarketing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Abstractions
{
    public interface IManagerContainer //: IManagerBase
    {
        int CreateList(EntityList list);
        EntityList UpdateList(EntityList list);
        EntityList GetList(int idList);
        EntityList GetList(string listName);

        EntitiesLists GetLists(
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        EntityGroup CreateGroup(EntityGroup group);

        EntityGroup UpdateGroup(EntityGroup group);
        void DeleteGroup(int idList, int idGroup);
        EntityGroup GetGroup(int idList, int idGroup);
        EntityGroup GetGroup(int idList, string groupName);

        EntitiesGroups GetGroups(
            int idList,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        int AddRecipientToList(int idList, EntityRecipient recipient, bool withCOI = false);
        int AddRecipientsToList(int idList, List<EntityRecipient> recipients, bool withCOI = false);
        EntityImportStatus CheckImportStatus(int idImport);
        bool ForceOptinOnUnsubscribedRecipient(int idList, string recipientEmail);
        void UnsubscribeRecipient(int idList, int idRecipient);
        int UnsubscribeRecipients(int idList, List<string> emailRecipients);
        object GetPersonalDataFields();
        EntityRecipient UpdateRecipient(EntityRecipient recipient);
        EnumSubscriptionStatus GetEnumSubscriptionStatus(int idList, int idRecipient);

        EnumSubscriptionStatus GetEnumSubscriptionStatus(
            int idList,
            string emailRecipient);

        IEnumerable<EntityRecipient> GetRecipientsInStatus(
            EnumSubscriptionStatus status,
            int idList,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        int AddRecipientToGroup(int idGroup, EntityRecipient recipient);
        int AddRecipientsToGroup(int idGroup, List<EntityRecipient> recipients);
        void RemoveRecipientFromGroup(int idGroup, int idRecipient);
        IEnumerable<EntityRecipientUnsubscription> GetMessageUnsubscriptionsByRecipient(
            int idRecipient,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);
        IEnumerable<EntityRecipientUrlClickDetail> GetMessageClicksDetailsByRecipient(
            int idRecipient,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);
        IEnumerable<EntityRecipientMessageView> GetMessageViewsByRecipient(
            int idRecipient,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        int CountMessageViewsByRecipient(int idRecipient, string filterExpression = "");

        IEnumerable<EntityRecipientClickDetail> GetMessageClicksByRecipient(
            int idRecipient,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);
    }
}

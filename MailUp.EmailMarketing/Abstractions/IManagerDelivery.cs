using MailUp.EmailMarketing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Abstractions
{
    public interface IManagerDelivery: IManagerBase
    {
        EntityMessage CreateMessage(
            int idList,
            EntityMessage message);

        IEnumerable<EntityMessage> GetMessages(
            int idList,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        IEnumerable<EntityPublishedMessage> GetPublishedMessages(
            int idList,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        IEnumerable<EntityArchivedMessage> GetArchivedMessages(
            int idList,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        EntityMessageDetails GetMessageDetails(int idList, int idMessage);

        List<EntityEmailAttachment> GetMessageAttachments(
            int idList,
            int idMessage);

        IEnumerable<EntityEmailSending> GetEmailSendingStatus(
            EntitySendingStatus status);

        DateTime GetDateForDeferredDispatch(int idSending);
        int GetSendingIDForImport(int idImport);

        EntityMessage UpdateMessage(
            int idList,
            EntityEmail message);

        IEnumerable<EntityTag> GetTags(
            int idList,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        EntityTag CreateTag(int idList, string name);
        EntityTag UpdateTag(int idList, EntityTag tag);
        void DeleteTag(int idList, int idTag);

        List<EntityEmailAttachment> AddMessageAttachment(
            EntityAddAttachmentRequest attachRequest);

        List<EntityEmailAttachment> DeleteMessageAttachment(
            int idList,
            int idMessage,
            int slot);

        EntityMailMessageSendResponse SendEmailMessageToList(
            int idList,
            int idMessage,
            string senderName = "",
            string senderAddress = "",
            DateTime? scheduledTime = null);

        EntityMailMessageSendResponse SendEmailMessageToGroup(
            int idGroup,
            int idMessage,
            string senderName = "",
            string senderAddress = "",
            DateTime? scheduledTime = null);

        EntityMailMessageSendResponse SendEmailMessage(
            EntityEmailSendToRecipientRequest request,
            string senderName = "",
            string senderAddress = "",
            DateTime? scheduledTime = null);

        EntityEmailSending SendEmailMessage(int idSending, DateTime? scheduledTime = null);

        IEnumerable<EntityMessageSendHistory> GetEmailMessageSendHistory(
            int idList,
            int idMessage,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        IEnumerable<EntityMessageRecipient> GetMessageRecipients(
            int idMessage,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        int CountMessageRecipients(int idMessage, string filterExpression = "");

        IEnumerable<EntityMessageView> GetMessageViews(
            int idMessage,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        int CountMessageViews(int idMessage, string filterExpression = "");

        IEnumerable<EntityMessageRecipientClickDetail> GetMessageClicks(
            int idMessage,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        int CountMessageClicks(int idMessage, string filterExpression = "");

        IEnumerable<EntityMessageUrlClick> GetMessageUrlClicks(
            int idMessage,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        IEnumerable<EntityMessageUrlClickDetail> GetMessageUrlClicksDetails(
            int idMessage,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        IEnumerable<EntityMessageBounce> GetMessageBounces(
            int idMessage,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        int CountMessageBounces(int idMessage, string filterExpression = "");

        IEnumerable<EntityMessageUnsubscription> GetMessageUnsubscriptions(
            int idMessage,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        int CountMessageUnsubscriptions(int idMessage, string filterExpression = "");

        IEnumerable<EntityMessageDelivery> GetMessageDeliveries(
            int idRecipient,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        int CountMessageDeliveries(int idRecipient, string filterExpression = "");



        int CountMessageClicksByRecipient(int idRecipient, string filterExpression = "");


        IEnumerable<EntityMessageBounce> GetMessageBouncesByRecipient(
            int idRecipient,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);

        int CountMessageBouncesByRecipient(int idRecipient, string filterExpression = "");

        IEnumerable<EntityMessageBounceDetail> GetMessageBouncesDetails(
            int idRecipient,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null);


        int CountMessageUnsubscriptionsByRecipient(int idRecipient, string filterExpression = "");
        EntitySendResponse SendMessage(EntityMessage messageInfo);
        EntitySendResponse SendTemplate(EntityTemplate messageInfo);

    }
}

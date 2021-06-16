using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Core;
using MailUp.EmailMarketing.Entities;
using System;
using System.Collections.Generic;
using MailUp.EmailMarketing.Model;

namespace MailUp.EmailMarketing.Implementation
{
    public class ManagerDelivery : ManagerBase, IManagerDelivery
    {
        public ManagerDelivery(AuthorizationModel model, MailUpConfigurations.MailUpApiv1 config)
            : base(model, config)
        {
        }

        public List<EntityEmailAttachment> AddMessageAttachment(EntityAddAttachmentRequest attachRequest)
        {
            throw new NotImplementedException();
        }

        public int CountMessageBounces(int idMessage, string filterExpression = "")
        {
            throw new NotImplementedException();
        }

        public int CountMessageBouncesByRecipient(int idRecipient, string filterExpression = "")
        {
            throw new NotImplementedException();
        }

        public int CountMessageClicks(int idMessage, string filterExpression = "")
        {
            throw new NotImplementedException();
        }

        public int CountMessageClicksByRecipient(int idRecipient, string filterExpression = "")
        {
            throw new NotImplementedException();
        }

        public int CountMessageDeliveries(int idRecipient, string filterExpression = "")
        {
            throw new NotImplementedException();
        }

        public int CountMessageRecipients(int idMessage, string filterExpression = "")
        {
            throw new NotImplementedException();
        }

        public int CountMessageUnsubscriptions(int idMessage, string filterExpression = "")
        {
            throw new NotImplementedException();
        }

        public int CountMessageUnsubscriptionsByRecipient(int idRecipient, string filterExpression = "")
        {
            throw new NotImplementedException();
        }

        public int CountMessageViews(int idMessage, string filterExpression = "")
        {
            throw new NotImplementedException();
        }

        public int CountMessageViewsByRecipient(int idRecipient, string filterExpression = "")
        {
            throw new NotImplementedException();
        }

        public EntityMessage CreateMessage(int idList, EntityMessage message)
        {
            throw new NotImplementedException();
        }

        public EntityTag CreateTag(int idList, string name)
        {
            throw new NotImplementedException();
        }

        public List<EntityEmailAttachment> DeleteMessageAttachment(int idList, int idMessage, int slot)
        {
            throw new NotImplementedException();
        }

        public void DeleteTag(int idList, int idTag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityArchivedMessage> GetArchivedMessages(int idList, string filterExpression = "",
            string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateForDeferredDispatch(int idSending)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessageSendHistory> GetEmailMessageSendHistory(int idList, int idMessage,
            string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityEmailSending> GetEmailSendingStatus(EntitySendingStatus status)
        {
            throw new NotImplementedException();
        }

        public List<EntityEmailAttachment> GetMessageAttachments(int idList, int idMessage)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessageBounce> GetMessageBounces(int idMessage, string filterExpression = "",
            string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessageBounce> GetMessageBouncesByRecipient(int idRecipient,
            string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessageBounceDetail> GetMessageBouncesDetails(int idRecipient,
            string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessageRecipientClickDetail> GetMessageClicks(int idMessage,
            string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityRecipientClickDetail> GetMessageClicksByRecipient(int idRecipient, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityRecipientUrlClickDetail> GetMessageClicksDetailsByRecipient(int idRecipient, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessageDelivery> GetMessageDeliveries(int idRecipient, string filterExpression = "",
            string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public EntityMessageDetails GetMessageDetails(int idList, int idMessage)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessageRecipient> GetMessageRecipients(int idMessage, string filterExpression = "",
            string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessage> GetMessages(int idList, string filterExpression = "",
            string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessageUnsubscription> GetMessageUnsubscriptions(int idMessage,
            string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityRecipientUnsubscription> GetMessageUnsubscriptionsByRecipient(int idRecipient, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessageUrlClick> GetMessageUrlClicks(int idMessage, string filterExpression = "",
            string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessageUrlClickDetail> GetMessageUrlClicksDetails(int idMessage,
            string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityMessageView> GetMessageViews(int idMessage, string filterExpression = "",
            string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityRecipientMessageView> GetMessageViewsByRecipient(int idRecipient, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityPublishedMessage> GetPublishedMessages(int idList, string filterExpression = "",
            string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public int GetSendingIDForImport(int idImport)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityTag> GetTags(int idList, string filterExpression = "", string sortExpression = "",
            int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public EntityMailMessageSendResponse SendEmailMessage(EntityEmailSendToRecipientRequest request,
            string senderName = "", string senderAddress = "", DateTime? scheduledTime = null)
        {
            throw new NotImplementedException();
        }

        public EntityEmailSending SendEmailMessage(int idSending, DateTime? scheduledTime = null)
        {
            throw new NotImplementedException();
        }

        public EntityMailMessageSendResponse SendEmailMessageToGroup(int idGroup, int idMessage, string senderName = "",
            string senderAddress = "", DateTime? scheduledTime = null)
        {
            throw new NotImplementedException();
        }

        public EntityMailMessageSendResponse SendEmailMessageToList(int idList, int idMessage, string senderName = "",
            string senderAddress = "", DateTime? scheduledTime = null)
        {
            throw new NotImplementedException();
        }

        public EntitySendResponse SendMessage(EntityMessage messageInfo)
        {
            throw new NotImplementedException();
        }

        public EntitySendResponse SendTemplate(EntityTemplate messageInfo)
        {
            throw new NotImplementedException();
        }

        public EntityMessage UpdateMessage(int idList, EntityEmail message)
        {
            throw new NotImplementedException();
        }

        public EntityTag UpdateTag(int idList, EntityTag tag)
        {
            throw new NotImplementedException();
        }
    }
}
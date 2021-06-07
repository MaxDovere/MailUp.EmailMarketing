using MailUp.EmailMarketing.Abstractions;
using MailUp.EmailMarketing.Configurations;
using MailUp.EmailMarketing.Core;
using MailUp.EmailMarketing.Entities;
using MailUp.EmailMarketing.Model;
using System;
using System.Collections.Generic;

namespace MailUp.EmailMarketing.Implementation
{
    public class ManagerContainer : ManagerBase, IManagerContainer
    {
        public ManagerContainer(AuthorizationModel model, ConfigurationsMailUp.MailUpApiv1 config)
        : base(model, config)
        {
        }

        public int AddRecipientsToGroup(int idGroup, List<EntityRecipient> recipients)
        {
            

            throw new NotImplementedException();
        }

        public int AddRecipientsToList(int idList, List<EntityRecipient> recipients, bool withCOI = false)
        {
            throw new NotImplementedException();
        }

        public int AddRecipientToGroup(int idGroup, EntityRecipient recipient)
        {
            throw new NotImplementedException();
        }

        public int AddRecipientToList(int idList, EntityRecipient recipient, bool withCOI = false)
        {
            throw new NotImplementedException();
        }

        public EntityImportStatus CheckImportStatus(int idImport)
        {
            throw new NotImplementedException();
        }

        public int CountMessageViewsByRecipient(int idRecipient, string filterExpression = "")
        {
            throw new NotImplementedException();
        }

        public EntityGroup CreateGroup(EntityGroup group)
        {
            throw new NotImplementedException();
        }

        public int CreateList(EntityList list)
        {
            throw new NotImplementedException();
        }

        public void DeleteGroup(int idList, int idGroup)
        {
            throw new NotImplementedException();
        }

        public bool ForceOptinOnUnsubscribedRecipient(int idList, string recipientEmail)
        {
            throw new NotImplementedException();
        }

        public EnumSubscriptionStatus GetEnumSubscriptionStatus(int idList, int idRecipient)
        {
            throw new NotImplementedException();
        }

        public EnumSubscriptionStatus GetEnumSubscriptionStatus(int idList, string emailRecipient)
        {
            throw new NotImplementedException();
        }

        public EntityGroup GetGroup(int idList, int idGroup)
        {
            throw new NotImplementedException();
        }

        public EntityGroup GetGroup(int idList, string groupName)
        {
            throw new NotImplementedException();
        }

        public EntitiesGroups GetGroups(int idList, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public EntityList GetList(int idList)
        {
            throw new NotImplementedException();
        }

        public EntityList GetList(string listName)
        {
            throw new NotImplementedException();
        }

        public EntitiesLists GetLists(string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
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

        public IEnumerable<EntityRecipientUnsubscription> GetMessageUnsubscriptionsByRecipient(int idRecipient, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityRecipientMessageView> GetMessageViewsByRecipient(int idRecipient, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public object GetPersonalDataFields()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityRecipient> GetRecipientsInStatus(EnumSubscriptionStatus status, int idList, string filterExpression = "", string sortExpression = "", int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveRecipientFromGroup(int idGroup, int idRecipient)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeRecipient(int idList, int idRecipient)
        {
            throw new NotImplementedException();
        }

        public int UnsubscribeRecipients(int idList, List<string> emailRecipients)
        {
            throw new NotImplementedException();
        }

        public EntityGroup UpdateGroup(EntityGroup group)
        {
            throw new NotImplementedException();
        }

        public EntityList UpdateList(EntityList list)
        {
            throw new NotImplementedException();
        }

        public EntityRecipient UpdateRecipient(EntityRecipient recipient)
        {
            throw new NotImplementedException();
        }
    }
}

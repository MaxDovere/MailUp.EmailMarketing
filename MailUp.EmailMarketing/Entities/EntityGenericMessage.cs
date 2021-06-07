using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Entities
{
    /// <summary>
    /// Represents a generic container for information regarding a message to be sent
    /// </summary>
    public class EntityGenericMessage
    {
        /// <summary>the subject of the message</summary>
        /// <returns></returns>
        public string Subject { get; set; }

        /// <summary>the sender's name and email address</summary>
        /// <returns></returns>
        public EntityEmailAddress From { get; set; }

        /// <summary>the list of recipients in To header</summary>
        /// <returns></returns>
        public List<EntityEmailAddress> To { get; set; }

        /// <summary>the list of recipients in Cc header</summary>
        /// <returns></returns>
        public List<EntityEmailAddress> Cc { get; set; }

        /// <summary>the list of recipients in Bcc</summary>
        /// <returns></returns>
        public List<EntityEmailAddress> Bcc { get; set; }

        /// <summary>the email address to be added into Reply-To header</summary>
        /// <returns></returns>
        public string ReplyTo { get; set; }

        /// <summary>the charset of the message body</summary>
        /// <returns></returns>
        public string CharSet { get; set; }

        /// <summary>
        /// list of custom headers (only SMTP headers that are approved by MailUp will be added)
        /// </summary>
        /// <returns></returns>
        public List<EntityNameValue> ExtendedHeaders { get; set; }

        /// <summary>list of attachments</summary>
        /// <returns></returns>
        public List<EntityMessagePart> Attachments { get; set; }

        /// <summary>list of embedded images</summary>
        /// <returns></returns>
        public List<EntityMessagePart> EmbeddedImages { get; set; }

        /// <summary>
        /// the X-SMTPAPI header value, used for custom aggregations and configurations
        /// </summary>
        /// <returns></returns>
        public EntityXSmtpAPI XSmtpAPI { get; set; }

        /// <summary>
        /// the SMTP+ user credentials: the API uses the same credentials as the SMTP relay
        /// </summary>
        /// <returns></returns>
        public EntitySmtpUser User { get; set; }
    }

}

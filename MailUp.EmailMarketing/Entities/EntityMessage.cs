using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Entities
{
    public class EntityHtml
    {
        /// <summary>the DOCTYPE directive</summary>
        /// <returns></returns>
        public string DocType { get; set; }

        /// <summary>the head content</summary>
        public string Head { get; set; }

        /// <summary>the body content</summary>
        /// <returns></returns>
        public string Body { get; set; }

        /// <summary>the body tag, default is "&lt;body&gt;"</summary>
        /// <returns></returns>
        public string BodyTag { get; set; }
    }
    public class EntityMessage : EntityGenericMessage
    {
        /// <summary>
        /// HTML part of the message (specify only the HTML inside the BODY tag)
        /// </summary>
        /// <returns></returns>
        public EntityHtml Html { get; set; }

        /// <summary>the plain text part of the message</summary>
        /// <returns></returns>
        public string Text { get; set; }
    }
}

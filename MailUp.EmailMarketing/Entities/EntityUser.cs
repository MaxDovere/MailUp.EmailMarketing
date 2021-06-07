using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Entities
{
    /// <summary>Information regarding a user</summary>
    public class EntityUserInfo
    {
        /// <summary>the username</summary>
        /// <returns></returns>
        public string Username { get; set; }

        /// <summary>the MailUp account ID</summary>
        /// <returns></returns>
        public long IdConsole { get; set; }

        /// <summary>the List Id within that account</summary>
        /// <returns></returns>
        public long IdList { get; set; }

        /// <summary>true if the user is enabled</summary>
        /// <returns></returns>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// default priority setting for messages sent by this user
        /// </summary>
        /// <returns></returns>
        public int Priority { get; set; }

        /// <summary>admin notes</summary>
        /// <returns></returns>
        public string Note { get; set; }

        /// <summary>creation date</summary>
        /// <returns></returns>
        public DateTime CreationDate { get; set; }

        /// <summary>last updated date</summary>
        /// <returns></returns>
        public DateTime UpdateDate { get; set; }

        /// <summary>list of allowed domains</summary>
        /// <returns></returns>
        public string DomainList { get; set; }

        /// <summary>list of allowed email senders</summary>
        /// <returns></returns>
        public string SenderList { get; set; }

        /// <summary>date until the user is blocked</summary>
        /// <returns></returns>
        public DateTime BlockUntil { get; set; }

        /// <summary>
        /// true if the user is blocked by the system administrator
        /// </summary>
        /// <returns></returns>
        public bool AdminBlock { get; set; }
    }

}

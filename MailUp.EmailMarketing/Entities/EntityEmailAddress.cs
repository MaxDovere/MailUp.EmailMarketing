using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Entities
{
    /// <summary>Recipient or sender of an email</summary>
    public class EntityEmailAddress
    {
        /// <summary>the name</summary>
        /// <returns></returns>
        public string Name { get; set; }

        /// <summary>the email address</summary>
        /// <returns></returns>
        public string Email { get; set; }
    }
}

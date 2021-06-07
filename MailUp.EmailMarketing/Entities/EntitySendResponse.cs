using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Entities
{
    public class EntitySendResponse
    {
        /// <summary>the status of the response can be "done" or "error"</summary>
        /// <returns></returns>
        public string Status { get; set; }

        /// <summary>the result code(see the Error Code table)</summary>
        /// <returns></returns>
        public string Code { get; set; }

        /// <summary>the result message</summary>
        /// <returns></returns>
        public string Message { get; set; }
    }
}

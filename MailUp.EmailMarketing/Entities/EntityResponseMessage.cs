using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Entities
{
    public class EntityResponseMessage
    {
        public int? idList { get; set; }

        public int? idMessage { get; set; }

        public string Subject { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Entities
{
    public class EntityImportStatus
    {
        public int idImport { get; set; }

        public bool Completed { get; set; }

        public int UpdatedRecipients { get; set; }

        public int ValidRecipients { get; set; }

        public int CreatedRecipients { get; set; }

        public int ImportedRecipients { get; set; }

        public int NotValidRecipients { get; set; }
    }
}

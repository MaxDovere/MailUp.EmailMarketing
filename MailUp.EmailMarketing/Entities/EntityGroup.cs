using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Entities
{
    
    public class EntityGroup: EntityBase
    {
        //{"Deletable":true,"Name":"Test REST API Group","Notes":"Notes should go here","idGroup":30,"idList":2}
        public bool Deletable { get; set; }
        public string Name{ get; set; }
        public string Notes { get; set; }
        public int IdGroup { get; set; }
        public int IdList { get; set; }
    }
}

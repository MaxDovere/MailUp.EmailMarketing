using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Entities
{
    public class Field
    {
        //  "Description":"name",
        public string Description { get; set; }
        //  "Id":1,
        public int Id { get; set; }
        //  "Value":"test"
        public string Value { get; set; }
    }
    public class EntityRecipient
    {
        //"Fields":[{
        public Field[] Fields { get; set; }
        //"Name":"String content",
        public string Name { get; set; }
        //"Email":"test@example.com",
        public string Email { get; set; }
        //"MobileNumber":"",
        public string MobileNumber { get; set; }
        //"MobilePrefix":""
        public string MobilePrefix { get; set; }
    }

    public class EntityResponseRecipient
    {
        //  "Removed": 0
        public int Removed { get; set; }
    }
}

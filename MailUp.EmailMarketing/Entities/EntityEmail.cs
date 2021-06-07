using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Entities
{
    public class EntityEmail : EntityBase
    { 
        //  "Email":"youremailaddress@yourdomain.eu"
        public string Email { get; set; }
        public int? IdMessage { get; set; }
    }
    
    public class EntityItemEmail : EntityBase
    {
        //  "AuthorizationDate": "",
        public string AuthorizationDate { get; set; }
        //  "CreationDate": "2016-11-28 12:34:36Z",
        public string CreationDate { get; set; }
        //  "EmailAddress": "youremailaddress@yourdomain.eu",
        public string EmailAddress { get; set; }
        //  "IdTrustedSender": "ror11zuGscWcjl7m",
        public string IdTrustedSender { get; set; }
        //  "StatusCode": 0,
        public int StatusCode { get; set; }
        //  "StatusDescription": "Not confirmed (status: NotConfirmed)",
        public string StatusDescription { get; set; }
        //  "UpdateDate": "2016-11-28 12:34:36Z"
        public string UpdateDate { get; set; }
    }
    public class EntityResponseEmail
    {
        //    "IsPaginated": false,
        public bool IsPaginated { get; set; }
        //    "Items": [
        public EntityItemEmail[] Items { get; set; }
        //    "PageNumber": 0,
        public int PageNumber { get; set; }
        //    "PageSize": 20,
        public int PageSize { get; set; }
        //    "Skipped": 0,
        public int Skipped { get; set; }
        //    "TotalElementsCount": 4
        public int TotalElementsCount { get; set; }
    }
}
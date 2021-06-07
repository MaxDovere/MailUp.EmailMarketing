using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Entities
{
    public class EntityList : EntityBase
    {  
        //   "Name":"New Arrivals",
        public string Name { get; set; }
        //   "Business":true,
        public bool Business { get; set; }
        //   "Customer":true,
        public bool Customer { get; set; }
        //   "OwnerEmail":"jane@example.com",
        public string OwnerEmail { get; set; }
        //   "ReplyTo":"mike@example.com",
        public string ReplyTo { get; set; }
        //   "NLSenderName": "Your sender name",
        public string NLSenderName { get; set; }
        //   "CompanyName":"Your company",
        public string CompanyName { get; set; }
        //   "ContactName":"Your name",
        public string ContactName { get; set; }
        //   "Address":"Your address",
        public string Address { get; set; }
        //   "City":"Your city",
        public string City { get; set; }
        //   "CountryCode":"IT",
        public string CountryCode { get; set; }
        //   "PermissionReminder":"Your permission reminder",
        public string PermissionReminder { get; set; }
        //   "DisplayAs": "Your sender name",
        public string DisplayAs { get; set; }
        //   "WebSiteUrl":"Your site",
        public string WebSiteUrl { get; set; }
        //"Description": "",
        public string Description { get; set; }
        //"Phone": "",
        public string Phone { get; set; }
        //"PostalCode": "",
        public string PostalCode { get; set; }
        //"StateOrProvince": "",
        public string StateOrProvince { get; set; }
        //"TimeZoneCode": "UTC+01:00.0",
        public string TimeZoneCode { get; set; }
        //"SmsSenderName": "",
        public string SmsSenderName { get; set; }
        //"DefaultPrefix": "0039",
        public string DefaultPrefix { get; set; }
        //"SendConfirmSms": false,
        public string SendConfirmSms { get; set; }
        //  "SendEmailOptout": false,
        public bool SendEmailOptout { get; set; }
        //  "Charset": "UTF-8",
        public string Charset { get; set; }
        //"Format": "html",
        public string Format { get; set; }
        //"MultipartText": true,
        public bool MultipartText { get; set; }
        //"KBMax": 100,
        public int KBMax { get; set; }
        //"NotifyEmail": "",
        public string NotifyEmail { get; set; }
        //"OptoutType": 3,
        public int OptoutType { get; set; }
        //"MultiOptoutList": "19",
        public string MultiOptoutList { get; set; }
        //"SubscribedEmail": true,
        public bool SubscribedEmail { get; set; }
        //"BouncedEmail": null,
        public string BouncedEmail { get; set; }
        //"FrontendForm": true,
        public bool FrontendForm { get; set; }
        //"Public": true,
        public bool Public { get; set; }
        //"ScopeCode": 0,
        public int ScopeCode { get; set; }
        //"TrackOnOpened": true
        public bool TrackOnOpened { get; set; }
        //"ConversionlabTrackCode": "",
        public string ConversionlabTrackCode { get; set; }
        //"LinkTrackingParameters": "",
        public string LinkTrackingParameters { get; set; }
        //"Disclaimer": "Per l'informativa sulla privacy D.Lgs 196/2003 visitare l'home page del sito. <br/> Policy AntiSPAM garantita da <a href=\"http://www.mailup.it/email-marketing/policy-antispam.asp\" target=_blank><img src=\"http://doc.mailupnet.it/logo_small_R.gif\" border=\"0\" align=\"middle\" /></a>",
        public string Disclaimer { get; set; }
        //"HeaderListUnsubscriber": "<[listunsubscribe]>,<[mailto_uns]>",
        public string HeaderListUnsubscriber { get; set; }
        //"HeaderXAbuse": "Please report abuse here:  http://www.mailup.it/email-marketing/Policy-antispam_ENG.asp",
        public string HeaderXAbuse { get; set; }
        //"IdSettings":14
        public int IdSettings { get; set; }
        //"UseDefaultSettings":true
        public bool UseDefaultSettings { get; set; }
        public int IdList { get; set; }
    }
    public class EntityCreateList : EntityBase
    {
        //  "IdList": 19,
        public int IdList { get; set; }
        //  "ListGuid": "57f9c675-63d6-498f-91b6-7f60f9f30b2e",
        public string ListGuid { get; set; }
        //  "Name": "New Arrivals",
        public string Name { get; set; }
        //  "Description": "",
        public string Description { get; set; }
        //  "NLSenderName": "Your sender name",
        public string NLSenderName { get; set; }
        //  "OwnerEmail": "jane@example.com",
        public string OwnerEmail { get; set; }
        //  "PermissionReminder": "Your permission reminder",
        public string PermissionReminder { get; set; }
        //  "Address": "Your address",
        public string Address { get; set; }
        //  "Business": true,
        public bool Business { get; set; }
        //  "City": "Your city",
        public string City { get; set; }
        //  "CompanyName": "Your company",
        public string CompanyName { get; set; }
        //  "ContactName": "Your name",
        public string ContactName { get; set; }
        //  "CountryCode": "IT",
        public string CountryCode { get; set; }
        //  "Customer": true,
        public bool Customer { get; set; }
        //  "DisplayAs": "",
        public string DisplayAs { get; set; }
        //  "Phone": "",
        public string Phone { get; set; }
        //  "PostalCode": "",
        public string PostalCode { get; set; }
        //  "ReplyTo": "mike@example.com",
        public string ReplyTo { get; set; }
        //  "StateOrProvince": "",
        public string StateOrProvince { get; set; }
        //  "WebSiteUrl": "www.mailup.it"
        public string WebSiteUrl { get; set; }
        //  "BouncedEmail": null,
        public string BouncedEmail { get; set; }
        //  "Charset": "UTF-8",
        public string Charset { get; set; }
        //  "ConversionlabTrackCode": "",
        public string ConversionlabTrackCode { get; set; }
        //  "DefaultPrefix": "0039",
        public string DefaultPrefix { get; set; }
        //  "Disclaimer": "Per l'informativa sulla privacy D.Lgs 196/2003 visitare l'home page del sito. <br/> Policy AntiSPAM garantita da <a href=\"http://www.mailup.it/email-marketing/policy-antispam.asp\" target=_blank><img src=\"http://doc.mailupnet.it/logo_small_R.gif\" border=\"0\" align=\"middle\" /></a>",
        public string Disclaimer { get; set; }
        //  "Format": "html",
        public string Format { get; set; }
        //  "FrontendForm": true,
        public bool FrontendForm { get; set; }
        //  "HeaderListUnsubscriber": "<[listunsubscribe]>,<[mailto_uns]>",
        public string HeaderListUnsubscriber { get; set; }
        //  "HeaderXAbuse": "Please report abuse here:  http://www.mailup.it/email-marketing/Policy-antispam_ENG.asp",
        public string HeaderXAbuse { get; set; }
        //  "KBMax": 100,
        public int KBMax { get; set; }
        //  "LinkTrackingParameters": "",
        public string LinkTrackingParameters { get; set; }
        //  "MultiOptoutList": "19",
        public string MultiOptoutList { get; set; }
        //  "MultipartText": true,
        public bool MultipartText { get; set; }
        //  "NotifyEmail": "",
        public string NotifyEmail { get; set; }
        //  "OptoutType": 3,
        public int OptoutType { get; set; }
        //  "Public": true,
        public bool Public { get; set; }
        //  "ScopeCode": 0,
        public int ScopeCode { get; set; }
        //  "SendConfirmSms": false,
        public bool SendConfirmSms { get; set; }
        //  "SendEmailOptout": false,
        public bool SendEmailOptout { get; set; }
        //  "SmsSenderName": "",
        public string SmsSenderName { get; set; }
        //  "SubscribedEmail": true,
        public bool SubscribedEmail { get; set; }
        //  "TimeZoneCode": "UTC+01:00.0",
        public string TimeZoneCode { get; set; }
        //  "TrackOnOpened": true
        public bool TrackOnOpened { get; set; }
    }
    public class EntityResponseList : EntityBase
    {
        //"BouncedEmail": null,
        public string BouncedEmail { get; set; }
        //"Charset": "UTF-8",
        public string Charset { get; set; }
        //"ConversionlabTrackCode": "",
        public string ConversionlabTrackCode { get; set; }
        //"DefaultPrefix": "0039",
        public string DefaultPrefix { get; set; }
        //"Description": "I ADDED THIS FEW LINES TO DESCRIBE THE PURPOUS OF THIS LIST",
        public string Description { get; set; }
        //"Disclaimer": "Per l'informativa sulla privacy D.Lgs 196/2003 visitare l'home page del sito. <br/> Policy AntiSPAM garantita da <a href=\"http://www.mailup.it/email-marketing/policy-antispam.asp\" target=_blank><img src=\"http://doc.mailupnet.it/logo_small_R.gif\" border=\"0\" align=\"middle\" /></a>",
        public string Disclaimer { get; set; }
        //"Format": "html",
        public string Format { get; set; }
        //"FrontendForm": true,
        public bool FrontendForm { get; set; }
        //"HeaderListUnsubscriber": "<[listunsubscribe]>,<[mailto_uns]>",
        public string HeaderListUnsubscriber { get; set; }
        //"HeaderXAbuse": "Please report abuse here:  http://www.mailup.it/email-marketing/Policy-antispam_ENG.asp",
        public string HeaderXAbuse { get; set; }
        //"KBMax": 100,
        public int KBMax { get; set; }
        //"LinkTrackingParameters": "",
        public string LinkTrackingParameters { get; set; }
        //"MultiOptoutList": "21",
        public string MultiOptoutList { get; set; }
        //"MultipartText": true,
        public bool MultipartText { get; set; }
        //"NotifyEmail": null,
        public string NotifyEmail { get; set; }
        //"OptoutType": 3,
        public int OptoutType { get; set; }
        //"Public": true,
        public bool Public { get; set; }
        //"ScopeCode": 0,
        public int ScopeCode { get; set; }
        //"SendConfirmSms": false,
        public bool SendConfirmSms { get; set; }
        //"SendEmailOptout": false,
        public bool SendEmailOptout { get; set; }
        //"SmsSenderName": "",
        public string SmsSenderName { get; set; }
        //"SubscribedEmail": true,
        public bool SubscribedEmail { get; set; }
        //"TimeZoneCode": "UTC+01:00.0",
        public string TimeZoneCode { get; set; }
        //"TrackOnOpened": true,
        public bool TrackOnOpened { get; set; }
        //"Address": "Your address",
        public string Address { get; set; }
        //"Business": true,
        public bool Business { get; set; }
        //"City": "Your city",
        public string City { get; set; }
        //"CompanyName": "Your MODIFIED company NAME",
        public string CompanyName { get; set; }
        //"ContactName": "Your name",
        public string ContactName { get; set; }
        //"CountryCode": "IT",
        public string CountryCode { get; set; }
        //"Customer": true,
        public bool Customer { get; set; }
        //"DisplayAs": "Your sender name",
        public string DisplayAs { get; set; }
        //"IdList": 21,
        public int IdList { get; set; }
        //"ListGuid": "1239a201-47d1-46fa-b5a1-384ad6e78c60",
        public string ListGuid { get; set; }
        //"NLSenderName": "Your list name",
        public string NLSenderName { get; set; }
        //"Name": "NEW LIST NAME",
        public string Name { get; set; }
        //"OwnerEmail": "jane@example.com",
        public string OwnerEmail { get; set; }
        //"PermissionReminder": "Your permission reminder",
        public string PermissionReminder { get; set; }
        //"Phone": "",
        public string Phone { get; set; }
        //"PostalCode": "",
        public string PostalCode { get; set; }
        //"ReplyTo": "mike@example.com",
        public string ReplyTo { get; set; }
        //"StateOrProvince": "",
        public string StateOrProvince { get; set; }
        //"WebSiteUrl": "My NEW site"
        public string WebSiteUrl { get; set; }
    }
    public class Item
    {
        public int? Count { get; set; }
        public bool Deletable { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int? idGroup { get; set; }
        public int? idList { get; set; }
    }
    public class Version
    {
        public int major { get; set; }
        public int minor { get; set; }
        public int build { get; set; }
        public int revision { get; set; }
        public int majorRevision { get; set; }
        public int minorRevision { get; set; }
    }
    public class EntitiesLists : EntityBase
    {
        public bool IsPaginated { get; set; }
        public Item[] Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Skipped { get; set; }
        public int TotalElementsCount { get; set; }
        public string Type { get; set; }
        public Version Version { get; set; }
        public int StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public string[] Headers { get; set; }
        public string[] TrailingHeaders { get; set; }
        public string Message { get; set; }
        public bool isSuccessStatusCode { get; set; }
    }
    public class EntitiesGroups : EntityBase
    {
        public bool IsPaginated { get; set; }
        public Item[] Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Skipped { get; set; }
        public int TotalElementsCount { get; set; }
        public string Type { get; set; }
        public Version Version { get; set; }
        public int StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public string[] Headers { get; set; }
        public string[] TrailingHeaders { get; set; }
        public string Message { get; set; }
        public bool isSuccessStatusCode { get; set; }
    }
}

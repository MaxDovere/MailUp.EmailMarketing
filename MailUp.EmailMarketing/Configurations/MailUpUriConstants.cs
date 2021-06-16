using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Configurations
{
    public static class MailUpUriConstants
    {
        public const string SCHEME_AUTH = "Bearer";
        /// <summary>Relative URL to create a new list</summary>
        public const string CREATE_LIST = "Console/User/Lists";
        /// <summary>Relative URL to update a list</summary>
        public const string UPDATE_LIST = "Console/User/List/{0}";
        /// <summary>Relative URL to retrieve user's lists</summary>
        public const string GET_LISTS = "Console/User/Lists";
        /// <summary>Relative URL to create a new group</summary>
        public const string CREATE_GROUP = "Console/List/{0}/Group";
        /// <summary>Relative URL to update a group</summary>
        public const string UPDATE_GROUP = "Console/List/{0}/Group/{1}";
        /// <summary>Relative URL to delete a group</summary>
        public const string DELETE_GROUP = "Console/List/{0}/Group/{1}";
        /// <summary>Relative URL to retrieve list's groups</summary>
        public const string GET_GROUPS = "Console/List/{0}/Groups";
        /// <summary>Relative URL to import a recipient</summary>
        public const string IMPORT_RECIPIENT = "Console/List/{0}/Recipient";
        /// <summary>Relative URL to import several recipients</summary>
        public const string IMPORT_RECIPIENTS = "Console/List/{0}/Recipients";
        /// <summary>Relative URL to check import status</summary>
        public const string CHECK_IMPORT_STATUS = "Console/Import/{0}";
        /// <summary>
        ///     Relative URL to force opt-in on unsubscribed recipients
        /// </summary>
        public const string FORCE_OPTIN_UNSUB_REC = "Console/List/{0}/Recipients?importType=asOptin";
        /// <summary>Relative URL to unsubscribe a recipient</summary>
        public const string UNSUBSCRIBE_RECIPIENT = "Console/List/{0}/Unsubscribe/{1}";
        /// <summary>Relative URL to unsubscribe several recipients</summary>
        public const string UNSUBSCRIBE_RECIPIENTS = "Console/List/{0}/Recipients?importType=asOptout";
        /// <summary>
        ///     Relative URL to subscribe several recipients with confirmed opt-in
        /// </summary>
        public const string SUBSCRIBE_RECIPIENTS_COI = "Console/List/{0}/Recipients?confirmMessage=true";
        /// <summary>Relative URL to read personal data fields</summary>
        public const string READ_DATA_FIELDS = "Console/Recipient/DynamicFields";
        /// <summary>Relative URL to update personal data fields</summary>
        public const string UPDATE_DATA_FIELDS = "Console/Recipient/Detail";
        /// <summary>
        ///     Relative URL to retrieve the unsubscribed recipients of a list
        /// </summary>
        public const string GET_UNSUBSCRIBED_RECIPIENTS = "Console/List/{0}/Recipients/Unsubscribed";
        /// <summary>
        ///     Relative URL to retrieve the pending recipients of a list
        /// </summary>
        public const string GET_PENDING_RECIPIENTS = "Console/List/{0}/Recipients/Pending";
        /// <summary>
        ///     Relative URL to retrieve the subscribed recipients of a list
        /// </summary>
        public const string GET_SUBSCRIBED_RECIPIENTS = "Console/List/{0}/Recipients/Subscribed";
        /// <summary>Relative URL to add a recipient to a group</summary>
        public const string ADD_RECIPIENT_TO_GROUP = "Console/Group/{0}/Recipient";
        /// <summary>Relative URL to add several recipients to a group</summary>
        public const string ADD_RECIPIENTS_TO_GROUP = "Console/Group/{0}/Recipients";
        /// <summary>Relative URL to remove a recipient from a group</summary>
        public const string REMOVE_RECIPIENT_FROM_GROUP = "Console/Group/{0}/Unsubscribe/{1}";
        /// <summary>Relative URL to create an email message</summary>
        public const string CREATE_MESSAGE = "Console/List/{0}/Email";
        /// <summary>Relative URL to retrieve email messages</summary>
        public const string GET_MESSAGES = "Console/List/{0}/Emails";
        /// <summary>Relative URL to retrieve online email messages</summary>
        public const string GET_PUBLISHED_MESSAGES = "Console/List/{0}/Online/Emails";
        /// <summary>Relative URL to retrieve archived email messages</summary>
        public const string GET_ARCHIVED_MESSAGES = "Console/List/{0}/Archived/Emails";
        /// <summary>
        ///     Relative URL to retrieve details of an email message
        /// </summary>
        public const string GET_MESSAGE_DETAILS = "Console/List/{0}/Email/{1}";
        /// <summary>
        ///     Relative URL to retrieve attachments of an email message
        /// </summary>
        public const string GET_MESSAGE_ATTACH = "Console/List/{0}/Email/{1}/Attachment";
        /// <summary>
        ///     Relative URL to retrieve the queue status for immediate sendings
        /// </summary>
        public const string GET_SEND_STATUS_IMMEDIATE = "Console/Email/Sendings/Immediate";
        /// <summary>
        ///     Relative URL to retrieve the queue status for deferred sendings
        /// </summary>
        public const string GET_SEND_STATUS_DEFERRED = "Console/Email/Sendings/Deferred";
        /// <summary>
        ///     Relative URL to retrieve the queue status for dispatches to be scheduled
        /// </summary>
        public const string GET_SEND_STATUS_UNDEF = "Console/Email/Sendings/Undefined";
        /// <summary>
        ///     Relative URL to retrieve the first useful date to schedule the given sending task
        /// </summary>
        public const string GET_DATE_FOR_DEFERRED = "Console/Email/Sending/{0}/Deferred";
        /// <summary>
        ///     Relative URL to retrieve the sending ID of the confirmation email related to given import task
        /// </summary>
        public const string GET_SENDING_ID_FOR_IMPORT = "Console/Import/{0}/Sending";
        /// <summary>Relative URL to update an email message</summary>
        public const string UPDATE_MESSAGE = "Console/List/{0}/Email/{1}";
        /// <summary>
        ///     Relative URL to get the enabled tag list for the specified list
        /// </summary>
        public const string GET_TAGS = "Console/List/{0}/Tags";
        /// <summary>
        ///     Relative URL to create a tag in the specified list
        /// </summary>
        public const string CREATE_TAG = "Console/List/{0}/Tag";
        /// <summary>
        ///     Relative URL to update a tag in the specified list
        /// </summary>
        public const string UPDATE_TAG = "Console/List/{0}/Tag/{1}";
        /// <summary>
        ///     Relative URL to delete a tag in the specified list
        /// </summary>
        public const string DELETE_TAG = "Console/List/{0}/Tag/{1}";
        /// <summary>
        ///     Relative URL to add an attachment to the specified message
        /// </summary>
        public const string ADD_ATTACH = "Console/List/{0}/Email/{1}/Attachment/{2}";
        /// <summary>
        ///     Relative URL to remove an attachment from the specified message
        /// </summary>
        public const string DELETE_ATTACH = "Console/List/{0}/Email/{1}/Attachment/{2}";
        /// <summary>
        ///     Relative URL to send the given email message immediately
        /// </summary>
        public const string SEND_EMAIL_IMMEDIATE = "Console/Email/Sendings/{0}/Immediate";
        /// <summary>
        ///     Relative URL to send the given email message deferred by a given time
        /// </summary>
        public const string SEND_EMAIL_DEFERRED = "Console/Email/Sendings/{0}/Deferred";
        /// <summary>
        ///     Relative URL to send an email message to the recipients in the specified list
        /// </summary>
        public const string SEND_EMAIL_TO_LIST = "Console/List/{0}/Email/{1}/Send";
        /// <summary>
        ///     Relative URL to send an email message to the recipients in the specified group
        /// </summary>
        public const string SEND_EMAIL_TO_GROUP = "Console/Group/{0}/Email/{1}/Send";
        /// <summary>
        ///     Relative URL to send single email message to specified recipient
        /// </summary>
        public const string SEND_EMAIL_TO_REC = "Console/Email/Send";
        /// <summary>
        ///     Relative URL to retrieve email message send history
        /// </summary>
        public const string GET_SEND_HISTORY = "Console/List/{0}/Email/{1}/SendHistory";
        /// <summary>
        ///     Relative URL to retrieve the list of recipients who received the specified email
        /// </summary>
        public const string GET_MESSAGE_RECIPIENTS = "Message/{0}/List/Recipients";
        /// <summary>
        ///     Relative URL to retrieve the number of recipients who received the specified email
        /// </summary>
        public const string COUNT_MESSAGE_RECIPIENTS = "Message/{0}/Count/Recipients";
        /// <summary>
        ///     Relative URL to retrieve the list of views of the specified email
        /// </summary>
        public const string GET_MESSAGE_VIEWS = "Message/{0}/List/Views";
        /// <summary>
        ///     Relative URL to retrieve the number of views of the specified email
        /// </summary>
        public const string COUNT_MESSAGE_VIEWS = "Message/{0}/Count/Views";
        /// <summary>
        ///     Relative URL to retrieve the list of clicks on a link in the specified email.
        /// </summary>
        public const string GET_MESSAGE_CLICKS = "Message/{0}/List/Clicks";
        /// <summary>
        ///     Relative URL to retrieve the number of clicks on a link in the specified email.
        /// </summary>
        public const string COUNT_MESSAGE_CLICKS = "Message/{0}/Count/Clicks";
        /// <summary>
        ///     Relative URL to retrieve the list of clicks on a link in the specified email
        /// </summary>
        public const string GET_MESSAGE_URL_CLICKS = "Message/{0}/List/UrlClicks";
        /// <summary>
        ///     Relative URL to retrieve the list of clicks on a link in the specified email with details
        /// </summary>
        public const string GET_MESSAGE_URL_CLICKS_DETAILS = "Message/{0}/List/UrlClickDetails";
        /// <summary>
        ///     Relative URL to retrieve the list of bounces from the specified email
        /// </summary>
        public const string GET_MESSAGE_BOUNCES = "Message/{0}/List/Bounces";
        /// <summary>
        ///     Relative URL to retrieve the number of bounces from the specified email
        /// </summary>
        public const string COUNT_MESSAGE_BOUNCES = "Message/{0}/Count/Bounces";
        /// <summary>
        ///     Relative URL to retrieve the list of unsubscriptions from the specified email
        /// </summary>
        public const string GET_MESSAGE_UNSUBSCRIPTIONS = "Message/{0}/List/Unsubscriptions";
        /// <summary>
        ///     Relative URL to retrieve the number of unsubscriptions from the specified email
        /// </summary>
        public const string COUNT_MESSAGE_UNSUBSCRIPTIONS = "Message/{0}/Count/Unsubscriptions";
        /// <summary>
        ///     Relative URL to retrieve the list of messages received by the specified recipient
        /// </summary>
        public const string GET_MESSAGE_DELIVERIES = "Recipient/{0}/List/Deliveries";
        /// <summary>
        ///     Relative URL to retrieve the number of messages received by the specified recipient
        /// </summary>
        public const string COUNT_MESSAGE_DELIVERIES = "Recipient/{0}/Count/Deliveries";
        /// <summary>
        ///     Relative URL to retrieve the list of messages viewed by the specified recipient
        /// </summary>
        public const string GET_MESSAGE_VIEWS_BY_REC = "Recipient/{0}/List/Views";
        /// <summary>
        ///     Relative URL to retrieve the number of messages viewed by the specified recipient
        /// </summary>
        public const string COUNT_MESSAGE_VIEWS_BY_REC = "Recipient/{0}/Count/Views";
        /// <summary>
        ///     Relative URL to retrieve the list of clicks on a link done by the specified recipient
        /// </summary>
        public const string GET_MESSAGE_CLICKS_BY_REC = "Recipient/{0}/List/Clicks";
        /// <summary>
        ///     Relative URL to retrieve the number of clicks on a link done by the specified recipient
        /// </summary>
        public const string COUNT_MESSAGE_CLICKS_BY_REC = "Recipient/{0}/Count/Clicks";
        /// <summary>
        ///     Relative URL to retrieve the list of clicks on a link done by the specified recipient with details
        /// </summary>
        public const string GET_MESSAGE_CLICKS_DETAILS_BY_REC = "Recipient/{0}/List/ClicksDetails";
        /// <summary>
        ///     Relative URL to retrieve the list of bounces returned by the specified recipient
        /// </summary>
        public const string GET_MESSAGE_BOUNCES_BY_REC = "Recipient/{0}/List/Bounces";
        /// <summary>
        ///     Relative URL to retrieve the number of bounces returned by the specified recipient
        /// </summary>
        public const string COUNT_MESSAGE_BOUNCES_BY_REC = "Recipient/{0}/Count/Bounces";
        /// <summary>
        ///     Relative URL to retrieve the list of bounces returned by the specified recipient with details
        /// </summary>
        public const string GET_MESSAGE_BOUNCES_DETAILS = "Recipient/{0}/List/BouncesDetails";
        /// <summary>
        ///     Relative URL to retrieve the list of unsubscriptions done by the specified recipient
        /// </summary>
        public const string GET_MESSAGE_UNSUBSCRIPTIONS_BY_REC = "Recipient/{0}/List/Unsubscriptions";
        /// <summary>
        ///     Relative URL to retrieve the number of unsubscriptions done by the specified recipient
        /// </summary>
        public const string COUNT_MESSAGE_UNSUBSCRIPTIONS_BY_REC = "Recipient/{0}/Count/Unsubscriptions";
        /// <summary>Relative URL to create an SMS message</summary>
        public const string CREATE_SMS = "Console/Sms/List/{0}/Message";
        /// <summary>Relative URL to retrieve SMS messages</summary>
        public const string GET_SMS = "Console/Sms/List/{0}/Messages";
        /// <summary>
        ///     Relative URL to retrieve details of an SMS message
        /// </summary>
        public const string GET_SMS_DETAILS = "Console/Sms/List/{0}/Message/{1}";
        /// <summary>
        ///     Relative URL to retrieve the queue status for immediate SMS sendings
        /// </summary>
        public const string GET_SMS_SEND_STATUS_IMMEDIATE = "Console/Sms/Sendings/Immediate";
        /// <summary>
        ///     Relative URL to retrieve the queue status for deferred SMS sendings
        /// </summary>
        public const string GET_SMS_SEND_STATUS_DEFERRED = "Console/Sms/Sendings/Deferred";
        /// <summary>
        ///     Relative URL to retrieve the queue status for SMS dispatches to be scheduled
        /// </summary>
        public const string GET_SMS_SEND_STATUS_UNDEF = "Console/Sms/Sendings/Undefined";
        /// <summary>
        ///     Relative URL to retrieve the first useful date to schedule the given SMS sending task
        /// </summary>
        public const string GET_SMS_DATE_FOR_DEFERRED = "Console/Sms/Sending/{0}/Deferred";
        /// <summary>Relative URL to update an SMS message</summary>
        public const string UPDATE_SMS = "Console/Sms/List/{0}/Message/{1}";
        /// <summary>
        ///     Relative URL to send an SMS message to the recipients in the specified list
        /// </summary>
        public const string SEND_SMS_TO_LIST = "Console/Sms/List/{0}/Message/{1}/Send";
        /// <summary>
        ///     Relative URL to send an SMS message to the recipients in the specified group
        /// </summary>
        public const string SEND_SMS_TO_GROUP = "Console/Sms/Group/{0}/Message/{1}/Send";
        /// <summary>
        ///     Relative URL to send single SMS message to specified recipient
        /// </summary>
        public const string SEND_SMS_TO_REC = "Console/Sms/Send";
        /// <summary>
        ///     Relative URL to retrieve the SMS message sending report by specifying SMS ID
        /// </summary>
        public const string GET_SMS_REPORT = "Console/Sms/{0}/Sendings/Report";
        /// <summary>
        ///     Relative URL to retrieve the SMS message sending report by specifying SMS ID with details
        /// </summary>
        public const string GET_SMS_REPORT_DETAILS = "Console/Sms/{0}/Sendings/ReportDetails";
        /// <summary>Relative URL to send transactional email</summary>
        public const string SEND_TRANS_MAIL = "messages/sendmessage";
        /// <summary>
        ///     Relative URL to send transactional email from a template
        /// </summary>
        public const string SEND_TRANS_TEMPLATE = "messages/sendtemplate";
        /// <summary>Relative URL to retreive SMTP+ user info</summary>
        public const string SMTP_USER_INFO = "users/listuserinfo ";
        /// <summary>
        /// The default format of the date/time used to provide input/output for REST Web Services
        /// </summary>
        public const string DEFAULT_DATETIME_FRMT = "yyyy-MM-dd HH:mm:ssZ";
        /// <summary>
        ///     The base endpoint for resources.
        ///     It can be overridden in configuration by adding the key 'MailUpBaseEndpoint'
        /// </summary>
        public static readonly string BaseEndpoint = ConfigurationManager.AppSettings["MailUpBaseEndpoint"] != null ? ConfigurationManager.AppSettings["MailUpBaseEndpoint"].ToString() : "https://services.mailup.com/API/v1.1/";
        /// <summary>
        ///     The base endpoint for authorization process.
        ///     It can be overridden in configuration by adding the key 'MailUpBaseAuthEndpoint'
        /// </summary>
        public static readonly string BaseAuthEndpoint = ConfigurationManager.AppSettings["MailUpBaseAuthEndpoint"] != null ? ConfigurationManager.AppSettings["MailUpBaseAuthEndpoint"].ToString() : "https://services.mailup.com/Authorization/";
        /// <summary>
        ///     The base endpoint for resources.
        ///     It can be overridden in configuration by adding the key 'MailUpSendEndpoint'
        /// </summary>
        public static readonly string SendEndpoint = ConfigurationManager.AppSettings["MailUpSendEndpoint"] != null ? ConfigurationManager.AppSettings["MailUpSendEndpoint"].ToString() : "https://send.mailup.com/api/v2.0/";
        /// <summary>The log-on endpoint</summary>
        public static readonly string LogonEndpoint = MailUpUriConstants.BaseAuthEndpoint + "OAuth/LogOn";
        /// <summary>The authorization endpoint</summary>
        public static readonly string AuthorizationEndpoint = MailUpUriConstants.BaseAuthEndpoint + "OAuth/Authorization";
        /// <summary>The token endpoint</summary>
        public static readonly string TokenEndpoint = MailUpUriConstants.BaseAuthEndpoint + "OAuth/Token";
        /// <summary>The console endpoint</summary>
        public static readonly string ConsoleEndpoint = MailUpUriConstants.BaseEndpoint + "Rest/ConsoleService.svc";
        /// <summary>The mail statistics endpoint</summary>
        public static readonly string MailStatisticsEndpoint = MailUpUriConstants.BaseEndpoint + "Rest/MailStatisticsService.svc";
    }
}

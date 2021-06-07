using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Abstractions
{
    public interface IManagerSMSMessage
    {
        #region SMS message
        ////public EntityMessage CreateSmsMessage(
        ////  int idList,
        ////  ConsoleSmsMessageDetailItem message)
        ////{
        ////    return this.CallMethod<EntityMessage>(Method.POST, string.Format("Console/Sms/List/{0}/Message", (object)idList), (object)message);
        ////}
        ////public IEnumerable<EntityMessage> GetSmsMessages(
        ////  int idList,
        ////  string filterExpression = "",
        ////  string sortExpression = "",
        ////  int? pageSize = null,
        ////  int? pageNumber = null)
        ////{
        ////    return this.CallMethod<IEnumerable<EntityMessage>>(Method.GET, string.Format("Console/Sms/List/{0}/Messages", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        ////}
        ////public ConsoleSmsMessageDetailItem GetSmsMessageDetails(
        ////  int idList,
        ////  int idMessage)
        ////{
        ////    return this.CallMethod<ConsoleSmsMessageDetailItem>(Method.GET, string.Format("Console/Sms/List/{0}/Message/{1}", (object)idList, (object)idMessage), (object)null);
        ////}
        ////public IEnumerable<SmsSendingItem> GetSmsSendingStatus(
        ////  SendingStatus status)
        ////{
        ////    string url;
        ////    switch (status)
        ////    {
        ////        case SendingStatus.Immediate:
        ////            url = "Console/Sms/Sendings/Immediate";
        ////            break;
        ////        case SendingStatus.Deferred:
        ////            url = "Console/Sms/Sendings/Deferred";
        ////            break;
        ////        case SendingStatus.Undefined:
        ////            url = "Console/Sms/Sendings/Undefined";
        ////            break;
        ////        default:
        ////            throw new MailUpException(-1, "Unknown Sending Status");
        ////    }
        ////    return this.CallMethod<IEnumerable<SmsSendingItem>>(Method.GET, url, (object)null);
        ////}
        ////public DateTime GetSmsDateForDeferredDispatch(int idSending)
        ////{
        ////    CultureInfo invariantCulture = CultureInfo.InvariantCulture;
        ////    string s = this.CallMethod<string>(Method.GET, string.Format("Console/Sms/Sending/{0}/Deferred", (object)idSending), (object)null);
        ////    try
        ////    {
        ////        return DateTime.ParseExact(s, "u", (IFormatProvider)invariantCulture);
        ////    }
        ////    catch
        ////    {
        ////        return DateTime.MaxValue;
        ////    }
        ////}
        ////public EntityMessage UpdateSmsMessage(
        ////  int idList,
        ////  ConsoleSmsMessageDetailItem message)
        ////{
        ////    if (message != null)
        ////    {
        ////        int? idMessage = message.idMessage;
        ////        if ((idMessage.GetValueOrDefault() > 0 ? 0 : (idMessage.HasValue ? 1 : 0)) == 0)
        ////            return this.CallMethod<EntityMessage>(Method.PUT, string.Format("Console/Sms/List/{0}/Message/{1}", (object)idList, (object)message.idMessage), (object)message);
        ////    }
        ////    return (EntityMessage)null;
        ////}
        ////public SmsMessageSendResponse SendSmsMessageToList(
        ////  int idList,
        ////  int idMessage,
        ////  string senderPrefix = "",
        ////  string senderNumber = "",
        ////  DateTime? scheduledTime = null)
        ////{
        ////    return this.CallMethod<SmsMessageSendResponse>(Method.POST, this.PrepareURLToSendSms(string.Format("Console/Sms/List/{0}/Message/{1}/Send", (object)idList, (object)idMessage), senderPrefix, senderNumber, scheduledTime), (object)null);
        ////}
        ////public SmsMessageSendResponse SendSmsMessageToGroup(
        ////  int idGroup,
        ////  int idMessage,
        ////  string senderPrefix = "",
        ////  string senderNumber = "",
        ////  DateTime? scheduledTime = null)
        ////{
        ////    return this.CallMethod<SmsMessageSendResponse>(Method.POST, this.PrepareURLToSendSms(string.Format("Console/Sms/Group/{0}/Message/{1}/Send", (object)idGroup, (object)idMessage), senderPrefix, senderNumber, scheduledTime), (object)null);
        ////}
        ////public SmsMessageSendResponse SendSmsMessage(
        ////  SmsSendToRecipientRequest request,
        ////  string senderPrefix = "",
        ////  string senderNumber = "",
        ////  DateTime? scheduledTime = null)
        ////{
        ////    return this.CallMethod<SmsMessageSendResponse>(Method.POST, this.PrepareURLToSendSms("Console/Sms/Send", senderPrefix, senderNumber, scheduledTime), (object)request);
        ////}
        ////private string PrepareURLToSendSms(
        ////  string action,
        ////  string senderPrefix,
        ////  string senderNumber,
        ////  DateTime? scheduledTime)
        ////{
        ////    action = ClientUtils.AddQuerystringParam(action, nameof(senderPrefix), senderPrefix);
        ////    action = ClientUtils.AddQuerystringParam(action, nameof(senderNumber), senderNumber);
        ////    if (scheduledTime.HasValue)
        ////        action = ClientUtils.AddQuerystringParam(action, "datetime", scheduledTime.Value.ToString("yyyy-MM-dd HH:mm:ssZ"));
        ////    return action;
        ////}
        ////public SmsMessageSendingReport GetSmsSendingReport(
        ////  int idMessage,
        ////  string filterExpression = "",
        ////  string sortExpression = "",
        ////  int? pageSize = null,
        ////  int? pageNumber = null)
        ////{
        ////    return this.CallMethod<SmsMessageSendingReport>(Method.GET, string.Format("Console/Sms/{0}/Sendings/Report", (object)idMessage), (object)null);
        ////}
        ////public IEnumerable<SmsMessageSendingReportDetail> GetSmsSendingReportDetails(
        ////  int idMessage,
        ////  string filterExpression = "",
        ////  string sortExpression = "",
        ////  int? pageSize = null,
        ////  int? pageNumber = null)
        ////{
        ////    return this.CallMethod<IEnumerable<SmsMessageSendingReportDetail>>(Method.GET, string.Format("Console/Sms/{0}/Sendings/ReportDetails", (object)idMessage), (object)null);
        ////}
        #endregion
    }
}

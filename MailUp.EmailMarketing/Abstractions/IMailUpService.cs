using MailUp.EmailMarketing.Entities;
using MailUp.EmailMarketing.Model;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Abstractions
{
    public interface IMailUpService
    {
        /////// <summary>
        /////// LogIOnUri: Compose path Uri with connect callback address http
        /////// </summary>
        /////// <param name="callbackUri"></param>
        /////// <returns></returns>
        ////string LogOnUri(string callbackUri);
        /////// <summary>
        /////// LoginToken: Connect User with access_token to login and refresh token
        /////// </summary>
        /////// <param name="code"></param>
        /////// <returns></returns>
        ////Task<AuthorizationModel> LoginWithCode();
        /////// <summary>
        /////// LoginWithPassword: Connect User with username and password in first time, return Access Token and Refresh Token
        /////// </summary>
        /////// <param name="username"></param>
        /////// <param name="password"></param>
        /////// <returns></returns>
        ////Task<AuthorizationModel> LoginWithPassword(string username, string password);
        /////// <summary>
        /////// RenewToken: Resfresh the Access Token with presentation to Refresh Token pass in last time.
        /////// </summary>
        /////// <returns></returns>
        ////Task<AuthorizationModel> LoginRenewToken();

        ////Task<ResponseGroup> ConsoleCreateGroup(int listid, RequestGroup group);
        ////Task<ResponseCreateList> ConsoleCreateList(RequestList list);
        ////Task<bool> ConsoleDeleteGroup(int listid, int groupid);
        ////Task<ResponseList> ConsoleDeleteList(int listid);
        ////Task<bool> ConsoleDeleteListIfExists(int listid, string listGuid);
        ////Task<ResponseRecipient> ConsoleDeleteRecipientsOnGroup(int listid, int groupid);
        ////Task<ResponseItemEmail> ConsoleDeleteTrustedSendersEmail(int idTrustedSender, string email);
        ////Task<ResponseList> ConsoleReadDetailList(int listid);
        /////// <summary>
        /////// ConsoleReadGroupsPagined
        /////// </summary>
        /////// <param name="listid"></param>
        /////// <param name="pageNumer"></param>
        /////// <param name="pageSize"></param>
        /////// <returns></returns>
        ////Task<ResponseGroups> ConsoleReadGroupsInListPagined(int listid, int pageNumer = 0, int pageSize = 20);
        /////// <summary>
        /////// ConsoleListInGroup: path http to console for List (number) in Group on MailUp {"/Console/List/{listid}/Groups"}
        /////// </summary>
        /////// <param name="listid"></param>
        /////// <returns></returns>
        ////Task<ResponseLists> ConsoleReadGroupsInList(int listid);
        ////Task<ResponseLists> ConsoleReadListAll();
        ////Task<ResponseEmail> ConsoleTrustedSendersEmail();
        ////Task<ResponseItemEmail> ConsoleTrustedSendersEmail(int idTrustedSender);
        ////Task<ResponseItemEmail> ConsoleTrustedSendersEmail(string email);
        ////Task<ResponseGroup> ConsoleUpdateGroup(int listid, int groupid, RequestGroup group);
        ////Task<ResponseList> ConsoleUpdateList(RequestList list);


        /////*  Console  */
        /////// apiUrl = "Console/Authentication/Info";
        //////  apiUrl = "/Console/List/" + ListId + "/Groups";
        //////  apiUrl = "/Console/Recipient/DynamicFields";
        //////  apiUrl = "/Console/Group/" + groupId + "/Recipients";
        //////  apiUrl = "/Console/Import/" + importId;
        //////  apiUrl = "/Console/Group/" + groupId + "/Recipients";
        //////  apiUrl = "/Console/Group/" + groupId + "/Unsubscribe/" + recipientId;
        //////  apiUrl = "/Console/List/" + ListId + "/Recipients/Subscribed";
        //////  apiUrl = "/Console/Recipient/Detail";
        //////  apiUrl = "/Console/List/" + ListId + "/Templates";
        //////  // Create the new message
        //////  apiUrl = "/Console/List/" + ListId + "/Email/Template/" + templateId;
        //////  // Request for messages list
        //////  apiUrl = "/Console/List/" + ListId + "/Emails";
        //////  apiUrl = "/Console/List/" + ListId + "/Images";
        //////  apiUrl = "/Console/Images";
        //////  apiUrl = "/Console/List/" + ListId + "/Email";
        //////  apiUrl = "/Console/List/" + ListId + "/Email/" + emailId + "/Attachment/1";
        //////  apiUrl = "/Console/List/" + ListId + "/Email/" + emailId;
        //////  Create a new tag
        //////  apiUrl = "/Console/List/" + ListId + "/Tag";
        //////  Pick up a message and retrieve detailed informations
        //////  apiUrl = "/Console/List/" + ListId + "/Email/" + emailId;
        //////  Add the tag to the message details and save
        //////  apiUrl = "/Console/List/" + ListId + "/Email/" + emailId;
        //////  Send email to all recipients in the list
        //////  apiUrl = "/Console/List/" + ListId + "/Email/" + emailId + "/Send";

        /////*  Mailstatistics  */
        //////  apiUrl = "/Message/" + emailId + "/List/Views?pageSize=5&pageNum=0";
    }
}
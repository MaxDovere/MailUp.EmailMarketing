// Decompiled with JetBrains decompiler
// Type: MailUp.Sdk.Base.MailUpClient
// Assembly: MailUp.Sdk, Version=1.1.7.0, Culture=neutral, PublicKeyToken=null
// MVID: 3ACC2C0B-6E9D-4FD1-B161-D2D4EFEBB08F
// Assembly location: C:\Users\maxdo\source\repos\WebApplication1\packages\MailUp.Sdk.1.1.7.0\lib\net40\MailUp.Sdk.dll

using fastJSON;
using MailUp.Framework.Contracts.API.Base;
using MailUp.Framework.Contracts.API.DataContracts.Console;
using MailUp.Framework.Contracts.API.DataContracts.Console.MailSend;
using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Bounces;
using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Clicks;
using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Deliveries;
using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Recipients;
using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Unsubscriptions;
using MailUp.Framework.Contracts.API.DataContracts.Statistics.Mail.Views;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace DeliveryMailUp.Core
//namespace MailUp.Sdk.Base
{
    /// <summary>
    /// Wrapper for REST APIs provided by MailUp service.
    /// It manages the OAuth2 authentication process to gain access to the resources and
    /// wraps the different resources as usual C# methods.
    /// </summary>
    public class MailUpClient
    {
        private Connector _oauth2Client;
        private RestClient client;

        /// <summary>
        /// Public identifier, part of the credentials used to authenticate this client
        /// </summary>
        public string ClientId
        {
            get => this._oauth2Client.ClientId;
            set => this._oauth2Client.ClientId = value;
        }

        /// <summary>
        /// Private identifier, part of the credentials used to authenticate this client
        /// </summary>
        public string ClientSecret
        {
            get => this._oauth2Client.ClientSecret;
            set => this._oauth2Client.ClientSecret = value;
        }

        /// <summary>
        /// The token provided from the web server after a successful authentication of the client.
        /// It is used to gain access to the REST resources.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The token provided from the web server after a successful authentication of the client.
        /// It is used to refresh the <see cref="P:MailUp.Sdk.Base.MailUpClient.AccessToken" />.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// The delegate used to manage the entire token eschange process if username/password are not provided.
        /// </summary>
        public ProcessAuthorizationFlow AuthorizationFlow { get; set; }

        /// <summary>
        /// Initialize the client with the credentials needed to setup the communication
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public MailUpClient(string clientId, string clientSecret)
        {
            this._oauth2Client = new Connector(Constants.TokenEndpoint, Constants.AuthorizationEndpoint, clientId, clientSecret);
            this.client = new RestClient(Constants.SendEndpoint);
        }

        /// <summary>
        /// Initialize the client with the credentials needed to setup the communication,
        /// taken from the configuration file.
        /// ClientID is taken from the configuration setting with key 'MailUpClientID'.
        /// ClientSecret is taken from the configuration setting with key 'MailUpClientSecret'.
        /// </summary>
        public MailUpClient()
          : this(ConfigurationManager.AppSettings["MailUpClientID"].ToString(), ConfigurationManager.AppSettings["MailUpClientSecret"].ToString())
        {
        }

        /// <summary>
        /// Setup the secure communication with the server by following the specified authorization flow.
        /// This procedure is performed from web apps, when a callbakUri has been specified in the constructor.
        /// </summary>
        /// <param name="authorizationFlow">procedure to follow in order to establish the secure communication</param>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">If an error occurs while retrieving the access token</exception>
        public void RetrieveAccessToken(ProcessAuthorizationFlow authorizationFlow)
        {
            this.AuthorizationFlow = authorizationFlow;
            this._oauth2Client.GetToken(authorizationFlow(Constants.AuthorizationEndpoint));
        }

        /// <summary>
        /// Setup the secure communication with the server by exchanging a code for the access token.
        /// This procedure is performed from web apps, when a callbakUri has been specified in the constructor.
        /// </summary>
        /// <param name="code">code to exchange with the AccessToken that enables the secure communication</param>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">If an error occurs while retrieving the access token</exception>
        public void RetrieveAccessToken(string code) => this._oauth2Client.GetToken(code);

        /// <summary>
        /// Retrieves the URL where the user should be redirected in 3-legged Authorization flow,
        /// when the RetrieveAccessToken(ProcessAuthorizationFlow) method is used.
        /// </summary>
        /// <param name="callbackUri">URL where the application will receive the code to be exchanged for a valid access token</param>
        /// <returns>URL where the user should be redirected in 3-legged Authorization flow</returns>
        public string GetLogOnUri(string callbackUri) => Constants.LogonEndpoint + "?client_id=" + this.ClientId + "&client_secret=" + this.ClientSecret + "&response_type=code&redirect_uri=" + callbackUri;

        /// <summary>
        /// Setup the secure communication with the server by asking an access token for the user identified by
        /// the pair username-password provided in configuration.
        /// The username is taken from configuration key 'MailUpUsername'.
        /// The password is taken from configuration key 'MailUpPassword'.
        /// This procedure can be applied always; no authorization flow is required.
        /// If username and/or password are empty, the authorization flow delegate is used (if available,
        /// otherwise an exception is thrown).
        /// </summary>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">If an error occurs while retrieving the access token,
        /// or username, password and authorization flow are not specified</exception>
        public void RetrieveAccessToken() => this.RetrieveAccessToken(ConfigurationManager.AppSettings["MailUpUsername"].ToString(), ConfigurationManager.AppSettings["MailUpPassword"].ToString());

        /// <summary>
        /// Setup the secure communication with the server by asking an access token for the user identified by
        /// the given pair login-password.
        /// This procedure can be applied always; no authorization flow is required.
        /// If username and/or password are empty, the authorization flow delegate is used (if available,
        /// otherwise an exception is thrown).
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">If an error occurs while retrieving the access token,
        /// or username, password and authorization flow are not specified</exception>
        public void RetrieveAccessToken(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                if (this.AuthorizationFlow == null)
                    throw new MailUpException(-1, "You haven't specified username and/or password, and no authorization flow has been set");
                this.RetrieveAccessToken(this.AuthorizationFlow);
            }
            else
            {
                this._oauth2Client.SetupUserCredentials(username, password);
                this._oauth2Client.GetToken();
            }
        }

        /// <summary>Call a web method with the given arguments</summary>
        /// <typeparam name="T">expected returning type</typeparam>
        /// <param name="verb">HTTP verb to use</param>
        /// <param name="url">relative endpoint of the REST resource, complete with eventual querystring parameters</param>
        /// <param name="requestData">body of the request</param>
        /// <param name="filterExpression">(optional) filtering expression</param>
        /// <param name="sortExpression">(optional) sort expression</param>
        /// <param name="pageSize">(optional) max number of results</param>
        /// <param name="pageNumber">(optional) pagination index</param>
        /// <param name="consoleEndpoint">true to call a method belonging to Console Endpoint,
        /// false to call a method belonging to MailStatistics Endpoint</param>
        /// <returns></returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        private T CallMethod<T>(
          HttpVerb verb,
          string url,
          object requestData,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null,
          bool consoleEndpoint = true)
          where T : class
        {
            string uri = url;
            string str1 = consoleEndpoint ? Constants.ConsoleEndpoint : Constants.MailStatisticsEndpoint;
            if (!url.StartsWith(str1))
            {
                string str2 = str1;
                if (!url.StartsWith("/"))
                    str2 += "/";
                uri = str2 + url;
            }
            object obj = this._oauth2Client.RequestResource<T>(uri, verb.ToString(), requestData, filterExpression, sortExpression, pageSize, pageNumber);
            switch (obj)
            {
                case null:
                    return default(T);
                case T _:
                    return obj as T;
                case Exception _:
                    throw new MailUpException(0, (obj as Exception).Message);
                case Dictionary<string, object> _:
                    throw new MailUpException(0, string.Join(",", (obj as Dictionary<string, object>).Select<KeyValuePair<string, object>, string>((Func<KeyValuePair<string, object>, string>)(kvp => string.Format("{0}:{1}", (object)kvp.Key, kvp.Value)))));
                default:
                    throw new MailUpException(0, obj.ToString());
            }
        }

        /// <summary>Call a web method with the given arguments</summary>
        /// <param name="verb">HTTP verb to use</param>
        /// <param name="url">relative endpoint of the REST resource, complete with eventual querystring parameters</param>
        /// <param name="requestData">body of the request</param>
        /// <param name="filterExpression">(optional) filtering expression</param>
        /// <param name="sortExpression">(optional) sort expression</param>
        /// <param name="pageSize">(optional) max number of results</param>
        /// <param name="pageNumber">(optional) pagination index</param>
        /// <param name="consoleEndpoint">true to call a method belonging to Console Endpoint,
        /// false to call a method belonging to MailStatistics Endpoint</param>
        /// <returns></returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        public long CallMethodWithNumericResponse(
          HttpVerb verb,
          string url,
          object requestData,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null,
          bool consoleEndpoint = true)
        {
            string uri = url;
            string str1 = consoleEndpoint ? Constants.ConsoleEndpoint : Constants.MailStatisticsEndpoint;
            if (!url.StartsWith(str1))
            {
                string str2 = str1;
                if (!url.StartsWith("/"))
                    str2 += "/";
                uri = str2 + url;
            }
            object obj = this._oauth2Client.RequestResource<long>(uri, verb.ToString(), requestData, filterExpression, sortExpression, pageSize, pageNumber);
            switch (obj)
            {
                case null:
                    return 0;
                case long num:
                    return num;
                case Exception _:
                    throw new MailUpException(0, (obj as Exception).Message);
                case Dictionary<string, object> _:
                    throw new MailUpException(0, string.Join(",", (obj as Dictionary<string, object>).Select<KeyValuePair<string, object>, string>((Func<KeyValuePair<string, object>, string>)(kvp => string.Format("{0}:{1}", (object)kvp.Key, kvp.Value)))));
                default:
                    throw new MailUpException(0, obj.ToString());
            }
        }

        /// <summary>Call a web method with the given arguments</summary>
        /// <typeparam name="T">expected returning type</typeparam>
        /// <param name="verb">HTTP verb to use</param>
        /// <param name="url">relative endpoint of the REST resource, complete with eventual querystring parameters</param>
        /// <param name="requestData">body of the request</param>
        /// <returns></returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        private T CallDirectMethod<T>(HttpVerb verb, string url, object requestData) where T : class
        {
            RestRequest restRequest = new RestRequest(url, verb.GetRestMethod());
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddBody(requestData);
            IRestResponse restResponse;
            try
            {
                restResponse = this.client.Execute((IRestRequest)restRequest);
            }
            catch (Exception ex)
            {
                throw new MailUpException(0, ex.Message);
            }
            string json = restResponse.StatusCode == HttpStatusCode.OK ? restResponse.Content : throw new MailUpException((int)restResponse.StatusCode, restResponse.ErrorMessage);
            if (string.IsNullOrEmpty(json))
                return default(T);
            try
            {
                return JSON.ToObject<T>(json);
            }
            catch (Exception ex)
            {
                throw new MailUpException(0, ex.Message);
            }
        }

        /// <summary>Create a new list with the given information</summary>
        /// <param name="list">set of details of the list to be created</param>
        /// <returns>the ID of the new list; 0 or negative if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Create List" title="How to create a list (from scratch or copying the settings from an existing list)" numberLines="true" outlining="true"></code>
        /// </example>
        public int CreateList(ConsoleListCreateDetails list) => (int)this.CallMethodWithNumericResponse(HttpVerb.POST, "Console/User/Lists", (object)list);

        /// <summary>
        /// Updates the given list with the given information.
        /// Make sure to fill the idList parameter to ensure to update the correct list.
        /// </summary>
        /// <param name="list">set of details to update on the list.
        /// The only mandatory parameter is the idList (should be positive).
        /// Leave a parameter with null value in order not to update that field.</param>
        /// <returns>The updated list; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Update List" title="How to update the fields of a list" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleListUpdateDetails UpdateList(ConsoleListUpdateDetails list) => list == null || list.IdList <= 0 ? (ConsoleListUpdateDetails)null : this.CallMethod<ConsoleListUpdateDetails>(HttpVerb.PUT, string.Format("Console/User/List/{0}", (object)list.IdList), (object)list);

        /// <summary>Retrieves the list with the given ID.</summary>
        /// <param name="idList">ID of the list to retrieve</param>
        /// <returns>The wanted list details; null if not found or any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get List By ID" title="How to retrieve a list with the given ID" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleListItem GetList(int idList) => this.CallMethod<CollectionOf<ConsoleListItem>>(HttpVerb.GET, "Console/User/Lists", (object)null, "idList == " + (object)idList).SafeFirstOrDefault<ConsoleListItem>();

        /// <summary>Retrieves the list with the given name.</summary>
        /// <param name="listName">name of the list to retrieve</param>
        /// <returns>The wanted list details; null if not found or any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get List By Name" title="How to retrieve a list with the given name" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleListItem GetList(string listName) => string.IsNullOrEmpty(listName) ? (ConsoleListItem)null : this.CallMethod<CollectionOf<ConsoleListItem>>(HttpVerb.GET, "Console/User/Lists", (object)null, "Name.Trim().Equals('" + listName.Trim() + "')").SafeFirstOrDefault<ConsoleListItem>();

        /// <summary>
        /// Retrieves the lists matching the specified filters, if any.
        /// </summary>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The wanted lists; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Lists" title="How to retrieve a set of lists matching the given filters, if any" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<ConsoleListItem> GetLists(
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<ConsoleListItem>>(HttpVerb.GET, "Console/User/Lists", (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }

        /// <summary>Create a new group with the given information</summary>
        /// <param name="group">set of details of the group to be created</param>
        /// <returns>the created group; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Create Group" title="How to create a group" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleGroupItem CreateGroup(ConsoleGroupItem group)
        {
            if (group != null)
            {
                int? idList = group.idList;
                if ((idList.GetValueOrDefault() > 0 ? 0 : (idList.HasValue ? 1 : 0)) == 0)
                    return this.CallMethod<ConsoleGroupItem>(HttpVerb.POST, string.Format("Console/List/{0}/Group", (object)group.idList), (object)group);
            }
            return (ConsoleGroupItem)null;
        }

        /// <summary>
        /// Updates the given group with the given information.
        /// Make sure to fill the idList and idGroup parameters to ensure to update the correct group.
        /// </summary>
        /// <param name="group">set of details to update on the group.
        /// The only mandatory parameters are the idList and idGroup (should be positive).
        /// Leave a parameter with null value in order not to update that field.</param>
        /// <returns>The updated group; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Update Group" title="How to update a group" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleGroupItem UpdateGroup(ConsoleGroupItem group)
        {
            if (group != null)
            {
                int? idList = group.idList;
                if ((idList.GetValueOrDefault() > 0 ? 0 : (idList.HasValue ? 1 : 0)) == 0)
                {
                    int? idGroup = group.idGroup;
                    if ((idGroup.GetValueOrDefault() > 0 ? 0 : (idGroup.HasValue ? 1 : 0)) == 0)
                        return this.CallMethod<ConsoleGroupItem>(HttpVerb.PUT, string.Format("Console/List/{0}/Group/{1}", (object)group.idList, (object)group.idGroup), (object)group);
                }
            }
            return (ConsoleGroupItem)null;
        }

        /// <summary>Deletes the given group.</summary>
        /// <param name="idList">ID of the list the group belongs to</param>
        /// <param name="idGroup">ID of the group to delete</param>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Delete Group" title="How to delete a group" numberLines="true" outlining="true"></code>
        /// </example>
        public void DeleteGroup(int idList, int idGroup) => this.CallMethodWithNumericResponse(HttpVerb.DELETE, string.Format("Console/List/{0}/Group/{1}", (object)idList, (object)idGroup), (object)null);

        /// <summary>Retrieves the group with the given ID.</summary>
        /// <param name="idList">ID of the list the group should belong to</param>
        /// <param name="idGroup">ID of the group to retrieve</param>
        /// <returns>The wanted group details; null if not found or any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Group By ID" title="How to retrieve a group from its ID" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleGroupItem GetGroup(int idList, int idGroup) => this.CallMethod<CollectionOf<ConsoleGroupItem>>(HttpVerb.GET, string.Format("Console/List/{0}/Groups", (object)idList), (object)null, "idGroup == " + (object)idGroup).SafeFirstOrDefault<ConsoleGroupItem>();

        /// <summary>Retrieves the group with the given name.</summary>
        /// <param name="idList">ID of the list the group should belong to</param>
        /// <param name="groupName">name of the group to retrieve</param>
        /// <returns>The wanted group details; null if not found or any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Group By Name" title="How to retrieve a group from its name" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleGroupItem GetGroup(int idList, string groupName) => string.IsNullOrEmpty(groupName) ? (ConsoleGroupItem)null : this.CallMethod<CollectionOf<ConsoleGroupItem>>(HttpVerb.GET, string.Format("Console/List/{0}/Groups", (object)idList), (object)null, "Name.Trim().Equals('" + groupName.Trim() + "')").SafeFirstOrDefault<ConsoleGroupItem>();

        /// <summary>
        /// Retrieves the groups matching the specified filters, if any.
        /// </summary>
        /// <param name="idList">ID of the list the groups should belong to</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The wanted groups; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Groups" title="How to retrieve a set of groups matching the given criteria" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<ConsoleGroupItem> GetGroups(
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<ConsoleGroupItem>>(HttpVerb.GET, string.Format("Console/List/{0}/Groups", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }

        /// <summary>
        /// Adds to the given list the recipient with the given information.
        /// </summary>
        /// <param name="idList">ID of the list which the recipient will be added to</param>
        /// <param name="recipient">details of the recipient to add</param>
        /// <param name="withCOI">true to perform the "Confirmed Optin" procedure.
        /// This recipient is put in a "Pending" status, and a confirmation email is sent to him/her.
        /// When the recipient confirms the subscription by clicking on the link on the confirmation email,
        /// the status changes into "Subscribed".</param>
        /// <returns>the ID of the added recipient; 0 or negative if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Add Recipient To List" title="How to add a recipient to the given list" numberLines="true" outlining="true"></code>
        /// </example>
        public int AddRecipientToList(int idList, ConsoleRecipientItem recipient, bool withCOI = false)
        {
            string url = string.Format("Console/List/{0}/Recipient", (object)idList);
            if (withCOI)
                url += "?ConfirmEmail=true";
            return (int)this.CallMethodWithNumericResponse(HttpVerb.POST, url, (object)recipient);
        }

        /// <summary>
        /// Adds to the given list the recipients with the given information.
        /// This is an asynchronous task, and will be performed in background.
        /// The caller can check the status of the import calling the method CheckImportStatus(int).
        /// </summary>
        /// <param name="idList">ID of the list which the recipients will be added to</param>
        /// <param name="recipients">details of the recipients to add</param>
        /// <param name="withCOI">true to perform the "Confirmed Optin" procedure.
        /// Those recipients are put in a "Pending" status, and a confirmation email is sent to them.
        /// When a recipient confirms the subscription by clicking on the link on the confirmation email,
        /// the status changes into "Subscribed".</param>
        /// <returns>the ID of the import task; 0 or negative if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Add Recipients To List" title="How to add several recipients to the given list" numberLines="true" outlining="true"></code>
        /// </example>
        public int AddRecipientsToList(int idList, List<ConsoleRecipientItem> recipients, bool withCOI = false)
        {
            string url = string.Format("Console/List/{0}/Recipients", (object)idList);
            if (withCOI)
                url += "?confirmMessage=true";
            return (int)this.CallMethodWithNumericResponse(HttpVerb.POST, url, (object)recipients);
        }

        /// <summary>
        /// Retrieves the information regarding the import task specified in input.
        /// </summary>
        /// <param name="idImport">ID of the import to analyze; it is the result of asynchronous methods like
        /// AddRecipientToList(int, ConsoleRecipientItem), AddRecipientsToList(int, List&lt;ConsoleRecipientItem&gt;), etc.</param>
        /// <returns>the status of the import task; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Add Recipients To List" title="How to check the status of an asynchronous task (e.g. import of multiple recipients in a list)" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleImportStatus CheckImportStatus(int idImport) => this.CallMethod<ConsoleImportStatus>(HttpVerb.GET, string.Format("Console/Import/{0}", (object)idImport), (object)null);

        /// <summary>
        /// Forces the given recipient in a "Subscribed" status for the specified list.
        /// </summary>
        /// <param name="idList">ID of the list which the recipient should be subscribed to</param>
        /// <param name="recipientEmail">email of the recipient to subscribe</param>
        /// <returns>false if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Force Optin On Unsubscribed Recipients" title="How to force the given recipient to be subscribed to the given list" numberLines="true" outlining="true"></code>
        /// </example>
        public bool ForceOptinOnUnsubscribedRecipient(int idList, string recipientEmail)
        {
            int idImport = (int)this.CallMethodWithNumericResponse(HttpVerb.POST, string.Format("Console/List/{0}/Recipients?importType=asOptin", (object)idList), (object)new List<ConsoleRecipientItem>()
      {
        new ConsoleRecipientItem() { Email = recipientEmail }
      });
            int num = 0;
            do
            {
                ConsoleImportStatus consoleImportStatus = this.CheckImportStatus(idImport);
                if (consoleImportStatus == null)
                    return false;
                if (consoleImportStatus.Completed)
                    return true;
                ++num;
                Thread.Sleep(500);
            }
            while (num < 20);
            return false;
        }

        /// <summary>
        /// Forces the given recipient in an "UnSubscribed" status for the specified list.
        /// </summary>
        /// <param name="idList">ID of the list which the recipient should be unsubscribed from</param>
        /// <param name="idRecipient">ID of the recipient to unsubscribe</param>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Unsubscribe Recipient" title="How to unsubscribe a recipient for the given list" numberLines="true" outlining="true"></code>
        /// </example>
        public void UnsubscribeRecipient(int idList, int idRecipient) => this.CallMethod<object>(HttpVerb.DELETE, string.Format("Console/List/{0}/Unsubscribe/{1}", (object)idList, (object)idRecipient), (object)null);

        /// <summary>
        /// Forces the given recipients in an "UnSubscribed" status for the specified list.
        /// This is an asynchronous task, and will be performed in background.
        /// The caller can check the status of the task calling the method CheckImportStatus(int).
        /// </summary>
        /// <param name="idList">ID of the list which the recipient should be unsubscribed from</param>
        /// <param name="emailRecipients">List of emails of the recipients to unsubscribe</param>
        /// <returns>the ID of the unsubscription task; 0 or negative if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Unsubscribe Recipients" title="How to unsubscribe several recipients for the given list" numberLines="true" outlining="true"></code>
        /// </example>
        public int UnsubscribeRecipients(int idList, List<string> emailRecipients)
        {
            if (emailRecipients == null || emailRecipients.Count <= 0)
                return -1;
            List<ConsoleRecipientItem> consoleRecipientItemList = new List<ConsoleRecipientItem>();
            foreach (string emailRecipient in emailRecipients)
                consoleRecipientItemList.Add(new ConsoleRecipientItem()
                {
                    Email = emailRecipient
                });
            return (int)this.CallMethodWithNumericResponse(HttpVerb.POST, string.Format("Console/List/{0}/Recipients?importType=asOptout", (object)idList), (object)consoleRecipientItemList);
        }

        /// <summary>
        /// Retrieves the personal data fields for the current user.
        /// </summary>
        /// <returns>the list of recipient dynamic field definitions; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Personal Data Fields" title="How to retrieve the information of the current user" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<ConsoleDynamicFieldItem> GetPersonalDataFields() => this.CallMethod<CollectionOf<ConsoleDynamicFieldItem>>(HttpVerb.GET, "Console/Recipient/DynamicFields", (object)null);

        /// <summary>Updates the details of the given recipient.</summary>
        /// <returns>the updated details of the recipient; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Update Recipient" title="How to update a recipient information" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleRecipientItem UpdateRecipient(ConsoleRecipientItem recipient) => this.CallMethod<ConsoleRecipientItem>(HttpVerb.PUT, "Console/Recipient/Detail", (object)recipient);

        /// <summary>
        /// Retrieves the subscription status of the given recipient.
        /// </summary>
        /// <param name="idList">ID of the list which the recipients belongs to</param>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <returns>the subscription status of the recipient; SubscriptionStatus.Unknown if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Subscription Status By ID" title="How to retrieve the subscription status of a recipient, given his/her ID" numberLines="true" outlining="true"></code>
        /// </example>
        public SubscriptionStatus GetSubscriptionStatus(int idList, int idRecipient)
        {
            string filter = "idRecipient==" + (object)idRecipient;
            return this.RetrieveSubscriptionStatus(idList, filter);
        }

        /// <summary>
        /// Retrieves the subscription status of the given recipient.
        /// </summary>
        /// <param name="idList">ID of the list which the recipients belongs to</param>
        /// <param name="emailRecipient">email address of the recipient to check</param>
        /// <returns>the subscription status of the recipient; SubscriptionStatus.Unknown if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Subscription Status By Email" title="How to retrieve the subscription status of a recipient, given his/her email" numberLines="true" outlining="true"></code>
        /// </example>
        public SubscriptionStatus GetSubscriptionStatus(
          int idList,
          string emailRecipient)
        {
            if (string.IsNullOrEmpty(emailRecipient))
                return SubscriptionStatus.Unknown;
            string filter = "Email.Trim().Equals('" + emailRecipient.Trim() + "')";
            return this.RetrieveSubscriptionStatus(idList, filter);
        }

        private SubscriptionStatus RetrieveSubscriptionStatus(
          int idList,
          string filter)
        {
            if (this.GetRecipientInStatus(SubscriptionStatus.Subscribed, idList, filter) != null)
                return SubscriptionStatus.Subscribed;
            if (this.GetRecipientInStatus(SubscriptionStatus.Pending, idList, filter) != null)
                return SubscriptionStatus.Pending;
            return this.GetRecipientInStatus(SubscriptionStatus.Unsubscribed, idList, filter) != null ? SubscriptionStatus.Unsubscribed : SubscriptionStatus.Unknown;
        }

        private ConsoleRecipientItem GetRecipientInStatus(
          SubscriptionStatus status,
          int idList,
          string filter)
        {
            return this.GetRecipientsInStatus(status, idList, filter, pageSize: new int?(1)).SafeFirstOrDefault<ConsoleRecipientItem>();
        }

        /// <summary>
        /// Retrieve the list of recipients in the given status for the given list
        /// </summary>
        /// <param name="status">status of the recipient</param>
        /// <param name="idList">ID of the list which the recipients belong to</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The recipients in the given status for the given list; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Recipients" title="How to retrieve the list of recipients in the given status for the given list" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<ConsoleRecipientItem> GetRecipientsInStatus(
          SubscriptionStatus status,
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            string format;
            switch (status)
            {
                case SubscriptionStatus.Pending:
                    format = "Console/List/{0}/Recipients/Pending";
                    break;
                case SubscriptionStatus.Subscribed:
                    format = "Console/List/{0}/Recipients/Subscribed";
                    break;
                case SubscriptionStatus.Unsubscribed:
                    format = "Console/List/{0}/Recipients/Unsubscribed";
                    break;
                default:
                    return (CollectionOf<ConsoleRecipientItem>)null;
            }
            return this.CallMethod<CollectionOf<ConsoleRecipientItem>>(HttpVerb.GET, string.Format(format, (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }

        /// <summary>
        /// Adds to the given group the recipient with the given information.
        /// This is an asynchronous task, and will be performed in background.
        /// The caller can check the status of the import calling the method CheckImportStatus(int).
        /// </summary>
        /// <param name="idGroup">ID of the group which the recipient will be added to</param>
        /// <param name="recipient">details of the recipient to add</param>
        /// <returns>the ID of the import task; 0 or negative if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Add Recipient to Group" title="How to add the given recipient to a group" numberLines="true" outlining="true"></code>
        /// </example>
        public int AddRecipientToGroup(int idGroup, ConsoleRecipientItem recipient) => (int)this.CallMethodWithNumericResponse(HttpVerb.POST, string.Format("Console/Group/{0}/Recipient", (object)idGroup), (object)recipient);

        /// <summary>
        /// Adds to the given group the recipients with the given information.
        /// </summary>
        /// <param name="idGroup">ID of the group which the recipients will be added to</param>
        /// <param name="recipients">details of the recipients to add</param>
        /// <returns>the ID of the added recipient; 0 or negative if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Add Recipients to Group" title="How to add multiple recipients to a group" numberLines="true" outlining="true"></code>
        /// </example>
        public int AddRecipientsToGroup(int idGroup, List<ConsoleRecipientItem> recipients) => (int)this.CallMethodWithNumericResponse(HttpVerb.POST, string.Format("Console/Group/{0}/Recipients", (object)idGroup), (object)recipients);

        /// <summary>Removes the given recipient from the specified group.</summary>
        /// <param name="idGroup">ID of the group which the recipient should be removed from</param>
        /// <param name="idRecipient">ID of the recipient to remove</param>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Remove Recipient from Group" title="How to remove the given recipient from a group" numberLines="true" outlining="true"></code>
        /// </example>
        public void RemoveRecipientFromGroup(int idGroup, int idRecipient) => this.CallMethod<object>(HttpVerb.DELETE, string.Format("Console/Group/{0}/Unsubscribe/{1}", (object)idGroup, (object)idRecipient), (object)null);

        /// <summary>Create an email message in the specified list.</summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="message">details of the message to add</param>
        /// <returns>the created message; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Create Email Message" title="How to create an email message in the specified list" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleMessageItem CreateMessage(
          int idList,
          ConsoleMessageDetailItem message)
        {
            return this.CallMethod<ConsoleMessageItem>(HttpVerb.POST, string.Format("Console/List/{0}/Email", (object)idList), (object)message);
        }

        /// <summary>
        /// Retrieve the email messages (cloned and not cloned) by the specified list matching the specified filters,
        /// if any.
        /// </summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The wanted messages; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Email Messages" title="How to retrieve a set of messages matching the given criteria" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<ConsoleMessageItem> GetMessages(
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<ConsoleMessageItem>>(HttpVerb.GET, string.Format("Console/List/{0}/Emails", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }

        /// <summary>
        /// Retrieve the email messages (visible online through the website) by the specified list matching the specified filters,
        /// if any.
        /// </summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The wanted messages; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Published Email Messages" title="How to retrieve a set of messages visible online through the website matching the given criteria" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<ConsolePublishedMessageItem> GetPublishedMessages(
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<ConsolePublishedMessageItem>>(HttpVerb.GET, string.Format("Console/List/{0}/Online/Emails", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }

        /// <summary>
        /// Retrieve the archived email messages by the specified list matching the specified filters,
        /// if any.
        /// </summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The wanted messages; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Archived Email Messages" title="How to retrieve a set of archived messages matching the given criteria" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<ConsoleArchivedMessageItem> GetArchivedMessages(
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<ConsoleArchivedMessageItem>>(HttpVerb.GET, string.Format("Console/List/{0}/Archived/Emails", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }

        /// <summary>
        /// Retrieve the details of the specified email message by the specified list,
        /// if any.
        /// </summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="idMessage">ID of the message to retrieve</param>
        /// <returns>The wanted messages; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Message Details" title="How to retrieve the details of an email message" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleMessageDetails GetMessageDetails(int idList, int idMessage) => this.CallMethod<ConsoleMessageDetails>(HttpVerb.GET, string.Format("Console/List/{0}/Email/{1}", (object)idList, (object)idMessage), (object)null);

        /// <summary>
        /// Retrieve the attachments of the specified email message by the specified list,
        /// if any.
        /// </summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="idMessage">ID of the message to retrieve</param>
        /// <returns>The wanted messages; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Message Attachments" title="How to retrieve the attachments of an email message" numberLines="true" outlining="true"></code>
        /// </example>
        public List<ConsoleEmailAttachmentItem> GetMessageAttachments(
          int idList,
          int idMessage)
        {
            return this.CallMethod<List<ConsoleEmailAttachmentItem>>(HttpVerb.GET, string.Format("Console/List/{0}/Email/{1}/Attachment", (object)idList, (object)idMessage), (object)null);
        }

        /// <summary>
        /// Retrieves the status of email sending queues for the given type.
        /// </summary>
        /// <param name="status">type of queue to check</param>
        /// <returns>The sending details of the specified queue; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Email Sending Status" title="How to retrieve the sending status of the specified queue" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<EmailSendingItem> GetEmailSendingStatus(
          SendingStatus status)
        {
            string url;
            switch (status)
            {
                case SendingStatus.Immediate:
                    url = "Console/Email/Sendings/Immediate";
                    break;
                case SendingStatus.Deferred:
                    url = "Console/Email/Sendings/Deferred";
                    break;
                case SendingStatus.Undefined:
                    url = "Console/Email/Sendings/Undefined";
                    break;
                default:
                    throw new MailUpException(-1, "Unknown Sending Status");
            }
            return this.CallMethod<CollectionOf<EmailSendingItem>>(HttpVerb.GET, url, (object)null);
        }

        /// <summary>
        /// Retrieves the first useful date to schedule the given sending task.
        /// </summary>
        /// <param name="idSending">ID of the sending task to schedule</param>
        /// <returns>The first useful date (in UTC) to schedule the given sending task; DateTime.MaxValue if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Date for Deferred Dispatch" title="How to retrieve the first useful date to schedule the given sending task" numberLines="true" outlining="true"></code>
        /// </example>
        public DateTime GetDateForDeferredDispatch(int idSending)
        {
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
            string s = this.CallMethod<string>(HttpVerb.GET, string.Format("Console/Email/Sending/{0}/Deferred", (object)idSending), (object)null);
            try
            {
                return DateTime.ParseExact(s, "u", (IFormatProvider)invariantCulture);
            }
            catch
            {
                return DateTime.MaxValue;
            }
        }

        /// <summary>
        /// Retrieves the sending ID of the confirmation email related to given import task.
        /// </summary>
        /// <param name="idImport">ID of the import task to check</param>
        /// <returns>The sending ID of the confirmation email related to given import task; -1 if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get SendingID for Import" title="How to retrieve the sending ID of the confirmation email related to given import task" numberLines="true" outlining="true"></code>
        /// </example>
        public int GetSendingIDForImport(int idImport)
        {
            if (idImport <= 0)
                return -1;
            ImportSendDetails importSendDetails = this.CallMethod<ImportSendDetails>(HttpVerb.GET, string.Format("Console/Import/{0}/Sending", (object)idImport), (object)null);
            return importSendDetails == null ? -1 : importSendDetails.idSending;
        }

        /// <summary>Update an email message in the specified list.</summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="message">details of the message to update; the only mandatory field is 'idMessage'</param>
        /// <returns>the updated message; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Update Email Message" title="How to update an email message in the specified list" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleMessageItem UpdateMessage(
          int idList,
          ConsoleMessageDetailItem message)
        {
            if (message != null)
            {
                int? idMessage = message.idMessage;
                if ((idMessage.GetValueOrDefault() > 0 ? 0 : (idMessage.HasValue ? 1 : 0)) == 0)
                    return this.CallMethod<ConsoleMessageItem>(HttpVerb.PUT, string.Format("Console/List/{0}/Email/{1}", (object)idList, (object)message.idMessage), (object)message);
            }
            return (ConsoleMessageItem)null;
        }

        /// <summary>
        /// Retrieve the enabled tag list for the specified list, matching the specified filters, if any.
        /// </summary>
        /// <param name="idList">ID of the list described by the tags</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The wanted tags; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Tags" title="How to retrieve the enabled tag list for the specified list" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<ConsoleTagItem> GetTags(
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<ConsoleTagItem>>(HttpVerb.GET, string.Format("Console/List/{0}/Tags", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }

        /// <summary>Create a new tag in the specified list.</summary>
        /// <param name="idList">ID of the list described by this tag</param>
        /// <param name="name">name of the tag to add</param>
        /// <returns>the created tag; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Create Tag" title="How to create a tag in the specified list" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleTagItem CreateTag(int idList, string name) => this.CallMethod<ConsoleTagItem>(HttpVerb.POST, string.Format("Console/List/{0}/Tag", (object)idList), (object)name);

        /// <summary>Update a tag in the specified list.</summary>
        /// <param name="idList">ID of the list described by this tag</param>
        /// <param name="tag">details of the tag to update; the only mandatory field is 'Id'</param>
        /// <returns>the updated tag; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Update Tag" title="How to update a tag in the specified list" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleTagItem UpdateTag(int idList, ConsoleTagItem tag) => tag == null || tag.Id <= 0 ? (ConsoleTagItem)null : this.CallMethod<ConsoleTagItem>(HttpVerb.PUT, string.Format("Console/List/{0}/Tag/{1}", (object)idList, (object)tag.Id), (object)tag);

        /// <summary>Delete a tag in the specified list.</summary>
        /// <param name="idList">ID of the list described by this tag</param>
        /// <param name="idTag">ID of the tag to delete</param>
        /// <returns>the updated tag; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Delete Tag" title="How to delete a tag in the specified list" numberLines="true" outlining="true"></code>
        /// </example>
        public void DeleteTag(int idList, int idTag) => this.CallMethodWithNumericResponse(HttpVerb.DELETE, string.Format("Console/List/{0}/Tag/{1}", (object)idList, (object)idTag), (object)null);

        /// <summary>Add an attachment to the specified message.</summary>
        /// <param name="attachRequest">details of the attachment; all the fields are mandatory.
        /// Note: Non alphanumeric characters will be removed from the name of the attachment</param>
        /// <returns>The created attachment(s); null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Add Attachment" title="How to add an attachment to the specified message" numberLines="true" outlining="true"></code>
        /// </example>
        public List<ConsoleEmailAttachmentItem> AddMessageAttachment(
          ConsoleAddAttachmentRequest attachRequest)
        {
            if (attachRequest == null || attachRequest.idList <= 0 || (attachRequest.idMessage <= 0 || attachRequest.Slot <= 0))
                return (List<ConsoleEmailAttachmentItem>)null;
            Regex regex = new Regex("[^a-zA-Z0-9 -._]");
            attachRequest.Name = attachRequest.Name == null ? "" : regex.Replace(attachRequest.Name, "");
            return this.CallMethod<List<ConsoleEmailAttachmentItem>>(HttpVerb.POST, string.Format("Console/List/{0}/Email/{1}/Attachment/{2}", (object)attachRequest.idList, (object)attachRequest.idMessage, (object)attachRequest.Slot), (object)attachRequest);
        }

        /// <summary>Delete an attachment from the specified message.</summary>
        /// <param name="idList">ID of the list which the message belongs to</param>
        /// <param name="idMessage">ID of the message which the attachment belongs to</param>
        /// <param name="slot">number of slot to remove</param>
        /// <returns>The delete attachment(s); null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Delete Attachment" title="How to delete an attachment from the specified message" numberLines="true" outlining="true"></code>
        /// </example>
        public List<ConsoleEmailAttachmentItem> DeleteMessageAttachment(
          int idList,
          int idMessage,
          int slot)
        {
            return this.CallMethod<List<ConsoleEmailAttachmentItem>>(HttpVerb.DELETE, string.Format("Console/List/{0}/Email/{1}/Attachment/{2}", (object)idList, (object)idMessage, (object)slot), (object)null);
        }

        /// <summary>
        /// Send an email message to the recipients in the specified list
        /// </summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="idMessage">ID of the message to send</param>
        /// <param name="senderName">name of the sender if different from the default one of the list</param>
        /// <param name="senderAddress">email address of the sender if different from the default one of the list</param>
        /// <param name="scheduledTime">UTC time for the deferred scheduling; leave null to send the message immediately</param>
        /// <returns>the sent message; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Send Email Message To List" title="How to send an email message to the recipients in the specified list" numberLines="true" outlining="true"></code>
        /// </example>
        public MailMessageSendResponse SendEmailMessageToList(
          int idList,
          int idMessage,
          string senderName = "",
          string senderAddress = "",
          DateTime? scheduledTime = null)
        {
            return this.CallMethod<MailMessageSendResponse>(HttpVerb.POST, this.PrepareURLToSendEmail(string.Format("Console/List/{0}/Email/{1}/Send", (object)idList, (object)idMessage), senderName, senderAddress, scheduledTime), (object)null);
        }

        /// <summary>
        /// Send an email message to the recipients in the specified group
        /// </summary>
        /// <param name="idGroup">ID of the group which the message will be sent to</param>
        /// <param name="idMessage">ID of the message to send</param>
        /// <param name="senderName">name of the sender if different from the default one of the group</param>
        /// <param name="senderAddress">email address of the sender if different from the default one of the group</param>
        /// <param name="scheduledTime">UTC time for the deferred scheduling; leave null to send the message immediately</param>
        /// <returns>the sent message; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Send Email Message To Group" title="How to send an email message to the recipients in the specified group" numberLines="true" outlining="true"></code>
        /// </example>
        public MailMessageSendResponse SendEmailMessageToGroup(
          int idGroup,
          int idMessage,
          string senderName = "",
          string senderAddress = "",
          DateTime? scheduledTime = null)
        {
            return this.CallMethod<MailMessageSendResponse>(HttpVerb.POST, this.PrepareURLToSendEmail(string.Format("Console/Group/{0}/Email/{1}/Send", (object)idGroup, (object)idMessage), senderName, senderAddress, scheduledTime), (object)null);
        }

        /// <summary>Send an email message to the specified recipient</summary>
        /// <param name="request">parameters of the request; all fields are mandatory</param>
        /// <param name="senderName">name of the sender if different from the default one of the group</param>
        /// <param name="senderAddress">email address of the sender if different from the default one of the group</param>
        /// <param name="scheduledTime">UTC time for the deferred scheduling; leave null to send the message immediately</param>
        /// <returns>the sent message; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Send Email Message To Recipient" title="How to send an email message to a recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public MailMessageSendResponse SendEmailMessage(
          EmailSendToRecipientRequest request,
          string senderName = "",
          string senderAddress = "",
          DateTime? scheduledTime = null)
        {
            return this.CallMethod<MailMessageSendResponse>(HttpVerb.POST, this.PrepareURLToSendEmail("Console/Email/Send", senderName, senderAddress, scheduledTime), (object)request);
        }

        private string PrepareURLToSendEmail(
          string action,
          string senderName,
          string senderAddress,
          DateTime? scheduledTime)
        {
            action = ClientUtils.AddQuerystringParam(action, "SenderName", senderName);
            action = ClientUtils.AddQuerystringParam(action, "SenderAddress", senderAddress);
            if (scheduledTime.HasValue)
                action = ClientUtils.AddQuerystringParam(action, "datetime", scheduledTime.Value.ToString("yyyy-MM-dd HH:mm:ssZ"));
            return action;
        }

        /// <summary>Send the given email message</summary>
        /// <param name="idSending">ID of the sending task to activate</param>
        /// <param name="scheduledTime">UTC time for the deferred scheduling; leave null to send the message immediately</param>
        /// <returns>the sending details; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Send Email Message From SendingID" title="How to send the given email message" numberLines="true" outlining="true"></code>
        /// </example>
        public EmailSendingItem SendEmailMessage(int idSending, DateTime? scheduledTime = null)
        {
            string url = string.Format("Console/Email/Sendings/{0}/Immediate", (object)idSending);
            ConsoleDeferredDispatchInfo deferredDispatchInfo = (ConsoleDeferredDispatchInfo)null;
            if (scheduledTime.HasValue)
            {
                url = string.Format("Console/Email/Sendings/{0}/Deferred", (object)idSending);
                deferredDispatchInfo = new ConsoleDeferredDispatchInfo()
                {
                    Date = scheduledTime.Value.ToString("u")
                };
            }
            return this.CallMethod<EmailSendingItem>(HttpVerb.POST, url, (object)deferredDispatchInfo);
        }

        /// <summary>
        /// Retrieve email message send history for the given message
        /// </summary>
        /// <param name="idList">ID of the list which the message belongs to</param>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The send history; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Email Message History" title="How to send an email message to the recipients in the specified list" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<ConsoleMessageSendHistoryItem> GetEmailMessageSendHistory(
          int idList,
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<ConsoleMessageSendHistoryItem>>(HttpVerb.GET, string.Format("Console/List/{0}/Email/{1}/SendHistory", (object)idList, (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }

        /// <summary>
        /// Retrieve the list of recipients who received the specified email
        /// </summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The recipients who received the specified email; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Recipients" title="How to retrieve the list of recipients who received the specified email" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<MessageRecipientItem> GetMessageRecipients(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<MessageRecipientItem>>(HttpVerb.GET, string.Format("Message/{0}/List/Recipients", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the number of recipients who received the specified email
        /// </summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <returns>The number of recipients who received the specified email; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Count Message Recipients" title="How to retrieve the number of recipients who received the specified email" numberLines="true" outlining="true"></code>
        /// </example>
        public int CountMessageRecipients(int idMessage, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(HttpVerb.GET, string.Format("Message/{0}/Count/Recipients", (object)idMessage), (object)null, filterExpression, consoleEndpoint: false);

        /// <summary>Retrieve the list of views of the specified email</summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The views of the specified email; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Views" title="How to retrieve the list of views of the specified email" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<MessageViewItem> GetMessageViews(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<MessageViewItem>>(HttpVerb.GET, string.Format("Message/{0}/List/Views", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>Retrieve the number of views of the specified email</summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <returns>The number of recipients who received the specified email; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Count Message Views" title="How to retrieve the number of views of the specified email" numberLines="true" outlining="true"></code>
        /// </example>
        public int CountMessageViews(int idMessage, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(HttpVerb.GET, string.Format("Message/{0}/Count/Views", (object)idMessage), (object)null, filterExpression, consoleEndpoint: false);

        /// <summary>
        /// Retrieve the list of clicks on a link in the specified email
        /// </summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The clicks on a link in the specified email; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Clicks" title="How to retrieve the list of clicks on a link in the specified email" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<MessageRecipientClickDetailItem> GetMessageClicks(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<MessageRecipientClickDetailItem>>(HttpVerb.GET, string.Format("Message/{0}/List/Clicks", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the number of clicks on a link in the specified email
        /// </summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <returns>The number of clicks on a link in the specified email; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Count Message Clicks" title="How to retrieve the number of clicks on a link in the specified email" numberLines="true" outlining="true"></code>
        /// </example>
        public int CountMessageClicks(int idMessage, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(HttpVerb.GET, string.Format("Message/{0}/Count/Clicks", (object)idMessage), (object)null, filterExpression, consoleEndpoint: false);

        /// <summary>
        /// Retrieve the list of clicks on a link in the specified email
        /// </summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The clicks on a link in the specified email; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Url Clicks" title="How to retrieve the list of clicks on a link in the specified email" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<MessageUrlClickItem> GetMessageUrlClicks(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<MessageUrlClickItem>>(HttpVerb.GET, string.Format("Message/{0}/List/UrlClicks", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the list of clicks on a link in the specified email with details
        /// </summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The clicks on a link in the specified email with details; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Url Clicks Details" title="How to retrieve the list of clicks on a link in the specified email with details" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<MessageUrlClickDetailItem> GetMessageUrlClicksDetails(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<MessageUrlClickDetailItem>>(HttpVerb.GET, string.Format("Message/{0}/List/UrlClickDetails", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>Retrieve the list of bounces from the specified email</summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The bounces from the specified email; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Bounces" title="How to retrieve the list of bounces from the specified email" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<MessageBounceItem> GetMessageBounces(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<MessageBounceItem>>(HttpVerb.GET, string.Format("Message/{0}/List/Bounces", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the number of bounces from the specified email
        /// </summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <returns>The number of bounces from the specified email; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Count Message Bounces" title="How to retrieve the number of bounces from the specified email" numberLines="true" outlining="true"></code>
        /// </example>
        public int CountMessageBounces(int idMessage, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(HttpVerb.GET, string.Format("Message/{0}/Count/Bounces", (object)idMessage), (object)null, filterExpression, consoleEndpoint: false);

        /// <summary>
        /// Retrieve the list of unsubscriptions from the specified email
        /// </summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The unsubscriptions from the specified email; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Unsubscriptions" title="How to retrieve the list of unsubscriptions from the specified email" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<MessageUnsubscriptionItem> GetMessageUnsubscriptions(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<MessageUnsubscriptionItem>>(HttpVerb.GET, string.Format("Message/{0}/List/Unsubscriptions", (object)idMessage), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the number of unsubscriptions from the specified email
        /// </summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <returns>The number of unsubscriptions from the specified email; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Count Message Unsubscriptions" title="How to retrieve the number of unsubscriptions from the specified email" numberLines="true" outlining="true"></code>
        /// </example>
        public int CountMessageUnsubscriptions(int idMessage, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(HttpVerb.GET, string.Format("Message/{0}/Count/Unsubscriptions", (object)idMessage), (object)null, filterExpression, consoleEndpoint: false);

        /// <summary>
        /// Retrieve the list of messages received by the specified recipient
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The messages received by the specified recipient; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Deliveries" title="How to retrieve the list of messages received by the specified recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<MessageDeliveryItem> GetMessageDeliveries(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<MessageDeliveryItem>>(HttpVerb.GET, string.Format("Recipient/{0}/List/Deliveries", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the number of messages received by the specified recipient
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <returns>The messages received by the specified recipient; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Count Message Deliveries" title="How to retrieve the number of messages received by the specified recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public int CountMessageDeliveries(int idRecipient, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(HttpVerb.GET, string.Format("Recipient/{0}/Count/Deliveries", (object)idRecipient), (object)null, filterExpression, consoleEndpoint: false);

        /// <summary>
        /// Retrieve the list of messages viewed by the specified recipient
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The messages viewed by the specified recipient; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Views By Recipient" title="How to retrieve the list of messages viewed by the specified recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<RecipientMessageViewItem> GetMessageViewsByRecipient(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<RecipientMessageViewItem>>(HttpVerb.GET, string.Format("Recipient/{0}/List/Views", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the number of messages viewed by the specified recipient
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <returns>The number of messages viewed by the specified recipient; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Count Message Views By Recipient" title="How to retrieve the number of messages viewed by the specified recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public int CountMessageViewsByRecipient(int idRecipient, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(HttpVerb.GET, string.Format("Recipient/{0}/Count/Views", (object)idRecipient), (object)null, filterExpression, consoleEndpoint: false);

        /// <summary>
        /// Retrieve the list of message clicks on a link done by the specified recipient
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The message clicks on a link done by the specified recipient; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Clicks By Recipient" title="How to retrieve the list of message clicks on a link done by the specified recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<RecipientClickDetailItem> GetMessageClicksByRecipient(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<RecipientClickDetailItem>>(HttpVerb.GET, string.Format("Recipient/{0}/List/Clicks", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the number of message clicks on a link done by the specified recipient
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <returns>The number of message clicks on a link done by the specified recipient; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Count Message Clicks By Recipient" title="How to retrieve the number of message clicks on a link done by the specified recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public int CountMessageClicksByRecipient(int idRecipient, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(HttpVerb.GET, string.Format("Recipient/{0}/Count/Clicks", (object)idRecipient), (object)null, filterExpression, consoleEndpoint: false);

        /// <summary>
        /// Retrieve the list of message clicks on a link done by the specified recipient with details
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The message clicks on a link done by the specified recipient with details; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Clicks Details By Recipient" title="How to retrieve the list of message clicks on a link done by the specified recipient with details" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<RecipientUrlClickDetailItem> GetMessageClicksDetailsByRecipient(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<RecipientUrlClickDetailItem>>(HttpVerb.GET, string.Format("Recipient/{0}/List/ClicksDetails", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the list of bounces returned by the specified recipient
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The bounces returned by the specified recipient; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Bounces By Recipient" title="How to retrieve the list of bounces returned by the specified recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<MessageBounceItem> GetMessageBouncesByRecipient(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<MessageBounceItem>>(HttpVerb.GET, string.Format("Recipient/{0}/List/Bounces", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the number of bounces returned by the specified recipient
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <returns>The number of bounces returned by the specified recipient; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Count Message Bounces By Recipient" title="How to retrieve the number of bounces returned by the specified recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public int CountMessageBouncesByRecipient(int idRecipient, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(HttpVerb.GET, string.Format("Recipient/{0}/Count/Bounces", (object)idRecipient), (object)null, filterExpression, consoleEndpoint: false);

        /// <summary>
        /// Retrieve the list of bounces returned by the specified recipient with details
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The bounces returned by the specified recipient with details; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Bounces Details" title="How to retrieve the list of bounces returned by the specified recipient with details" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<MessageBounceDetailItem> GetMessageBouncesDetails(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<MessageBounceDetailItem>>(HttpVerb.GET, string.Format("Recipient/{0}/List/BouncesDetails", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the list of unsubscriptions done by the specified recipient
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The unsubscriptions done by the specified recipient; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Get Message Unsubscriptions By Recipient" title="How to retrieve the list of unsubscriptions done by the specified recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<RecipientUnsubscriptionItem> GetMessageUnsubscriptionsByRecipient(
          int idRecipient,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<RecipientUnsubscriptionItem>>(HttpVerb.GET, string.Format("Recipient/{0}/List/Unsubscriptions", (object)idRecipient), (object)null, filterExpression, sortExpression, pageSize, pageNumber, false);
        }

        /// <summary>
        /// Retrieve the number of unsubscriptions done by the specified recipient
        /// </summary>
        /// <param name="idRecipient">ID of the recipient to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <returns>The number of unsubscriptions done by the specified recipient; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\MailStatistics\MailStatisticsFixtures.cs" region="Count Message Unsubscriptions By Recipient" title="How to retrieve the number of unsubscriptions done by the specified recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public int CountMessageUnsubscriptionsByRecipient(int idRecipient, string filterExpression = "") => (int)this.CallMethodWithNumericResponse(HttpVerb.GET, string.Format("Recipient/{0}/Count/Unsubscriptions", (object)idRecipient), (object)null, filterExpression, consoleEndpoint: false);

        /// <summary>Create an SMS message in the specified list.</summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="message">details of the message to add</param>
        /// <returns>the created message; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Create SMS Message" title="How to create an SMS message in the specified list" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleMessageItem CreateSmsMessage(
          int idList,
          ConsoleSmsMessageDetailItem message)
        {
            return this.CallMethod<ConsoleMessageItem>(HttpVerb.POST, string.Format("Console/Sms/List/{0}/Message", (object)idList), (object)message);
        }

        /// <summary>
        /// Retrieve the SMS messages by the specified list matching the specified filters,
        /// if any.
        /// </summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The wanted messages; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get SMS Messages" title="How to retrieve a set of messages matching the given criteria" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<ConsoleMessageItem> GetSmsMessages(
          int idList,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<ConsoleMessageItem>>(HttpVerb.GET, string.Format("Console/Sms/List/{0}/Messages", (object)idList), (object)null, filterExpression, sortExpression, pageSize, pageNumber);
        }

        /// <summary>
        /// Retrieve the details of the specified SMS message by the specified list,
        /// if any.
        /// </summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="idMessage">ID of the message to retrieve</param>
        /// <returns>The wanted messages; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get SMS Message Details" title="How to retrieve the details of an SMS message" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleSmsMessageDetailItem GetSmsMessageDetails(
          int idList,
          int idMessage)
        {
            return this.CallMethod<ConsoleSmsMessageDetailItem>(HttpVerb.GET, string.Format("Console/Sms/List/{0}/Message/{1}", (object)idList, (object)idMessage), (object)null);
        }

        /// <summary>
        /// Retrieves the status of SMS sending queues for the given type.
        /// </summary>
        /// <param name="status">type of queue to check</param>
        /// <returns>The sending details of the specified queue; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get SMS Sending Status" title="How to retrieve the sending status of the specified queue" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<SmsSendingItem> GetSmsSendingStatus(
          SendingStatus status)
        {
            string url;
            switch (status)
            {
                case SendingStatus.Immediate:
                    url = "Console/Sms/Sendings/Immediate";
                    break;
                case SendingStatus.Deferred:
                    url = "Console/Sms/Sendings/Deferred";
                    break;
                case SendingStatus.Undefined:
                    url = "Console/Sms/Sendings/Undefined";
                    break;
                default:
                    throw new MailUpException(-1, "Unknown Sending Status");
            }
            return this.CallMethod<CollectionOf<SmsSendingItem>>(HttpVerb.GET, url, (object)null);
        }

        /// <summary>
        /// Retrieves the first useful date to schedule the given SMS sending task.
        /// </summary>
        /// <param name="idSending">ID of the sending task to schedule</param>
        /// <returns>The first useful date (in UTC) to schedule the given sending task; DateTime.MaxValue if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get Date for SMS Deferred Dispatch" title="How to retrieve the first useful date to schedule the given SMS sending task" numberLines="true" outlining="true"></code>
        /// </example>
        public DateTime GetSmsDateForDeferredDispatch(int idSending)
        {
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
            string s = this.CallMethod<string>(HttpVerb.GET, string.Format("Console/Sms/Sending/{0}/Deferred", (object)idSending), (object)null);
            try
            {
                return DateTime.ParseExact(s, "u", (IFormatProvider)invariantCulture);
            }
            catch
            {
                return DateTime.MaxValue;
            }
        }

        /// <summary>Update an SMS message in the specified list.</summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="message">details of the message to update; the only mandatory field is 'idMessage'</param>
        /// <returns>the updated message; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Update SMS Message" title="How to update an SMS message in the specified list" numberLines="true" outlining="true"></code>
        /// </example>
        public ConsoleMessageItem UpdateSmsMessage(
          int idList,
          ConsoleSmsMessageDetailItem message)
        {
            if (message != null)
            {
                int? idMessage = message.idMessage;
                if ((idMessage.GetValueOrDefault() > 0 ? 0 : (idMessage.HasValue ? 1 : 0)) == 0)
                    return this.CallMethod<ConsoleMessageItem>(HttpVerb.PUT, string.Format("Console/Sms/List/{0}/Message/{1}", (object)idList, (object)message.idMessage), (object)message);
            }
            return (ConsoleMessageItem)null;
        }

        /// <summary>
        /// Send an SMS message to the recipients in the specified list
        /// </summary>
        /// <param name="idList">ID of the list which the message will be sent to</param>
        /// <param name="idMessage">ID of the message to send</param>
        /// <param name="senderPrefix">prefix of the sender if different from the default one of the list</param>
        /// <param name="senderNumber">number of the sender if different from the default one of the list</param>
        /// <param name="scheduledTime">UTC time for the deferred scheduling; leave null to send the message immediately</param>
        /// <returns>the sent message; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Send SMS Message To List" title="How to send an SMS message to the recipients in the specified list" numberLines="true" outlining="true"></code>
        /// </example>
        public SmsMessageSendResponse SendSmsMessageToList(
          int idList,
          int idMessage,
          string senderPrefix = "",
          string senderNumber = "",
          DateTime? scheduledTime = null)
        {
            return this.CallMethod<SmsMessageSendResponse>(HttpVerb.POST, this.PrepareURLToSendSms(string.Format("Console/Sms/List/{0}/Message/{1}/Send", (object)idList, (object)idMessage), senderPrefix, senderNumber, scheduledTime), (object)null);
        }

        /// <summary>
        /// Send an SMS message to the recipients in the specified group
        /// </summary>
        /// <param name="idGroup">ID of the group which the message will be sent to</param>
        /// <param name="idMessage">ID of the message to send</param>
        /// <param name="senderPrefix">prefix of the sender if different from the default one of the group</param>
        /// <param name="senderNumber">number of the sender if different from the default one of the group</param>
        /// <param name="scheduledTime">UTC time for the deferred scheduling; leave null to send the message immediately</param>
        /// <returns>the sent message; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Send SMS Message To Group" title="How to send an SMS message to the recipients in the specified group" numberLines="true" outlining="true"></code>
        /// </example>
        public SmsMessageSendResponse SendSmsMessageToGroup(
          int idGroup,
          int idMessage,
          string senderPrefix = "",
          string senderNumber = "",
          DateTime? scheduledTime = null)
        {
            return this.CallMethod<SmsMessageSendResponse>(HttpVerb.POST, this.PrepareURLToSendSms(string.Format("Console/Sms/Group/{0}/Message/{1}/Send", (object)idGroup, (object)idMessage), senderPrefix, senderNumber, scheduledTime), (object)null);
        }

        /// <summary>Send an SMS message to the specified recipient</summary>
        /// <param name="request">parameters of the request; all fields are mandatory</param>
        /// <param name="senderPrefix">prefix of the sender if different from the default one of the group</param>
        /// <param name="senderNumber">number of the sender if different from the default one of the group</param>
        /// <param name="scheduledTime">UTC time for the deferred scheduling; leave null to send the message immediately</param>
        /// <returns>the sent message; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Send SMS Message To Recipient" title="How to send an SMS message to a recipient" numberLines="true" outlining="true"></code>
        /// </example>
        public SmsMessageSendResponse SendSmsMessage(
          SmsSendToRecipientRequest request,
          string senderPrefix = "",
          string senderNumber = "",
          DateTime? scheduledTime = null)
        {
            return this.CallMethod<SmsMessageSendResponse>(HttpVerb.POST, this.PrepareURLToSendSms("Console/Sms/Send", senderPrefix, senderNumber, scheduledTime), (object)request);
        }

        private string PrepareURLToSendSms(
          string action,
          string senderPrefix,
          string senderNumber,
          DateTime? scheduledTime)
        {
            action = ClientUtils.AddQuerystringParam(action, nameof(senderPrefix), senderPrefix);
            action = ClientUtils.AddQuerystringParam(action, nameof(senderNumber), senderNumber);
            if (scheduledTime.HasValue)
                action = ClientUtils.AddQuerystringParam(action, "datetime", scheduledTime.Value.ToString("yyyy-MM-dd HH:mm:ssZ"));
            return action;
        }

        /// <summary>
        /// Retrieve the SMS message sending report by specifying SMS ID
        /// </summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The SMS message sending report by specifying SMS ID; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get SMS Report" title="How to retrieve the SMS message sending report by specifying SMS ID" numberLines="true" outlining="true"></code>
        /// </example>
        public SmsMessageSendingReport GetSmsSendingReport(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<SmsMessageSendingReport>(HttpVerb.GET, string.Format("Console/Sms/{0}/Sendings/Report", (object)idMessage), (object)null);
        }

        /// <summary>
        /// Retrieve the SMS message sending report by specifying SMS ID with details
        /// </summary>
        /// <param name="idMessage">ID of the message to check</param>
        /// <param name="filterExpression">(optional) filter to apply to the query</param>
        /// <param name="sortExpression">(optional) the sorting order to apply to the results of the query</param>
        /// <param name="pageSize">(optional) the maximum number of results to expect; if not specified a maximum of 20 items will be returned</param>
        /// <param name="pageNumber">(optional) if the result is paginated, use this parameter to specify the following chunks of results</param>
        /// <returns>The SMS message sending report by specifying SMS ID with details; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Console\ConsoleFixtures.cs" region="Get SMS Report Details" title="How to retrieve the SMS message sending report by specifying SMS ID with details" numberLines="true" outlining="true"></code>
        /// </example>
        public CollectionOf<SmsMessageSendingReportDetail> GetSmsSendingReportDetails(
          int idMessage,
          string filterExpression = "",
          string sortExpression = "",
          int? pageSize = null,
          int? pageNumber = null)
        {
            return this.CallMethod<CollectionOf<SmsMessageSendingReportDetail>>(HttpVerb.GET, string.Format("Console/Sms/{0}/Sendings/ReportDetails", (object)idMessage), (object)null);
        }

        /// <summary>
        /// Send a transactional message to multiple recipients. This method is not intended for bulk mailings.
        /// The message content is passed as input with HTML code or plain text.The message may also include
        /// attachments and embedded images.
        /// </summary>
        /// <param name="messageInfo">Complete information of the message</param>
        /// <returns>The result of the sending task</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Transactional\TransactionalFixtures.cs" region="Send Message" title="How to send a transaction email" numberLines="true" outlining="true"></code>
        /// </example>
        public SendResponseDTO SendMessage(MessageDTO messageInfo) => this.CallDirectMethod<SendResponseDTO>(HttpVerb.POST, "messages/sendmessage", (object)messageInfo);

        /// <summary>
        /// Send a transactional message to multiple recipients. This method is not intended for bulk mailings.
        /// The message content (both HTML body and plain text) is obtained by specifying the unique ID of a message
        /// previously created and stored in the MailUp admin console.
        /// The message may also include attachments and embedded images.
        /// </summary>
        /// <param name="messageInfo">Complete information of the message</param>
        /// <returns>The result of the sending task</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Transactional\TransactionalFixtures.cs" region="Send Template" title="How to send a transaction email as a template" numberLines="true" outlining="true"></code>
        /// </example>
        public SendResponseDTO SendTemplate(TemplateDTO messageInfo) => this.CallDirectMethod<SendResponseDTO>(HttpVerb.POST, "messages/sendtemplate", (object)messageInfo);

        /// <summary>Retrieve the information regarding SMTP users</summary>
        /// <param name="username">username of the caller</param>
        /// <param name="password">password of the caller</param>
        /// <param name="userToRead">(optional) User from which to read the profile, if omitted the method returns all users</param>
        /// <returns>The information regarding SMTP users; null if any error occurred</returns>
        /// <exception cref="T:MailUp.Sdk.Base.MailUpException">if any error occurred</exception>
        /// <example>
        /// This example include the code from a source .cs in folder (also of another project).
        ///     <code lang="C#" source="MailUp.Sdk.Fixtures\Integration\Transactional\TransactionalFixtures.cs" region="List Users" title="How to retrieve SMTP users information" numberLines="true" outlining="true"></code>
        /// </example>
        public List<UserInfoDTO> ListUserInfo(
          string username,
          string password,
          string userToRead = "")
        {
            UserResponseDTO userResponseDto = this.CallDirectMethod<UserResponseDTO>(HttpVerb.POST, "users/listuserinfo ", (object)new ListUsersDTO()
            {
                User = new SmtpUserDTO()
                {
                    Username = username,
                    Secret = password
                },
                Username = userToRead
            });
            return userResponseDto == null ? new List<UserInfoDTO>() : userResponseDto.UserList;
        }
    }
}

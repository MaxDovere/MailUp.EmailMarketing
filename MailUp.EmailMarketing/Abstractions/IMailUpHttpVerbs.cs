using MailUp.EmailMarketing.Model;
using RestSharp;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Abstractions
{
    internal interface IMailUpHttpVerbs
    {
        Task<T> CallMethodAsync<T>(
            Method verb, 
            string url, 
            object requestData, 
            string scheme, 
            string authorizedToken,
            bool consoleEndpoint = true)
            where T : class;
        Task<T> CallMethodAsync<T>(
            Method verb,
            string url,
            object requestData,
            string scheme,
            string authorizedToken,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null,
            bool consoleEndpoint = true)
            where T : class;

        Task<T> CallDirectMethodAsync<T>(
            Method verb, 
            string url, 
            object requestData,
            string scheme,
            string authorizedToken,
            bool consoleEndpoint = true)
            where T : class;
        long CallMethodWithNumericResponseAsync(
            Method verb,
            string url,
            object requestData,
            string scheme,
            string authorizedToken,
            bool consoleEndpoint = true);
        long CallMethodWithNumericResponseAsync(
            Method verb,
            string url,
            object requestData,
            string scheme,
            string authorizedToken,
            string filterExpression = "",
            string sortExpression = "",
            int? pageSize = null,
            int? pageNumber = null,
            bool consoleEndpoint = true);


    }
}

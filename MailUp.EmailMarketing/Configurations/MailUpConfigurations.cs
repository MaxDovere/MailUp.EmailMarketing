using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailUp.EmailMarketing.Configurations
{
    public class MailUpConfigurations
    {
        public class MailUpApiv1
        {
            public const string Key = "MailUpApiv1";

            public string MailUpLogon { get; init; }
            public string MailUpAuthorization { get; init; }
            public string MailUpToken { get; init; }
            public string MailUpConsole { get; init; }
            public string MailUpStatisticals { get; init; }
            //public string CallBackUri { get; set; }
            public string MailUpClientId { get; set; }
            public string MailUpClientSecret { get; set; }

            public MailUpApiv1()
            {
                try
                {
                    var appSettings = ConfigurationManager.AppSettings;
                    MailUpLogon = appSettings[Key + ":" + nameof(MailUpLogon)] ?? "Not Found";
                    MailUpAuthorization = appSettings[Key + ":" + nameof(MailUpAuthorization)] ?? "Not Found";
                    MailUpToken = appSettings[Key + ":" + nameof(MailUpToken)] ?? "Not Found";
                    MailUpConsole = appSettings[Key + ":" + nameof(MailUpConsole)] ?? "Not Found";
                    MailUpStatisticals = appSettings[Key + ":" + nameof(MailUpStatisticals)] ?? "Not Found";
                    //CallBackUri = appSettings[nameof(CallBackUri)] ?? "Not Found";
                    MailUpClientId = appSettings[Key + ":" + nameof(MailUpClientId)] ?? "Not Found";
                    MailUpClientSecret = appSettings[Key + ":" + nameof(MailUpClientSecret)] ?? "Not Found";
                }
                catch (ConfigurationErrorsException ex)
                {
                    throw new ConfigurationErrorsException(ex.Message);
                }
            }
        }
        //public class ApplicationServices
        //{
        //    public const string Key = "AppServices";

        //    public string ConnectionString { get; set; }
        //    public string TypeStore { get; set; }
        //}
    }
}
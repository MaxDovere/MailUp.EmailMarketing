using DeliveryMailUp.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Configuration;

#region snippet1
[assembly: HostingStartup(typeof(DeliveryMailUp.ServicesHostStartup))]

namespace DeliveryMailUp
{
    public class ServicesHostStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            var mailupApiv1 = (IConfiguration)ConfigurationManager.GetSection(ConfigurationsMailUp.MailUpApiv1.Key);

        }
    }
}
#endregion
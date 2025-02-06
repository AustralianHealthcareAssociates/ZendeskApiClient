using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using ZendeskApi.Client.Options;
#pragma warning disable 618

namespace ZendeskApi.Client.IntegrationTests.Factories
{
    public class ZendeskClientFactory
    {

        private readonly IConfiguration _configuration;
        public ZendeskClientFactory(TestHostBuilder host) {
            _configuration = host.TestHost.Services.GetRequiredService<IConfiguration>();
        }
        public static IZendeskClient zendeskClient;
        public IZendeskClient GetClient()
        {
            zendeskClient ??= new ZendeskClient(GetApiClient());
            return zendeskClient;
        }

        public static ZendeskApiClient apiClient;
        public ZendeskApiClient GetApiClient()
        {
            apiClient ??= new ZendeskApiClient(
                    new OptionsWrapper<ZendeskOptions>(new ZendeskOptions
                    {
                        EndpointUri = _configuration.GetValue<string>("ZendeskApi_Credentials_Url"),
                        Username = _configuration.GetValue<string>("ZendeskApi_Credentials_Username"),
                        Token = _configuration.GetValue<string>("ZendeskApi_Credentials_Token"),
                    })
             ); ;
            return apiClient;
        }
    }
}

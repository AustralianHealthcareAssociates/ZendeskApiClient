using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client.IntegrationTests.Factories
{
    public class TestHostBuilder
    {

        public TestHostBuilder()
        {
            TestHost = CreateHostBuilder().Build();
            Task.Run(() => TestHost.RunAsync());
        }

        public IHost TestHost { get; }

        public static IHostBuilder CreateHostBuilder(string[] args = null) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddEnvironmentVariables();
                    config.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddOptions();
                    services.Configure<ZendeskClientFactory>(context.Configuration);

                });
               
    }
}

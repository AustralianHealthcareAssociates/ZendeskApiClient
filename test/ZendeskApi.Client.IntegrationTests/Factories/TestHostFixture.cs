using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZendeskApi.Client.IntegrationTests.Factories
{
    public class TestHostFixture
    {
        public TestHostBuilder HostBuilder { get; }

        public TestHostFixture()
        {
            HostBuilder = new TestHostBuilder();
        }
    }
}

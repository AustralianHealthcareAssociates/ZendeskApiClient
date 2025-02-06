using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class TicketFieldsResourceTests : IClassFixture<TestHostFixture>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public TicketFieldsResourceTests(
            TestHostFixture testHostFixture)
        {
            _clientFactory = new ZendeskClientFactory(testHostFixture.HostBuilder);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnTicketFields()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .TicketFields.GetAllAsync(new CursorPager());

            Assert.NotNull(results);
        }
    }
}
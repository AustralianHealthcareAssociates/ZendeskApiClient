using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class OrganizationFieldsResourceTests : IClassFixture<TestHostFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public OrganizationFieldsResourceTests(
            ITestOutputHelper output,
            TestHostFixture testHostFixture)
        {
            _clientFactory = new ZendeskClientFactory(testHostFixture.HostBuilder);

            _output = output;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnFields()
        {
            var client = _clientFactory.GetClient();

            var fields = await client
                .OrganizationFields
                .GetAllAsync(new CursorPager());

            Assert.NotEmpty(fields);
        }
    }
}

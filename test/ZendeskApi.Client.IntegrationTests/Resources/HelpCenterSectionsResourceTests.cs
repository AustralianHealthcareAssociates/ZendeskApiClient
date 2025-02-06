using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class HelpCenterSectionsResourceTests : IClassFixture<TestHostFixture>

    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public HelpCenterSectionsResourceTests(
            ITestOutputHelper output,
            TestHostFixture testHostFixture)
        {
            _clientFactory = new ZendeskClientFactory(testHostFixture.HostBuilder);
            _output = output;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldReturnSections()
        {
            var client = _clientFactory.GetClient();

            var sections = await client
                .HelpCenter
                .Sections
                .GetAllAsync("en-gb");

            Assert.NotEmpty(sections);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnSections()
        {
            var client = _clientFactory.GetClient();

            var sections = await client
                .HelpCenter
                .Sections
                .GetAllAsync(new CursorPager(), "en-gb");

            Assert.NotEmpty(sections);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCategory_ShouldReturnSections()
        {
            var client = _clientFactory.GetClient();

            var sections = await client
                .HelpCenter
                .Sections
                .GetAllAsync(360000599157, "en-gb");

            Assert.NotEmpty(sections);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldReturnSection()
        {
            var client = _clientFactory.GetClient();

            var section = await client
                .HelpCenter
                .Sections
                .GetAsync(360001138437, "en-gb");

            Assert.NotNull(section);
        }
    }
}
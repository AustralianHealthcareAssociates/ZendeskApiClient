using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class UserIdentitiesResourceTests : IClassFixture<TestHostFixture>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public UserIdentitiesResourceTests(
            TestHostFixture testHostFixture)
        {
            _clientFactory = new ZendeskClientFactory(testHostFixture.HostBuilder);
        }
        
        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnUserIdentities()
        {
            var client = _clientFactory.GetClient();
            var results = new UserIdentitiesCursorResponse();

            try
            {
                await client.UserIdentities.CreateUserIdentityAsync(new UserIdentity()
                {
                    Type = "twitter",
                    Value = "handle"
                }, 368420617118);

                results = (UserIdentitiesCursorResponse)await client
                    .UserIdentities.GetAllByUserIdAsync(368420617118, new CursorPager());

                Assert.NotNull(results);
            }
            finally
            {
                await client.UserIdentities.DeleteAsync(368420617118, (long)results.First().Id);
            }
        }
    }
}
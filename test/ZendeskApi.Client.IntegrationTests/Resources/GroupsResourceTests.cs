using System;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class GroupsResourceTests : IClassFixture<TestHostFixture>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public GroupsResourceTests(TestHostFixture testHostFixture)
        {
            _clientFactory = new ZendeskClientFactory(testHostFixture.HostBuilder);

        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnGroups()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .Groups.GetAllAsync(new CursorPager());

            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalledWithCursorPagination_ShouldReturnGroups()
        {
            var client = _clientFactory.GetClient();
            long? userId = null;
            long? groupId = null;

            string TestName(string entity) => $"{typeof(GroupsResourceTests).FullName}-{entity}-{DateTime.UtcNow}";

            try
            {
                var user = await client.Users.CreateAsync(new UserCreateRequest(TestName("user")));
                userId = user.Id;

                var group = await client.Groups.CreateAsync(new GroupCreateRequest(TestName("group")));
                groupId = group.Id;

                await client.Users.UpdateAsync(new UserUpdateRequest(userId.Value)
                {
                    DefaultGroupId = group.Id
                });

                var results = await client
                    .Groups.GetAllByUserIdAsync(userId.Value, new CursorPager());

                Assert.NotNull(results);
            }
            finally
            {
                if (userId.HasValue)
                {
                    await client.Users.DeleteAsync(userId.Value);
                }
                if (groupId.HasValue)
                {
                    await client.Groups.DeleteAsync(groupId.Value);
                }
            }
        }

        [Fact]
        public async Task GetAllByAssignableAsync_WhenCalledWithCursorPagination_ShouldReturnGroups()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .Groups.GetAllByAssignableAsync(new CursorPager());

            Assert.NotNull(results);
        }
    }
}
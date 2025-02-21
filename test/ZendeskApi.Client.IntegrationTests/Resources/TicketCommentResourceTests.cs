using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class TicketCommentResourceTests : IClassFixture<TestHostFixture>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public TicketCommentResourceTests(
            TestHostFixture testHostFixture)
        {
            _clientFactory = new ZendeskClientFactory(testHostFixture.HostBuilder);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnTicketComments()
        {
            var client = _clientFactory.GetClient();

            var ticket = await client.Tickets.CreateAsync(new TicketCreateRequest()
            {
                Comment = new TicketComment() {Body = "Printer is on fire"}
            });

            var results = await client
                .TicketComments.GetAllAsync(ticket.Ticket.Id, new CursorPager());

            Assert.NotNull(results);
            Assert.Single(results.Comments);
        }
    }
}
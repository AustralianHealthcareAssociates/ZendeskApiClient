using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class TicketResourceTests : IClassFixture<TestHostFixture>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public TicketResourceTests(
            TestHostFixture testHostFixture)
        {
            _clientFactory = new ZendeskClientFactory(testHostFixture.HostBuilder);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnTickets()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .Tickets.GetAllAsync(new CursorPager());

            Assert.NotNull(results);
            Assert.Equal(100, results.Count());
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnSpecificAmountOfTickets()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .Tickets.GetAllAsync(new CursorPager()
                {
                    Size = 10
                });

            Assert.NotNull(results);
            Assert.Equal(10, results.Count());
        }

        [Fact]
        public async Task GetAllByOrganizationIdAsync_WhenCalledWithCursorPagination_ShouldReturnTickets()
        {
            var client = _clientFactory.GetClient();
            TicketResponse createdTicket = null;
            try
            {
                createdTicket = await client.Tickets.CreateAsync(new Requests.TicketCreateRequest()
                {
                    Comment = new TicketComment() { Body = "Printer is on fire" },
                    OrganisationId = 360195486037,
                });
                var results = (TicketsListCursorResponse)await client
                    .Tickets.GetAllByOrganizationIdAsync(360195486037, new CursorPager());

                Assert.NotNull(results);
            }
            finally
            {
                if (createdTicket != null)
                    await client.Tickets.DeleteAsync(createdTicket.Ticket.Id);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldBePaginatable()
        {
            var client = _clientFactory.GetClient();

            var cursorPager = new CursorPager { Size = 5 };
            var ticketsPageOne = await client
                .Tickets.GetAllAsync(cursorPager);

            Assert.NotNull(ticketsPageOne);
            Assert.Equal(5, ticketsPageOne.Count());
            Assert.True(ticketsPageOne.Meta.HasMore);

            cursorPager.AfterCursor = ticketsPageOne.Meta.AfterCursor;

            var ticketsPageTwo = await client.Tickets.GetAllAsync(cursorPager);
            Assert.NotNull(ticketsPageTwo);
            Assert.Equal(5, ticketsPageTwo.Count());

            var ticketIdsPageOne = ticketsPageOne.Select(ticket => ticket.Id).ToList();
            var ticketIdsPageTwo = ticketsPageTwo.Select(ticket => ticket.Id).ToList();
            Assert.NotEqual(ticketIdsPageOne, ticketIdsPageTwo);
        }
    }
}
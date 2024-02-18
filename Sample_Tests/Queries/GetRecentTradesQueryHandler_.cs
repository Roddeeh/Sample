using AutoFixture;
using FluentAssertions;
using Moq;
using Sample.Application.Clients;
using Sample.Application.Models;
using Sample.Application.Queries;

namespace Sample_Tests.Queries
{
    public class GetRecentTradesQueryHandler_
    {
        private Mock<IIndependentReserveClient> _clientMock;
        private GetRecentTradesQueryHandler _handler;
        private Fixture _fixture;

        public GetRecentTradesQueryHandler_()
        {
            _fixture = new Fixture();
            _clientMock = new Mock<IIndependentReserveClient>();
            _handler = new GetRecentTradesQueryHandler(_clientMock.Object);
        }

        [Fact]
        public async void Shoud_Call_GetRecentTrade_And_Return_Result()
        {
            //Arrange
             _clientMock.Setup(x=> x.GetRecentTrade("xbt", "aud", 10)).ReturnsAsync(_fixture.Create<RecentTrade>());
           
            //Act
            var result = await _handler.Handle(new GetRecentTradesQuery(), CancellationToken.None);

            //Assert
            _clientMock.Verify(m => m.GetRecentTrade("xbt", "aud", 10), Times.Once);
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async void Shoud_Call_GetRecentTrade_And_Return_Empty_Result()
        {
            //Arrange
            _clientMock.Setup(x => x.GetRecentTrade("xbt", "aud", 10)).ReturnsAsync(value: null);

            //Act
            var result = await _handler.Handle(new GetRecentTradesQuery(), CancellationToken.None);

            //Assert
            _clientMock.Verify(m => m.GetRecentTrade("xbt", "aud", 10), Times.Once);
            result.Should().BeEmpty();
        }
    }
}

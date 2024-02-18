using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Sample.Application.Commands;
using Sample.Application.Models;
using Sample.Application.Queries;
using Sample.Hubs;

namespace Sample_Tests.Commands
{
    public class UpdateAverageStatisticCommandHandler_
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IHubContext<StatisticHub>> _hubContextMock;
        private UpdateAverageStatisticCommandHandler _handler;
        private Fixture _fixture;

        public UpdateAverageStatisticCommandHandler_()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _mediatorMock = new Mock<IMediator>();
            _hubContextMock = _fixture.Create<Mock<IHubContext<StatisticHub>>>();
            _handler = new UpdateAverageStatisticCommandHandler(_mediatorMock.Object, _hubContextMock.Object);
        }

        [Fact]
        public async Task Should_Handle_ValidResponse_And_Send_UpdateAverageTradeStatistic()
        {
            // Arrange
            var trades = new List<Trade>
            {
                new Trade { TradeTimestampUtc = DateTime.UtcNow.AddMinutes(-10), PrimaryCurrencyAmount = 100 },
                new Trade { TradeTimestampUtc = DateTime.UtcNow.AddMinutes(-5), PrimaryCurrencyAmount = 200 }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRecentTradesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(trades);

            // Act
            await _handler.Handle(new UpdateAverageStatisticCommand(), CancellationToken.None);

            // Assert
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetRecentTradesQuery>(), CancellationToken.None), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<EstabilishTickerConnecionCommand>(), CancellationToken.None), Times.Once);
        }
    }
}

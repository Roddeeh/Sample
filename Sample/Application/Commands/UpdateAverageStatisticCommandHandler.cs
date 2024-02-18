using MediatR;
using Microsoft.AspNetCore.SignalR;
using Sample.Application.Helpers;
using Sample.Application.Models;
using Sample.Application.Queries;
using Sample.Hubs;
using System.Net.WebSockets;

namespace Sample.Application.Commands
{
    public class UpdateAverageStatisticCommandHandler : IRequestHandler<UpdateAverageStatisticCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<StatisticHub> _hubContext;

        public UpdateAverageStatisticCommandHandler(IMediator mediator, IHubContext<StatisticHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        public async Task<Unit> Handle(UpdateAverageStatisticCommand request, CancellationToken cancellationToken)
        {
            var trades = (await _mediator.Send(new GetRecentTradesQuery())).ToList();

            var totalTrades = trades.Count();
            var totalVolume = trades.Sum(x => x.PrimaryCurrencyAmount);
            var totalMinutes = (trades.Max(x => x.TradeTimestampUtc) - trades.Min(x => x.TradeTimestampUtc)).TotalMinutes;

            var statistic = new AverageTradeStatistic
            {
                AverageTradesPerMinute = StatisticCalculationHelper.CalculateAverageTradesPerMinute(totalTrades, totalMinutes),
                AverageVolumePerMinute = StatisticCalculationHelper.CalculateAverageVolumePerMinute(totalVolume, totalMinutes)
            };

            await _hubContext.Clients.All.SendAsync("UpdateAverageTradeStatistic", statistic, cancellationToken);

            await _mediator.Send(new EstabilishTickerConnecionCommand(
                    trades.Min(x => x.TradeTimestampUtc),
                    totalTrades,
                    totalVolume));

            return Unit.Value;

        }
    }
}

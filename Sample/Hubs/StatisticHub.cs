using MediatR;
using Microsoft.AspNetCore.SignalR;
using Sample.Application.Models;

namespace Sample.Hubs
{
    public class StatisticHub: Hub
    {
        public async Task GetAverageStatistic()
        {

        }

        public async Task BroadcastAverageTradeStatistic(AverageTradeStatistic averageTradeStatistic)
        {
            await Clients.All.SendAsync("UpdateAverageTradeStatistic", averageTradeStatistic);
        }
    }
}

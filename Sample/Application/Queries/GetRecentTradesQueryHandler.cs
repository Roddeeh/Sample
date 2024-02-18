using MediatR;
using Sample.Application.Clients;
using Sample.Application.Models;

namespace Sample.Application.Queries
{
    public class GetRecentTradesQueryHandler : IRequestHandler<GetRecentTradesQuery, IEnumerable<Trade>>
    {
        private readonly IIndependentReserveClient _client;

        public GetRecentTradesQueryHandler(IIndependentReserveClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Trade>> Handle(GetRecentTradesQuery request, CancellationToken cancellationToken)
        {
            var recentTrade = await _client.GetRecentTrade();

            return recentTrade?.Trades ?? Enumerable.Empty<Trade>();
        }
    }
}

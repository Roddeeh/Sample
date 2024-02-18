using MediatR;
using Sample.Application.Models;

namespace Sample.Application.Queries
{
    public class GetRecentTradesQuery : IRequest<IEnumerable<Trade>>
    {
    }
}

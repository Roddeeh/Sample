using Sample.Application.Models;

namespace Sample.Application.Clients
{
    public interface IIndependentReserveClient
    {
        public Task<RecentTrade> GetRecentTrade(
            string primaryCurrencyCode = "xbt",
            string secondaryCurrencyCode = "aud",
            int numberTradesToRetreive = 10);
    }
}

using Sample.Application.Models;

namespace Sample.Application.Clients
{
    public class IndependentReserveClient : IIndependentReserveClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndependentReserveClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<RecentTrade> GetRecentTrade(
            string primaryCurrencyCode, 
            string secondaryCurrencyCode, 
            int numberTradesToRetreive)
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"https://api.independentreserve.com/Public/GetRecentTrades?primaryCurrencyCode={primaryCurrencyCode}&secondaryCurrencyCode={secondaryCurrencyCode}&numberOfRecentTradesToRetrieve={numberTradesToRetreive}");

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<RecentTrade>();
                
                return responseData;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve recent trades. Status code: {response.StatusCode}");
            }
        }
    }
}

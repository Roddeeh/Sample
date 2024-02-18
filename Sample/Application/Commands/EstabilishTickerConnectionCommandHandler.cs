using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Sample.Application.Helpers;
using Sample.Application.Models;
using Sample.Hubs;
using System.Net.WebSockets;
using System.Text;

namespace Sample.Application.Commands
{
    public class EstabilishTickerConnectionCommandHandler: IRequestHandler<EstabilishTickerConnecionCommand, Unit>
    {
        private readonly IHubContext<StatisticHub> _hubContext;
        private readonly string _webSocketUrl;

        public EstabilishTickerConnectionCommandHandler(IHubContext<StatisticHub> hubContext)
        {
            _hubContext = hubContext;
            _webSocketUrl = "wss://websockets.independentreserve.com?subscribe=ticker-xbt";
        }

        public async Task<Unit> Handle(EstabilishTickerConnecionCommand request, CancellationToken cancellationToken)
        {
            var initialDate = request.InitialDate;
            var count = request.Count;
            var volume = request.Volume;
           
            using (ClientWebSocket client = new ClientWebSocket())
            {
                try
                {
                    await client.ConnectAsync(new Uri(_webSocketUrl), cancellationToken);

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                        WebSocketReceiveResult result = await client.ReceiveAsync(buffer, cancellationToken);

                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                            if (message.Contains("Trade"))
                            {
                                var tickerData = JsonConvert.DeserializeObject<TickerTrade>(message);

                                if (tickerData != null && tickerData.Data != null)
                                {
                                    var totalMinutes = (tickerData.Data.TradeDate.ToUniversalTime() - initialDate).TotalMinutes;
                                    volume += tickerData.Data.Volume;
                                    count++;

                                    var statistic = new AverageTradeStatistic
                                    {
                                        AverageTradesPerMinute = StatisticCalculationHelper.CalculateAverageTradesPerMinute(count, totalMinutes),
                                        AverageVolumePerMinute = StatisticCalculationHelper.CalculateAverageVolumePerMinute(volume, totalMinutes)
                                    };

                                    await _hubContext.Clients.All.SendAsync("UpdateAverageTradeStatistic", statistic);
                                }
                            }
                        }
                        else if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "", cancellationToken);
                        }
                    }
                }
                catch (WebSocketException ex)
                {
                    Console.WriteLine($"WebSocket exception: {ex.Message}");
                }

                return Unit.Value;
            }
        }
    }
}

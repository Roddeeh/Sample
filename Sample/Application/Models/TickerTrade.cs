namespace Sample.Application.Models
{
    public class TickerTrade
    {
        public string Event { get; set; }
        public string Channel { get; set; }
        public int Nonce { get; set; }
        public long Time { get; set; }
        public Data Data { get; set; }
    }
}

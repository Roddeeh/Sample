namespace Sample.Application.Models
{
    public class Trade
    {
        public DateTime TradeTimestampUtc { get; set; }
        public double PrimaryCurrencyAmount { get; set; }
        public double SecondaryCurrencyTradePrice { get; set; }
        public string TradeGuid { get; set; }
        public string Taker { get; set; }
    }
}


namespace Sample.Application.Models
{
    public class RecentTrade
    {
        public List<Trade> Trades { get; set; }
        public string PrimaryCurrencyCode { get; set; }
        public string SecondaryCurrencyCode { get; set; }
        public DateTime CreatedTimestampUtc { get; set; }
    }
}

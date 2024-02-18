using Sample.Application.Models;

public class Data
{
    public string TradeGuid { get; set; }
    public DateTime TradeDate { get; set; }
    public double Volume { get; set; }
    public Price Price { get; set; }
    public string BidGuid { get; set; }
    public string OfferGuid { get; set; }
    public string BidClientId { get; set; }
    public string OfferClientId { get; set; }
    public string Side { get; set; }
}
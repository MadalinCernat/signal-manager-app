
namespace SignalManagerAppWebApi.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public Signal Signal { get; set; }
        public DateTime DateOrdered { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal Stoploss { get; set; }
    }
}

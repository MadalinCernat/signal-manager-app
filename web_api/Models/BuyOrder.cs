namespace SignalManagerAppWebApi.Models
{
    public class BuyOrder : IOrder
    {
        public string OrderId { get; set; } // unique identifier for buy order
        public Signal Signal { get; set; } // the signal choosed
        public DateTime DateOrdered { get; set; }
        public decimal BuyPrice { get; set; } // price at the time of buy order
        public decimal Stoploss { get; set; } // automatically set stoploss
    }
}

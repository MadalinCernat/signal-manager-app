namespace SignalManagerAppWebApi.Models
{
    public class Signal
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public Buy Buy { get; set; }
        public decimal[] Sell { get; set; }
    }

    public class Buy
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
    }
}

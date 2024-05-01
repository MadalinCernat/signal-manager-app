using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SignalManagerAppWebApi.Models
{
    public class Signal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId BsonId { get; set; }
        public string Id { get; set; } // unique identifier for signal
        public string Symbol { get; set; } // symbol of currency (BTC, ETH, ...)
        public string Exchange { get; set; } // exchange platform
        public Buy Buy { get; set; } // buy min and max
        public decimal[] Sell { get; set; } // sell array
    }

    public class Buy
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
    }
}

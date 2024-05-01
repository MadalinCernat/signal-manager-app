using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SignalManagerAppWebApi.Models
{
    public class SellOrder : IOrder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId BsonId { get; set; }
        public string OrderId { get; set;}
        public BuyOrder Buy { get; set; }
        public DateTime DateOrdered { get; set; }
        public decimal SellPrice { get; set; } // price at the time of sell
        public decimal Profit { get; set; } // SellPrice - Buy.BuyPrice
    }
}

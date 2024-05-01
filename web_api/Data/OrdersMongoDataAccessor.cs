using MongoDB.Driver;
using SignalManagerAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalManagerAppWebApi.Data
{
    public class OrdersMongoDataAccessor<T> : IOrdersDataAccessor<T> where T : IOrder
    {
        private readonly IMongoCollection<T> _collection;

        public OrdersMongoDataAccessor(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<T>(collectionName);
        }

        public List<T> ReadOrders()
        {
            return _collection.Find(_ => true).ToList();
        }

        public void AddOrder(T newOrder)
        {
            _collection.InsertOne(newOrder);
        }

        public void DeleteOrder(string orderId)
        {
            var filter = Builders<T>.Filter.Eq(order => order.OrderId, orderId);
            var result = _collection.DeleteOne(filter);
            if (result.DeletedCount == 0)
            {
                throw new ArgumentException($"Order with ID '{orderId}' not found.");
            }
        }
    }
}

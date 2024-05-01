using MongoDB.Driver;
using SignalManagerAppWebApi.Models;

namespace SignalManagerAppWebApi.Data
{
    public class SignalsMongoDataAccessor : ISignalsDataAccessor
    {
        private readonly IMongoCollection<Signal> _signalsCollection;

        public SignalsMongoDataAccessor(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _signalsCollection = database.GetCollection<Signal>(collectionName);
        }

        public void AddSignal(Signal newSignal)
        {
            _signalsCollection.InsertOne(newSignal);
        }

        public void DeleteSignal(string signalId)
        {
            var filter = Builders<Signal>.Filter.Eq(s => s.Id, signalId);
            _signalsCollection.DeleteOne(filter);
        }

        public List<Signal> ReadSignals()
        {
            return _signalsCollection.Find(_ => true).ToList();
        }
    }
}

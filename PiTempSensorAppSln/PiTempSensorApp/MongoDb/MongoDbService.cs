using MongoDB.Driver;
using PiTempSensorApp.Models;
using PiTempSensorApp.Services;

namespace PiTempSensorApp.MongoDb
{
    internal class MongoDbService : IDataService
    {
        private const string ConnectionString = "";
        private IMongoDatabase? _database;
        private IMongoCollection<EnvironmentData> _collection;

        public MongoDbService()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nikod:mongodbpassword@cluster0.qfirq.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            _database = client.GetDatabase("MeasurementsDB");
            _collection = _database.GetCollection<EnvironmentData>("Measurements");
        }
        public async Task PostAsync(EnvironmentData data)
        {
            await _collection.InsertOneAsync(data);
        }
    }
}

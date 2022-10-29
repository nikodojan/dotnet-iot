using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PiTempSensorApp.Reader.Models;

namespace PiTempSensorApp.Reader.Services
{
    internal class MongoDbService : IDataService
    {
        private IMongoDatabase? _database;
        private IMongoCollection<EnvironmentData> _collection;

        public MongoDbService(IConfiguration config)
        {
            var settings = MongoClientSettings.FromConnectionString(config.GetConnectionString("MongoDb"));
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            _database = client.GetDatabase("MeasurementsDB");
            _collection = _database.GetCollection<EnvironmentData>("Measurements");
        }

        public async Task SendAsync(EnvironmentData data)
        {
            await _collection.InsertOneAsync(data);
        }
    }
}

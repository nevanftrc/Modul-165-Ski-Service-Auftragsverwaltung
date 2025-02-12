using MongoDB.Driver;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Data
{
    public class DatabaseContext
    {
        private readonly IMongoDatabase _database;

        public DatabaseContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("SkiServiceDb");
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<ServiceOrder> ServiceOrders => _database.GetCollection<ServiceOrder>("ServiceOrders");
    }
}

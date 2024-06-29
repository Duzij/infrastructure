using MongoDB.Driver;

namespace Infrastructure.MongoDB
{
    public class MongoDbContext : IMongoDbContext
    {
        public MongoDbContext(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);

            Database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoDatabase Database { get; }

        public IMongoCollection<T> GetCollection<T>()
        {
            return Database.GetCollection<T>(MongoUtils.GetCollectionName<T>());
        }
    }
}

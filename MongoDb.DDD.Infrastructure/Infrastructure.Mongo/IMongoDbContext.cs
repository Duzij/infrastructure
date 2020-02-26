using MongoDB.Driver;

namespace Infrastructure.MongoDb
{
    public interface IMongoDbContext
    {
        IMongoDatabase Database { get; }

        IMongoCollection<T> GetCollection<T>();

    }
}
using MongoDB.Driver;

namespace Infrastructure.MongoDB
{
    public interface IMongoDbContext
    {
        IMongoDatabase Database { get; }

        IMongoCollection<T> GetCollection<T>();

    }
}
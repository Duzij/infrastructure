using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MongoDB
{
    public class MongoDbContext : IMongoDbContext
    {
        public MongoDbContext(IMongoDbSettings settings)
        {
            var client = new MongoClient(new MongoClientSettings()
            {
                Server = new MongoServerAddress(settings.ServerAddress),
                WaitQueueTimeout = TimeSpan.FromSeconds(1),
                MaxConnectionPoolSize = 1000,
            });

            Database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoDatabase Database { get; }

        public IMongoCollection<T> GetCollection<T>() => Database.GetCollection<T>(MongoUtils.GetCollectionName<T>());

    }
}

using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MongoDb
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDbSettings settings;

        public MongoDbContext(IMongoDbSettings settings)
        {
            this.settings = settings;
            var client = new MongoClient(settings.ConnectionString);
            Database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoDatabase Database { get; }

        public IMongoCollection<T> GetCollection<T>() => Database.GetCollection<T>(MongoUtils.GetCollectionName<T>());
      
    }
}

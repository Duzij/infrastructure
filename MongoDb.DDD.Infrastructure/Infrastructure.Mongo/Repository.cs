using Infrastructure.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
{
    public class Repository<T> : IRepository<T, string> where T : Entity
    {
        private readonly IMongoCollection<T> collection;
        private readonly EventWriter eventWriter;
        private readonly IMongoDbContext dbContext;

        public Repository(EventWriter eventWriter, IMongoDbContext dbContext)
        {
            this.eventWriter = eventWriter;
            this.dbContext = dbContext;
            collection = dbContext.Database.GetCollection<T>(typeof(T).FullName);
           
        }

        public async Task<List<T>> GetAsync()
        {
            var document = await collection.FindAsync(book => true);
            return document.ToList();
        }

        public async Task<T> GetById(string id)
        {
            var entity = await collection.FindAsync(entity => entity.Id.Equals(id));
            return await entity.FirstOrDefaultAsync();
        }

        public async Task<bool> Exists(string id)
        {
            var entity = await collection.FindAsync(entity => entity.Id.Equals(id));
            return entity != null;
        }

        public async Task<T> Create(T entity)
        {
            try
            {
                await collection.InsertOneAsync(entity);
                SaveEntityEvents(entity.GetEvents(), entity.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return entity;
        }

        private void SaveEntityEvents(IList<object> events, string entityId)
        {
            foreach (var @event in events)
            {
                var mongoEvent = new Event(@event.GetType(), @event, entityId);
                eventWriter.Write(mongoEvent);
            }
        }

        public async Task Update(string id, T entity) =>
           await collection.ReplaceOneAsync(entity => entity.Id.Equals(id), entity);

        public async Task Remove(T entity) =>
           await collection.DeleteOneAsync(book => book.Id.Equals(entity.Id));

        public async Task Remove(string id) =>
           await collection.DeleteOneAsync(book => book.Id.Equals(id));


    }
}

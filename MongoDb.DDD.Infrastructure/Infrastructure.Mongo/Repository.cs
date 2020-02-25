using Infrastructure.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
{
    public class Repository<T, TKey> : IRepository<T, string> where T : IEntity<string>
    {
        private readonly IMongoCollection<T> collection;
        private readonly EventWriter eventWriter;

        public Repository(EventWriter eventWriter, IMongoDbContext dbContext)
        {
            this.eventWriter = eventWriter;
            collection = dbContext.Database.GetCollection<T>(typeof(T).FullName);
        }

        public async Task<List<T>> GetAsync()
        {
            var document = await collection.FindAsync(entity => true);
            return document.ToList();
        }

        public async Task<List<T>> GetAsync(FilterDefinition<T> filter)
        {
            var document = await collection.FindAsync(filter);
            return document.ToList();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var entity = await collection.FindAsync(entity => entity.Id.Equals(id));
            return entity != null;
        }

        private void SaveEntityEvents(IList<object> events, string entityId)
        {
            foreach (var @event in events)
            {
                var mongoEvent = new Event(Guid.NewGuid().ToString(), @event.GetType(), @event, entityId.ToString());
                eventWriter.Write(mongoEvent);
            }
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await collection.InsertOneAsync(entity);
                SaveEntityEvents(entity.GetEvents(), entity.Id.Value.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return entity;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id.Value", id);
            var entity = await collection.FindAsync(filter);
            return await entity.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id.Value", id);
            await collection.FindOneAndDeleteAsync(filter);
        }

        public async Task GetAndModify(string id, Func<T,T> modifyFunc)
        {
            var filter = Builders<T>.Filter.Eq("_id.Value", id);
            var entity = await GetByIdAsync(id);
            entity = modifyFunc(entity);
            //atomic Mongo update
            SaveEntityEvents(entity.GetEvents(), id);
            await collection.FindOneAndReplaceAsync(filter,entity);
        }
    }
}

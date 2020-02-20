using Infrastructure.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
{
    public class Repository<T> : IRepository<T, string> where T : IEntity<string>
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
            var document = await collection.FindAsync(book => true);
            return document.ToList();
        }

        public async Task<bool> Exists(string id)
        {
            var entity = await collection.FindAsync(entity => entity.Id.Equals(id));
            return entity != null;
        }

        private void SaveEntityEvents(IList<object> events, string entityId)
        {
            foreach (var @event in events)
            {
                var mongoEvent = new Event(@event.GetType(), @event, entityId);
                eventWriter.Write(mongoEvent);
            }
        }

        public async Task<T> CreateAsync(T entity)
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

        public async Task<T> GetByIdAsync(string id)
        {
            var entity = await collection.FindAsync(entity => entity.Id.Equals(id));
            return await entity.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(string id)
        {
            await collection.DeleteOneAsync(book => book.Id.Equals(id));
        }

        public async Task GetAndModify(string id, Func<T,T> modifyFunc)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var entity = await GetByIdAsync(id);
            entity = modifyFunc(entity);
            //atomic Mongo update
            await collection.FindOneAndReplaceAsync(filter,entity);
        }
    }
}

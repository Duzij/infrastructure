using Infrastructure.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
{
    public class Repository<T> : IRepository<T> where T : MongoEntity
    {
        private readonly IMongoCollection<T> collection;
        private readonly IEventWriter<MongoEvent> eventWriter;

        public Repository(IMongoDbSettings settings, IEventWriter<MongoEvent> eventWriter)
        {
            collection = new MongoClient().GetDatabase(settings.ConnectionString).GetCollection<T>(nameof(T));
            this.eventWriter = eventWriter;
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
            SaveEntityEvents(entity.GetEvents());
            await collection.InsertOneAsync(entity);
            return entity;
        }

        private void SaveEntityEvents(List<IEvent> events)
        {
            foreach (var @event in events)
            {
                eventWriter.Write((MongoEvent)@event);
            }
        }

        public async Task Update(string id, T entity) =>
           await collection.ReplaceOneAsync(entity => entity.Id.Equals(id), entity);

        public async Task Remove(T entity) =>
           await collection.DeleteOneAsync(book => book.Id == entity.Id);

        public async Task Remove(string id) =>
           await collection.DeleteOneAsync(book => book.Id.Equals(id));


    }
}

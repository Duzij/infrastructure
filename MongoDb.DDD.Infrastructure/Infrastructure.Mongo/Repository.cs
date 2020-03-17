using Infrastructure.Core;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
{
    public class Repository<T, TKey> : IRepository<T, string> where T : IEntity<string>
    {
        private readonly IMongoCollection<T> collection;
        private readonly IMongoDbContext dbContext;
        private readonly IMongoDbSettings mongoDbSettings;
        private readonly ILogger<Repository<T, TKey>> logger;

        public Repository(IMongoDbContext dbContext, IMongoDbSettings mongoDbSettings, ILogger<Repository<T, TKey>> logger)
        {
            this.dbContext = dbContext;
            this.mongoDbSettings = mongoDbSettings;
            this.logger = logger;
            collection = dbContext.Database.GetCollection<T>(MongoUtils.GetCollectionName<T>());
        }

        private async Task<Type> GetFirstFromCollectionAsync<Type>(IMongoCollection<Type> entityCollection, FilterDefinition<Type> filter)
        {
            var entity = await entityCollection.FindAsync(filter);
            return entity.FirstOrDefault();
        }


        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(MongoDefaultSettings.IdName, id);
            return await GetFirstFromCollectionAsync(collection, filter);
        }

        public async Task RemoveAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(MongoDefaultSettings.IdName, id);
            await collection.FindOneAndDeleteAsync(filter);
        }

        public async Task InsertNewAsync(T entity)
        {
            using (var session = dbContext.Database.Client.StartSession())
            {
                try
                {
                    await session.WithTransaction(
                    async (s, ct) =>
                    {
                        entity.Etag = Guid.NewGuid().ToString();
                        await collection.InsertOneAsync(entity);

                        foreach (var @event in entity.GetEvents())
                        {
                            var mongoEvent = new Event(Guid.NewGuid().ToString(), @event.GetType(), @event, entity.Id.Value);
                            var eventsCollection = session.Client.GetDatabase(mongoDbSettings.DatabaseName).GetCollection<Event>(MongoDefaultSettings.EventsDocumentName);
                            await eventsCollection.InsertOneAsync(mongoEvent);
                        }
                    });
                }
                catch (Exception exception)
                {
                    logger.LogError($"{Messages.NonTransientExceptionCaught} ${exception.Message}.", exception);
                }

            }
        }

        public async Task ReplaceAsync(T entity)
        {
            using (var session = dbContext.Database.Client.StartSession())
            {
                try
                {
                  await session.WithTransaction(
                      async (s, ct) =>
                      {
                          var filter = Builders<T>.Filter.Eq(MongoDefaultSettings.IdName, entity.Id.Value);
                          await collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = false });

                          foreach (var @event in entity.GetEvents())
                          {
                              var mongoEvent = new Event(Guid.NewGuid().ToString(), @event.GetType(), @event, entity.Id.Value);
                              var eventsCollection = session.Client.GetDatabase(mongoDbSettings.DatabaseName).GetCollection<Event>(MongoDefaultSettings.EventsDocumentName);
                              await eventsCollection.InsertOneAsync(mongoEvent);
                          }
                      });
                }
                catch (Exception exception)
                {
                    logger.LogError($"{Messages.NonTransientExceptionCaught} ${exception.Message}.", exception);
                }
            }
        }

        public async Task ModifyAsync(Action<T> modifyAction, string id)
        {
            using (var session = dbContext.Database.Client.StartSession())
            {
                try
                {
                    await session.WithTransaction(
                     async (s, ct) =>
                     {
                         var entityCollection = session.Client.GetDatabase(mongoDbSettings.DatabaseName).GetCollection<T>(MongoUtils.GetCollectionName<T>());
                         var eventsCollection = session.Client.GetDatabase(mongoDbSettings.DatabaseName).GetCollection<Event>(MongoDefaultSettings.EventsDocumentName);

                         ReplaceOneResult result;
                         T foundEntity;

                         do
                         {
                             var filter = Builders<T>.Filter.Eq(MongoDefaultSettings.IdName, id);
                             foundEntity = await GetFirstFromCollectionAsync(entityCollection, filter);
                             var version = foundEntity.Etag;
                             foundEntity.Etag = Guid.NewGuid().ToString();

                             modifyAction(foundEntity);
                             filter = filter & Builders<T>.Filter.Eq(MongoDefaultSettings.EtagName, version);

                             result = await entityCollection.ReplaceOneAsync(filter, foundEntity, new ReplaceOptions { IsUpsert = false });

                         } while (result.ModifiedCount != 1);

                         //events are saved after sucessfull transformation
                         if (result.ModifiedCount == 1)
                         {
                             foreach (var @event in foundEntity.GetEvents())
                             {
                                 var mongoEvent = new Event(Guid.NewGuid().ToString(), @event.GetType(), @event, foundEntity.Id.Value);
                                 eventsCollection.InsertOne(mongoEvent);
                             }
                         }
                     });
                }
                catch (Exception exception)
                {
                    logger.LogError($"{Messages.NonTransientExceptionCaught} ${exception.Message}.", exception);
                }
            }
        }
    }
}

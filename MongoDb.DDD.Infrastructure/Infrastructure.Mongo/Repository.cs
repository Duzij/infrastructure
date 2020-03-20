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
    public class Repository<T, TKey> : IRepository<T, string> where T : IDomainAggreagate<string>
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
            var domainAggregate = await entityCollection.FindAsync(filter);
            var d = domainAggregate.FirstOrDefault();
            if (d == null)
            {
                throw new EntityNotFoundException(typeof(Type));
            }
            return d;
        }

        public async Task InsertNewAsync(T domainAggregate)
        {
            using (var session = dbContext.Database.Client.StartSession())
            {
                try
                {
                    await session.WithTransaction(
                    async (s, ct) =>
                    {
                        domainAggregate.Etag = Guid.NewGuid().ToString();
                        await collection.InsertOneAsync(domainAggregate);

                        foreach (var @event in domainAggregate.GetEvents())
                        {
                            var mongoEvent = new Event(Guid.NewGuid().ToString(), @event.GetType(), @event, domainAggregate.Id.Value);
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

        public async Task ReplaceAsync(T domainAggregate)
        {
            using (var session = dbContext.Database.Client.StartSession())
            {
                try
                {
                  await session.WithTransaction(
                      async (s, ct) =>
                      {
                          var filter = Builders<T>.Filter.Eq(MongoDefaultSettings.IdName, domainAggregate.Id.Value);
                          await collection.ReplaceOneAsync(filter, domainAggregate, new ReplaceOptions { IsUpsert = false });

                          foreach (var @event in domainAggregate.GetEvents())
                          {
                              var mongoEvent = new Event(Guid.NewGuid().ToString(), @event.GetType(), @event, domainAggregate.Id.Value);
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

        public async Task ModifyAsync(Action<T> modifyLogic, IId<string> id)
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
                         T foundDomainAggregate;

                         do
                         {
                             var filter = Builders<T>.Filter.Eq(MongoDefaultSettings.IdName, id.Value);
                             foundDomainAggregate = await GetFirstFromCollectionAsync(entityCollection, filter);
                             var version = foundDomainAggregate.Etag;
                             foundDomainAggregate.Etag = Guid.NewGuid().ToString();

                             modifyLogic(foundDomainAggregate);
                             filter = filter & Builders<T>.Filter.Eq(MongoDefaultSettings.EtagName, version);

                             result = await entityCollection.ReplaceOneAsync(filter, foundDomainAggregate, new ReplaceOptions { IsUpsert = false });

                         } while (result.ModifiedCount != 1);

                         //events are saved after sucessfull transformation
                         if (result.ModifiedCount == 1)
                         {
                             foreach (var @event in foundDomainAggregate.GetEvents())
                             {
                                 var mongoEvent = new Event(Guid.NewGuid().ToString(), @event.GetType(), @event, foundDomainAggregate.Id.Value);
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

        public async Task<T> GetByIdAsync(IId<string> id)
        {
            var filter = Builders<T>.Filter.Eq(MongoDefaultSettings.IdName, id.Value);
            return await GetFirstFromCollectionAsync(collection, filter);
        }

        public async Task RemoveAsync(IId<string> id)
        {
            var filter = Builders<T>.Filter.Eq(MongoDefaultSettings.IdName, id.Value);
            await collection.FindOneAndDeleteAsync(filter);
        }
    }
}

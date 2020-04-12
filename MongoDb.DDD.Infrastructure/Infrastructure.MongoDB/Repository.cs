﻿using Infrastructure.Core;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.MongoDB
{
    public class Repository<T, TKey> : IRepository<T, string> where T : IAggregate<string>
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
            var aggregate = await entityCollection.FindAsync(filter);
            var d = aggregate.FirstOrDefault();
            if (d == null)
            {
                throw new EntityNotFoundException(typeof(Type));
            }
            return d;
        }

        public async Task InsertNewAsync(T aggregate)
        {
            using (var session = dbContext.Database.Client.StartSession())
            {
                try
                {
                    await session.WithTransaction(
                    async (s, ct) =>
                    {
                        aggregate.RegenerateEtag();
                        await collection.InsertOneAsync(aggregate);

                        foreach (var @event in aggregate.GetEvents())
                        {
                            var mongoEvent = new EventWrapper(Guid.NewGuid().ToString(), @event.GetType(), @event, aggregate.Id.Value);
                            var eventsCollection = session.Client.GetDatabase(mongoDbSettings.DatabaseName).GetCollection<EventWrapper>(MongoDefaultSettings.EventsDocumentName);
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

        public async Task ReplaceAsync(T aggregate)
        {
            using (var session = dbContext.Database.Client.StartSession())
            {
                try
                {
                  await session.WithTransaction(
                      async (s, ct) =>
                      {
                          var filter = Builders<T>.Filter.Eq(MongoDefaultSettings.IdName, aggregate.Id.Value);
                          await collection.ReplaceOneAsync(filter, aggregate, new ReplaceOptions { IsUpsert = false });

                          foreach (var @event in aggregate.GetEvents())
                          {
                              var mongoEvent = new EventWrapper(Guid.NewGuid().ToString(), @event.GetType(), @event, aggregate.Id.Value);
                              var eventsCollection = session.Client.GetDatabase(mongoDbSettings.DatabaseName).GetCollection<EventWrapper>(MongoDefaultSettings.EventsDocumentName);
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
                         var eventsCollection = session.Client.GetDatabase(mongoDbSettings.DatabaseName).GetCollection<EventWrapper>(MongoDefaultSettings.EventsDocumentName);

                         ReplaceOneResult result;
                         T foundAggregate;

                         do
                         {
                             var filter = Builders<T>.Filter.Eq(MongoDefaultSettings.IdName, id.Value);
                             foundAggregate = await GetFirstFromCollectionAsync(entityCollection, filter);
                             var version = foundAggregate.Etag;
                             foundAggregate.RegenerateEtag();

                             modifyLogic(foundAggregate);
                             filter = filter & Builders<T>.Filter.Eq(MongoDefaultSettings.EtagName, version);

                             result = await entityCollection.ReplaceOneAsync(filter, foundAggregate, new ReplaceOptions { IsUpsert = false });

                         } while (result.ModifiedCount != 1);

                         //events are saved after sucessfull transformation
                         if (result.ModifiedCount == 1)
                         {
                             foreach (var @event in foundAggregate.GetEvents())
                             {
                                 var mongoEvent = new EventWrapper(Guid.NewGuid().ToString(), @event.GetType(), @event, foundAggregate.Id.Value);
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
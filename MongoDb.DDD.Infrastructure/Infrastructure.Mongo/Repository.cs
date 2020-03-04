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

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id.Value", id);
            return await GetByTypedIdAsync(collection, id, filter);
        }

        public async Task RemoveAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id.Value", id);
            await collection.FindOneAndDeleteAsync(filter);
        }

        public async Task InsertNewAsync(T entity)
        {
            entity.Etag = Guid.NewGuid().ToString();
            await collection.InsertOneAsync(entity);
        }

        public async Task ModifyAsync(Action<T> modifyLogic, string id)
        {
            using (var session = dbContext.Database.Client.StartSession())
            {
                try
                {
                    await RunTransactionWithRetry(id, modifyLogic, session, false);
                }
                catch (Exception exception)
                {
                    logger.LogError($"{Messages.NonTransientExceptionCaught} ${exception.Message}.", exception);
                }
            }
        }

        public async Task RunTransactionWithRetry(string id, Action<T> modifyLogic, IClientSessionHandle session, bool optimisticConcurrencyCheck)
        {

            var entityCollection = session.Client.GetDatabase(mongoDbSettings.DatabaseName).GetCollection<T>(MongoUtils.GetCollectionName<T>());
            var eventsCollection = session.Client.GetDatabase(mongoDbSettings.DatabaseName).GetCollection<Event>(MongoDefaultSettings.EventsDocumentName);

            var filter = Builders<T>.Filter.Eq("_id.Value", id);
            var foundEntity = await GetByTypedIdAsync(entityCollection, id, filter);

            if (!optimisticConcurrencyCheck)
            {
                modifyLogic(foundEntity);
                foundEntity.Etag = Guid.NewGuid().ToString();
                await entityCollection.ReplaceOneAsync(
                      c => c.Id.Value == id, foundEntity,
                      new ReplaceOptions { IsUpsert = false });
            }
            else
            {
                try
                {
                    ReplaceOneResult result;

                    do
                    {
                        var IdFilter = Builders<T>.Filter.Eq("_id.Value", id);
                        foundEntity = await GetByTypedIdAsync(entityCollection, id, IdFilter);

                        var version = foundEntity.Etag;
                        foundEntity.Etag = Guid.NewGuid().ToString();

                        modifyLogic(foundEntity);
                        filter = Builders<T>.Filter.Eq("_id.Value", id) & Builders<T>.Filter.Eq("Etag", version);

                        foreach (var @event in foundEntity.GetEvents())
                        {
                            var mongoEvent = new Event(Guid.NewGuid().ToString(), @event.GetType(), @event, foundEntity.Id.Value.ToString());
                            eventsCollection.InsertOne(mongoEvent);
                        }

                        result = await entityCollection.ReplaceOneAsync(
                            filter, foundEntity,
                            new ReplaceOptions { IsUpsert = false });
                        logger.LogDebug($"Etag is: {version}");
                        logger.LogDebug($"Result ModifiedCount: {result.ModifiedCount}");
                        logger.LogDebug($"Result MatchedCount: {result.MatchedCount}");

                    } while (result.ModifiedCount != 1);
                }
                catch (Exception ex)
                {
                    logger.LogError("Error", ex);
                    throw;
                }
            }



        }

        private async Task<Type> GetByTypedIdAsync<Type>(IMongoCollection<Type> entityCollection, string id, FilterDefinition<Type> filter)
        {
            var entity = await entityCollection.FindAsync(filter);
            return entity.FirstOrDefault();
        }

        private void CommitWithRetry(IClientSessionHandle session)
        {
            while (true)
            {
                try
                {
                    session.CommitTransaction();
                    logger.LogWarning(Messages.TransactionCommitted);
                    break;
                }
                catch (MongoException exception)
                {
                    // can retry commit
                    if (exception.HasErrorLabel(MongoDefaultSettings.UnknownTransactionCommitResult))
                    {
                        logger.LogWarning(Messages.UnknownTransactionCommitResult);
                        continue;
                    }
                    else
                    {
                        logger.LogError($"{Messages.CommitError}: {exception.Message}.", exception);
                        throw;
                    }
                }
            }
        }

        public async Task ModifyWithOptimisticConcurrencyAsync(Action<T> modifyLogic, string id)
        {
            using (var session = dbContext.Database.Client.StartSession())
            {
                try
                {
                    await RunTransactionWithRetry(id, modifyLogic, session, true);
                }
                catch (Exception exception)
                {
                    logger.LogError($"{Messages.NonTransientExceptionCaught} ${exception.Message}.", exception);
                }
            }
        }
    }
}

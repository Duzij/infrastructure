using Infrastructure.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
{
    public class Repository<T, TKey> : IRepository<T, string> where T : IEntity<string>
    {
        private readonly IMongoCollection<T> collection;
        private readonly EventWriter eventWriter;
        private readonly IMongoDbContext dbContext;
        private readonly IMongoDbSettings mongoDbSettings;

        public Repository(EventWriter eventWriter, IMongoDbContext dbContext, IMongoDbSettings mongoDbSettings)
        {
            this.eventWriter = eventWriter;
            this.dbContext = dbContext;
            this.mongoDbSettings = mongoDbSettings;
            collection = dbContext.Database.GetCollection<T>(MongoUtils.GetCollectionName<T>());
        }

        private void SaveEntityEvents(IList<object> events, string entityId)
        {
           
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

        public async Task InsertNewAsync(T entity)
        {
            await collection.InsertOneAsync(entity);
        }

        public async Task ModifyAsync(Action<T> modifyLogic, string id)
        {
            using (var session = dbContext.Database.Client.StartSession())
            {
                try
                {
                    RunTransactionWithRetry(id, modifyLogic, session);
                }
                catch (Exception exception)
                {
                    // do something with error
                    Console.WriteLine($"Non transient exception caught during transaction: ${exception.Message}.");
                }
            }
        }

        public void RunTransactionWithRetry(string id, Action<T> modifyLogic, IClientSessionHandle session)
        {
            while (true)
            {
                try
                {
                    session.StartTransaction(new TransactionOptions(
                        readConcern: ReadConcern.Snapshot,
                        writeConcern: WriteConcern.WMajority,
                        readPreference: ReadPreference.Primary));

                    try
                    {
                        var entityCollection = session.Client.GetDatabase(mongoDbSettings.DatabaseName).GetCollection<T>(MongoUtils.GetCollectionName<T>());
                        var eventsCollection = session.Client.GetDatabase(mongoDbSettings.DatabaseName).GetCollection<Event>(MongoDefaultSettings.EventsDocumentName);

                        var filter = Builders<T>.Filter.Eq("_id.Value", id);
                        var foundEntity = entityCollection.FindAsync(filter).GetAwaiter().GetResult().FirstOrDefault();
                        modifyLogic(foundEntity);
                        entityCollection.FindOneAndReplace(filter, foundEntity);

                        foreach (var @event in foundEntity.GetEvents())
                        {
                            var mongoEvent = new Event(Guid.NewGuid().ToString(), @event.GetType(), @event, foundEntity.Id.Value.ToString());
                            eventsCollection.InsertOne(mongoEvent);
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"Caught exception during transaction, aborting: {exception.Message}.");
                        session.AbortTransaction();
                        throw;
                    }

                    CommitWithRetry(session);

                    break;
                }
                catch (MongoException exception)
                {
                    // if transient error, retry the whole transaction
                    if (exception.HasErrorLabel("TransientTransactionError"))
                    {
                        Console.WriteLine("TransientTransactionError, retrying transaction.");
                        continue;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }


        private void CommitWithRetry(IClientSessionHandle session)
        {
            while (true)
            {
                try
                {
                    session.CommitTransaction();
                    Console.WriteLine("Transaction committed.");
                    break;
                }
                catch (MongoException exception)
                {
                    // can retry commit
                    if (exception.HasErrorLabel("UnknownTransactionCommitResult"))
                    {
                        Console.WriteLine("UnknownTransactionCommitResult, retrying commit operation");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Error during commit: {exception.Message}.");
                        throw;
                    }
                }
            }
        }


    }
}

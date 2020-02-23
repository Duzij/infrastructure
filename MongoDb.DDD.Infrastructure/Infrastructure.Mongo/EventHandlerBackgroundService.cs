using Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
{
    public class EventHandlerBackgroundService : BackgroundService
    {
        private readonly IMongoDbContext dbContext;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger<EventHandlerBackgroundService> logger;

        public EventHandlerBackgroundService(IMongoDbContext dbContext, IServiceScopeFactory serviceScopeFactory, ILogger<EventHandlerBackgroundService> logger)
        {
            this.dbContext = dbContext;
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var collection = dbContext.Database.GetCollection<Event>(MongoDefaultSettings.EventsDocument);
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Event>>()
           .Match(x => x.OperationType == ChangeStreamOperationType.Insert);
            using (var cursor = await collection.WatchAsync(pipeline))
            {
                await cursor.ForEachAsync(change =>
                {
                    logger.LogError($"{change.CollectionNamespace.FullName} changed. Processing change.");
                    ProcessEvent(change);
                });
            }
        }

        private void ProcessEvent(ChangeStreamDocument<Event> change)
        {
            var @event = change.FullDocument;

            using (var factory = serviceScopeFactory.CreateScope())
            {
                var eventType = Type.GetType(@event.EvenType);
                Type handlerGenericType = typeof(IEventHandler<>).MakeGenericType(eventType);
                var service = factory.ServiceProvider.GetRequiredService(handlerGenericType);
                var methodInfo = service.GetType().GetMethod("Handle");

                var parameters = new object[1]{@event.EventValue};
                methodInfo.Invoke(service, parameters);
            }
           
        }
    }
}

using Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.MongoDB
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
            var collection = dbContext.Database.GetCollection<EventWrapper>(MongoDefaultSettings.EventsDocumentName);
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<EventWrapper>>()
           .Match(x => x.OperationType == ChangeStreamOperationType.Insert);
            using var cursor = await collection.WatchAsync(pipeline);
            await cursor.ForEachAsync(change =>
            {
                logger.LogInformation($"{change.CollectionNamespace.FullName} changed. Processing change.");
                try
                {
                    ProcessEvent(change);
                }
                catch (System.Exception ex)
                {
                    logger.LogError(ex.Message, ex);
                }
            });
        }

        private void ProcessEvent(ChangeStreamDocument<EventWrapper> change)
        {
            var @event = change.FullDocument;

            using var factory = serviceScopeFactory.CreateScope();
            Type handlerGenericType = typeof(IEventHandler<>).MakeGenericType(Type.GetType(@event.EventType));
            var service = factory.ServiceProvider.GetRequiredService(handlerGenericType);
            HandleEvent(service, @event);

        }

        private void HandleEvent(object service, EventWrapper @event)
        {
            var methodInfo = service.GetType().GetMethod("Handle");
            var eventParameter = new object[1] { @event.EventValue };
            methodInfo.Invoke(service, eventParameter);
        }
    }
}

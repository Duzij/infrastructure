using Infrastructure.Core;
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
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<EventHandlerBackgroundService> logger;

        public EventHandlerBackgroundService(IMongoDbContext dbContext, IServiceProvider serviceProvider, ILogger<EventHandlerBackgroundService> logger)
        {
            this.dbContext = dbContext;
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var collection = dbContext.Database.GetCollection<Event>(MongoDefaultSettings.EventsDocument);

            using (var cursor = await collection.WatchAsync())
            {
                await cursor.ForEachAsync(change =>
                {
                    logger.LogError($"{change.CollectionNamespace.FullName} changed. Processing change.");
                    ProcessEvent(change);
                });
            }
        }

        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                Assembly assembly = Assembly.Load(a.FullName);
                foreach (Type assemblyType in assembly.GetTypes())
                {
                    if (assemblyType.Name == typeName)
                        return assemblyType;
                }
            }
            return null;
        }

        private void ProcessEvent(ChangeStreamDocument<Event> change)
        {
            var @event = change.FullDocument;

            var eventHandlerType = @event.EvenType + "EventHandler";
            var service = serviceProvider.GetService(GetType(eventHandlerType)) as IEventHandler<Event>;
            if (service == null)
            {
                logger.LogError(new EventHandlerNotFoundException(@event.EvenType), "EventHandlerNotFoundException occured");
            }
            else
            {
                service.Handle(@event);
            }
        }
    }
}

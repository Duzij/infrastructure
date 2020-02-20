using Infrastructure.Core;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
{
    public class EventHandlerBackgroundService : BackgroundService
    {
        private readonly IMongoDbContext dbContext;
        private readonly IServiceProvider serviceProvider;

        public EventHandlerBackgroundService(IMongoDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.dbContext = dbContext;
            this.serviceProvider = serviceProvider;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var collection = dbContext.Database.GetCollection<Event>("__events");
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Event>>()
            .Match(x => x.OperationType == ChangeStreamOperationType.Insert);
            using (var cursor = collection.Watch(pipeline))
            {
                foreach (var change in cursor.ToEnumerable())
                {
                   ProcessEvent(change);
                }
            }

            return Task.CompletedTask;
        }

        private void ProcessEvent(ChangeStreamDocument<Event> change)
        {
            var @event = change.FullDocument;

            var eventHandlerType = @event.EvenType.Name + "EventHandler";
            var service = serviceProvider.GetService(Type.GetType(eventHandlerType)) as IEventHandler<Event>;
            if (service == null)
            {
                throw new EventHandlerNotFoundException(@event.EvenType);
            }
            else
            {
                service.Handle(@event);
            }
        }
    }
}

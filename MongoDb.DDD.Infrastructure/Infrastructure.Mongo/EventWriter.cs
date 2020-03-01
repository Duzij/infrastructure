using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core;
using MongoDB.Driver;

namespace Infrastructure.MongoDb
{
    public class EventWriter : IEventWriter<Event, string>
    {
        private IMongoCollection<Event> collection;

        public EventWriter(IMongoDbContext dbContext)
        {
            collection = dbContext.Database.GetCollection<Event>(MongoDefaultSettings.EventsDocumentName);
        }
        public void Write(Event @event)
        {
            @event.CreatedTime = DateTime.UtcNow;
            collection.InsertOne(@event);
        }
    }
}

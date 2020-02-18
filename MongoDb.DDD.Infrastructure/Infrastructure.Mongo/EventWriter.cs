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

        public EventWriter(IMongoDbSettings dbSettings)
        {
            collection = new MongoClient().GetDatabase(dbSettings.ConnectionString).GetCollection<Event>("events");
        }
        public void Write(Event @event)
        {
            collection.InsertOne(@event);
        }
    }
}

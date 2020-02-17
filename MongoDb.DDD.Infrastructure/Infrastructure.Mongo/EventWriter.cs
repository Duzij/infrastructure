using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core;
using MongoDB.Driver;

namespace Infrastructure.MongoDb
{
    public class EventWriter : IEventWriter<MongoEvent>
    {
        private IMongoCollection<MongoEvent> collection;

        public EventWriter(IMongoDbSettings dbSettings)
        {
            collection = new MongoClient().GetDatabase(dbSettings.ConnectionString).GetCollection<MongoEvent>("events");
        }
        public void Write(MongoEvent @event)
        {
            collection.InsertOne(@event);
        }
    }
}

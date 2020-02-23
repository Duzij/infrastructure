using System;
using System.Runtime.Serialization;
using Infrastructure.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.MongoDb
{
    public class Event : IEvent<string>
    {
        [BsonElement]
        public string EvenType { get; set; }

        [BsonElement]
        public object EventValue { get; set; }

        [BsonId]
        public string EntityId { get; set; }

        public Event(Type evenType, object @event, string entityId)
        {
            EvenType = evenType.AssemblyQualifiedName;
            EventValue = @event;
            EntityId = entityId;
        }

    }
}
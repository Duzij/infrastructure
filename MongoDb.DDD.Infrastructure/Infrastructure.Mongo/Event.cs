using System;
using System.Runtime.Serialization;
using Infrastructure.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.MongoDb
{
    public class Event : IEvent<string>
    {
        [BsonId]
        public string Id { get; protected set; }
        public string EntityId { get; set; }

        public string EventType { get; set; }

        public object EventValue { get; set; }

        public DateTime CreatedTime { get; set; }

        public Event(string id, Type evenType, object @event, string entityId)
        {
            Id = id;
            EventType = evenType.AssemblyQualifiedName;
            EventValue = @event;
            EntityId = entityId;
        }

    }
}
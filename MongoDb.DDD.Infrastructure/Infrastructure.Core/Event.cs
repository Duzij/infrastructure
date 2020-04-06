using System;
using System.Runtime.Serialization;
using Infrastructure.Core;

namespace Infrastructure.Core
{
    public class Event : IEvent<string>
    {
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
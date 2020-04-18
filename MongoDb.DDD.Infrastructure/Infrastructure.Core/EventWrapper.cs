using System;
using System.Runtime.Serialization;
using Infrastructure.Core;

namespace Infrastructure.Core
{
    public class EventWrapper 
    {
        public string Id { get; private set; }
        public string EntityId { get; private set; }

        public string EventType { get; private set; }

        public object EventValue { get; private set; }

        public DateTime CreatedTime { get; private set; }

        public EventWrapper(string eventId, Type eventType, object @event, string entityId)
        {
            Id = eventId;
            EventType = eventType.AssemblyQualifiedName;
            EventValue = @event;
            EntityId = entityId;
            CreatedTime = DateTime.UtcNow;
        }

    }
}
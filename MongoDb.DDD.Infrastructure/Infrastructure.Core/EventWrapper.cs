using System;
using System.Runtime.Serialization;
using Infrastructure.Core;

namespace Infrastructure.Core
{
    public class EventWrapper : IEventWrapper<string>
    {
        public string Id { get; protected set; }
        public string EntityId { get; protected set; }

        public string EventType { get; set; }

        public object EventValue { get; set; }

        public DateTime CreatedTime { get; set; }

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
using System;
using System.Runtime.Serialization;
using Infrastructure.Core;

namespace Infrastructure.Core
{
    public class EventWrapper : IEventWrapper<string>
    {
        public string Id { get; protected set; }
        public string EntityId { get; protected set; }

        public string EventType { get; protected set; }

        public object EventValue { get; protected set; }

        public DateTime CreatedTime { get; protected set; }

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
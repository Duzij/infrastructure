using System;
using Infrastructure.Core;

namespace Infrastructure.MongoDb
{
    public class Event : IEvent<string>
    {
        public Type EvenType { get; }
        public object EventValue { get; }
        public string EntityId { get; }

        public Event(Type evenType, object @event, string entityId)
        {
            EvenType = evenType;
            EventValue = @event;
            EntityId = entityId;
        }

    }
}
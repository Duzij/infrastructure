using System.Runtime.Serialization;

namespace Infrastructure.MongoDB.Exception
{
    [Serializable]
    internal class EventHandlerNotFoundException : System.Exception
    {
        private readonly Type eventType;

        public EventHandlerNotFoundException(Type eventType)
        {
            this.eventType = eventType;
        }
        public override string Message => $"Event handler for event type {eventType} was not found.";

        public EventHandlerNotFoundException(string message) : base(message)
        {
        }

        public EventHandlerNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected EventHandlerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using Infrastructure.Core;

namespace Infrastructure.MongoDb
{
    public interface IEventWriter<TEvent> where TEvent : class, IEvent
    {
        void Write(TEvent @event);
    }
}
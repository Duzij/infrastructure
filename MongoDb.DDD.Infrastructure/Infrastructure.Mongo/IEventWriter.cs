using Infrastructure.Core;

namespace Infrastructure.MongoDb
{
    public interface IEventWriter<TEvent, TEntityKey> where TEvent : class, IEvent<TEntityKey>
    {
        void Write(TEvent @event);
    }
}
using Infrastructure.Core;

namespace Infrastructure.Core
{
    public interface IEventWriter<TEvent, TEntityKey> where TEvent : class, IEvent<TEntityKey>
    {
        void Write(TEvent @event);
    }
}
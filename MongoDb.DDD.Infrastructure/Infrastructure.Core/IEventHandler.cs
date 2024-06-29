namespace Infrastructure.Core
{
    public interface IEventHandler<TEvent>
    {
        Task Handle(TEvent @event);
    }
}

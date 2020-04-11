using System;

namespace Infrastructure.Core
{
    public interface IEventWrapper<TKey>
    {
        TKey EntityId { get; }
        TKey Id { get; }
        string EventType { get; }
        object EventValue { get; }
    }
}
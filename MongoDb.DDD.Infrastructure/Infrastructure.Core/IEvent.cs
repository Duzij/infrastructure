using System;

namespace Infrastructure.Core
{
    public interface IEvent<TKey>
    {
        public TKey EntityId { get; }
    }
}
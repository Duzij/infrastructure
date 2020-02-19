using System;

namespace Infrastructure.Core
{
    public interface IEvent<TKey>
    {
        //Id of an entity this event happened on
        public TKey EntityId { get; }
    }
}
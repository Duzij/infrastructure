using System;

namespace Infrastructure.Core
{
    public interface IEvent
    {
        //Id of an entity this event happened on
        public Guid EntityId { get; set; }
    }
}
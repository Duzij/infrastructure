using System;
using Infrastructure.Core;

namespace Infrastructure.MongoDb
{
    public class MongoEvent : IEvent
    {
        public Guid EntityId { get; set; }
    }
}
using System;
using Infrastructure.Core;

namespace Infrastructure.MongoDb
{
    public class Event : IEvent<string>
    {
        public string EntityId { get; set; }
    }
}
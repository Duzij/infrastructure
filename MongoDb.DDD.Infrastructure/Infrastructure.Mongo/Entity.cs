using Infrastructure.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.MongoDb
{
    public abstract class Entity : IEntity<string>
    {
        [BsonId]
        public string Id { get; set; }
        private IList<object> Events { get; }

        public IList<object> GetEvents() => Events.ToList<object>();

        protected Entity()
        {
            Events = new List<object>();
        }

        //for complex state cheking between properties
        public abstract bool CheckState();

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected void AddEvent(object @event)
        {
            CheckState();
            Events.Add(@event);
        }

        void IEntity<string>.AddEvent(object @event)
        {
            AddEvent(@event);
        }
    }
}

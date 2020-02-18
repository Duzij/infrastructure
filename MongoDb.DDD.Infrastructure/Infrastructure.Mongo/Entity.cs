using Infrastructure.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MongoDb
{
    public abstract class Entity : IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        private List<IEvent<string>> Events { get; }

        public List<IEvent<string>> GetEvents() => Events;

        public Entity()
        {
            Events = new List<IEvent<string>>();
        }

        protected void AddEvent(IEvent<string> @event)
        {
            CheckState();
            Events.Add(@event);
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
    }
}

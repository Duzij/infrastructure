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
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        private IList<object> Events { get; }

        public IList<object> GetEvents() => Events.ToList<object>();

        protected Entity()
        {
            Events = new List<object>();
        }

        protected void AddEvent(object @event)
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

        bool IEntity<string>.Equals(object other)
        {
            throw new NotImplementedException();
        }

        int IEntity<string>.GetHashCode()
        {
            throw new NotImplementedException();
        }

        bool IEntity<string>.CheckState()
        {
            throw new NotImplementedException();
        }

        void IEntity<string>.AddEvent(object @event)
        {
            throw new NotImplementedException();
        }

        IList<object> IEntity<string>.GetEvents()
        {
            throw new NotImplementedException();
        }
    }
}

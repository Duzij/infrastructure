using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Core
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        private IList<object> Events { get; set; }
        public IId<TKey> Id { get; set; }
        public string Etag { get; set; }

        public IList<object> GetEvents(){
            if (Events == null)
            {
                return new List<object>();
            }
            return Events;
        }

        public Entity()
        {
            Events = new List<object>();
        }

        //for complex state cheking between properties
        public abstract void CheckState();

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

        public void AddEvent(object @event)
        {
            CheckState();
            if (Events == null)
            {
                Events = new List<object>();
            }
            Events.Add(@event);
        }
    }
}

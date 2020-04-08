using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core
{
    /// <summary>
    /// High level object
    /// </summary>
    public abstract class Aggregate<TKey> : Entity<TKey>, IAggregate<TKey>
    {
        public IList<object> GetEvents()
        {
            if (Events == null)
            {
                return new List<object>();
            }
            return Events;
        }

        public Aggregate()
        {
            Events = new List<object>();
        }

        public string Etag { get; set; }

        private IList<object> Events { get; set; }

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

        protected void AddEvent(object @event)
        {
            CheckState();
            if (Events == null)
            {
                Events = new List<object>();
            }
            Events.Add(@event);
        }

        void IAggregate<TKey>.AddEvent(object @event)
        {
            AddEvent(@event);
        }
    }
}

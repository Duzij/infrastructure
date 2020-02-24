using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Domain
{
    public abstract class Entity : IEntity<string>
    {
        private IList<object> Events { get; }
        public IId<string> Id { get; set; }

        public IList<object> GetEvents() => Events.ToList<object>();

        protected Entity()
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
            Events.Add(@event);
        }
    }
}

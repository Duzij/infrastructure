using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core
{
    public abstract class Entity : IEntity
    {

        public List<IEvent> Events { get; }

        public Entity()
        {
            Events = new List<IEvent>();
        }

        protected void AddEvent(IEvent @event)
        {
            Apply(@event);
            CheckState();
            Events.Add(@event);
        }

        //change of the state
        public abstract void Apply(object @event);  

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

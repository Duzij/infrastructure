namespace Infrastructure.Core
{
    public abstract class Aggregate<TKey> : Entity<TKey>, IAggregate<TKey>
    {
        public Aggregate()
        {
            Events = [];
        }

        public string Etag { get; protected set; }

        public void RegenerateEtag()
        {
            Etag = Guid.NewGuid().ToString();
        }

        private List<object> Events { get; set; }

        public abstract void CheckState();

        public override bool Equals(object other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected void AddEvent(object @event)
        {
            Events ??= [];
            Events.Add(@event);
        }

        public IEnumerable<object> GetEvents()
        {
            if (Events == null)
            {
                return [];
            }
            return Events;
        }
    }
}

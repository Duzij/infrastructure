using System.Collections.Generic;

namespace Infrastructure.Core
{
    public interface IAggregate<TKey> : IEntity<TKey>
    {
        void CheckState();
        public string Etag { get; set; }
        bool Equals(object other);
        int GetHashCode();
        void AddEvent(object @event);
        IList<object> GetEvents();
    }
}
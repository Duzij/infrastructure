using System.Collections.Generic;

namespace Infrastructure.Core
{
    public interface IDomainAggreagate<TKey> : IEntity<TKey>
    {
        public string Etag { get; set; }
        bool Equals(object other);
        int GetHashCode();
        void CheckState();
        void AddEvent(object @event);
        IList<object> GetEvents();
    }
}
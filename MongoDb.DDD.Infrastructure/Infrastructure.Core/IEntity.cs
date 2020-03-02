using System.Collections.Generic;

namespace Infrastructure.Core
{
    public interface IEntity<TKey> 
    {
        public IId<TKey> Id { get; set; }
        public string Etag { get; set; }
        bool Equals(object other);
        int GetHashCode();
        void CheckState();
        void AddEvent(object @event);
        IList<object> GetEvents();
    }

    public interface IId<TKey>
    {
        public TKey Value { get; set; }
    }
}
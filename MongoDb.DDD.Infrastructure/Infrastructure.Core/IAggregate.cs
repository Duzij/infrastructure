using System.Collections.Generic;

namespace Infrastructure.Core
{
    public interface IAggregate<TKey> : IEntity<TKey>
    {
        void CheckState();
        public string Etag { get; }
        void RegenerateEtag();
        bool Equals(object other);
        int GetHashCode();
        public IEnumerable<object> GetEvents();
    }
}
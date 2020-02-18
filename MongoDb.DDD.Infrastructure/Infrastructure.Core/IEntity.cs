using System.Collections.Generic;

namespace Infrastructure.Core
{
    public interface IEntity<TKey> 
    {
        public TKey Id { get; set; }
        bool Equals(object other);
        int GetHashCode();
        bool CheckState();
        List<IEvent<string>> GetEvents();
    }
}
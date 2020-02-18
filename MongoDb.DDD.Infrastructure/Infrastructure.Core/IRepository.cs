using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public interface IRepository<T, TKey> where T : IEntity<TKey>
    {
        Task<T> Create(T entity);
        Task<bool> Exists(TKey id);
        Task<List<T>> GetAsync();
        Task<T> GetById(TKey id);
        Task Remove(TKey id);
        Task Remove(T entity);
        Task Update(TKey id, T entity);
    }
}
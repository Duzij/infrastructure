using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public interface IRepository<T, TKey> where T : IEntity<TKey>
    {
        Task<T> CreateAsync(T entity);
        Task<bool> Exists(TKey id);
        Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(TKey id);
        Task RemoveAsync(TKey id);
        Task GetAndModify(TKey id, Func<T, T> modifyFunc);
    }
}
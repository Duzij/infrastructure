using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public interface IRepository<T, TKey> where T : IEntity<TKey>
    {
        Task SaveAsync(T entity);
        Task<T> GetByIdAsync(TKey id);
        Task RemoveAsync(TKey id);
        Task Update(TKey id, Func<T, T> modifyFunc);
    }
}
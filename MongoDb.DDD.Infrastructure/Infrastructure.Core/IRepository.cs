using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public interface IRepository<T, TKey> where T : IEntity<TKey>
    {
        Task InsertNewAsync(T entity);
        Task ModifyAsync(Action<T> modifyLogic, TKey id);
        Task<T> GetByIdAsync(TKey id);
        Task RemoveAsync(TKey id);
    }
}
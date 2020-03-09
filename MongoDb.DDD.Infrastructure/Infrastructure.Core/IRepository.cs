using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public interface IRepository<T, TKey> where T : IEntity<TKey>
    {
        /// <summary>
        /// New entity is inserted to collection named after entity type
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertNewAsync(T entity);

        /// <summary>
        /// Entity is simply replaced - last wins
        /// </summary>
        /// <param name="modifyLogic"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task ReplaceAsync(Action<T> modifyLogic, TKey id);

        /// <summary>
        /// Entity is modified with optimistic concurrency 
        /// </summary>
        /// <param name="modifyLogic"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task ModifyAsync(Action<T> modifyLogic, TKey id);

        /// <summary>
        /// Return entity by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(TKey id);

        /// <summary>
        /// Entity is removed by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveAsync(TKey id);
    }
}
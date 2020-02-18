using Infrastructure.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
{
    public interface IRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
    {
        Task<TEntity> Create(TEntity entity);
        Task<bool> Exists(string id);
        Task<List<TEntity>> GetAsync();
        Task<TEntity> GetById(string id);
        Task Remove(string id);
        Task Remove(TEntity entity);
        Task Update(string id, TEntity entity);
    }
}
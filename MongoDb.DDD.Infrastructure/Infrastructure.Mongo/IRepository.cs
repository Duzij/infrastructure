using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
{
    public interface IRepository<T> where T : MongoEntity
    {
        Task<T> Create(T entity);
        Task<bool> Exists(string id);
        Task<List<T>> GetAsync();
        Task<T> GetById(string id);
        Task Remove(string id);
        Task Remove(T entity);
        Task Update(string id, T entity);
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public interface IRepository
    {
        
        Task<T> Load<T>(string entityId) where T : Entity; 
        Task Save<T>(T entity) where T : Entity; 
        Task<bool> Exists<T>(string entityId);

    }
}

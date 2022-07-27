using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Add(T item);
        Task<T> GetById(string id); 
        Task<int> Update(T entity);
        Task<bool> Remove(string id);
        Task<IEnumerable<T>> GetAllReadOnly();
        Task<IEnumerable<Object>> GetForDropDown();
        Task<bool> UpdateStatus(string id, Object status);
    }
    
}

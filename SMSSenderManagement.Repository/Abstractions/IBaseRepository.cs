using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<bool> GetAsync(Guid id, string number);
        IQueryable<T> Table { get; }
    }
}

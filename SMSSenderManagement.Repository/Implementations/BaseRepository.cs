using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext ?_context;
        protected readonly DbSet<T> _dbSet;
        public IQueryable<T> Table
        {
            get { return _dbSet; }
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<bool> GetAsync(Guid Id, string Number)
        {
            var result = await _dbSet.FindAsync(Id);
            if(result != null ) return true;
            return false;
        }
    }
}

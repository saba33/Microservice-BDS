using Microsoft.EntityFrameworkCore;
using SMSSenderManagement.Domain;
using SMSSenderManagement.Domain.Requests;
using SMSSenderManagement.Repository.SmsManagementContect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SMSSenderManagement.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        

        public BaseRepository(SMSManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
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

        public async Task<bool> GetAsyncBySessionid(T request)
        {
            var result = await _dbSet.ToListAsync();
            
            if (result != null) return true;
            return false;
        }
        
        public async Task<bool> GetAsync(Guid Id, string Number)
        {
            var result = await _context.FindAsync(typeof(EntityState),Id);
            if(result != null ) return true;
            return false;
        }
    }
}

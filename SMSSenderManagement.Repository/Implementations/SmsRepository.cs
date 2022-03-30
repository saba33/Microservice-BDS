using Microsoft.EntityFrameworkCore;
using SMSSenderManagement.Domain;
using SMSSenderManagement.Domain.Requests;
using SMSSenderManagement.Repository.Abstractions;
using SMSSenderManagement.Repository.SmsManagementContect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Repository.Implementations
{
    public class SmsRepository : ISmsRepository
    {
        //public IBaseRepository<SmsSentHistoryWithotp> _repository;
        protected readonly DbContext _context;
        protected readonly DbSet<SmsSentHistoryWithotp> _dbSet;
        protected readonly DbSet<MessagesInfo> _dbSet1;


        public SmsRepository(SMSManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<SmsSentHistoryWithotp>();
            _dbSet1 = _context.Set<MessagesInfo>();
        }

        public IQueryable<MessagesInfo> MessagesTable => _dbSet1;

        IQueryable<SmsSentHistoryWithotp> ISmsRepository.Table => _dbSet;

        //public SmsRepository(IBaseRepository<SmsSentHistoryWithotp> repository)
        //{
        //    _repository = repository;
        //}

        public async Task AddAsync(SmsSentHistoryWithotp sender)
        {

            await _dbSet.AddAsync(sender);

            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(MessagesInfo sender)
        {

            await _dbSet1.AddAsync(sender);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id, string number)
        {
            return await _dbSet.AnyAsync(x => x.Id == id && x.PhoneNumber == number);
        }

        public async Task<List<SmsSentHistoryWithotp>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<bool> GetAsync(Guid id, string otp)
        {
            return await _dbSet.AnyAsync(x => x.Id == id && x.Otp == otp);
        }

        public async Task<bool> GetAsyncWithSessionId(ValidateOtpRequest request)
        {
            return await _dbSet.AnyAsync(x=> x.SmsProvider == request.SessionId && x.Otp == request.Otp);
        }
    }
}

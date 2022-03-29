using SMSSenderManagement.Domain;
using SMSSenderManagement.Domain.Requests;
using SMSSenderManagement.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Repository.Implementations
{
    public class SmsRepository : ISmsRepository
    {
        public IBaseRepository<SmsSentHistoryWithotp> _repository;

        public SmsRepository(IBaseRepository<SmsSentHistoryWithotp> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(SmsSentHistoryWithotp sender)
        {
            await _repository.AddAsync(sender);
        }

        public async Task<bool> Exists(Guid id, string number)
        {
            var result = await _repository.GetAsync(id, number);
            return result;
        }

        public async Task<List<SmsSentHistoryWithotp>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<bool> GetAsync(Guid id, string otp)
        {
            return await _repository.GetAsync(id, otp);
        }
    }
}

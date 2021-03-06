using SMSSenderManagement.Domain;
using SMSSenderManagement.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Repository.Abstractions
{
    public interface ISmsRepository
    {
        Task<bool> Exists(Guid id, string otp);
        Task AddAsync(SmsSentHistoryWithotp request);
        Task AddAsync(MessagesInfo request);
        Task<List<SmsSentHistoryWithotp>> GetAllAsync();
        Task<bool> GetAsync(Guid id, string otp);
        Task<bool> GetAsyncWithSessionId(ValidateOtpRequest request);
        IQueryable<SmsSentHistoryWithotp> Table { get; }
        IQueryable<MessagesInfo> MessagesTable { get; }
        
    }
}

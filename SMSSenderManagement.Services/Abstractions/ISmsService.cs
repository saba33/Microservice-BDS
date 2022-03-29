using SMSSenderManagement.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Services.Abstractions
{
    public interface ISmsService
    {
        Task<bool> SendSms(SentSmsRequest sentRequest);
        Task<string> SendOtp(SendOtpRequest otpSentRequestModel);
    }
}

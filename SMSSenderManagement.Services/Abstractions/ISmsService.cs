using SMSSenderManagement.Domain;
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
        Task<bool> SendSms(SmsSentRequest sentRequest);
        Task<string> SendOtp(SendOtpRequest otpSentRequestModel);
        Task<bool> ValidateOtp(ValidateOtpRequest request);
    }
}

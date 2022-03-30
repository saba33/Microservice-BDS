using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Domain.Requests
{
    public class ValidateOtpRequest
    {
        public string SessionId { get; set; }

        public string Otp { get; set; }
    }
}

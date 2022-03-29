using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Domain
{
    public class SmsSentRequest
    {
        public string[] PhoneNumbers { get; set; }

        public string SmsText { get; set; }
    }
}

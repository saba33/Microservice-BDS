using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Domain.Requests
{
    public class SendOtpRequest
    {
        public string[] receivers { get; set; }
        //public string sender { get; set; }

        public string text { get; set; }

    }
}

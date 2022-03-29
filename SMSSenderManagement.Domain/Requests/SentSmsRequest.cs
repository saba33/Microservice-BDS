using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Domain.Requests
{
    public class SentSmsRequest
    {
      
        public string[] receivers { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Domain
{
    public class ApiTokenInfo
    {
        public string sender { get; set; }

        public string Authorization { get; set; }
        public string Url { get; set; }
    }
}

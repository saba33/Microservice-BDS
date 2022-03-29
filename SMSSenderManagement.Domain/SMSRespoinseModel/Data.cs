using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Domain.SMSResoinseModel
{
    public class Data
    {
        public List<string> ?messages { get; set; }
        public int totalPrice { get; set; }
        public List<string> ?rejected { get; set; }
    }
}

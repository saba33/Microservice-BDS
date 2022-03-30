using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Domain
{
    public class MessagesInfo
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
        public DateTime CreateOn { get; set; }
    }
}

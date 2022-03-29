using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Domain
{
    //db - Id - guid2), PhoneNumber, Text, 1)Otp, CreatedOn = datetime.now, ValidateOn = datetime.now, smsprovider id
    public class SmsSentHistoryWithotp
    {
        public Guid Id { get; set; }
        public string Otp { get; set; }
        public DateTime CreatedOn { get; set; }
        public string SmsProvider { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ValidateOn { get; set; }
        public string Text { get; set; }

    }
}

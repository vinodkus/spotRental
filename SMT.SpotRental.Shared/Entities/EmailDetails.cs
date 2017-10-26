using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Shared.Entities
{
    public class EmailDetails
    {

        public int RecID { get; set; }
        public string EmailType { get; set; }
        public string UserEmailID { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string UserMobile { get; set; }
        public string UserName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string SMSBody { get; set; }
        public int IsSend { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime SendEmailOn { get; set; }
        public string TripLink { get; set; }
        public string EmailTemplateCode { get; set; }
        public string ActionLink { get; set; }
        public string QueryNo { get; set; }

    }
}

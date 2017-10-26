using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Shared.Entities
{
    public class AdhocRequestFor
    {
        public string UserName { get; set; }
        public string EmployeeID { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string PaymentMode { get; set; }
        public string CreditCard { get; set; }
        public string Remarks { get; set; }
        public string HomeAddress { get; set; }
        public string CostCenter { get; set; }
        public string RequestBy { get; set; }
        public string IsOfficialTrip { get; set; }
    }
}

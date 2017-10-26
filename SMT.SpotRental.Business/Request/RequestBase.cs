using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.Request
{
    public class RequestBase
    {
        public string UserID { get; set; }
        public string EmployeeID { get; set; }
        public string UserType { get; set; }
        public string TokenNo { get; set; }
    }
}

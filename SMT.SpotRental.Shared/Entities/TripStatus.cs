using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Shared.Entities
{
   public class TripStatus
    {
        public string StatusID { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public string ActionName { get; set; }
        public string DisplayFor { get; set; }
        public string GroupName { get; set; }
        public string ReasonID { get; set; }
        public string ReasonRemarks { get; set; }
        public string RequestID { get; set; }
        public string RequestIDs { get; set; }
        public string UserID { get; set; }
        public string VendorId { get; set; }
        public string DriverID { get; set; }
        public string VehicleID { get; set; }
        public string GuardID { get; set; }
        public string ActionBy { get; set; }
        public string ActionById { get; set; }
    }
}

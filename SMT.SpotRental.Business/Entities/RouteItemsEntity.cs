using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.Entities
{
    public class RouteItemsEntity
    {
        public string RouteType { get; set; }
        public string CarType { get; set; }
        public string RentalCity { get; set; }
        public string ReportingDate { get; set; }
        public string ReportingTime { get; set; }
        public DateTime PickUpTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int EstimatedCost { get; set; }
        public string LandMarkSource { get; set; }
        public string LandMarkDestination { get; set; }
        public string RequestId { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string DriverId { get; set; }
        public string DriverName { get; set; }
        public string VehicleID { get; set; }
        public string VehicleType { get; set; }
        public string RequestOn { get; set; }
        public string Time { get; set; }
        public string Gender { get; set; }
        public string ESG { get; set; }
        public string StatusID { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeID { get; set; }
        public string Remarks { get; set; }
        public string RequestBy { get; set; }
        public string RequestByName { get; set; }
        public string RequestHistoryId { get; set; }
        public string ReasonID { get; set; }
        public string ReasonName { get; set; }
        public string ReasonRemarks { get; set; }
        public string ActionBy { get; set; }
        public string CreatedDate { get; set; }
        public string IsOfficialTrip { get; set; }

    }
}

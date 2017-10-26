using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Shared.Entities
{
    public class VehicleMaster
    {
        public string VehicleID { get; set; }
        public string VehicleType { get; set; }
        public string VehicleTypeID { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string VehicleNo { get; set; }
        public int Capacity { get; set; }
        public string AssociatedDriverID { get; set; }
        public string DriverName { get; set; }
        public string Active { get; set; }
        public string RegistrationNo { get; set; }
        public string BillingPlanId { get; set; }
        public string BillingPlanName { get; set; }
        public string LocCode { get; set; }
        public string LocationName { get; set; }
        public int KMLimit { get; set; }
        public string IsGPSInstalled { get; set; }
        public DateTime GPSInstallDate { get; set; }
        public string GPSInstalledBy { get; set; }
        public string SubVendor { get; set; }
        public int RC_Year { get; set; }
        public DateTime INS_Expiry_Date { get; set; }
        public DateTime ManufacturerDate { get; set; }
        public DateTime Fit_Crt_Expiry_Date { get; set; }
        public DateTime Permit_Expiry_Date { get; set; }
        public DateTime PUC_Expiry_Date { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModOn { get; set; }
        public string CreatedBy { get; set; }
        public string ODO_Reading { get; set; }
        public string Remarks { get; set; }
        public string Own_Attached { get; set; }

        //--------For Pending trip-----------//
        public string Source { get; set; }
        public string Destination { get; set; }
        public string PickTime { get; set; }
    }
}

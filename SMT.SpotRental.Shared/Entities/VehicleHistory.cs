using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Shared.Entities
{
    public class VehicleHistory
    {
        public string VehicleHistoryID { get; set; }
        public string VehicleID { get; set; }
        public string LocationName { get; set; }
        public string VendorName { get; set; }
        public string VehicleNo { get; set; }
        public string RegistrationNo { get; set; }
        public string VehicleType { get; set; }
        public string BillingName { get; set; }
        public string Active { get; set; }
        public int KmLimit { get; set; }
        public DateTime ManufactureDate { get; set; }
        public int RC_Year { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime GPSInstallDate { get; set; }
        public string IsGPSInstalled { get; set; }
        public string GPSInstalledBy { get; set; }
        public DateTime ModOn { get; set; }
        public string SubVendor { get; set; }
        public DateTime INS_Expiry_Date { get; set; }
        public DateTime Fit_Crt_Expiry_Date { get; set; }
        public DateTime Permit_Expiry_Date { get; set; }
        public DateTime PUC_Expiry_Date { get; set; }
        public string ODO_Reading { get; set; }
        public string Own_Attached { get; set; }
        public DateTime HistoryDate { get; set; }
    }
}

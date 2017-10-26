using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Shared.Entities
{
    public class VendorMaster
    {
        public IList<VehicleMaster> listVehicle { get; set; } // Not in use
        public string VendorId { get; set; }
        public string UserId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string ShortName { get; set; }
        public string PrefixTag { get; set; }
        public string IsActive { get; set; }
        public string CompanyName { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZIPCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string EmailID { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string CreatedBy { get; set; }
    }
}

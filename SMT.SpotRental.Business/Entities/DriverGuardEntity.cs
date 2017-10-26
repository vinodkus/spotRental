using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.Entities
{
    public class DriverGuardEntity
    {
        public string DriverGuardId { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string LocCode { get; set; }
        public string LocationName { get; set; }
        public string EmpCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmpType { get; set; } // Whether driver or Guard
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string MobileNo { get; set; }
        public string FatherName { get; set; }
        public DateTime DOB { get; set; }
        public string Active { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonAddress { get; set; }
        public string LicenceNo { get; set; }
        public DateTime LicenceExpiryDate { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime DOL { get; set; }
        public DateTime ModOn { get; set; }
        public string ModBy { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string SkinColor { get; set; }
        public string MaritalStatus { get; set; }
        public string PoliceVerification { get; set; }
        public string Education { get; set; }
        public string FingerPrint { get; set; }
        public string LeftFingerPrint { get; set; }
        public string RightFingerPrint { get; set; }
        public string BadgeNo { get; set; }
        public DateTime BadgeExpiryDate { get; set; }
        public string AddVerification { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

    }
}

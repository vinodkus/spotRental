using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.Entities
{
    public class UserEntity
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public DateTime DOB { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string CostCenter { get; set; }
        public string EmployeeCode { get; set; }
        public string DesignationName { get; set; }
        public string DesignationID { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorID { get; set; }
        public string VendorID { get; set; }
        public string OfficeLocation { get; set; }
        public string Roles { get; set; }
        public string RoleNames { get; set; }
        public string RoleIds { get; set; }
        public bool TempPwd { get; set; }
        public string Gender { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string HomeAddress { get; set; }
        public string CreditCard { get; set; }
        public string ProfilePic { get; set; }
        public string PaymentMode { get; set; }
    }
}

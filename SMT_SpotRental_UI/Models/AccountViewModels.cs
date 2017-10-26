using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMT.SpotRental.UI.Models
{


    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }


    }

    public class UserViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FName { get; set; }
        public string MName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Employee ID/code is required")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Mobile no. is required")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailID { get; set; }
        public string UserType { get; set; }
        public string CostCenter { get; set; }
        public string DesignationName { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string DesignationID { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorID { get; set; }
        public string VendorID { get; set; }
        public string OfficeLocation { get; set; }
        public string Roles { get; set; }
        public string RoleIds { get; set; }
        public bool TempPwd { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string HomeAddress { get; set; }
        public string CreditCard { get; set; }
        public  int UserID { get; set; }
        public string ProfilePic { get; set; }
    }




}

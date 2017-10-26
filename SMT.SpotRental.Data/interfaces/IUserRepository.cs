using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Data.interfaces
{
   public  interface IUserRepository
    {
        string[] ManageEmployee(User objUser);
        User ValidateUser(string UserName_Email, string UserPassword);
        IList<Menu> GetNavigationDetails(string LoginCred);
        IList<User> SearchEmployees(string EmployeeCode, string FName, string LName, string MobileNo,string EmailID);
        string ForgetPassword(string EmailID);
        string ChangePassword(string EmailID, string Password);
        string UpdateProfilePicture(string UserID, string ProfilePic);
        IList<Roles> GetAllRoles();
        string ManageRoles(Roles request);
        IList<User> GetUserList();
    }
}

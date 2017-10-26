using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Database.interfaces
{
    public interface IUser
    {
        User ValidateUser(string UserCred, string UserPassword);
        string[] ManageEmployee(User objUser);
        IList<Menu> GetNavigationDetails(string LoginCred);
        IList<User> SearchEmployees(string EmployeeCode, string FName, string LName, string MobileNo, string EmailID);
        string ForgetPassword(string EmailID);
        string ChangePassword(string EmailID, string Password);
        string UpdateProfilePicture(string UserID, string ProfilePic);
        IList<Roles> GetAllRoles();
        string ManageRoles(Roles request);
        IList<User> GetUserList();
    }
}

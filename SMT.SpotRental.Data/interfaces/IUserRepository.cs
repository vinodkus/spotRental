using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Data.interfaces
{
    public interface IUserRepository
    {
        string[] ManageEmployee(User objUser);
        User ValidateUser(string UserName_Email, string UserPassword);
        IList<Menu> GetNavigationDetails(string LoginCred, string RoleID = "0", string QueryNo = "1");
        IList<User> SearchEmployees(string EmployeeCode, string FName, string LName, string MobileNo, string EmailID);
        string ForgetPassword(string EmailID);
        string ChangePassword(string EmailID, string Password);
        string UpdateProfilePicture(string UserID, string ProfilePic);
        IList<Roles> GetAllRoles();
        string ManageRoles(Roles request);
        IList<User> GetUserList();
        string RegisterPortalUser(User req);
        string UpdatePortalUser(User req);
        string MapActionRole(Menu req);
        IList<Menu> GetMenuList(string LoginCred);
        IList<Menu> GetParentMenu(string LoginCred);
        string ManageMenus(Menu request);

        IList<Location> GetLocationList(string LoginCred);
        string ManageLocation(Location request);
    }
}

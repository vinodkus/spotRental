using SMT.SpotRental.Data.interfaces;
using SMT.SpotRental.Database;
using SMT.SpotRental.Database.interfaces;
using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Data
{
    public class UserRepository : IUserRepository
    {
        IUser users = null;

        public UserRepository()
        {
            users = new ManageUser();
        }
        public string[] ManageEmployee(User objUser)
        {
            return users.ManageEmployee(objUser);
        }
        public User ValidateUser(string UserName_Email, string UserPassword)
        {
            return users.ValidateUser(UserName_Email, UserPassword);
        }

        public IList<Menu> GetNavigationDetails(string LoginCred, string RoleID = "0", string QueryNo = "1")
        {
            return users.GetNavigationDetails(LoginCred, RoleID,QueryNo);
        }

        public IList<User> SearchEmployees(string EmployeeCode, string FName, string LName, string MobileNo,string EmailID)
        {
            return users.SearchEmployees(EmployeeCode, FName, LName, MobileNo, EmailID);
        }

        public string ForgetPassword(string EmailID)
        {
            return users.ForgetPassword(EmailID);
        }
        public string ChangePassword(string EmailID, string Password)
        {
            return users.ChangePassword(EmailID, Password);
        }

        public string UpdateProfilePicture(string UserID, string ProfilePic)
        {
            return users.UpdateProfilePicture(UserID, ProfilePic);
        }
        public IList<Roles> GetAllRoles()
        {
            return users.GetAllRoles();
        }

        public string ManageRoles(Roles request)
        {
            return users.ManageRoles(request);
        }
        public IList<User> GetUserList()
        {
            return users.GetUserList();
        }

        public string RegisterPortalUser(User req)
        {
            return users.RegisterPortalUser(req);
        }

        public string UpdatePortalUser(User req)
        {
            return users.UpdatePortalUser(req);
        }
        public string MapActionRole(Menu req)
        {
            return users.MapActionRole(req);
        }
        public IList<Menu> GetMenuList(string LoginCred)
        {
            return users.GetMenuList(LoginCred);
        }
        public IList<Menu> GetParentMenu(string LoginCred)
        {
            return users.GetParentMenu(LoginCred);
        }
        public string ManageMenus(Menu request)
        {
            return users.ManageMenus(request);
        }

        public IList<Location> GetLocationList(string LoginCred)
        {
            return users.GetLocationList(LoginCred);
        }
        public string ManageLocation(Location request)
        {
            return users.ManageLocation(request);
        }

    }
}

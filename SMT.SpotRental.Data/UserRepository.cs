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

        public IList<Menu> GetNavigationDetails(string LoginCred)
        {
            return users.GetNavigationDetails(LoginCred);
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
    }
}

using SMT.SpotRental.Business.Entities;
using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.EntityMapper
{
    public class UserMapper
    {
        public static User MapUser(UserEntity u)
        {
            return new User()
            {
                CostCenter = u.CostCenter,
                CreditCard = u.CreditCard,
                DesignationName = u.DesignationName,
                DesignationID = u.DesignationID,
                DOB = u.DOB,
                EmailID = u.EmailID,
                EmployeeCode = u.EmployeeCode,
                FName = u.FName,
                Gender = u.Gender,
                HomeAddress = u.HomeAddress,
                Latitude = u.Latitude,
                LName = u.LName,
                Longitude = u.Longitude,
                MName = u.MName,
                MobileNo = u.MobileNo,
                OfficeLocation = u.OfficeLocation,
                Roles = u.Roles,
                SupervisorID = u.SupervisorID,
                SupervisorName = u.SupervisorName,
                TempPwd = u.TempPwd,
                UserName = u.FName + " " + u.LName,
                UserType = u.UserType,
                VendorID = u.VendorID,
                UserID = u.UserID,
                ProfilePic=u.ProfilePic,
                PaymentMode=u.PaymentMode,
                RoleIds=u.RoleIds,
                RoleNames=u.RoleNames
            };
        }
        public static UserEntity MapUserEntity(User u)
        {
            return new UserEntity()
            {
                CostCenter = u.CostCenter,
                CreditCard = u.CreditCard,
                DesignationName = u.DesignationName,
                DesignationID = u.DesignationID,
                DOB = u.DOB,
                EmailID = u.EmailID,
                EmployeeCode = u.EmployeeCode,
                FName = u.FName,
                Gender = u.Gender,
                HomeAddress = u.HomeAddress,
                Latitude = u.Latitude,
                LName = u.LName,
                Longitude = u.Longitude,
                MName = u.MName,
                MobileNo = u.MobileNo,
                OfficeLocation = u.OfficeLocation,
                Roles = u.Roles,
                SupervisorID = u.SupervisorID,
                SupervisorName = u.SupervisorName,
                TempPwd = u.TempPwd,
                UserName = u.FName + " " + u.LName,
                UserType = u.UserType,
                VendorID = u.VendorID,
                UserID = u.UserID,
                PaymentMode=u.PaymentMode,
                ProfilePic=u.ProfilePic,
                RoleIds = u.RoleIds,
                RoleNames = u.RoleNames
            };
        }
        public static IList<UserEntity> MapUserEntityList(IList<User> x)
        {
            return x.AsEnumerable().Select(u => new UserEntity()
            {
                CostCenter = u.CostCenter,
                CreditCard = u.CreditCard,
                DesignationName = u.DesignationName,
                DesignationID = u.DesignationID,
                DOB = u.DOB,
                EmailID = u.EmailID,
                EmployeeCode = u.EmployeeCode,
                FName = u.FName,
                Gender = u.Gender,
                HomeAddress = u.HomeAddress,
                Latitude = u.Latitude,
                LName = u.LName,
                Longitude = u.Longitude,
                MName = u.MName,
                MobileNo = u.MobileNo,
                OfficeLocation = u.OfficeLocation,
                Roles = u.Roles,
                SupervisorID = u.SupervisorID,
                SupervisorName = u.SupervisorName,
                TempPwd = u.TempPwd,
                UserName = u.FName + " " + u.LName,
                UserType = u.UserType,
                VendorID = u.VendorID,
                UserID = u.UserID,
                PaymentMode=u.PaymentMode,
                ProfilePic=u.ProfilePic,
                RoleIds = u.RoleIds,
                RoleNames = u.RoleNames

            }).ToList();
        }
        public static IList<MenuEntity> MapMenuEntityList(IList<Menu> x)
        {
            return x.AsEnumerable().Select(c => new MenuEntity()
            {
                ActionID = c.ActionID,
                ActionName = c.ActionName,
                Active = c.Active,
                Icon = c.Icon,
                IsMenuItems = c.IsMenuItems,
                MenuOrder = c.MenuOrder,
                ActionText = c.ActionText,
                ControllerName = c.ControllerName,
                RootID = c.RootID,
                UserID = c.UserID                
            }).ToList();
        }
        public static Menu MapMenuEntity(MenuEntity c)
        {
            return new Menu() {
                ActionID = c.ActionID,
                ActionName = c.ActionName,
                Active = c.Active,
                Icon = c.Icon,
                IsMenuItems = c.IsMenuItems,
                MenuOrder = c.MenuOrder,
                ActionText = c.ActionText,
                ControllerName = c.ControllerName,
                RootID = c.RootID,
                UserID = c.UserID,
                QueryNo=c.QueryNo                
            };
        }
        public static IList<RolesEntity> MapRoleEntityList(IList<Roles> m)
        {
            return m.AsEnumerable().Select(u => new RolesEntity()
            {
                Active = u.Active,
                QueryNo = u.QueryNo,
                RoleID = u.RoleID,
                RoleName = u.RoleName
            }).ToList();
        }
        public static RolesEntity MapRoleEntity(Roles u)
        {
            return new RolesEntity()
            {
                Active = u.Active,
                QueryNo = u.QueryNo,
                RoleID = u.RoleID,
                RoleName = u.RoleName
            };
        }
        public static Roles MapRoles(RolesEntity u)
        {
            return new Roles()
            {
                Active = u.Active,
                QueryNo = u.QueryNo,
                RoleID = u.RoleID,
                RoleName = u.RoleName
            };
        }
    }
}

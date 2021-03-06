﻿using SMT.SpotRental.Business.Entities;
using SMT.SpotRental.Business.EntityMapper;
using SMT.SpotRental.Data.Factory;
using SMT.SpotRental.Data.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business
{
    public class BLUsers
    {
        IUserRepository userRepository = null;

        public BLUsers()
        {
            userRepository = DataRepositoryFactory.CreateUserRepository();
        }
        public string[] AddEmployee(UserEntity user)
        {
            return userRepository.ManageEmployee(UserMapper.MapUser(user));
        }
        public UserEntity ValidateUser(string LoginCred, string Password)
        {
            return UserMapper.MapUserEntity(userRepository.ValidateUser(LoginCred, Password));
        }
        public IList<MenuEntity> GetNavigationDetails(string LoginCred, string RoleID = "0", string QueryNo="1")
        {
            return UserMapper.MapMenuEntityList(userRepository.GetNavigationDetails(LoginCred,RoleID,QueryNo));
        }
        public IList<UserEntity> SearchEmployees(string EmployeeCode, string FName, string LName, string MobileNo, string EmailID)
        {
            return UserMapper.MapUserEntityList(userRepository.SearchEmployees(EmployeeCode, FName, LName, MobileNo, EmailID));
        }
        public string ForgetPassword(string EmailID)
        {
            return userRepository.ForgetPassword(EmailID);
        }

        public string ChangePassword(string EmailID, string Password)
        {
            return userRepository.ChangePassword(EmailID, Password);
        }
        public string UpdateProfilePicture(string UserID, string ProfilePic)
        {
            return userRepository.UpdateProfilePicture(UserID, ProfilePic);
        }

        public IList<RolesEntity> GetAllRoles()
        {
            return UserMapper.MapRoleEntityList(userRepository.GetAllRoles());
        }
        public string ManageRoles(RolesEntity requst)
        {
            return userRepository.ManageRoles(UserMapper.MapRoles(requst));
        }
        public IList<UserEntity> GetUserList()
        {
            return  UserMapper.MapUserEntityList(userRepository.GetUserList());
        }
        public string RegisterPortalUser(UserEntity req)
        {
            return userRepository.RegisterPortalUser(UserMapper.MapUser(req));
        }
        public string UpdatePortalUser(UserEntity req)
        {
            return userRepository.UpdatePortalUser(UserMapper.MapUser(req));
        }
        public string MapActionRole(MenuEntity req)
        {
            return userRepository.MapActionRole(UserMapper.MapMenuEntity(req));
        }
        public IList<MenuEntity> GetMenuList(string LoginCred)
        {
            return UserMapper.MapMenuEntityList(userRepository.GetMenuList(LoginCred));
        }

        public IList<MenuEntity> GetParentMenu(string LoginCred)
        {
            return UserMapper.MapMenuEntityList(userRepository.GetParentMenu(LoginCred));
        }
        public string ManageMenus(MenuEntity requst)
        {
            return userRepository.ManageMenus(UserMapper.MapMenuEntity(requst));
        }

        public IList<LocationEntity> GetLocationList(string LoginCred)
        {
            return UserMapper.MapLocationEntityList(userRepository.GetLocationList(LoginCred));
        }
        public string ManageLocation(LocationEntity request)
        {
            return userRepository.ManageLocation(UserMapper.MapLocationEntity(request));
        }
    }
}

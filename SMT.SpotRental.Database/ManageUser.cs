using Dapper;
using SMT.SpotRental.Database.interfaces;
using SMT.SpotRental.Shared.Entities;
using System.Collections.Generic;
using System.Data;
using System;

/**********************************************************************************************************************
Description : This class/module is used for managing users, thier roles and rights. This also includes CRUD 
              operation for users.
Date        : 02 AUG 2017
Created By  : Kapil D. Tripathi
Copyright by: iControlBiz Consulting Pvt. Ltd.
************************************************************************************************************************/
namespace SMT.SpotRental.Database
{
    public class ManageUser : ConstantBase, IUser
    {
        /// <summary>
        /// This method add update employee based on user id.
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns>return message of success/failure</returns>
        public string[] ManageEmployee(User objUser)
        {
            string[] strResponse = new string[3];
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objParam = new DynamicParameters();
                objParam.Add("@UserId", objUser.UserID);
                objParam.Add("@EmployeeName", objUser.UserName);
                objParam.Add("@EmployeeCode", objUser.EmployeeCode);
                objParam.Add("@EmailId", objUser.EmailID);
                objParam.Add("@MobileNo", objUser.MobileNo);
                objParam.Add("@Gender", objUser.Gender);
                objParam.Add("@designationid", objUser.DesignationID);
                objParam.Add("@supervisorid", objUser.SupervisorID);
                objParam.Add("@HomeAddress", objUser.HomeAddress);
                objParam.Add("@OfficeLocation", objUser.OfficeLocation);
                objParam.Add("@CreditCard", objUser.CreditCard);
                objParam.Add("@CostCenter", objUser.CostCenter);
                objParam.Add("@Latitude", objUser.Latitude);
                objParam.Add("@Longitude", objUser.Longitude);
                objParam.Add("@pwd", objUser.Password);
                objParam.Add("@Roles", objUser.Roles);
                objParam.Add("@FName", objUser.FName);
                objParam.Add("@MName", objUser.MName);
                objParam.Add("@LName", objUser.LName);

                objParam.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);// send output parameter
                objParam.Add("@TempPasword", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);// send output parameter
                objParam.Add("@template", dbType: DbType.String, direction: ParameterDirection.Output, size: 8000);// send output parameter


                int iRes = dc.ExecuteProc(SR_USP_MANAGEEMPLOYEE, objParam);
                strResponse[0] = objParam.Get<string>("@Result");// Get output parameter value
                strResponse[1] = objParam.Get<string>("@TempPasword");// Get output parameter value
                strResponse[2] = objParam.Get<string>("@template");// Get output parameter value of email template

            }

            return strResponse;
        }

        /// <summary> Validate user on login with their userid/email/employee code and password
        /// </summary>
        /// <param name="UserName_Email">UserID/EmailId/Employee code as string</param>
        /// <param name="UserPassword">login password of user</param>
        /// <returns>return object of User</returns>
        public User ValidateUser(string UserCred, string UserPassword)
        {
            User objUser = new User();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objParam = new DynamicParameters();
                objParam.Add("@UserName", UserCred);
                objParam.Add("@Password", UserPassword);
                objUser = dc.ExecuteProc_Object<User>(SR_USP_LOGINCHECK, objParam);
            }

            return objUser;
        }

        /// <summary>
        /// This method return all menu/navigation details for any valid user based on their roles
        /// </summary>
        /// <param name="LoginCred">user email/login id</param>
        /// <returns>List of menu items</returns>
        public IList<Menu> GetNavigationDetails(string LoginCred)
        {
            IList<Menu> objMenuList = new List<Menu>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objParam = new DynamicParameters();
                objParam.Add("@UserID", LoginCred);
                objMenuList = dc.ExecuteProc<Menu>(SR_USP_GETNEVIGATIONDETAIL, objParam);
            }

            return objMenuList;
        }

        /// <summary> This method search employee based on different criteria        /// </summary>
        /// <param name="EmployeeCode">optional param as string</param>
        /// <param name="FName"></param>
        /// <param name="LName"></param>
        /// <param name="MobileNo"></param>
        /// <returns>List of searched uses</returns>
        public IList<User> SearchEmployees(string EmployeeCode, string FName, string LName, string MobileNo, string EmailID)
        {
            IList<User> listUser = new List<User>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objParam = new DynamicParameters();
                objParam.Add("@EmployeeCode", EmployeeCode);
                objParam.Add("@FName", FName);
                objParam.Add("@LName", LName);
                objParam.Add("@MobileNo", MobileNo);
                objParam.Add("@EmailID", EmailID);
                listUser = dc.ExecuteProc<User>(SR_USP_SEARCHEMPLOYEE, objParam);
            }

            return listUser;
        }

        /// <summary>
        /// This method reset password and send email to user
        /// </summary>
        /// <param name="EmailID">email id as string</param>
        /// <returns>return result as string</returns>
        public string ForgetPassword(string EmailID)
        {

            string Result = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objParam = new DynamicParameters();
                objParam.Add("@EmialOrUserID", EmailID);
                objParam.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);// send output parameter
                int iRes = dc.ExecuteProc(SR_USP_FORGETPWD, objParam);
                Result = objParam.Get<string>("@Result");// Get output parameter value
            }
            return Result;
        }

        /// <summary>
        /// This method change user password from temprory password or whenever user want to change.
        /// </summary>
        /// <param name="EmailID">Email id as string</param>
        /// <param name="Password">Password as string</param>
        /// <returns>return result as string</returns>
        public string ChangePassword(string EmailID, string Password)
        {
            string Result = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objParam = new DynamicParameters();
                objParam.Add("@EmailID", EmailID);
                objParam.Add("@Password", Password);
                objParam.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);// send output parameter
                int iRes = dc.ExecuteProc(SR_USP_CHANGEPASSWORD, objParam);
                Result = objParam.Get<string>("@Result");// Get output parameter value
            }
            return Result;
        }

        /// <summary>
        /// This stored procedure update profile picture
        /// </summary>
        /// <param name="UserID">User Id as a string</param>
        /// <param name="ProfilePic">profile picture as string having JPG, PNG, GIF or JPEG only</param>
        /// <returns>result as string</returns>
        public string UpdateProfilePicture(string UserID, string ProfilePic)
        {
            string Result = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objParam = new DynamicParameters();
                objParam.Add("@UserID", UserID);
                objParam.Add("@ProfilePic", ProfilePic);
                objParam.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);// send output parameter
                int iRes = dc.ExecuteProc(SR_USP_UPDATEPROFILEPIC, objParam);
                Result = objParam.Get<string>("@Result");// Get output parameter value onl
            }
            return Result;
        }

        /// <summary>
        /// This methd return all available roles
        /// </summary>
        /// <returns>List of roles</returns>
        public IList<Roles> GetAllRoles()
        {
            IList<Roles> listRoles = new List<Roles>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@QueryNo", 1);
                listRoles = dc.ExecuteProc<Roles>(SR_USP_MANAGEROLES, objparams);
            }
            return listRoles;
        }

        /// <summary>
        /// This method update/ insert roles detail into table 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>status of operation</returns>
        public string ManageRoles(Roles request)
        {
            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@RoleID", request.RoleID);
                objparams.Add("@RoleName", request.RoleName);
                objparams.Add("@Active", request.Active == "Y" ? true : false);
                objparams.Add("@QueryNo", request.QueryNo);
                objparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                strResult = Convert.ToString(dc.ExecuteProc(SR_USP_MANAGEROLES, objparams));
                strResult = objparams.Get<string>("@Result");
            }
            return strResult;
        }

        /// <summary>
        /// This method returns all user list with their role
        /// </summary>
        /// <returns></returns>
        public IList<User> GetUserList()
        {
            IList<User> listUser = new List<User>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@QueryNo", 1);
                listUser = dc.ExecuteProc<User>(SR_USP_GETUSERLIST, objparams);
            }
            return listUser;
        }
    }
}

using SMT.SpotRental.Business;
using SMT.SpotRental.Business.Entities;
using SMT.SpotRental.Business.Request;
using SMT.SpotRental.Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMT.SpotRental.API.Controllers
{

    // Note these all API need to implement security # Token Based
    [RoutePrefix("api/users")]
    public class UsersController : BaseController
    {

        [Route("validateuserlogin")]
        [HttpGet]
        [AllowAnonymous]
        public UserResponse ValidateUserLogin(string UserCred, string Password)
        {
            UserResponse objUser = null;

            try
            {

                if (!VerifyUser())
                {
                    // TO DO :: Need to implement token based security.............
                }
                else
                {
                    BLUsers blUser = new BLUsers();
                    objUser = new UserResponse();
                    objUser.userEntity = new UserEntity();
                    objUser.userEntity = blUser.ValidateUser(UserCred, Password);
                    if (objUser.userEntity != null && objUser.userEntity.UserID == -2)
                    {
                        objUser.Result = false;
                        objUser.IsExcep = false;
                        objUser.IsError = true;
                        objUser.ResultId = 0;
                        objUser.Message = "Invalid login credentials";
                    }
                    else
                    {
                        objUser.Result = true;
                        objUser.IsExcep = false;
                        objUser.IsError = false;
                        objUser.ResultId = 1;
                        objUser.Message = "TRUE";
                    }

                }
            }
            catch (Exception ex)
            {
                objUser = new UserResponse();
                objUser.ExceptionMessage = ex.Message;
                objUser.Result = false;
                objUser.IsExcep = true;
                objUser.IsError = true;
                objUser.ResultId = -1;
                objUser.Message = "";
            }
            return objUser;
        }

        [Route("getnavigationdetails")]
        [HttpGet]
        public MenuResponse GetNavigationDetails(string UserCred, string RoleID = "0", string QueryNo="1")
        {
            MenuResponse objMenu = null;

            try
            {

                if (!VerifyUser())
                {
                    // TO DO :: Need to implement token based security.............
                }
                else
                {
                    BLUsers blUser = new BLUsers();
                    objMenu = new MenuResponse();
                    objMenu.menuItems = new List<MenuEntity>();
                    objMenu.menuItems = blUser.GetNavigationDetails(UserCred,RoleID, QueryNo);
                    if (objMenu.menuItems != null && objMenu.menuItems.Count > 0)
                    {
                        objMenu.Result = true;
                        objMenu.IsExcep = false;
                        objMenu.IsError = false;
                        objMenu.ResultId = 1;
                        objMenu.Message = "TRUE";


                    }
                    else
                    {
                        objMenu.Result = false;
                        objMenu.IsExcep = false;
                        objMenu.IsError = true;
                        objMenu.ResultId = 0;
                        objMenu.Message = "Invalid user or invalid access rights";
                    }

                }
            }
            catch (Exception ex)
            {
                objMenu = new MenuResponse();
                objMenu.ExceptionMessage = ex.Message;
                objMenu.Result = false;
                objMenu.IsExcep = true;
                objMenu.IsError = true;
                objMenu.ResultId = -1;
                objMenu.Message = "";
            }
            return objMenu;
        }

        [Route("searchemployees")]
        [HttpGet]
        public EmployeeResponse SearchEmployees(string EmployeeCode = "", string FName = "", string LName = "", string MobileNo = "", string EmailID = "")
        {
            EmployeeResponse objEmployee = new EmployeeResponse();
            try
            {

                if (!VerifyUser())
                {
                    // TO DO :: Need to implement token based security.............
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    objEmployee.userList = new List<UserEntity>();
                    objEmployee.userList = objUser.SearchEmployees(EmployeeCode, FName, LName, MobileNo, EmailID);
                    if (objEmployee.userList != null && objEmployee.userList.Count > 0)
                    {
                        objEmployee.ResultId = 1;
                        objEmployee.IsExcep = false;
                        objEmployee.Message = "TRUE";
                        objEmployee.Result = true;
                    }
                    else
                    {
                        objEmployee.ResultId = 0;
                        objEmployee.IsError = true;
                        objEmployee.Message = "NO RESULT";
                        objEmployee.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objEmployee.ResultId = -2;
                objEmployee.IsExcep = true;
                objEmployee.ExceptionMessage = ex.Message;
                objEmployee.Result = false;
            }
            return objEmployee;
        }

        [Route("addemployee")]
        [HttpPost]
        public EmployeeResponse AddEmployee(UserEntity request)
        {
            EmployeeResponse response = new EmployeeResponse();
            try
            {

                if (!VerifyUser())
                {
                    // TO DO :: Need to implement token based security.............
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    string[] strresponse = objUser.AddEmployee(request);
                    if (strresponse != null && strresponse.Length > 2 && strresponse[0] == "INSERTED")
                    {
                        response.Message = "TRUE";
                        response.ResultId = 1;
                        response.IsExcep = false;
                        response.Result = true;
                        response.OtherMessages = new string[3];
                        response.OtherMessages[0] = strresponse[0]; // temp password
                        response.OtherMessages[1] = strresponse[1]; // temp password
                        response.OtherMessages[2] = strresponse[2]; // email template
                    }
                    else if (strresponse != null)
                    {
                        response.Message = strresponse[0];
                        response.ResultId = 0;
                        response.IsExcep = false;
                        response.Result = false;
                    }


                }
            }
            catch (Exception ex)
            {
                response.ResultId = -2;
                response.IsExcep = true;
                response.ExceptionMessage = ex.Message;
                response.Result = false;
            }
            return response;
        }

        [Route("forgetpwd")]
        [HttpGet]
        public string ForgetPwd(string EmailID)
        {
            string strResult = "";
            try
            {

                if (!VerifyUser())
                {
                    // TO DO :: Need to implement token based security.............
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    string strresponse = objUser.ForgetPassword(EmailID);
                    if (strresponse != null && strresponse == "1")
                    {
                        strResult = "1";
                    }
                    else
                    {
                        strResult = "0";
                    }


                }
            }
            catch (Exception ex)
            {
                strResult = "-1";
            }
            return strResult;
        }

        [Route("changepassword")]
        [HttpPost]
        public string ChangePassword(UserEntity request)
        {
            string strresponse = "";
            try
            {

                if (!VerifyUser())
                {
                    // TO DO :: Need to implement token based security.............
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    strresponse = objUser.ChangePassword(request.EmailID, request.Password);
                }
            }
            catch (Exception ex)
            {
                strresponse = "ERROR";
            }
            return strresponse;
        }

        [Route("updateprofilepicure")]
        [HttpPost]
        public string UpdateProfilePicure(UserEntity request)
        {
            string strresponse = "";
            try
            {

                if (!VerifyUser())
                {
                    // TO DO :: Need to implement token based security.............
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    strresponse = objUser.UpdateProfilePicture(request.UserID.ToString(), request.ProfilePic);
                }
            }
            catch (Exception ex)
            {
                strresponse = "ERROR";
            }
            return strresponse;
        }

        [Route("getallroles")]
        [HttpGet]
        public RolesResponse GetAllRoles()
        {
            RolesResponse objResponse = new RolesResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    objResponse.listRoles = new List<RolesEntity>();
                    objResponse.listRoles = objUser.GetAllRoles();
                    if (objResponse.listRoles != null && objResponse.listRoles.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("manageroles")]
        [HttpPost]
        public string ManageRoles(RolesEntity request)
        {
            string Result = "";
            try
            {

                if (!VerifyUser())
                {
                    //To do: Need to implement security details
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    Result = objUser.ManageRoles(request);
                }
            }
            catch (Exception ex)
            {
                Result = "Error: " + ex.Message;
            }
            return Result;
        }

        [Route("getuserslist")]
        [HttpGet]
        public EmployeeResponse GetUserList()
        {
            EmployeeResponse objResopnse = new EmployeeResponse();
            try
            {

                if (!VerifyUser())
                {
                    //To do: Need to implement security details
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    objResopnse.userList = new List<UserEntity>();
                    objResopnse.userList = objUser.GetUserList();
                    if(objResopnse.userList!=null && objResopnse.userList.Count>0)
                    {
                        objResopnse.Result = true;
                    }
                    else
                    {
                        objResopnse.Result = false;
                        objResopnse.ErrorMessage = "NOREC";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                objResopnse.IsExcep = true;
                objResopnse.ErrorMessage = "Error: " + ex.Message;
            }
            return objResopnse;
        }

        [Route("registerportaluser")]
        [HttpPost]
        public UserResponse RegisterPortalUser(UserEntity request)
        {
            UserResponse objUser = null;

            try
            {

                if (!VerifyUser())
                {
                    // TO DO :: Need to implement token based security.............
                }
                else
                {
                    BLUsers blUser = new BLUsers();
                    objUser = new UserResponse();
                    string Result = blUser.RegisterPortalUser(request);
                    if (Result != null )
                    {
                        objUser.Result = true;
                        objUser.IsExcep = false;
                        objUser.IsError = false;
                        objUser.ResultId = 1;
                        objUser.Message = Result;
                    }
                    else
                    {
                        objUser.Result = false;
                        objUser.IsExcep = false;
                        objUser.IsError = false;
                        objUser.ResultId = 0;
                        objUser.Message = "FALSE";
                    }

                }
            }
            catch (Exception ex)
            {
                objUser = new UserResponse();
                objUser.ExceptionMessage = ex.Message;
                objUser.Result = false;
                objUser.IsExcep = true;
                objUser.IsError = true;
                objUser.ResultId = -1;
                objUser.Message = "";
            }
            return objUser;
        }

        [Route("updateportaluser")]
        [HttpPost]
        public UserResponse UpdatePortalUser(UserEntity request)
        {
            UserResponse objUser = null;

            try
            {

                if (!VerifyUser())
                {
                    // TO DO :: Need to implement token based security.............
                }
                else
                {
                    BLUsers blUser = new BLUsers();
                    objUser = new UserResponse();
                    string Result = blUser.UpdatePortalUser(request);
                    if (Result != null)
                    {
                        objUser.Result = true;
                        objUser.IsExcep = false;
                        objUser.IsError = false;
                        objUser.ResultId = 1;
                        objUser.Message = Result;
                    }
                    else
                    {
                        objUser.Result = false;
                        objUser.IsExcep = false;
                        objUser.IsError = false;
                        objUser.ResultId = 0;
                        objUser.Message = "FALSE";
                    }

                }
            }
            catch (Exception ex)
            {
                objUser = new UserResponse();
                objUser.ExceptionMessage = ex.Message;
                objUser.Result = false;
                objUser.IsExcep = true;
                objUser.IsError = true;
                objUser.ResultId = -1;
                objUser.Message = "";
            }
            return objUser;
        }

        [Route("mapactionrole")]
        [HttpPost]
        public string MapActionRole(MenuEntity request)
        {
            string Result = "";
            try
            {

                if (!VerifyUser())
                {
                    //To do: Need to implement security details
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    Result = objUser.MapActionRole(request);
                }
            }
            catch (Exception ex)
            {
                Result = "Error: " + ex.Message;
            }
            return Result;
        }

        [Route("getmenulist")]
        [HttpGet]
        public MenuResponse GetMenuList(string UserID)
        {
            MenuResponse objResopnse = new MenuResponse();
            try
            {

                if (!VerifyUser())
                {
                    //To do: Need to implement security details
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    objResopnse.menuItems = new List<MenuEntity>();
                    objResopnse.menuItems = objUser.GetMenuList(UserID);
                    if (objResopnse.menuItems != null && objResopnse.menuItems.Count > 0)
                    {
                        objResopnse.Result = true;
                    }
                    else
                    {
                        objResopnse.Result = false;
                        objResopnse.ErrorMessage = "NOREC";

                    }
                }
            }
            catch (Exception ex)
            {
                objResopnse.IsExcep = true;
                objResopnse.ErrorMessage = "Error: " + ex.Message;
            }
            return objResopnse;
        }

        [Route("getparentmenu")]
        [HttpGet]
        public MenuResponse GetParentMenu(string UserID)
        {
            MenuResponse objResopnse = new MenuResponse();
            try
            {

                if (!VerifyUser())
                {
                    //To do: Need to implement security details
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    objResopnse.menuItems = new List<MenuEntity>();
                    objResopnse.menuItems = objUser.GetParentMenu(UserID);
                    if (objResopnse.menuItems != null && objResopnse.menuItems.Count > 0)
                    {
                        objResopnse.Result = true;
                    }
                    else
                    {
                        objResopnse.Result = false;
                        objResopnse.ErrorMessage = "NOREC";

                    }
                }
            }
            catch (Exception ex)
            {
                objResopnse.IsExcep = true;
                objResopnse.ErrorMessage = "Error: " + ex.Message;
            }
            return objResopnse;
        }

        [Route("managemenus")]
        [HttpPost]
        public string ManageMenus(MenuEntity request)
        {
            string Result = "";
            try
            {

                if (!VerifyUser())
                {
                    //To do: Need to implement security details
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    Result = objUser.ManageMenus(request);
                }
            }
            catch (Exception ex)
            {
                Result = "Error: " + ex.Message;
            }
            return Result;
        }


        [Route("getlocationlist")]
        [HttpGet]
        public LocationResponse GetLocationList(string UserID)
        {
            LocationResponse objResopnse = new LocationResponse();
            try
            {

                if (!VerifyUser())
                {
                    //To do: Need to implement security details
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    objResopnse.LocationItems= new List<LocationEntity>();
                    objResopnse.LocationItems = objUser.GetLocationList(UserID);
                    if (objResopnse.LocationItems != null && objResopnse.LocationItems.Count > 0)
                    {
                        objResopnse.Result = true;
                    }
                    else
                    {
                        objResopnse.Result = false;
                        objResopnse.ErrorMessage = "NOREC";

                    }
                }
            }
            catch (Exception ex)
            {
                objResopnse.IsExcep = true;
                objResopnse.ErrorMessage = "Error: " + ex.Message;
            }
            return objResopnse;
        }

        [Route("managelocation")]
        [HttpPost]
        public string ManageLocation(LocationEntity request)
        {
            string Result = "";
            try
            {

                if (!VerifyUser())
                {
                    //To do: Need to implement security details
                }
                else
                {
                    BLUsers objUser = new BLUsers();
                    Result = objUser.ManageLocation(request);
                }
            }
            catch (Exception ex)
            {
                Result = "Error: " + ex.Message;
            }
            return Result;
        }
    }
}

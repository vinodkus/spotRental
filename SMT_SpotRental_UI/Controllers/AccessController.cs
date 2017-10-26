using SMT.SpotRental.Shared.Entities;
using SMT.SpotRental.UI.APIGateway;
using SMT.SpotRental.UI.Filters;
using SMT.SpotRental.UI.Models;
using SMT.SpotRental.UI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMT.SpotRental.UI.Controllers
{
    [UserSessionTimeout]
    public class AccessController : BaseController
    {
        WebAPICommunicator webAPI = null;

        #region MANAGE ROLES
        public ActionResult ManageRoles()
        {
            if (Session["User"] != null)
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                DashboardViewModel modelDashboard = new DashboardViewModel();
                modelDashboard.listRoles = new List<Roles>();
                modelDashboard.userDetails = new UserViewModel();
                modelDashboard.userDetails = empmodel;
                ViewBag.TempPassword = modelDashboard.userDetails.TempPwd;
                ViewBag.ProfilePicPath = PRF_PATH;
                ViewBag.ProfilePic = !string.IsNullOrEmpty(empmodel.ProfilePic) ? PRF_PATH + empmodel.ProfilePic : PRF_PATH + "PRF_PIC.png";
                modelDashboard.objMenu = GetMenuDetails(modelDashboard.userDetails.UserID);

                RolesResponse response = GetAllRoles();
                if (response != null && response.listRoles.Count > 0)
                {
                    modelDashboard.listRoles = response.listRoles;
                }

                empmodel.TempPwd = false;
                Session["User"] = empmodel;
                if (modelDashboard.objMenu == null || modelDashboard.objMenu.menuItems == null || modelDashboard.objMenu.menuItems.Count <= 0 || modelDashboard.objMenu.IsError || modelDashboard.objMenu.IsExcep)
                {
                    return RedirectToAction("InvalidAccess", "Message");
                }
                else
                {
                    return View("ManageRoles", modelDashboard);
                }
            }
            else
            {
                return RedirectToAction("InvalidAccess", "Message");
            }

        }
        private RolesResponse GetAllRoles()
        {
            RolesResponse responseRole = new RolesResponse();
            try
            {
                webAPI = new WebAPICommunicator();
                responseRole = webAPI.GetResponse<RolesResponse>(responseRole, "U", "getallroles");

            }
            catch
            {

            }

            return responseRole;
        }
        public JsonResult ChangeRoleStatus(string RoleID)
        {
            if (RoleID != null && RoleID != "" && RoleID != "0")
            {
                string str = ChangeStatus(RoleID);
                return Json(new { Result = str.Contains("TRUE") ? "TRUE" : "There are some issue in updating status." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid role id supplied." }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateRole(string RoleID, string RoleName, string Status)
        {
            if (RoleID != null && RoleID != "" && RoleID != "0" && RoleName != null)
            {
                string Result = "";
                string str = UpdateRoles(RoleID, RoleName, Status, ref Result);
                return Json(new { Result = Result.ToUpper().Contains("TRUE") ? "TRUE" : Result.ToUpper().Contains("DUPLICATE") ? "This role name already exist." : "There are some issue in updating role." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddNewRole(string RoleName, string Status)
        {
            if (Status != null && RoleName != null && RoleName.Length > 3)
            {
                string Result = "";
                string str = AddRole(RoleName, Status, ref Result);
                return Json(new { Result = Result.ToUpper().Contains("TRUE") ? "TRUE" : Result.ToUpper().Contains("DUPLICATE") ? "This role name already exist." : "There are some issue in updating role." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }
        private string ChangeStatus(string RoleID)
        {
            string strResult = "";
            try
            {
                webAPI = new WebAPICommunicator();
                Roles request = new Roles();
                request.RoleID = Convert.ToInt32(RoleID);
                request.QueryNo = 3;
                strResult = webAPI.PostRequest<Roles>(request, "U", "manageroles");

            }
            catch (Exception ex)
            {
                strResult = "EXE:" + ex.Message;
            }

            return strResult;
        }
        private string UpdateRoles(string RoleID, string RoleName, string Status, ref string Result)
        {
            string strResult = "";
            try
            {
                webAPI = new WebAPICommunicator();
                Roles request = new Roles();
                request.RoleID = Convert.ToInt32(RoleID);
                request.RoleName = RoleName;
                request.Active = Status;
                request.QueryNo = 4;
                strResult = webAPI.PostRequest<Roles>(request, "U", "manageroles", ref Result);

            }
            catch (Exception ex)
            {
                strResult = "EXE:" + ex.Message;
            }

            return strResult;
        }
        private string AddRole(string RoleName, string Status, ref string Result)
        {
            string strResult = "";
            try
            {
                webAPI = new WebAPICommunicator();
                Roles request = new Roles();
                request.RoleID = 0;
                request.RoleName = RoleName;
                request.Active = Status;
                request.QueryNo = 2;
                strResult = webAPI.PostRequest<Roles>(request, "U", "manageroles", ref Result);

            }
            catch (Exception ex)
            {
                strResult = "EXE:" + ex.Message;
            }

            return strResult;
        }
        [HttpPost]
        public PartialViewResult BindAllRoles()
        {
            DashboardViewModel modelDashboard = new DashboardViewModel();
            modelDashboard.listRoles = new List<Roles>();
            RolesResponse response = GetAllRoles();
            if (response != null && response.listRoles.Count > 0)
            {
                modelDashboard.listRoles = response.listRoles;
            }
            return PartialView("_RoleList", modelDashboard);
        }
        #endregion

        #region MANAGE PORTAL USERS
        public ActionResult ManagePortalUser()
        {
            if (Session["User"] != null)
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                DashboardViewModel modelDashboard = new DashboardViewModel();
                modelDashboard.userDetails = new UserViewModel();
                modelDashboard.userDetails = empmodel;
                ViewBag.TempPassword = modelDashboard.userDetails.TempPwd;
                ViewBag.ProfilePicPath = PRF_PATH;
                ViewBag.ProfilePic = !string.IsNullOrEmpty(empmodel.ProfilePic) ? PRF_PATH + empmodel.ProfilePic : PRF_PATH + "PRF_PIC.png";
                modelDashboard.objMenu = GetMenuDetails(modelDashboard.userDetails.UserID);
                empmodel.TempPwd = false;
                Session["User"] = empmodel;
                if (modelDashboard.objMenu == null || modelDashboard.objMenu.menuItems == null || modelDashboard.objMenu.menuItems.Count <= 0 || modelDashboard.objMenu.IsError || modelDashboard.objMenu.IsExcep)
                {
                    return RedirectToAction("InvalidAccess", "Message");
                }
                else
                {
                    
                 
                    return View("ManagePortalUsers", modelDashboard);
                }
            }
            else
            {
                return RedirectToAction("InvalidAccess", "Message");
            }

        }
        public PartialViewResult BindUsers()
        {
            EmployeeResponse response = new EmployeeResponse();
            response = GetUserList();
            return PartialView("_UsersList", response);
        }
        private EmployeeResponse GetUserList()
        {
            EmployeeResponse response = new EmployeeResponse();
            try
            {
                webAPI = new WebAPICommunicator();
                response = webAPI.GetResponse<EmployeeResponse>(response, "U", "getuserslist");

            }
            catch
            {

            }

            return response;
        }

        [HttpGet]
        public JsonResult ResetPassword(string EmailID)
        {
            if (EmailID != null && EmailID != "" && EmailID.Length>=8 )
            {
                webAPI = new WebAPICommunicator();
                object[] paramValue = new object[1]; string[] paramName = new string[1] { "EmailID" };
                paramValue[0] = EmailID;

                string strResult = webAPI.GetResponse("U", "forgetpwd", paramValue, paramName); // Need to do email sending functionality in SP[from DB]
                if (strResult != null && strResult == "1")
                {
                    return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
                }
                else if (strResult != null && strResult == "0")
                {
                    return Json(new { Result = false, Message = "This email id does not exist in our database." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = false, Message = "There is some issue in reseting password." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Result = false, Message = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }
        
        #endregion
    }
}
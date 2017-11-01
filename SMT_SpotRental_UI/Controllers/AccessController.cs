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
            if (EmailID != null && EmailID != "" && EmailID.Length >= 8)
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

        [HttpPost]
        public JsonResult UpdatePortalUser(User request)
        {
            if (request != null && request.EmailID != null && request.UserID != 0 && request.UserName != "" && request.RoleIds != null)
            {
                webAPI = new WebAPICommunicator();
                UserResponse response = new UserResponse();
                response = webAPI.PostRequest_Object<UserResponse, User>(response, request, "U", "updateportaluser");
                if (response != null && response.Result == true)
                {
                    if (response.Message == "INVDATA")
                    {
                        return Json(new { Result = true, Message = "Invalid Mobile No/Email ID." }, JsonRequestBehavior.AllowGet);
                    }
                    else if (response.Message == "EMMOB")
                    {
                        return Json(new { Result = true, Message = "Mobile No/Email ID already exist." }, JsonRequestBehavior.AllowGet);
                    }
                    else if (response.Message == "TRUE")
                    {
                        return Json(new { Result = true, Message = "User updated successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else if (response.Message == "ERROR")
                    {
                        return Json(new { Result = true, Message = "There is some error in updating user." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Result = true, Message = response.Message }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { Result = true, Message = "There is some error in updating user." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Result = false, Message = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AddPortalUser(User request)
        {
            if (request != null && request.EmailID != null && request.UserName != "" && request.RoleIds != null)
            {
                webAPI = new WebAPICommunicator();
                UserResponse response = new UserResponse();
                response = webAPI.PostRequest_Object<UserResponse, User>(response, request, "U", "registerportaluser");
                if (response != null && response.Result == true)
                {
                    if (response.Message == "INVDATA")
                    {
                        return Json(new { Result = true, Message = "Invalid Mobile No/Email ID." }, JsonRequestBehavior.AllowGet);
                    }
                    else if (response.Message == "EMMOB")
                    {
                        return Json(new { Result = true, Message = "Mobile No/Email ID already exist." }, JsonRequestBehavior.AllowGet);
                    }
                    else if (response.Message == "TRUE")
                    {
                        return Json(new { Result = true, Message = "User added successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else if (response.Message == "ERROR")
                    {
                        return Json(new { Result = true, Message = "There is some error in updating user." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Result = true, Message = response.Message }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { Result = true, Message = "There is some error in updating user." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Result = false, Message = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region MANAGE ROLE MAP
        public ActionResult ManageRoleAction()
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

                empmodel.TempPwd = false;
                Session["User"] = empmodel;
                if (modelDashboard.objMenu == null || modelDashboard.objMenu.menuItems == null || modelDashboard.objMenu.menuItems.Count <= 0 || modelDashboard.objMenu.IsError || modelDashboard.objMenu.IsExcep)
                {
                    return RedirectToAction("InvalidAccess", "Message");
                }
                else
                {
                    return View("ManageRoleAction", modelDashboard);
                }
            }
            else
            {
                return RedirectToAction("InvalidAccess", "Message");
            }

        }

        [UserSessionTimeout]

        public PartialViewResult GetAllMenu(string RoleID)
        {
            MenuResponse response = new MenuResponse();
            if (RoleID != null && RoleID != "" && RoleID != "0")
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                response = GetMenuDetails(empmodel.UserID, RoleID, "3");
            }
            return PartialView("_RoleActionMap", response);
        }

        public JsonResult UpdateActionRole(string RoleID, string ActionIds)
        {
            if (RoleID != null && RoleID != "" && RoleID != "0" && ActionIds != null && ActionIds != "" && ActionIds.Length>1)
            {
                webAPI = new WebAPICommunicator();
                Menu request = new Menu();
                request.RoleID = Convert.ToInt32(RoleID);
                request.ActionIDs = ActionIds;
                string Result = webAPI.PostRequest<Menu>(request, "U", "mapactionrole");
                return Json(new { Result=true,  Message = Result.ToUpper().Contains("TRUE") ? "TRUE" : "There is some issue in mapping actions with role."  }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Message = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region MANAGE MENU
        public ActionResult Menu()
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


                    return View("ManageMenu", modelDashboard);
                }
            }
            else
            {
                return RedirectToAction("InvalidAccess", "Message");
            }

        }

        public PartialViewResult BindMenus()
        {
            MenuResponse response = new MenuResponse();
            response = GetMenuList();
            return PartialView("_MenuList", response);
        }
        private MenuResponse GetMenuList()
        {
            MenuResponse response = new MenuResponse();
            try
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                object[] paramValue = new object[1]; string[] paramName = new string[1] { "UserID", };
                paramValue[0] = empmodel.UserID;
                webAPI = new WebAPICommunicator();
                response = webAPI.GetResponse<MenuResponse>(response, "U", "getmenulist", paramValue, paramName);

            }
            catch
            {

            }

            return response;
        }

        public JsonResult GetParentMenu()
        {
            MenuResponse response = new MenuResponse();

            UserViewModel empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            object[] paramValue = new object[1]; string[] paramName = new string[1] { "UserID", };
            paramValue[0] = empmodel.UserID;
            webAPI = new WebAPICommunicator();
            response = webAPI.GetResponse<MenuResponse>(response, "U", "getparentmenu", paramValue, paramName);
            if (response != null && response.menuItems != null)
            {
                var parentMenu = response.menuItems.Where(x => x.RootID == 0);
                return Json(new { Result = true, List = parentMenu }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddNewMenu(string ActionName = "", string ActionText = "", int RootID = 0, Boolean status = true, string ControllerName = "", int MenuOrder = 0, Boolean IsMenuItems = true)
        {
            if (ActionName != null && ActionText != null && ControllerName != null)
            {
                string Result = "";
                string str = AddMenu(ActionName, ActionText, RootID, status, ControllerName, MenuOrder, IsMenuItems, ref Result);
                return Json(new { Result = Result.ToUpper().Contains("TRUE") ? "TRUE" : Result.ToUpper().Contains("DUPLICATE") ? "This menu already exist." : "There are some issue in adding menu." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }
        private string AddMenu(string ActionName, string ActionText, int RootID, Boolean status, string ControllerName, int MenuOrder, Boolean IsMenuItems, ref string Result)
        {
            string strResult = "";
            try
            {
                webAPI = new WebAPICommunicator();
                Menu request = new Menu();
                request.ActionText = ActionText;
                request.ActionName = ActionName;
                request.ControllerName = ControllerName;
                request.MenuOrder = MenuOrder;
                request.IsMenuItems = IsMenuItems == true ? 1 : 0;
                request.Active = status;
                request.RootID = RootID;
                request.QueryNo = 1;
                strResult = webAPI.PostRequest<Menu>(request, "U", "managemenus", ref Result);

            }
            catch (Exception ex)
            {
                strResult = "EXE:" + ex.Message;
            }

            return strResult;
        }

        public JsonResult UpdateNewMenu(int ActionId, string ActionName = "", string ActionText = "", int RootID = 0, Boolean status = true, string ControllerName = "", int MenuOrder = 0, Boolean IsMenuItems = true)
        {
            if (ActionName != null && ActionText != null && ControllerName != null)
            {
                string Result = "";
                string str = UpdateMenu(ActionId, ActionName, ActionText, RootID, status, ControllerName, MenuOrder, IsMenuItems, ref Result);
                return Json(new { Result = Result.ToUpper().Contains("TRUE") ? "TRUE" : Result.ToUpper().Contains("DUPLICATE") ? "This menu already exist." : "There are some issue in updating menu." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }

        private string UpdateMenu(int ActionId, string ActionName, string ActionText, int RootID, Boolean status, string ControllerName, int MenuOrder, Boolean IsMenuItems, ref string Result)
        {
            string strResult = "";
            try
            {
                webAPI = new WebAPICommunicator();
                Menu request = new Menu();
                request.ActionID = ActionId;
                request.ActionText = ActionText;
                request.ActionName = ActionName;
                request.ControllerName = ControllerName;
                request.MenuOrder = MenuOrder;
                request.IsMenuItems = IsMenuItems == true ? 1 : 0;
                request.Active = status;
                request.RootID = RootID;
                request.QueryNo = 2;
                strResult = webAPI.PostRequest<Menu>(request, "U", "managemenus", ref Result);

            }
            catch (Exception ex)
            {
                strResult = "EXE:" + ex.Message;
            }

            return strResult;
        }
        #endregion

        #region MANAGE LOCATION
        public ActionResult Location()
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


                    return View("ManageLocation", modelDashboard);
                }
            }
            else
            {
                return RedirectToAction("InvalidAccess", "Message");
            }

        }
        public PartialViewResult BindLocation()
        {
            LocationResponse response = new LocationResponse();
            response = GetLocationList();
            return PartialView("_LocationList", response);
        }
        public LocationResponse GetLocationList()
        {
            LocationResponse response = new LocationResponse();
            try
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                object[] paramValue = new object[1]; string[] paramName = new string[1] { "UserID", };
                paramValue[0] = empmodel.UserID;
                webAPI = new WebAPICommunicator();
                response = webAPI.GetResponse<LocationResponse>(response, "U", "getlocationlist", paramValue, paramName);

            }
            catch
            {

            }

            return response;
        }

        public JsonResult AddNewLocation(string LocationCode = "", string LocationName = "", string ShortName = "",string City="", string Status = "", string Visible = "", string EmailId = "")
        {
            if (LocationCode.Length >0 && LocationName.Length > 0 && ShortName.Length > 0)
            {
                string Result = "";
                string str = AddLocation(LocationCode, LocationName, ShortName, City, Status, Visible, EmailId, ref Result);
                return Json(new { Result = Result.ToUpper().Contains("TRUE") ? "TRUE" : Result.ToUpper().Contains("DUPLICATE") ? "This "+ Result + " already exist." : "There are some issue in adding location." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }
        private string AddLocation(string LocationCode , string LocationName , string ShortName , string City , string Status , string Visible , string EmailId, ref string Result)
        {
            string strResult = "";
            try
            {
                webAPI = new WebAPICommunicator();
                Location request = new Location();
                request.Active = Status;
                request.City = City;
                request.EmailId = EmailId;
                request.LocationCode = LocationCode;
                request.LocationName = LocationName;
                request.ShortName = ShortName;
                request.Visible = Visible;
                request.QueryNo = 1;
                strResult = webAPI.PostRequest<Location>(request, "U", "managelocation", ref Result);

            }
            catch (Exception ex)
            {
                strResult = "EXE:" + ex.Message;
            }

            return strResult;
        }

        public JsonResult UpdateNewLocation(string LocationCode = "", string LocationName = "", string ShortName = "", string City = "", string Status = "", string Visible = "", string EmailId = "")
        {
            if (LocationCode.Length > 0 && LocationName.Length > 0 && ShortName.Length > 0)
            {
                string Result = "";
                string str = UpdateLocation(LocationCode, LocationName, ShortName, City, Status, Visible, EmailId, ref Result);
                return Json(new { Result = Result.ToUpper().Contains("TRUE") ? "TRUE" : Result.ToUpper().Contains("DUPLICATE") ? "This "+ Result + " already exist." : "There are some issue in updating location." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }

        private string UpdateLocation(string LocationCode , string LocationName , string ShortName , string City , string Status , string Visible , string EmailId , ref string Result)
        {
            string strResult = "";
            try
            {
                webAPI = new WebAPICommunicator();
                Location request = new Location();
                request.LocationCode = LocationCode;
                request.LocationName = LocationName;
                request.ShortName = ShortName;
                request.City = City;
                request.Active = Status;
                request.Visible = Visible;
                request.EmailId = EmailId;
               
                request.QueryNo = 2;
                strResult = webAPI.PostRequest<Location>(request, "U", "managelocation", ref Result);

            }
            catch (Exception ex)
            {
                strResult = "EXE:" + ex.Message;
            }

            return strResult;
        }
        #endregion
    }
}
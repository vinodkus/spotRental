using SMT.SpotRental.Shared.Entities;
using SMT.SpotRental.UI.APIGateway;
using SMT.SpotRental.UI.Filters;
using SMT.SpotRental.UI.Models;
using SMT.SpotRental.UI.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMT.SpotRental.UI.Controllers
{
    [UserSessionTimeout]
    public class MasterItemsController : BaseController
    {
        WebAPICommunicator webAPI = null;
        

        #region MANAGE VENDORS
        public ActionResult ViewVendors()
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
                    return View("ManageVendors", modelDashboard);
                }
            }
            else
            {
                return RedirectToAction("InvalidAccess", "Message");
            }
        }

        [HttpPost]
        public PartialViewResult SearchVendors(string LocCode, string MobileNo, string FName, string LName)
        {
            VendorMasterResponse objResponse = new VendorMasterResponse();
            VendorMaster request = new VendorMaster();
            request.LocationCode = LocCode;
            request.Phone1 = MobileNo;
            request.FName = FName;
            request.LName = LName;
            objResponse.listVendor = new List<VendorMaster>();
            objResponse = SearchVendors(request);
            return PartialView("_VendorList", objResponse);
        }

        [HttpPost]
        public JsonResult ChangeVendorStatus(string VendorID)
        {
            if (VendorID != null && VendorID != "" && VendorID != "0")
            {
                string strResult = "";
                string str = ChangeVendorsStatus(VendorID, ref strResult);
                return Json(new { Result = strResult.Contains("TRUE") ? "TRUE" : strResult.Replace('\"', ' ') }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid vendor id supplied." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AddVendor(VendorMaster request)
        {
            if (request != null && request.ShortName != null && request.CompanyName != null)
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                request.CreatedBy = empmodel.UserID.ToString();
                string strResult = "";
                string str = AddUpdateVendor(request, "ADD", ref strResult);
                return Json(new { Result = strResult.Contains("TRUE") ? "TRUE" : strResult.Replace('\"', ' ').ToUpper().Contains("SHORT NAME") ? "Short name already exist. Please choose another one." : "There are some error in adding vendor. Please try after some time." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateVendor(VendorMaster request)
        {
            if (request != null && request.VendorId != null && request.ShortName != null && request.CompanyName != null)
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                request.CreatedBy = empmodel.UserID.ToString();
                string strResult = "";
                string str = AddUpdateVendor(request, "UDPATE", ref strResult);
                return Json(new { Result = strResult.Contains("TRUE") ? "TRUE" : "There are some issue in updating vendor. Please after some time." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region MANAGE VEHICLES
        public ActionResult ViewVehicle()
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
                ViewBag.DOCS_PATH = DOCS_PATH;
                empmodel.TempPwd = false;
                Session["User"] = empmodel;
                if (modelDashboard.objMenu == null || modelDashboard.objMenu.menuItems == null || modelDashboard.objMenu.menuItems.Count <= 0 || modelDashboard.objMenu.IsError || modelDashboard.objMenu.IsExcep)
                {
                    return RedirectToAction("InvalidAccess", "Message");
                }
                else
                {
                    return View("ManageVehicle", modelDashboard);
                }
            }
            else
            {
                return RedirectToAction("InvalidAccess", "Message");
            }
        }

        [HttpPost]
        public PartialViewResult SearchVehicles(string LocCode, string VehicleNo, string RegistrationNo)
        {
            VehicleMasterResponse objResponse = new VehicleMasterResponse();
            VehicleMaster request = new VehicleMaster();
            request.LocCode = LocCode;
            request.VehicleNo = VehicleNo;
            request.RegistrationNo = RegistrationNo;
          
            objResponse.listVehicle = new List<VehicleMaster>();
            objResponse = SearchVehicles(request);
            return PartialView("_VehicleList", objResponse);
        }

        [HttpPost]
        public PartialViewResult GetVehicleHistoryDetails(string VehicleId)
        {
            VehicleHistoryResponse objResponse = new VehicleHistoryResponse();
            VehicleHistory request = new VehicleHistory();
            request.VehicleID = VehicleId;

            objResponse.listVehicleHistory = new List<VehicleHistory>();
            objResponse = GetVehicleHistoryDetails(request);
            return PartialView("_ViewVehicleHistoryDetails", objResponse);
        }
        [HttpPost]
        public JsonResult AddVehicle(VehicleMaster request)
        {
            if (request != null && request.VendorID != null && request.RegistrationNo != null)
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                request.CreatedBy = empmodel.UserID.ToString();
                string strResult = "";
                string str = AddUpdateVehicle(request, "ADD", ref strResult);
                return Json(new { Result = strResult.Contains("TRUE") ? "TRUE" : strResult.Replace('\"', ' ').ToUpper().Contains("REGEXIST") ? "This registration no already exist." : "There are some error in adding vehicle. Please try after some time." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateVehicle(VehicleMaster request)
        {
            if (request != null && request.VendorID != null && request.VehicleID != null && request.RegistrationNo != null)
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                request.CreatedBy = empmodel.UserID.ToString();
                string strResult = "";
                string str = AddUpdateVehicle(request, "UPDATE", ref strResult);
                return Json(new { Result = strResult.Contains("TRUE") ? "TRUE" : strResult.Replace('\"', ' ').ToUpper().Contains("REGEXIST") ? "This registration no already exist." : "There are some error in adding vehicle. Please try after some time." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UploadDocumentDetails(UploadedDocument request)
        {
            if (request != null && request.DocumentFileName != null && request.DocumentFileName != "" && (request.VehicleID != 0 || request.DriverGuardID != 0) && request.DocumentID != 0)
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                request.UploadedBy = empmodel.UserID;
             
                string str = UploadDocs(request);
                return Json(new { Result = str }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }

        private string UploadDocs(UploadedDocument request)
        {
            string strResult = "ERROR";
            try
            {
                webAPI = new WebAPICommunicator();
                strResult = webAPI.PostRequest(request, "I", "uploaddocuments");
            }
            catch
            {

            }

            return strResult;
        }
        #endregion

        #region MANAGE DRIVER/GUARD
        public ActionResult ViewDriverGuard()
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
                ViewBag.DOCS_PATH = DOCS_PATH;
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
                    return View("ManageDriverGuard", modelDashboard);
                }
            }
            else
            {
                return RedirectToAction("InvalidAccess", "Message");
            }
        }

        [HttpPost]
        public PartialViewResult SearchDriverGuard(string LocCode, string VendorCode)
        {
            DriverGuardResponse objResponse = new DriverGuardResponse();
            DriverGuard request = new DriverGuard();
            request.LocCode = LocCode;
            request.VendorId = VendorCode;

            objResponse.listDriverGuard = new List<DriverGuard>();
            objResponse = SearchDriverGuard(request);
            return PartialView("_DriverGuardList", objResponse);
        }

        [HttpPost]
        public JsonResult AddDriverGuard(DriverGuard request)
        {
            if (request != null && request.LicenceNo != null && request.LicenceExpiryDate != null)
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                request.CreatedBy = empmodel.UserID.ToString();
                string strResult = "";
                string str = AddUpdateDriverGuard(request, "ADD", ref strResult);
                return Json(new { Result = strResult.Contains("TRUE") ? "TRUE" : strResult.Replace('\"', ' ').ToUpper().Contains("REGEXIST") ? "This registration no already exist." : "There are some error in adding vehicle. Please try after some time." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateDriverGuard(DriverGuard request)
        {
            if (request != null && request.LicenceNo != null && request.DriverGuardId != null && request.LicenceExpiryDate != null)
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                request.ModBy = empmodel.UserID.ToString();
                string strResult = "";
                string str = AddUpdateDriverGuard(request, "UPDATE", ref strResult);
                return Json(new { Result = strResult.Contains("TRUE") ? "TRUE" : strResult.Replace('\"', ' ').ToUpper().Contains("REGEXIST") ? "This registration no already exist." : "There are some error in adding vehicle. Please try after some time." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Invalid data supplied." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteDriverGuard(DriverGuard request)
        {
            UserViewModel empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            request.ModBy = empmodel.UserID.ToString();
            string strResult = "";
            string str = DeleteDriverGuard(request, ref strResult);
            return Json(new { Result = strResult.Contains("TRUE") ? "TRUE" : "There are some error in deleting Driver/Guard. Please try after some time." }, JsonRequestBehavior.AllowGet);
        }

        //DeleteDriverGuard
        #endregion
    }
}
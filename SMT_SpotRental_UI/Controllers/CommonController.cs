using SMT.SpotRental.Shared.Entities;
using SMT.SpotRental.UI.APIGateway;
using SMT.SpotRental.UI.Controllers;
using SMT.SpotRental.UI.Filters;
using SMT.SpotRental.UI.Models;
using SMT.SpotRental.UI.Request;
using SMT.SpotRental.UI.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SMT_Amazon_UI.Controllers
{



    public class CommonController : BaseController
    {
        WebAPICommunicator webAPI = null;


        public JsonResult GetDesignation()
        {
            webAPI = new WebAPICommunicator();
            DesignationResponse objResponse = new DesignationResponse();
            objResponse = webAPI.GetResponse<DesignationResponse>(objResponse, "I", "getdesignationlist");
            if (objResponse != null && objResponse.listDesignation != null)
            {
                return Json(new { Result = true, List = objResponse }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetBaseLocation()
        {
            webAPI = new WebAPICommunicator();
            BaseLocationResponse objResponse = new BaseLocationResponse();
            objResponse = webAPI.GetResponse<BaseLocationResponse>(objResponse, "I", "getbaselocation");
            if (objResponse != null && objResponse.listBaseLocation != null)
            {
                return Json(new { Result = true, List = objResponse }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetReasonList(string GroupName)
        {
            webAPI = new WebAPICommunicator();
            ReasonResponse objResponse = new ReasonResponse();
            object[] paramValue = new object[1]; string[] paramName = new string[1] { "ReasonGroup" };
            paramValue[0] = GroupName;

            objResponse = webAPI.GetResponse<ReasonResponse>(objResponse, "I", "getreason", paramValue, paramName);

            if (objResponse != null && objResponse.reasonList != null)
            {
                return Json(new { Result = true, List = objResponse }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [UserSessionTimeout]
        [HttpPost]
        public PartialViewResult SupervisorList(string FName, string LName, string MobileNo)
        {
            EmployeeResponse objEmployee = new EmployeeResponse();
            objEmployee.userList = new List<User>();
            objEmployee = SearchEmployees(FName, LName, MobileNo, "");
            return PartialView("_SupervisorList", objEmployee);
        }

        [UserSessionTimeout]
        [HttpGet]
        public new JsonResult GetTripStatusList(string DisplayFor)
        {
            TripStatusResponse objResponse = new TripStatusResponse();
            objResponse.listTripStatus = new List<TripStatus>();
            objResponse = base.GetTripStatusList(DisplayFor);
            if (objResponse != null && objResponse.listTripStatus != null)
            {
                return Json(new { Result = true, List = objResponse }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserSessionTimeout]
        [HttpGet]
        public JsonResult GetAllVehicleListForVendor()
        {

            webAPI = new WebAPICommunicator();
            VehicleMasterResponse objResponse = new VehicleMasterResponse();
            objResponse.listVehicle = new List<VehicleMaster>();
            UserViewModel empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;

            string VendorId = empmodel.VendorID.ToString();
            object[] paramValue = new object[1]; string[] paramName = new string[1] { "VendorID" };
            paramValue[0] = VendorId;

            objResponse = webAPI.GetResponse<VehicleMasterResponse>(objResponse, "I", "getvehiclelist", paramValue, paramName);
            if (objResponse != null && objResponse.listVehicle != null)
            {
                return Json(new { Result = true, List = objResponse }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserSessionTimeout]
        public JsonResult GetAllVehicle()
        {
            webAPI = new WebAPICommunicator();
            VehicleTypeMasterResponse objResponse = new VehicleTypeMasterResponse();
            objResponse = webAPI.GetResponse<VehicleTypeMasterResponse>(objResponse, "I", "getallvehicleTypes");
            if (objResponse != null && objResponse.listVehicleType != null)
            {
                return Json(new { Result = true, List = objResponse }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserSessionTimeout]
        public JsonResult GetAllVendors()
        {
            VendorMasterResponse objResponse = new VendorMasterResponse();
            VendorMaster request = new VendorMaster();
            objResponse.listVendor = new List<VendorMaster>();
            objResponse = SearchVendors(request);
            if (objResponse != null && objResponse.listVendor != null)
            {
                return Json(new { Result = true, List = objResponse }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserSessionTimeout]
        public JsonResult GetDocumentList(string DocType)
        {
            DocumentMasterResponse objResponse = new DocumentMasterResponse();
            objResponse.listDocs = new List<DocumentMaster>();
            DocumentMaster request = new DocumentMaster();
            request.Active = 'Y';
            request.DocType = DocType.ToCharArray()[0];
            webAPI = new WebAPICommunicator();
            objResponse = webAPI.PostRequest_Object<DocumentMasterResponse, DocumentMaster>(objResponse, request, "I", "GetDocumentList");
            if (objResponse != null && objResponse.listDocs != null)
            {
                return Json(new { Result = true, List = objResponse }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserSessionTimeout]
        [HttpGet]
        public JsonResult GetAllDriverVehicleForVendor()
        {

            webAPI = new WebAPICommunicator();
            DriverGuardResponse objResponse = new DriverGuardResponse();
            objResponse.listDriverGuard = new List<DriverGuard>();
            UserViewModel empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;

            string VendorId = empmodel.VendorID.ToString();
            object[] paramValue = new object[2]; string[] paramName = new string[2] { "VendorID", "EmpType" };
            paramValue[0] = VendorId;
            paramValue[1] = "ALL";

            objResponse = webAPI.GetResponse<DriverGuardResponse>(objResponse, "I", "getdriverGuardlist", paramValue, paramName);
            if (objResponse != null && objResponse.listDriverGuard != null)
            {
                return Json(new { Result = true, List = objResponse }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [InputChecker]
        public ActionResult GetTripDetails()
        {
            //string enc = Encrypt("32|12");

            //string dec = Decrypt(enc);

            string URL = Request.Url.AbsoluteUri;

            ViewBag.URL = URL;
            string[] UrlArray = URL.Split('?');

            if (UrlArray != null && UrlArray.Length == 2 && UrlArray[1] != null && UrlArray[1].Length == 24)
            {
                string URLData = Decrypt(UrlArray[1]);

                string[] UrlDataArray = URLData.Split('|');

                if (UrlDataArray != null && UrlDataArray.Length == 2 && UrlDataArray[0] != null && UrlDataArray[1] != null)
                {
                    string GroupNo = UrlDataArray[0];
                    string UserID = UrlDataArray[1];
                    AdhocResponse response = new AdhocResponse();
                    response.itemsList = new List<RouteItems>();
                    response = GetAdhocListOutsider(DateTime.Now.ToString(), DateTime.Now.ToString(), "", "PFA", null, "EMAIL", "0", GroupNo);
                    return View("GetTripDetails", response);
                }
                else
                {
                    return RedirectToAction("Error", "Message");
                }
            }
            else
            {
                return RedirectToAction("Error", "Message");
            }

        }

        [CustomException]
        [HttpPost]
        public JsonResult CancelTripByUser(string ReqID, string URL)
        {


            if (ReqID != null)
            {
                string strResult = "", UserID = "0";
                URL = URL.Replace("#", "");
                string[] UrlArray = URL.Split('?');

                if (UrlArray != null && UrlArray.Length == 2 && UrlArray[1] != null && UrlArray[1].Length == 24)
                {
                    string URLData = Decrypt(UrlArray[1]);

                    string[] UrlDataArray = URLData.Split('|');

                    if (UrlDataArray != null && UrlDataArray.Length == 2 && UrlDataArray[0] != null && UrlDataArray[1] != null)
                    {
                        UserID = UrlDataArray[1];
                    }
                    TripStatusChangeRequest request = new TripStatusChangeRequest();
                    request.entity = new TripStatus();
                    request.entity.ReasonID = "0";
                    request.entity.ReasonRemarks = "Cancelled By user";
                    request.entity.StatusCode = "CBU";
                    request.entity.ActionById = UserID;
                    request.entity.UserID = UserID;
                    request.entity.DriverID = "0";
                    request.entity.VehicleID = "0";
                    request.entity.VendorId = "0";

                    if (ReqID.Contains(","))
                    {
                        request.entity.RequestIDs = ReqID;
                    }
                    else
                    {
                        request.entity.RequestID = ReqID;
                    }

                    request.entity.ActionBy = "USR";


                    string RES = ManageTripStatus(request, ref strResult);

                    if (RES != null && RES == "TRUE")
                    {
                        return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserSessionTimeout]
        [HttpPost]
        public JsonResult UploadProfilePic()
        {
            UserViewModel empModel;
            string strFilePath = string.Empty;
            bool Res = false;
            string Msg = string.Empty;
            string strActualFileName = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {

                    if (Session["User"] != null)
                    {
                        HttpFileCollectionBase files = Request.Files;
                        HttpPostedFileBase file = files[0]; // Get only single file at a time
                        empModel = (UserViewModel)Session["User"];
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            strFilePath = testfiles[testfiles.Length - 1];
                        }
                        else
                        {

                            strFilePath = empModel.UserID + "_Profile_Pic_" + file.FileName;
                            strActualFileName = strFilePath;

                        }

                        strFilePath = Path.Combine(Server.MapPath(PRF_PATH), strFilePath);
                        file.SaveAs(strFilePath);
                        Msg = ChangeProfilePicture(strActualFileName, ref Res);
                    }

                }
                catch (Exception err)
                {
                    Msg = "Invalid file format# Error: " + err.Message;
                    strFilePath = "";
                    Res = false;
                }
            }
            else
            {
                Msg = "Please upload invoice";
                strFilePath = "";
                Res = false;
            }

            return Json(new { Result = Res, Message = Msg, FilePath = strActualFileName });

        }

        private string ChangeProfilePicture(string fileName, ref bool Res)
        {
            string strResponse = "";
            try
            {
                if (Session["User"] != null)
                {
                    UserViewModel model = Session["User"] as UserViewModel;
                    User objUser = new User();
                    objUser.UserID = (Session["User"] as UserViewModel).UserID;
                    objUser.ProfilePic = fileName;

                    webAPI = new WebAPICommunicator();
                    strResponse = webAPI.PostRequest(objUser, "U", "updateprofilepicure");
                    if (strResponse.ToUpper() == "TRUE")
                    {
                        Res = true;
                        model.ProfilePic = fileName;
                        Session["User"] = model;
                    }
                    else
                    {
                        Res = false;
                        strResponse = "There is some issue in updating profile picture.";
                    }
                }
                else
                {
                    Res = false;
                    strResponse = "Your session has been expired. Please login to continue.";
                }
            }
            catch (Exception err)
            {
                strResponse = "ERROR";
            }

            return strResponse;

        }

        [UserSessionTimeout]
        [HttpPost]
        public JsonResult UploadDocument()
        {

            string strFilePath = string.Empty;
            bool Res = false;
            string Msg = string.Empty;
            string strActualFileName = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {

                    if (Session["User"] != null)
                    {
                        HttpFileCollectionBase files = Request.Files;
                        HttpPostedFileBase file = files[0]; // Get only single file at a time

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            strFilePath = testfiles[testfiles.Length - 1];
                        }
                        else
                        {

                            strFilePath = file.FileName;


                        }

                        var fileExt = strFilePath.Split('.');
                        string Ext = fileExt[fileExt.Length - 1];

                        strFilePath = "DOCS_" + GetRandomFileName() + "." + Ext;
                        strActualFileName = strFilePath; // send return back to json to store in DB [without path]

                        strFilePath = Path.Combine(Server.MapPath(DOCS_PATH), strFilePath);
                        file.SaveAs(strFilePath);
                        Msg = "TRUE";
                        Res = true;


                    }

                }
                catch (Exception err)
                {
                    Msg = "Invalid file format# Error: " + err.Message;
                    strFilePath = "";
                    Res = false;
                }
            }
            else
            {
                Msg = "Please upload invoice";
                strFilePath = "";
                Res = false;
            }

            return Json(new { Result = Res, Message = Msg, FilePath = strActualFileName });

        }

        private string GetRandomFileName()
        {
            return Guid.NewGuid().ToString().Substring(0, 16).ToUpper().Replace("-", "");
        }

        [UserSessionTimeout]
        [HttpPost]
        public PartialViewResult GetUploadedDocsDetails(string VehicleID, string DriverGuardID)
        {
            webAPI = new WebAPICommunicator();
            UploadedDocsResponse objResponse = new UploadedDocsResponse();
            objResponse.listUploadedDocs = new List<UploadedDocument>();
            UploadedDocument request = new UploadedDocument();
            request.VehicleID = VehicleID != null || VehicleID != "" ? Convert.ToInt32(VehicleID) : 0;
            request.DriverGuardID = DriverGuardID != null || DriverGuardID != "" ? Convert.ToInt32(DriverGuardID) : 0;

            objResponse = webAPI.PostRequest_Object<UploadedDocsResponse, UploadedDocument>(objResponse, request, "I", "getuploadeddocumentlist");
            return PartialView("_ViewUploadedDocuments", objResponse);
        }
    }
}
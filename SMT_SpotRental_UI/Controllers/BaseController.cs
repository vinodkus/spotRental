using SMT.SpotRental.Shared.Entities;
using SMT.SpotRental.UI.APIGateway;
using SMT.SpotRental.UI.Filters;
using SMT.SpotRental.UI.Models;
using SMT.SpotRental.UI.Request;
using SMT.SpotRental.UI.Response;
using SMT.SpotRental.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace SMT.SpotRental.UI.Controllers
{
    public class BaseController : Controller
    {

        WebAPICommunicator webAPI = null;
        public string PRF_PATH = ConfigurationManager.AppSettings["PRF_PATH"];
        public string DOCS_PATH = ConfigurationManager.AppSettings["DOCS_PATH"];
        public string URL_PREF = ConfigurationManager.AppSettings["URL_PREF"];


        public const string FORGETPWD = "forgetpwd";
        public const string GETRATEDETAILS = "getratedetails";
        public const string VEHICLE_BOOKING_REQUEST = "createadhocrequest";
        public const string GET_ADHOC_REQUEST = "getadhocrequest";
        public const string GET_ADHOC_REQUEST_HISTORY = "getadhocrequesthistory";
        public const string MANAGE_TRIP_STATUS = "managetripstatus";
        public const string MANAGE_SEND_EMAILS = "managesendemails";


        public string BEFORE_PICKUP_TIME_FOR_AIRPORT = ConfigurationManager.AppSettings["BEFORE_PICKUP_TIME_FOR_AIRPORT"].ToString();
        public string BEFORE_PICKUP_TIME_FOR_ALL = ConfigurationManager.AppSettings["BEFORE_PICKUP_TIME_FOR_ALL"].ToString();
        public string TRIP_ACTION_LINK = ConfigurationManager.AppSettings["TRIP_ACTION_LINK"].ToString();


        string encryptionkey = "SAUW193BX628TD57";
        //EmployeeViewModel empmodel;
        public BaseController()
        {
            ViewBag.URL_PREF = URL_PREF;
            //if (Session["User"] != null)
            //{
            //    empmodel = new EmployeeViewModel();
            //    empmodel = Session["User"] as EmployeeViewModel;
            //    ViewBag.ProfilePicPath = PRF_PATH;
            //    ViewBag.ProfilePic = !string.IsNullOrEmpty(empmodel.ProfilePic) ? PRF_PATH + empmodel.ProfilePic : PRF_PATH + "PRF_PIC.png";
            //}
        }
        public string RenderPartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult =
                    ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext,
                    viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        public Dictionary<string, object> GetErrorsFromModelState(ref bool isValid)
        {
            var errors = new Dictionary<string, object>();
            foreach (var key in ModelState.Keys)
            {
                // Only send the errors to the client.
                if (ModelState[key].Errors.Count > 0)
                {
                    errors[key] = ModelState[key].Errors;
                    isValid = false;
                }
            }

            return errors;
        }
        public Dictionary<string, object> GetModelKeys()
        {
            var errors = new Dictionary<string, object>();
            foreach (var key in ModelState.Keys)
            {
                errors[key] = ModelState[key];
            }

            return errors;
        }
        public MenuResponse GetMenuDetails(int UserID, string RoleID = "0", string QueryNo="1")
        {
            webAPI = new WebAPICommunicator();
            MenuResponse objMenu = new MenuResponse();
            object[] paramValue = new object[3]; string[] paramName = new string[3] { "UserCred", "RoleID", "QueryNo" };
            paramValue[0] = UserID;
            paramValue[1] = RoleID;
            paramValue[2] = QueryNo;
            objMenu = webAPI.GetResponse<MenuResponse>(objMenu, "U", "getnavigationdetails", paramValue, paramName);
            return objMenu;
        }
        public EmployeeResponse SearchEmployees(string FName = "", string LName = "", string MobileNo = "", string EmployeeCode = "")
        {
            webAPI = new WebAPICommunicator();
            EmployeeResponse objEmployee = new EmployeeResponse();
            objEmployee.userList = new List<User>();
            object[] paramValue = new object[4]; string[] paramName = new string[4] { "EmployeeCode", "FName", "LName", "MobileNo" };
            paramValue[0] = EmployeeCode;
            paramValue[1] = FName;
            paramValue[2] = LName;
            paramValue[3] = MobileNo;
            objEmployee = webAPI.GetResponse<EmployeeResponse>(objEmployee, "U", "searchemployees", paramValue, paramName);
            return objEmployee;
        }
        public TripStatusResponse GetTripStatusList(string DisplayFor, string GroupName="")
        {
            webAPI = new WebAPICommunicator();
            TripStatusResponse objResponse = new TripStatusResponse();
            objResponse.listTripStatus = new List<TripStatus>();
            object[] paramValue = new object[2]; string[] paramName = new string[2] { "DisplayFor", "GroupName" };
            paramValue[0] = DisplayFor;
            paramValue[1] = GroupName;
            objResponse = webAPI.GetResponse<TripStatusResponse>(objResponse, "I", "gettripstatuslist", paramValue, paramName);
            return objResponse;
        }
        [UserSessionTimeout]
        public AdhocResponse GetAdhocList(string FromDate, string ToDate, string EmailID, string StatusCode, string EmployeeCode = null, string ForInterFace = null, string VendorId = "0", string GroupNo = "0")
        {
            AdhocResponse response = new AdhocResponse();
            response.itemsList = new List<RouteItems>();
            if (Session["User"] != null)
            {
                webAPI = new WebAPICommunicator();
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;

                string UserID = empmodel.UserID.ToString();
                object[] paramValue = new object[9]; string[] paramName = new string[9] { "FromDate", "ToDate", "EmailID", "StatusCode", "UserID", "EmployeeCode", "ForInterFace", "VendorId", "GroupNo" };
                paramValue[0] = FromDate;
                paramValue[1] = ToDate;
                paramValue[2] = EmailID;
                paramValue[3] = StatusCode;
                paramValue[4] = UserID;
                paramValue[5] = empmodel.Roles.ToUpper() == "EMPLOYEE" ? empmodel.EmployeeCode : EmployeeCode;
                paramValue[6] = ForInterFace;
                paramValue[7] = VendorId == "0" ? (empmodel.VendorID != null ? empmodel.VendorID : "0") : VendorId;
                paramValue[8] = GroupNo;

                response = webAPI.GetResponse<AdhocResponse>(response, "V", GET_ADHOC_REQUEST, paramValue, paramName);
            }
            return response;
        }
        public AdhocResponse GetAdhocListOutsider(string FromDate, string ToDate, string EmailID, string StatusCode, string EmployeeCode = null, string ForInterFace = null, string VendorId = "0", string GroupNo = "0")
        {
            AdhocResponse response = new AdhocResponse();
            response.itemsList = new List<RouteItems>();

            webAPI = new WebAPICommunicator();
            object[] paramValue = new object[9]; string[] paramName = new string[9] { "FromDate", "ToDate", "EmailID", "StatusCode", "UserID", "EmployeeCode", "ForInterFace", "VendorId", "GroupNo" };
            paramValue[0] = FromDate;
            paramValue[1] = ToDate;
            paramValue[2] = EmailID;
            paramValue[3] = StatusCode;
            paramValue[4] = "0";
            paramValue[5] = EmployeeCode;
            paramValue[6] = ForInterFace;
            paramValue[7] = VendorId;
            paramValue[8] = GroupNo;

            response = webAPI.GetResponse<AdhocResponse>(response, "V", GET_ADHOC_REQUEST, paramValue, paramName);

            return response;
        }
        public string ManageTripStatus(TripStatusChangeRequest request, ref string strResult)
        {
            webAPI = new WebAPICommunicator();
            return webAPI.PostRequest<TripStatusChangeRequest>(request, "V", MANAGE_TRIP_STATUS, ref strResult);
        }
        public string ManageSendingEmail(EmailDetails request, ref string strResult)
        {
            webAPI = new WebAPICommunicator();
            return webAPI.PostRequest(request, "I", MANAGE_SEND_EMAILS, ref strResult);
        }
        [CustomException]
        public string Encrypt(string inputText)
        {
            byte[] keybytes = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] plainText = Encoding.Unicode.GetBytes(inputText);
            PasswordDeriveBytes pwdbytes = new PasswordDeriveBytes(encryptionkey, keybytes);
            using (ICryptoTransform encryptrans = rijndaelCipher.CreateEncryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16)))
            {
                using (MemoryStream mstrm = new MemoryStream())
                {
                    using (CryptoStream cryptstm = new CryptoStream(mstrm, encryptrans, CryptoStreamMode.Write))
                    {
                        cryptstm.Write(plainText, 0, plainText.Length);
                        cryptstm.Close();
                        return Convert.ToBase64String(mstrm.ToArray());
                    }
                }
            }
        }
        [CustomException]
        public string Decrypt(string encryptText)
        {
            string strResult = "";
            if (encryptText != null && encryptText.Length == 24)
            {
                try
                {
                    byte[] keybytes = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
                    RijndaelManaged rijndaelCipher = new RijndaelManaged();
                    byte[] encryptedData = Convert.FromBase64String(encryptText.Replace(" ", "+"));
                    PasswordDeriveBytes pwdbytes = new PasswordDeriveBytes(encryptionkey, keybytes);
                    using (ICryptoTransform decryptrans = rijndaelCipher.CreateDecryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16)))
                    {
                        using (MemoryStream mstrm = new MemoryStream(encryptedData))
                        {
                            using (CryptoStream cryptstm = new CryptoStream(mstrm, decryptrans, CryptoStreamMode.Read))
                            {
                                byte[] plainText = new byte[encryptedData.Length];
                                int decryptedCount = cryptstm.Read(plainText, 0, plainText.Length);
                                strResult = Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                            }
                        }
                    }
                }
                catch
                {

                    RedirectToAction("InvalidAccess", "Message");
                }
            }


            return strResult;

        }
        public EmployeeResponse CheckEmployeeDetails(UserViewModel empModel)
        {
            webAPI = new WebAPICommunicator();
            EmployeeResponse objEmployee = new EmployeeResponse();
            objEmployee.userList = new List<User>();
            object[] paramValue = new object[5]; string[] paramName = new string[5] { "EmployeeCode", "FName", "LName", "MobileNo", "EmailID" };
            paramValue[0] = empModel.EmployeeCode;
            paramValue[1] = "";
            paramValue[2] = "";
            paramValue[3] = empModel.MobileNo;
            paramValue[4] = empModel.EmailID;
            objEmployee = webAPI.GetResponse<EmployeeResponse>(objEmployee, "U", "searchemployees", paramValue, paramName);
            return objEmployee;


        }

        [UserSessionTimeout]
        public VendorMasterResponse SearchVendors(VendorMaster request)
        {
            webAPI = new WebAPICommunicator();
            VendorMasterResponse objResponse = new VendorMasterResponse();
            objResponse.listVendor = new List<VendorMaster>();
            objResponse = webAPI.PostRequest_Object<VendorMasterResponse, VendorMaster>(objResponse, request, "I", "searchvenders");
            return objResponse;
        }

        [UserSessionTimeout]
        public string ChangeVendorsStatus(string VendorID, ref string strResult)
        {
            webAPI = new WebAPICommunicator();
            VendorMaster objRequest = new VendorMaster();
            objRequest.VendorId = VendorID;
            return webAPI.PostRequest<VendorMaster>(objRequest, "I", "changevendorstatus", ref strResult);
        }

        [UserSessionTimeout]
        public string AddUpdateVendor(VendorMaster request, string OperationName, ref string strResult)
        {
            webAPI = new WebAPICommunicator();
            return webAPI.PostRequest<VendorMaster>(request, "I", OperationName == "ADD" ? "registervendor" : "updatevendor", ref strResult);
        }

        [UserSessionTimeout]
        public VehicleMasterResponse SearchVehicles(VehicleMaster request)
        {
            webAPI = new WebAPICommunicator();
            VehicleMasterResponse objResponse = new VehicleMasterResponse();
            objResponse.listVehicle = new List<VehicleMaster>();
            objResponse = webAPI.PostRequest_Object<VehicleMasterResponse, VehicleMaster>(objResponse, request, "I", "searchvehicles");
            return objResponse;
        }
        [UserSessionTimeout]
        public VehicleHistoryResponse GetVehicleHistoryDetails(VehicleHistory request)
        {
            webAPI = new WebAPICommunicator();
            VehicleHistoryResponse objResponse = new VehicleHistoryResponse();
            objResponse.listVehicleHistory = new List<VehicleHistory>();
            objResponse = webAPI.PostRequest_Object<VehicleHistoryResponse, VehicleHistory>(objResponse, request, "I", "getvehiclehistorydetails");
            return objResponse;
        }
        [UserSessionTimeout]
        public string AddUpdateVehicle(VehicleMaster request, string OperationName, ref string strResult)
        {
            webAPI = new WebAPICommunicator();
            return webAPI.PostRequest<VehicleMaster>(request, "I", OperationName == "ADD" ? "registervehicle" : "updatevehicle", ref strResult);
        }
        [UserSessionTimeout]
        public DriverGuardResponse SearchDriverGuard(DriverGuard request)
        {
            webAPI = new WebAPICommunicator();
            DriverGuardResponse objResponse = new DriverGuardResponse();
            objResponse.listDriverGuard = new List<DriverGuard>();
            objResponse = webAPI.PostRequest_Object<DriverGuardResponse, DriverGuard>(objResponse, request, "I", "searchdriverguard");
            return objResponse;
        }

        [UserSessionTimeout]
        public string AddUpdateDriverGuard(DriverGuard request, string OperationName, ref string strResult)
        {
            webAPI = new WebAPICommunicator();
            return webAPI.PostRequest<DriverGuard>(request, "I", OperationName == "ADD" ? "registerdriverguard" : "updatedriverguard", ref strResult);
        }

        [UserSessionTimeout]
        public string DeleteDriverGuard(DriverGuard request, ref string strResult)
        {
            webAPI = new WebAPICommunicator();
            return webAPI.PostRequest<DriverGuard>(request, "I", "deletedriverguard", ref strResult);
        }
    }
}
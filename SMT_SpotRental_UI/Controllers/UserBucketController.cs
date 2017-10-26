using SMT.SpotRental.Shared.Entities;
using SMT.SpotRental.UI.APIGateway;
using SMT.SpotRental.UI.Filters;
using SMT.SpotRental.UI.Models;
using SMT.SpotRental.UI.Request;
using SMT.SpotRental.UI.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMT.SpotRental.UI.Controllers
{
    [NoDirectAccess]
    public class UserBucketController : BaseController
    {
        #region VARIABLE AND OBJECTS
        DashboardViewModel modelDashboard;
        WebAPICommunicator webAPI;
        UserViewModel empmodel;
       
        #endregion



        public UserBucketController()
        {
           
        }
        #region SPOT FUNCTIONALITY

        [UserSessionTimeout]
        public ActionResult Index()
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            modelDashboard = new DashboardViewModel();
            modelDashboard.userDetails = new UserViewModel();
            modelDashboard.userDetails = empmodel;
            ViewBag.ProfilePicPath = PRF_PATH;
            ViewBag.ProfilePic = !string.IsNullOrEmpty(empmodel.ProfilePic) ? PRF_PATH + empmodel.ProfilePic : PRF_PATH + "PRF_PIC.png";
            ViewBag.TempPassword = modelDashboard.userDetails.TempPwd;
            modelDashboard.objMenu = GetMenuDetails(modelDashboard.userDetails.UserID);
            ViewBag.Roles= modelDashboard.userDetails.Roles.ToUpper();
            TempData["RouteCount"] = null;
            return View("AdhocRequest", modelDashboard);

        }

        [UserSessionTimeout]
        public ActionResult ViewAdhocRequest()
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            modelDashboard = new DashboardViewModel();
            modelDashboard.userDetails = new UserViewModel();
            modelDashboard.userDetails = empmodel;
            ViewBag.ProfilePicPath = PRF_PATH;
            ViewBag.ProfilePic = !string.IsNullOrEmpty(empmodel.ProfilePic) ? PRF_PATH + empmodel.ProfilePic : PRF_PATH + "PRF_PIC.png";
            modelDashboard.objMenu = GetMenuDetails(modelDashboard.userDetails.UserID);
            ViewBag.FromDate = DateTime.Now.AddDays(-30).ToString("MM-dd-yyyy");
            ViewBag.ToDate = DateTime.Now.ToString("MM-dd-yyyy");
            return View("SearchAdhocRequest", modelDashboard);
        }

        [UserSessionTimeout]
        [HttpGet]
        public PartialViewResult GetAdhocRequestList(string FromDate, string ToDate, string EmailID, string StatusCode)
        {
            AdhocResponse response = new AdhocResponse();
            response.itemsList = new List<RouteItems>();
            response = GetAdhocList(FromDate, ToDate, EmailID, StatusCode);
            return PartialView("_ViewAdhocRequest", response);

        }

        [UserSessionTimeout]
        [HttpGet]
        public PartialViewResult GetAdhocRequestHistory(string RequestID)
        {
            AdhocResponse response = new AdhocResponse();
            response.itemsList = new List<RouteItems>();
            response = GetAdhocHistory(RequestID);
            return PartialView("_ViewAdhocHistory", response);

        }
        private AdhocResponse GetAdhocHistory(string RequestID)
        {
            AdhocResponse response = new AdhocResponse();
            response.itemsList = new List<RouteItems>();
            if (Session["User"] != null)
            {
                webAPI = new WebAPICommunicator();
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;

                string UserID = empmodel.UserID.ToString();
                object[] paramValue = new object[1]; string[] paramName = new string[1] { "RequestID" };
                paramValue[0] = RequestID;

                response = webAPI.GetResponse<AdhocResponse>(response, "V", GET_ADHOC_REQUEST_HISTORY, paramValue, paramName);
            }
            return response;
        }

        [UserSessionTimeout]
        [HttpPost]
        public PartialViewResult RenderRouteDetail()
        {

            modelDashboard = new DashboardViewModel();
            modelDashboard.routeDetails = new RouteDetailsList();
            modelDashboard.routeDetails.routeList = new List<RouteDetails>();
            if (TempData["RouteDetail"] != null && Session["User"] != null)
            {
                TempData.Keep("RouteDetail");
            }
            if (TempData["RouteCount"] != null && Session["User"] != null)
            {
                TempData["RouteCount"] = Convert.ToInt32(TempData["RouteCount"]) + 1;
                TempData.Keep("RouteCount");
            }
            else
            {
                TempData["RouteCount"] = 0;
            }
            return PartialView("_RouteDetail", modelDashboard);
        }

        [UserSessionTimeout]
        [HttpGet]
        public JsonResult GetSourceAndDestinationWithRate(string RentalCityName, string CarType, string RouteType)
        {
            SourceDestinationRentalResponse response = GetRateDetails(RentalCityName, CarType, RouteType);
            if (response != null && response.locationList != null && response.locationList.Count > 0)
            {
                return Json(new { Result = true, List = response }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }
        private SourceDestinationRentalResponse GetRateDetails(string RentalCityName, string CarType, string RouteType)
        {
            SourceDestinationRentalResponse objResponse = new SourceDestinationRentalResponse();
            if (Session["User"] != null)
            {
                webAPI = new WebAPICommunicator();
                UserViewModel empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                objResponse.locationList = new List<SourceDestination>();

                string UserID = empmodel.UserID.ToString();
                object[] paramValue = new object[4]; string[] paramName = new string[4] { "UserID", "RentalCityName", "CarType", "RouteType" };
                paramValue[0] = UserID;
                paramValue[1] = RentalCityName;
                paramValue[2] = CarType;
                paramValue[3] = RouteType;

                objResponse = webAPI.GetResponse<SourceDestinationRentalResponse>(objResponse, "I", GETRATEDETAILS, paramValue, paramName);
            }
            return objResponse;
        }

        [UserSessionTimeout]
        [HttpGet]
        public JsonResult GetEmployeeDetails(string EmployeeCode)
        {
            EmployeeResponse objEmployee = new EmployeeResponse();
            objEmployee.userList = new List<User>();
            objEmployee = SearchEmployees("", "", "", EmployeeCode);
            if (objEmployee != null && objEmployee.userList != null && objEmployee.userList.Count > 0)
            {
                return Json(new { Result = true, List = objEmployee }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserSessionTimeout]
        [HttpPost]
        public JsonResult CreateAdhocRequest(List<RouteItems> requestList, AdhocRequestFor userDetails)
        {
            string strValidateResult = ValidatedRequest(requestList, userDetails);
            if (strValidateResult == "TRUE")
            {
                string response = AddRequest(requestList, userDetails);
                if (response.Contains("TRUE"))
                {
                    SendEmail(response, userDetails);
                    return Json(new { Result = true, Msg = "TRUE" }, JsonRequestBehavior.AllowGet);
                }
                else if (response.Contains("DUPLICATE"))
                {
                    return Json(new { Result = false, Msg = "DUPLICATE" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = false, Msg = "FALSE" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Result = false, Msg = strValidateResult }, JsonRequestBehavior.AllowGet);
            }
        }
        private string SendEmail(string response, AdhocRequestFor userDetails)
        {
            string strResponse = "";
            try
            {
                string[] arrayResponse = response.Split('-');
                if (arrayResponse.Length == 2)
                {
                    string EncryptedUserData = Encrypt(arrayResponse[1]);
                    EncryptedUserData = TRIP_ACTION_LINK + EncryptedUserData;
                    EmailDetails objEmail = new EmailDetails();
                    objEmail.ActionLink = EncryptedUserData;
                    objEmail.UserEmailID = userDetails.EmailID;
                    objEmail.EmailType = "SPOT BOOKING";
                    objEmail.EmailTemplateCode = "TD";
                    objEmail.UserName = userDetails.UserName;
                    objEmail.EmailBody = "";
                    objEmail.EmailSubject = "";
                    objEmail.UserMobile = userDetails.MobileNo;
                    objEmail.QueryNo = "3";
                    ManageSendingEmail(objEmail, ref strResponse);
                }
            }
            catch
            {
                strResponse = "";
            }

            return strResponse;
        }
        private string ValidatedRequest(List<RouteItems> requestList, AdhocRequestFor userDetails)
        {
            string strResult = "TRUE";
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            string UserID = empmodel.UserID.ToString();
            userDetails.RequestBy = UserID;

            // If source is [HOME] for any routes then update this by employee's [HomeAddress]
            requestList.Where(w => w.Source.ToUpper() == "HOME").ToList().ForEach(i => i.Source = userDetails.HomeAddress);

            // If destination is [HOME] for any routes then update this by employee's [HomeAddress]
            requestList.Where(w => w.Destination.ToUpper() == "HOME").ToList().ForEach(i => i.Destination = userDetails.HomeAddress);

            // Set Pickup time
            requestList.ForEach(i => i.PickUpTime = Convert.ToDateTime(i.ReportingDate + " " + i.ReportingTime));

            // Check whether pickup time is greater than 6 HRS [or as per configuration]  for Airport from HOME and other normal time constraints
            foreach (var Items in requestList)
            {
                if ((Items.Source.ToUpper() == "HOME" && Items.Destination.ToUpper().Contains("AIRPORT")) || (Items.Destination.ToUpper() == "HOME" && Items.Source.ToUpper().Contains("AIRPORT")))
                {
                    if (Items.PickUpTime.Subtract(DateTime.Now).Days <= 0 && (Items.PickUpTime.Subtract(DateTime.Now)).Hours < Convert.ToInt32(BEFORE_PICKUP_TIME_FOR_AIRPORT))
                    {
                        strResult = "Invalid Pickup Time for Home/Airport. It must greater or equal to " + BEFORE_PICKUP_TIME_FOR_AIRPORT + " hours from time of booking.";
                        break;
                    }
                }
                else if (Items.PickUpTime.Subtract(DateTime.Now).Days <= 0 && (Items.PickUpTime.Subtract(DateTime.Now)).Hours < Convert.ToInt32(BEFORE_PICKUP_TIME_FOR_ALL))
                {
                    strResult = "Invalid Pickup Time. It must greater or equal to " + BEFORE_PICKUP_TIME_FOR_ALL + " hours from time of booking.";
                    break;
                }

            }


            return strResult;
        }
        private string AddRequest(List<RouteItems> requestList, AdhocRequestFor userDetails)
        {
            webAPI = new WebAPICommunicator();

            AdhocRequest request = new AdhocRequest();

            request.routeList = new List<RouteItems>();
            request.routeList = requestList;
            request.adhocRequest = new AdhocRequestFor();
            request.adhocRequest = userDetails;
            AdhocResponse response = new AdhocResponse();
            response = webAPI.PostRequest_Object(response, request, "V", VEHICLE_BOOKING_REQUEST);
            return response.Message;
        }

        [UserSessionTimeout]
        [HttpGet]
        public JsonResult CancelTrip(string ReqID, string StatusCode, string ReasonID, string Remarks)
        {
            string strResult = "";

            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            string UserID = empmodel.UserID.ToString();

            TripStatusChangeRequest request = new TripStatusChangeRequest();
            request.entity = new TripStatus();

            if (ReasonID.Contains(","))
            {
                request.entity.RequestIDs = ReasonID;
            }
            else
            {
                request.entity.ReasonID = ReasonID;
            }
            request.entity.ReasonRemarks = Remarks;
            request.entity.StatusCode = StatusCode;
            request.entity.ActionById = UserID;
            request.entity.RequestID = ReqID;
            request.entity.ActionBy = "SPOT";


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


        #endregion

        #region SUPERVISOR FUNCTIONALITY

        [UserSessionTimeout]
        public ActionResult SupervisorBucket()
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            modelDashboard = new DashboardViewModel();
            modelDashboard.userDetails = new UserViewModel();
            modelDashboard.userDetails = empmodel;
            modelDashboard.objMenu = GetMenuDetails(modelDashboard.userDetails.UserID);
            ViewBag.FromDate = DateTime.Now.AddDays(-30).ToString("MM-dd-yyyy");
            ViewBag.ToDate = DateTime.Now.ToString("MM-dd-yyyy");
            return View("SupervisorBucket", modelDashboard);
        }

        [UserSessionTimeout]
        public PartialViewResult RenderSupervisorApprovalList()
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            modelDashboard = new DashboardViewModel();
            modelDashboard.userDetails = new UserViewModel();
            modelDashboard.userDetails = empmodel;

            AdhocResponse response = new AdhocResponse();
            response.itemsList = new List<RouteItems>();
            response = GetAdhocList(DateTime.Now.ToString(), DateTime.Now.ToString(), "", "PFA", null, "SUP", "0");
            return PartialView("_ViewSupervisorApprovalList", response);
        }

        [UserSessionTimeout]
        public PartialViewResult RenderSupervisorApprovedList()
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            modelDashboard = new DashboardViewModel();
            modelDashboard.userDetails = new UserViewModel();
            modelDashboard.userDetails = empmodel;

            AdhocResponse response = new AdhocResponse();
            response.itemsList = new List<RouteItems>();
            response = GetAdhocList(DateTime.Now.ToString(), DateTime.Now.ToString(), "", "VA", null, "SUP", "0");
            return PartialView("_ViewSupervisorApprovedList", response);
        }


        [UserSessionTimeout]
        public PartialViewResult RenderSupervisorRejectedList()
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            modelDashboard = new DashboardViewModel();
            modelDashboard.userDetails = new UserViewModel();
            modelDashboard.userDetails = empmodel;

            AdhocResponse response = new AdhocResponse();
            response.itemsList = new List<RouteItems>();
            response = GetAdhocList(DateTime.Now.ToString(), DateTime.Now.ToString(), "", "RFS", null, "SUP", "0");
            return PartialView("_ViewSupervisorRejectedList", response);
        }

        [UserSessionTimeout]
        [HttpGet]
        public JsonResult ApprovedTrip(string ReqID)
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;

            TripStatusChangeRequest request = new TripStatusChangeRequest();
            request.entity = new TripStatus();
            request.entity.ReasonID = "0";
            request.entity.ReasonRemarks = "Approved by supervisor";
            request.entity.StatusCode = "AFS";
            request.entity.ActionById = empmodel.UserID.ToString();
            request.entity.RequestID = ReqID;
            request.entity.ActionBy = "SUP";
            request.entity.VendorId = "0";

            string strResult = "";
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

        [UserSessionTimeout]
        [HttpGet]
        public JsonResult RejectTripBySupervisor(string ReqID)
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;

            TripStatusChangeRequest request = new TripStatusChangeRequest();
            request.entity = new TripStatus();
            request.entity.ReasonID = "0";
            request.entity.ReasonRemarks = "Rejected by supervisor";
            request.entity.StatusCode = "RFS";
            request.entity.ActionById = empmodel.UserID.ToString();
            request.entity.RequestID = ReqID;
            request.entity.ActionBy = "SUP";
            request.entity.VendorId = "0";

            string strResult = "";
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

        #endregion

        #region VENDOR FUNCTIONALITY
        [UserSessionTimeout]
        public ActionResult VendorAction()
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            modelDashboard = new DashboardViewModel();
            modelDashboard.userDetails = new UserViewModel();
            modelDashboard.userDetails = empmodel;
            ViewBag.ProfilePicPath = PRF_PATH;
            ViewBag.ProfilePic = !string.IsNullOrEmpty(empmodel.ProfilePic) ? PRF_PATH + empmodel.ProfilePic : PRF_PATH + "PRF_PIC.png";
            ViewBag.TempPassword = modelDashboard.userDetails.TempPwd;
            modelDashboard.objMenu = GetMenuDetails(modelDashboard.userDetails.UserID);
            return View("VendorBucket", modelDashboard);

        }

        [UserSessionTimeout]
        [CheckVendorAccess]
        public PartialViewResult RenderVendorBucketForAccept()
        {
            string VendorId = "0";
            UserViewModel empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;

            VendorId = empmodel.VendorID.ToString();
            AdhocResponse response = new AdhocResponse();
            response.itemsList = new List<RouteItems>();
            response = GetAdhocList(DateTime.Now.ToString(), DateTime.Now.ToString(), "", "VA", null, "VEN", VendorId);

            return PartialView("_ViewAdhocRequestForVendorApproval", response);
        }

        [UserSessionTimeout]
        [CheckVendorAccess]
        public PartialViewResult RenderAcceptedVendorBucket()
        {
            string VendorId = "0";
            UserViewModel empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;

            VendorId = empmodel.VendorID.ToString();
            AdhocResponse response = new AdhocResponse();
            response.itemsList = new List<RouteItems>();
            response = GetAdhocList(DateTime.Now.ToString(), DateTime.Now.ToString(), "", "AC", null, "VEN", VendorId);

            return PartialView("_ViewAdhocRequestAcceptedByVendor", response);
        }

        [UserSessionTimeout]
        [CheckVendorAccess]
        public PartialViewResult RenderRejectedVendorBucket()
        {
            string VendorId = "0";
            UserViewModel empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;

            VendorId = empmodel.VendorID.ToString();
            AdhocResponse response = new AdhocResponse();
            response.itemsList = new List<RouteItems>();
            response = GetAdhocList(DateTime.Now.ToString(), DateTime.Now.ToString(), "", "RJ", null, "VEN", VendorId);

            return PartialView("_ViewAdhocRequesRejectedByVendor", response);
        }

        [UserSessionTimeout]
        [CheckVendorAccess]
        [HttpGet]
        public JsonResult AcceptTrip(string ReqID)
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;

            TripStatusChangeRequest request = new TripStatusChangeRequest();
            request.entity = new TripStatus();
            request.entity.ReasonID = "0";
            request.entity.ReasonRemarks = "";
            request.entity.StatusCode = "AC";
            request.entity.ActionById = empmodel.UserID.ToString();
            request.entity.RequestID = ReqID;
            request.entity.ActionBy = "VEN";
            request.entity.VendorId = empmodel.VendorID;

            string strResult = "";
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

        [UserSessionTimeout]
        [CheckVendorAccess]
        [HttpGet]
        public JsonResult RejectTrip(string ReqID, string StatusCode, string ReasonID, string ReasonRemarks)
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;

            TripStatusChangeRequest request = new TripStatusChangeRequest();
            request.entity = new TripStatus();
            request.entity.ReasonID = ReasonID;
            request.entity.ReasonRemarks = ReasonRemarks;
            request.entity.StatusCode = StatusCode;
            request.entity.ActionById = empmodel.UserID.ToString();
            request.entity.RequestID = ReqID;
            request.entity.ActionBy = "VEN";
            request.entity.VendorId = empmodel.VendorID;

            string strResult = "";
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

        [UserSessionTimeout]
        [CheckVendorAccess]
        [HttpGet]
        public JsonResult AssignVehicleAndDriver(string ReqID, string StatusCode, string VehicleID, string DriverID, string GuardID, string ReasonID, string ReasonRemarks)
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;

            TripStatusChangeRequest request = new TripStatusChangeRequest();
            request.entity = new TripStatus();
            request.entity.StatusCode = StatusCode;
            request.entity.ActionById = empmodel.UserID.ToString();
            request.entity.RequestID = ReqID;
            request.entity.ActionBy = "VEN";
            request.entity.DriverID = DriverID;
            request.entity.VehicleID = VehicleID;
            request.entity.GuardID = GuardID;
            request.entity.ReasonID = ReasonID;
            request.entity.ReasonRemarks = ReasonRemarks;

            string strResult = "";
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


        #endregion

        #region USER PROFILE
        public ActionResult UserProfile()
        {
            empmodel = new UserViewModel();
            empmodel = Session["User"] as UserViewModel;
            modelDashboard = new DashboardViewModel();
            ViewBag.ProfilePicPath = PRF_PATH;
            ViewBag.ProfilePic = !string.IsNullOrEmpty(empmodel.ProfilePic) ? PRF_PATH + empmodel.ProfilePic : PRF_PATH + "PRF_PIC.png";
            modelDashboard.userDetails = new UserViewModel();
            modelDashboard.userDetails = empmodel;
            ViewBag.TempPassword = modelDashboard.userDetails.TempPwd;
            modelDashboard.objMenu = GetMenuDetails(modelDashboard.userDetails.UserID);
            ViewBag.OfficeLocation = empmodel.OfficeLocation; // seletedd Base/Office Address
            return View("UserProfile", modelDashboard);
        }

        [UserSessionTimeout]
        [HttpPost]
        public JsonResult UpdateUserProfile(UserViewModel empmodelToUpdate)
        {
            if (empmodelToUpdate != null && !string.IsNullOrEmpty(empmodelToUpdate.EmailID))
            {
                empmodel = new UserViewModel();
                empmodel = Session["User"] as UserViewModel;
                empmodelToUpdate.EmployeeCode = empmodel.EmployeeCode;
                empmodelToUpdate.UserID = empmodel.UserID;

                webAPI = new WebAPICommunicator();
                EmployeeResponse objEmployee = new EmployeeResponse();
                objEmployee.userList = new List<User>();
                string Message = "";
                bool result = true;
                objEmployee = CheckEmployeeDetails(empmodelToUpdate);
                if (objEmployee != null && objEmployee.userList != null && objEmployee.userList.Count > 0)
                {
                    foreach (var items in objEmployee.userList)
                    {
                        if (items.EmployeeCode.ToUpper() != empmodelToUpdate.EmployeeCode.ToUpper() && items.MobileNo.Trim() == empmodelToUpdate.MobileNo.Trim())
                        {
                            Message = "This mobile no is already exist.";
                            result = false;
                            break;
                        }
                        else if (items.EmployeeCode.ToUpper() != empmodelToUpdate.EmployeeCode.ToUpper() && !string.IsNullOrEmpty(items.EmailID) && items.EmailID.ToUpper() == empmodelToUpdate.EmailID.ToUpper())
                        {
                            Message = "This email id is already exist.";
                            result = false;
                            break;
                        }
                    }

                }
                if (result)
                {

                    Message = UpdateProfile(empmodelToUpdate, ref result);
                    return Json(new { Result = result, Msg = Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = result, Msg = Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Result = false, Msg = "Invalid data to update." }, JsonRequestBehavior.AllowGet);
            }
        }

        [UserSessionTimeout]
        private string UpdateProfile(UserViewModel profile, ref bool result)
        {
            string Message = "";
            webAPI = new WebAPICommunicator();
            EmployeeResponse response = new EmployeeResponse();
            response.OtherMessages = new string[3];
            try
            {
                response = webAPI.PostRequest_Object<EmployeeResponse, UserViewModel>(response, profile, "U", "addemployee");
                if (response != null && response.Message == "UPDATED")
                {
                    result = true;
                    Message = "TRUE.";
                }
                else
                {
                    Message = "Unable to update profile. Please contact to admin.";
                    result = false;
                }
            }
            catch
            {

            }
            return Message;
        }

        #endregion

    }
}
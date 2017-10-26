using System.Web.Mvc;
using SMT.SpotRental.UI.Models;
using SMT.SpotRental.UI.APIGateway;
using SMT.SpotRental.UI.Response;
using SMT.SpotRental.Shared.Entities;
using System.Collections.Generic;
using SMT.SpotRental.UI.MessageService;

namespace SMT.SpotRental.UI.Controllers
{
    public class AccountController : BaseController
    {


        WebAPICommunicator webAPI = null;
        public AccountController()
        {
        }

        public ActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
            return View("Login", model);
        }

        public ActionResult AddEmployee(UserViewModel empModel)
        {
            string strMessage = "";
            bool chkDuplicate = CheckDuplicateEmployee(empModel, ref strMessage);
            if (chkDuplicate)
            {
                return Json(new
                {
                    IsDuplicate = chkDuplicate,
                    Result = false,
                    Message = strMessage
                });
            }
            else
            {
                bool res = RegisterEmployee(empModel, ref strMessage);
                return Json(new
                {
                    IsDuplicate = false,
                    Result = res,
                    Message = strMessage
                });
            }
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel lgnModel)
        {
            webAPI = new WebAPICommunicator();
            UserResponse objResponse = new UserResponse();
            object[] paramValue = new object[2]; string[] paramName = new string[2] { "UserCred", "Password" };
            paramValue[0] = lgnModel.Email;
            paramValue[1] = lgnModel.Password;

            objResponse = webAPI.GetResponse<UserResponse>(objResponse, "U", "validateuserlogin", paramValue, paramName);
            if (objResponse != null && objResponse.userEntity != null)
            {
                if (objResponse.ResultId == 0)
                {
                    lgnModel.ErrorMessage = "Invalid login credential supplied.";
                    return View("Login", lgnModel);
                }
                else if (objResponse.ResultId == 1)
                {
                    UserViewModel empModel = new UserViewModel()
                    {
                        CostCenter = objResponse.userEntity.CostCenter,
                        CreditCard = objResponse.userEntity.CreditCard,
                        DesignationName = objResponse.userEntity.DesignationName,
                        DesignationID = objResponse.userEntity.DesignationID,
                        EmailID = objResponse.userEntity.EmailID,
                        EmployeeCode = objResponse.userEntity.EmployeeCode,
                        FName = objResponse.userEntity.FName,
                        Gender = objResponse.userEntity.Gender,
                        HomeAddress = objResponse.userEntity.HomeAddress,
                        Latitude = objResponse.userEntity.Latitude,
                        LName = objResponse.userEntity.LName,
                        Longitude = objResponse.userEntity.Longitude,
                        MName = objResponse.userEntity.MName,
                        MobileNo = objResponse.userEntity.MobileNo,
                        OfficeLocation = objResponse.userEntity.OfficeLocation,
                        Roles = objResponse.userEntity.Roles,
                        SupervisorID = objResponse.userEntity.SupervisorID,
                        SupervisorName = objResponse.userEntity.SupervisorName,
                        TempPwd = objResponse.userEntity.TempPwd,
                        UserType = objResponse.userEntity.UserType,
                        VendorID = objResponse.userEntity.VendorID,
                        UserID = objResponse.userEntity.UserID,
                        ProfilePic= objResponse.userEntity.ProfilePic


                    };

                    Session["User"] = empModel;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    lgnModel.ErrorMessage = "There are some issue with API.";
                    return View("Login", lgnModel);
                }
            }
            else
            {
                ViewBag.Error = "Invalid login credential supplied.";
                return View("Login", lgnModel);
            }
        }

        private bool CheckDuplicateEmployee(UserViewModel empModel, ref string Message)
        {
            bool result = false;
            webAPI = new WebAPICommunicator();
            EmployeeResponse objEmployee = new EmployeeResponse();
            objEmployee.userList = new List<User>();
            objEmployee=CheckEmployeeDetails(empModel);
            if (objEmployee != null && objEmployee.userList != null && objEmployee.userList.Count > 0)
            {
                foreach (var items in objEmployee.userList)
                {
                    if (!string.IsNullOrEmpty(items.EmployeeCode) && items.EmployeeCode.ToUpper() == empModel.EmployeeCode.ToUpper())
                    {
                        Message = "This employee code is already exist.";
                        result = true;
                        break;
                    }
                    else if (!string.IsNullOrEmpty(items.MobileNo) && items.MobileNo.ToUpper() == empModel.MobileNo.ToUpper())
                    {
                        Message = "This mobile no is already exist.";
                        result = true;
                        break;
                    }
                    else if (!string.IsNullOrEmpty(items.EmailID) && items.EmailID.ToUpper() == empModel.EmailID.ToUpper())
                    {
                        Message = "This email id is already exist.";
                        result = true;
                        break;
                    }


                }
            }
                return result;
        }

        private bool RegisterEmployee(UserViewModel empModel, ref string Message)
        {
            bool result = false;
            webAPI = new WebAPICommunicator();
            EmployeeResponse response = new EmployeeResponse();
            response.OtherMessages = new string[3];
            try
            {
                response = webAPI.PostRequest_Object<EmployeeResponse, UserViewModel>(response, empModel, "U", "addemployee");
                if (response != null && response.Message == "TRUE")
                {

                    result = true;
                    Message = "Your profile has been created successfully. Please Check your mail to login";
                    //if (response.OtherMessages.Length >= 2)
                    //{
                    //    MailServices mailObj = new MailServices();
                    //    bool blSendEmail = mailObj.SendMail(response.OtherMessages[2], "ER");
                    //}

                }
                else
                {
                    Message = "Unable to register. Please contact to admin.";
                    result = false;
                }
            }
            catch
            {

            }
            return result;
        }

        [HttpGet]
        public JsonResult ForgetPassword(string loginCredentials)
        {
            webAPI = new WebAPICommunicator();
            object[] paramValue = new object[1]; string[] paramName = new string[1] { "EmailID" };
            paramValue[0] = loginCredentials;

            string strResult = webAPI.GetResponse("U", "forgetpwd", paramValue, paramName);
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
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }
        }

      

    }
}
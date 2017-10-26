using SMT.SpotRental.Shared.Entities;
using SMT.SpotRental.UI.APIGateway;
using SMT.SpotRental.UI.Models;
using SMT.SpotRental.UI.Response;
using System.Configuration;
using System.Web.Mvc;

namespace SMT.SpotRental.UI.Controllers
{
    public class DashboardController : BaseController
    {
        WebAPICommunicator webAPI = null;
        string PRF_PATH = ConfigurationManager.AppSettings["PRF_PATH"];
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel= Session["User"] as UserViewModel;
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
                    return View("Dashboard", modelDashboard);
                }
            }
            else
            {
                return RedirectToAction("InvalidAccess", "Message");
            }
        }
        public ActionResult SignOut()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public JsonResult ChangePassword(string strPwd)
        {
            string strRes = ChangePWD(strPwd);
            return Json(new { Result = strRes != null && strRes == "TRUE" ? true : false }, JsonRequestBehavior.AllowGet);
        }

        private string ChangePWD(string strPWD)
        {

            User objUser = new User();
            objUser.Password = strPWD;
            objUser.UserID = (Session["User"] as UserViewModel).UserID;
            objUser.EmailID = (Session["User"] as UserViewModel).EmailID;

            webAPI = new WebAPICommunicator();
            string strResult = webAPI.PostRequest(objUser, "U", "changepassword");
            return strResult;
        }
    }
}
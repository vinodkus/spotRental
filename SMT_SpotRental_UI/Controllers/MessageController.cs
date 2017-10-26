using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMT_Amazon_UI.Controllers.ControllerBase
{
    public class MessageController : Controller
    {
        [AllowAnonymous]

        public ActionResult ContactUs()
        {
            return View("Thankyou");
        }
        public ActionResult SessionExpire()
        {
            return View("SessionExpire");
        }
        public ActionResult InvalidAccess()
        {
            Session.Clear();
            return View("InvalidAccess");
        }
        public ActionResult Error()
        {
            Session.Clear();
            return View("Error");
        }
    }
}
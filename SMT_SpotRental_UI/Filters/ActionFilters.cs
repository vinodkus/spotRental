using SMT.SpotRental.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMT.SpotRental.UI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoDirectAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (HttpContext.Current.Session["User"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Message", action = "InvalidAccess", area = "" }));
                }
            }
            catch
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Message", action = "InvalidAccess", area = "" }));

            }
        }
    }
    public class UserSessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["User"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                              RouteValueDictionary(new { controller = "Message", action = "SessionExpire", area = "" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class CheckVendorAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["User"] != null)
            {
                UserViewModel empmodel = new UserViewModel();
                empmodel = HttpContext.Current.Session["User"] as UserViewModel;
                if (empmodel.VendorID == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Message", action = "InvalidAccess", area = "" }));
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                                             RouteValueDictionary(new { controller = "Message", action = "SessionExpire", area = "" }));
            }
            base.OnActionExecuting(filterContext);
        }


    }

    public class InputCheckerAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (filterContext.HttpContext.Request.QueryString["TripData"] == null)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Message", action = "InvalidAccess", area = "" }));
        //    }
        //    else if (filterContext.HttpContext.Request.QueryString["TripData"] == "")
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Message", action = "InvalidAccess", area = "" }));
        //    }

        //}
    }

    public class CustomExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled )
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Message", action = "Error", area = "" }));
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
using Shopper.Models.ViewDetailModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopper.Controllers
{
    public class BaseController : Controller
    {

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var cookies = Request.Cookies["KeepMeLogin"];
            if (Session[Common.Constans.SESSION_LOGIN_USER] == null && cookies != null)
            {
                var userSession = new UserSession();
                userSession.UserName = cookies.Values["UserName"].ToString();
                userSession.UserID = long.Parse(cookies.Values["ID"].ToString());
                Session.Add(Common.Constans.SESSION_LOGIN_USER, userSession);
            }
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}
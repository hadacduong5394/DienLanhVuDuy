using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopper.Controllers
{
    public class LogoutController : Controller
    {
        //
        // GET: /Logout/
        public ActionResult Logout()
        {
            var session = Session[Shopper.Common.Constans.SESSION_LOGIN_USER] as Shopper.Models.ViewDetailModel.UserSession;
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }

            Session[Common.Constans.SESSION_LOGIN_USER] = null;
            if (Request.Cookies["KeepMeLogin"] != null)
            {
                HttpCookie accCookies = new HttpCookie("KeepMeLogin");
                accCookies.Expires = System.DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(accCookies);
            }

            return Redirect("/");
        }
	}
}
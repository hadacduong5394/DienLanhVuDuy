using Shopper.Areas.Admin.Models.ViewDetailModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.DAO;
using Shopper.Common;
using Shopper.Areas.Admin.Models;


namespace Shopper.Areas.Admin.Controllers
{
    public class RegestorController : Controller
    {
        UserDAO dao = new UserDAO();
        //
        // GET: /Admin/Regestor/
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (Session[Common.Constans.USER_SESSION] != null)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ModelLogin model)
        {
            if (ModelState.IsValid)
            {
                var res = dao.loginAdmin(model.account, Common.Encrytor.Encrypt(model.password));
                if (res)
                {
                    var user = dao.getByName(model.account);
                    var roleName = dao.getPrivilege(user.ID);
                    var userSession = new LoginSession();
                    userSession.UserID = user.ID;
                    userSession.UserName = user.UserName;
                    userSession.RoleName = roleName;
                    Session.Add(Common.Constans.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng Nhập Thất Bại");
                }


            }
            return View(model);
        }

        public ActionResult Logout()
        {
            if (Session[Common.Constans.USER_SESSION] == null)
            {
                return RedirectToAction("Login", "Regestor");
            }

            Session[Common.Constans.USER_SESSION] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
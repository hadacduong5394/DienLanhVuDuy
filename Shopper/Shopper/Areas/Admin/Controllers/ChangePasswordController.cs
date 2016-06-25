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
    public class ChangePasswordController : BaseController
    {
        UserDAO daoUser = new UserDAO();
        //
        // GET: /Admin/ChangePassword/
        public ActionResult Index()
        {
            ChangepasswordModel model = new ChangepasswordModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ChangepasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var session = Session[Common.Constans.USER_SESSION] as LoginSession;
                var user = daoUser.getByName(session.UserName);
                model.newPassword = Common.Encrytor.Encrypt(model.newPassword);
                if (daoUser.changePassword(user.ID, model.newPassword))
                {
                    SetAlert("Đổi mật khẩu thành công", "success");
                    Session[Common.Constans.USER_SESSION] = null;
                    return RedirectToAction("Login", "Regestor");
                }
                else
                {
                    SetAlert("Đổi mật khẩu thất bại, hãy thử lại", "error");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
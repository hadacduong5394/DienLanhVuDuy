using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.DAO;
using Shopper.Models.EF;
using Shopper.Models.ViewDetailModel;
using Shopper.Common;
using BotDetect.Web.Mvc;
using Facebook;
using System.Configuration;
using Shopper.Areas.Admin.Models;

namespace Shopper.Controllers
{
    public class UserController : BaseController
    {
        UserDAO daoUser = new UserDAO();
        //
        // GET: /User/

        [HttpGet]
        public ActionResult CreateNew()
        {
            var session = Session[Shopper.Common.Constans.SESSION_LOGIN_USER] as Shopper.Models.ViewDetailModel.UserSession;
            if (session != null)
            {
                SetAlert("Bạn đã đăng nhập rồi!", "warning");
                return Redirect("/");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult CreateNew(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                if (!daoUser.checkUserName(model.userName))
                {
                    ModelState.AddModelError("", "tài khoản này đã tồn tại");
                    return View(model);
                }
                user.UserName = model.userName;
                user.FullName = model.name;
                user.PassWord = Common.Encrytor.Encrypt(model.password);
                user.Phone = model.phoneNumber;
                user.PrivateID = 2;
                user.Status = true;
                user.CreateDate = DateTime.Now;
                user.CreateBy = "hadacduong";
                if (!daoUser.checkUserEmail(model.email))
                {
                    ModelState.AddModelError("", "email này đã tồn tại");
                    return View(model);
                }
                user.Email = model.email;
                user.Address = model.address;
                if (daoUser.insertUser(user))
                {
                    SetAlert("Đăng kí thành viên thành công, hãy đăng nhập hệ thống để khám phá thêm!", "success");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            var session = Session[Shopper.Common.Constans.SESSION_LOGIN_USER] as Shopper.Models.ViewDetailModel.UserSession;
            if (session != null)
            {
                SetAlert("Bạn đã đăng nhập rồi!", "warning");
                return Redirect("/");
            }
            LoginModel model = new LoginModel();
            model.isKeep = true;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string account = model.userName;
                string password = Common.Encrytor.Encrypt(model.passWord);
                if (daoUser.loginUser(account, password))
                {
                    var user = daoUser.getByName(model.userName);
                    var userSession = new UserSession();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(Common.Constans.SESSION_LOGIN_USER, userSession);
                    if (model.isKeep == true)
                    {
                        HttpCookie accCookies = new HttpCookie("KeepMeLogin");
                        accCookies.Values["UserName"] = model.userName;
                        accCookies.Values["ID"] = user.ID.ToString();
                        accCookies.Expires = System.DateTime.Now.AddDays(1);
                        Response.Cookies.Add(accCookies);
                    }

                    SetAlert("Đăng nhập thành công", "success");
                    return Redirect("/");
                }
                else
                {
                    SetAlert("Đăng nhập thất bại, hảy thử lại", "error");
                    return View(model);
                }
            }
            ModelState.AddModelError("", "Đăng nhập thất bại");
            return View(model);
        }

        public ActionResult UserInfo(string UserName)
        {
            var session = Session[Shopper.Common.Constans.SESSION_LOGIN_USER] as Shopper.Models.ViewDetailModel.UserSession;
            if (session == null)
            {
                SetAlert("Bạn chưa đăng nhập!", "warning");
                return RedirectToAction("Login", "User");
            }
            var model = daoUser.getByName(UserName);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserInfo(User model)
        {
            if (ModelState.IsValid)
            {
                if (daoUser.checkEmailToUpdate(model.ID, model.Email))
                {
                    ModelState.AddModelError("", "Email này đã tồn tại");
                    return View(model);
                }
                model.ModifiedDate = DateTime.Now;
                var session = Session[Shopper.Common.Constans.SESSION_LOGIN_USER] as Shopper.Models.ViewDetailModel.UserSession;
                model.ModifiedBy = session.UserName;
                if (daoUser.update(model))
                {
                    SetAlert("Thông tin cá nhân của quý khách đã được thay đổi thành công!", "success");
                    return Redirect("/");
                }
            }
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            var session = Session[Common.Constans.SESSION_LOGIN_USER] as UserSession;
            if (session == null)
            {
                SetAlert("Bạn chưa đăng nhập!", "warning");
                return RedirectToAction("Login", "User");
            }
            ChangepasswordModel model = new ChangepasswordModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangepasswordModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var session = Session[Common.Constans.SESSION_LOGIN_USER] as UserSession;
                    var user = daoUser.getByName(session.UserName);
                    model.newPassword = Common.Encrytor.Encrypt(model.newPassword);
                    if (daoUser.changePassword(user.ID, model.newPassword))
                    {
                        Session[Common.Constans.SESSION_LOGIN_USER] = null;
                        SetAlert("Đổi mật khẩu thành công, hãy đăng nhập lại!", "success");
                        return Redirect("/");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đổi mật khẩu thất bại, hãy thử lại");
                        return View(model);
                    }
                }
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", "Đổi mật khẩu thất bại, hãy thử lại");
                return View(model);
            }

        }
    }
}
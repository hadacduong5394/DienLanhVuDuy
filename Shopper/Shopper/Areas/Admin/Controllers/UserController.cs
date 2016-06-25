using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.DAO;
using Shopper.Models.EF;
using PagedList;
using PagedList.Mvc;
using Shopper.Common;
using Shopper.Areas.Admin.Models.ViewDetailModels;

namespace Shopper.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        UserDAO daoUser = new UserDAO();
        PrivilegeDAO daoPri = new PrivilegeDAO();
        StatusDAO daoStatus = new StatusDAO();
        //
        // GET: /Admin/User/
        [HasCredential]
        public ActionResult Index(string searchString, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var lstUser = daoUser.getAllUser(searchString);
            ViewBag.SearchString = searchString;
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }

        private SelectList loadPrivilege(int? PrivateID = null)
        {

            return new SelectList(daoPri.getALLPrivilege(), "ID", "Content", PrivateID);
        }
        private SelectList loadStatus(bool? StatusID = null)
        {

            return new SelectList(daoStatus.getALLStatusName(), "ID", "Content", StatusID);
        }

        [HttpGet]
        [HasCredential]
        public ActionResult AddNewUser()
        {
            ViewBag.PrivateID = loadPrivilege();
            ViewBag.Status = loadStatus();
            Shopper.Models.EF.User model = new User();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewUser(User model)
        {

            if (ModelState.IsValid)
            {
                if (!daoUser.checkUserName(model.UserName))
                {
                    SetAlert("Người Dùng Này Đã Tồn Tại", "warning");
                    return View(model);
                }
                if (!daoUser.checkUserEmail(model.Email))
                {
                    SetAlert("Email Này Đã Tồn Tại", "warning");
                    return View(model);
                }
                var session = Session[Common.Constans.USER_SESSION] as LoginSession;
                model.CreateBy = session.UserName;
                model.CreateDate = DateTime.Now;
                model.PassWord = Common.Encrytor.Encrypt(model.PassWord);
                model.Status = model.Status ?? false;
                if (daoUser.insertUser(model))
                {
                    SetAlert("Thêm mới người dùng thành công!", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Thêm mới người dùng thất bại!", "warning");
                    ViewBag.PrivateID = loadPrivilege(model.PrivateID);
                    ViewBag.Status = loadStatus(model.Status);
                    return View(model);
                }
            }
            ViewBag.PrivateID = loadPrivilege(model.PrivateID);
            ViewBag.Status = loadStatus(model.Status);
            return View(model);
        }

        [HttpGet]
        [HasCredential]
        public ActionResult Update(long userID)
        {
            var user = daoUser.getByID(userID);
            ViewBag.PrivateID = loadPrivilege(user.PrivateID);
            ViewBag.Status = loadStatus(user.Status);
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(User model)
        {
            if (ModelState.IsValid)
            {
                if (daoUser.checkEmailToUpdate(model.ID, model.Email))
                {
                    SetAlert("Email này đã tồn tại, hãy thử email khác", "warning");
                    ViewBag.PrivateID = loadPrivilege(model.PrivateID);
                    ViewBag.Status = loadStatus(model.Status);
                    return View(model);
                }

                var session = Session[Common.Constans.USER_SESSION] as LoginSession;
                model.ModifiedBy = session.UserName;
                model.ModifiedDate = DateTime.Now;
                model.Status = model.Status ?? false;
                if (daoUser.update(model))
                {
                    SetAlert("Sửa thông tin người dùng thành công!", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Sửa thất bại", "warning");
                    ViewBag.PrivateID = loadPrivilege(model.PrivateID);
                    ViewBag.Status = loadStatus(model.Status);
                    return View(model);
                }

            }
            ViewBag.PrivateID = loadPrivilege(model.PrivateID);
            ViewBag.Status = loadStatus(model.Status);
            return View(model);
        }

        [HasCredential]
        public ActionResult Delete(int UserID)
        {
            try
            {
                string errorMess = string.Empty;
                var user = daoUser.getByID(UserID);
                if (user == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }

                if (daoUser.Delete(user, out errorMess))
                {
                    SetAlert(errorMess, "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert(errorMess, "error");
                    return RedirectToAction("Index", "User");
                }
            }
            catch
            {
                SetAlert("Có lỗi xảy ra, hãy thử lại", "error");
                return RedirectToAction("Index", "User");

            }
        }

        [HttpPost]
        public JsonResult ChangeStatus(long userID)
        {
            if (daoUser.getByID(userID).PrivateID == 1)
            {
                SetAlert("Không thể khóa tài khoản ADMIN","error");
                return Json(new
                {
                    status = false
                });
            }
            var res = daoUser.ChangeStatus(userID);
            return Json(new
            {
                status = res
            });
        }

        [HttpPost]
        public JsonResult Detail(long id)
        {
            var res = daoUser.getByID(id);
            return Json(new
            {
                data = res,
                status = true
            });
        }
    }
}
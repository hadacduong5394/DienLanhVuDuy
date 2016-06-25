using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.DAO;
using Shopper.Models.EF;

namespace Shopper.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        AboutDAO dao = new AboutDAO();
        //
        // GET: /Admin/About/
        public ActionResult Index()
        {
            return View(dao.getAbouts());
        }

        [HttpPost]
        public JsonResult changeStatus(long id)
        {
            var res = dao.changeStatus(id);
            return Json(new
            {
                status = res
            });
        }

        [HttpPost]
        public JsonResult Detial(long id)
        {
            var res = dao.getByID(id);
            return Json(new
            {
                data = res,
                status = true
            });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = dao.getByID(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(About model)
        {
            try
            {

                var session = Session[Common.Constans.USER_SESSION] as Common.LoginSession;
                model.ModifiedBy = session.UserName;
                model.ModifiedDate = DateTime.Now;
                if (dao.edit(model))
                {
                    SetAlert("Sửa thông tin thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Xẩy ra lỗi, hãy thử lại", "error");
                    return View(model);
                }
            }
            catch
            {
                SetAlert("Xẩy ra lỗi, hãy thử lại", "error");
                return View(model);
            }
        }
    }
}
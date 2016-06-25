using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.DAO;
using Shopper.Models.EF;

namespace Shopper.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        ContactDAO dao = new ContactDAO();
        //
        // GET: /Admin/Contact/
        public ActionResult Index()
        {
            return View(dao.getByID(1));
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View(dao.getByID(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Contact model)
        {
            try
            {
                if (dao.edit(model))
                {
                    SetAlert("sửa thông tin thành công!", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("có lỗi xảy ra, hảy thử lại", "error");
                    return View(model);
                }
            }
            catch
            {
                SetAlert("có lỗi xảy ra, hảy thử lại", "error");
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Detail(long id)
        {
            var res = dao.getByID(id);
            return Json(new { 
                data = res
            });
        }
    }
}
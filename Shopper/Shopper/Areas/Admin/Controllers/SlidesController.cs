using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.EF;
using Shopper.Models.DAO;

namespace Shopper.Areas.Admin.Controllers
{
    public class SlidesController : BaseController
    {
        SliderDAO daoSlider = new SliderDAO();

        //
        // GET: /Admin/Slides/
        public ActionResult Index()
        {
            return View(daoSlider.getSlider());
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            Slide model = new Slide();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateNew(Slide model)
        {
            if(ModelState.IsValid){
                try
                {
                    var user = Session[Common.Constans.USER_SESSION] as Shopper.Common.LoginSession;
                    model.CreateBy = user.UserName;
                    model.CreateDate = DateTime.Now;
                    model.Status = model.Status ?? false;
                    if (daoSlider.CreateNew(model))
                    {
                        SetAlert("thêm mới thành công", "success");
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
            return View(model);
            
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = daoSlider.getByID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Slide model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = Session[Common.Constans.USER_SESSION] as Shopper.Common.LoginSession;
                    model.ModifiedBy = user.UserName;
                    model.ModifiedDate = DateTime.Now;
                    if (daoSlider.Edit(model))
                    {
                        SetAlert("Sửa thông tin thành công", "success");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        SetAlert("có lỗi xảy ra, hãy thử lại", "error");
                        return View(model);
                    }
                }
                catch
                {
                    SetAlert("có lỗi xảy ra, hãy thử lại", "error");
                    return View(model);
                }
            }
            return View(model);

        }


        public ActionResult Delete(long id)
        {
            try
            {
                if (daoSlider.Delete(id))
                {
                    SetAlert("Xóa thông tin thành công!", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("có lỗi xảy ra, hãy thử lại", "error");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                SetAlert("có lỗi xảy ra, hãy thử lại", "error");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult changeStatus(long id)
        {
            var res = daoSlider.ChangeStatus(id);
            return Json(new
            {
                status = res
            });
        }
    }
}
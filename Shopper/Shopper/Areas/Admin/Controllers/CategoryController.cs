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

namespace Shopper.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        StatusDAO daoStatus = new StatusDAO();
        CategoryDAO daoCategory = new CategoryDAO();
        //
        // GET: /Admin/Category/
        [HasCredential]
        public ActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(daoCategory.getAllProductCategory().ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [HasCredential]
        public ActionResult CreateNewCategory()
        {
            ViewBag.ShowOnHome = loadStatus();
            return View(new ProductCategory());
        }

        [HttpPost]
        public ActionResult CreateNewCategory(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.MetaTitile = Common.Constans.ToUnsignString(model.Name);
                    model.CreateDate = DateTime.Now;
                    var session = Session[Common.Constans.USER_SESSION] as LoginSession;
                    model.CreateBy = session.UserName;
                    model.ShowOnHome = model.ShowOnHome ?? false;
                    if (daoCategory.checkMetatitle(model.MetaTitile))
                    {
                        SetAlert("Tên này đã tồn tại", "warning");
                        ViewBag.ShowOnHome = loadStatus();
                        return View(model);
                    }
                    if (daoCategory.Create(model))
                    {
                        SetAlert("Thêm mới danh mục sản phẩm thành công", "success");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        SetAlert("Có sự cố xảy ra, hãy thử lại", "error");
                        ViewBag.ShowOnHome = loadStatus();
                        return View(model);
                    }
                }
                catch
                {
                    SetAlert("Có sự cố xảy ra, hãy thử lại", "error");
                    ViewBag.ShowOnHome = loadStatus();
                    return View(model);
                }
            }
            ViewBag.ShowOnHome = loadStatus();
            return View(model);
        }

        [HasCredential]
        public ActionResult Delete(long id)
        {
            string messageError = string.Empty;
            try
            {
                if (daoCategory.Delete(id, out messageError))
                {
                    SetAlert(messageError, "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert(messageError, "error");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                SetAlert(messageError, "error");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [HasCredential]
        public ActionResult Update(long ID)
        {
            ViewBag.ShowOnHome = loadStatus(daoCategory.getByID(ID).ShowOnHome);
            return View(daoCategory.getByID(ID));
        }
        [HttpPost]
        public ActionResult Update(ProductCategory model)
        {

            if (ModelState.IsValid)
            {

                model.MetaTitile = Common.Constans.ToUnsignString(model.Name);
                if (daoCategory.checkMetatitle(model.MetaTitile, model.ID))
                {
                    SetAlert("Tên này đã tồn tại", "warning");
                    ViewBag.ShowOnHome = loadStatus(model.ShowOnHome);
                    return View(model);
                }
                var session = Session[Common.Constans.USER_SESSION] as Common.LoginSession;
                model.ModifiedBy = session.UserName;
                model.ModifiedDate = DateTime.Now;
                if (daoCategory.updateProductCategory(model))
                {
                    SetAlert("Sửa thông tin thành công", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("sửa thông tin thất bại, hãy thử lại!", "error");
                    return RedirectToAction("Index", "Category");
                }
            }
            ViewBag.ShowOnHome = loadStatus(model.ShowOnHome);
            return View(model);
        }

        private SelectList loadStatus(bool? StatusID = false)
        {

            return new SelectList(daoStatus.getALLStatusName(), "ID", "Content", StatusID);
        }


        [HttpPost]
        public JsonResult ShowOnHome(long id)
        {
            var res = daoCategory.showOnHome(id);
            return Json(new
            {
                status = res
            });
        }

    }
}
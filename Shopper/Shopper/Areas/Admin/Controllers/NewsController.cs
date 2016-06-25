using Shopper.Areas.Admin.Models;
using Shopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.DAO;
using PagedList;
using PagedList.Mvc;
using Shopper.Areas.Admin.Models.ViewDetailModels;

namespace Shopper.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        ContentDAO daoContent = new ContentDAO();
        CategoryDAO daoCategory = new CategoryDAO();
        StatusDAO daoStatus = new StatusDAO();
        //
        // GET: /Admin/News/
        public ActionResult Index(ContentModelDetail model, int? page)
        {
            try
            {
                int pageIndex = page != null ? page.Value : 1;
                int pageSize = 10;
                int total = 0;
                List<Content> lst = daoContent.getByFilter(model.categoryID, model.name, out total);
                model.lstCategory = daoCategory.getAllProductCategory();
                model.pageList = lst.ToPagedList<Content>(pageIndex,pageSize);
                return View(model);
            }
            catch
            {
                SetAlert("Xẩy ra lỗi, hãy thử lại", "error");
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            Shopper.Models.EF.Content model = new Shopper.Models.EF.Content();
            ViewData["CatagoryID"] = daoCategory.getAllProductCategory();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content model)
        {
            try
            {
                model.CreateDate = DateTime.Now;
                model.TopHot = model.CreateDate;
                model.ViewCount = 0;
                model.MetaTitile = Common.Constans.ToUnsignString(model.Name);
                var session = Session[Common.Constans.USER_SESSION] as Common.LoginSession;
                model.CreateBy = session.UserName;

                if(daoContent.createNew(model))
                {
                    SetAlert("Thêm mới tin tức thành công", "error");
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.CatagoryID = daoCategory.getAllProductCategory();
                    SetAlert("Xẩy ra lỗi, hãy thử lại", "success");
                    return View(model);
                }
            }
            catch
            {
                ViewData["CatagoryID"] = daoCategory.getAllProductCategory();
                SetAlert("Xẩy ra lỗi, hãy thử lại", "error");
                return View(model);
            }
        }

        public ActionResult Edit(long ID)
        {
            var model = daoContent.getById(ID);
            ViewBag.CatagoryID = new SelectList(daoCategory.getAllProductCategory(),"ID","Name",model.CatagoryID);
            ViewBag.Status = new SelectList(daoStatus.getALLStatusName(),"ID","Content",model.Status);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content model)
        {
            try
            {
                
                var session = Session[Common.Constans.USER_SESSION] as Common.LoginSession;
                model.MetaTitile = Common.Constans.ToUnsignString(model.Name);
                model.ModifiedBy = session.UserName;
                model.ModifiedDate = DateTime.Now;

                if (daoContent.edit(model))
                {
                    SetAlert("Sửa thông tin thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.CatagoryID = new SelectList(daoCategory.getAllProductCategory(), "ID", "Name", model.CatagoryID);
                    SetAlert("Xẩy ra lỗi, hãy thử lại", "error");
                    return View(model);
                }
            }
            catch
            {
                ViewBag.CatagoryID = new SelectList(daoCategory.getAllProductCategory(), "ID", "Name", model.CatagoryID);
                SetAlert("Xẩy ra lỗi, hãy thử lại", "error");
                return View(model);
            }
        }
        public ActionResult Delete(long ID)
        {
            try
            {
                if(daoContent.delete(ID))
                {
                    SetAlert("Xóa thành công!", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Xẩy ra lỗi, hãy thử lại", "error");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                SetAlert("Xẩy ra lỗi, hãy thử lại", "error");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult changeStatus(long id)
        {
            var res = daoContent.changeStatus(id);
            return Json(new { 
                status = res
            });
        }
        [HttpPost]
        public JsonResult upTop(long id)
        {
            var res = daoContent.upTop(id);
            return Json(new
            {
                status = res
            });
        }

        [HttpPost]
        public JsonResult Detail(long id)
        {
            var content = daoContent.getById(id);
            var category = daoCategory.getByID((long)content.CatagoryID);
            var data =  new ContentDetail();
            data.category = category;
            data.content = content;
            return Json(new { 
                data = data,
                status = true
            });
            
        }
	}
}
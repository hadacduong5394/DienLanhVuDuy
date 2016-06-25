using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.DAO;
using Shopper.Models.EF;
using PagedList;
using PagedList.Mvc;
using Shopper.Areas.Admin.Models.ViewDetailModels;
using Shopper.Areas.Admin.Models;
using Shopper.Common;

namespace Shopper.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        ProductDAO daoProduct = new ProductDAO();
        CategoryDAO daoCategory = new CategoryDAO();
        StatusDAO daostatus = new StatusDAO();
        //
        // GET: /Admin/Product/
        [HasCredential]
        public ActionResult Index(ProductModelDetail model, int? page)
        {
            try
            {
                int pageIndex = page != null ? page.Value : 1;
                int pageSize = 10;
                int total = 0;
                List<Product> lst = daoProduct.getPageListProduct(model.categoryID, model.searchString, out total);
                model.lstCategory = daoCategory.getAllProductCategory();
                model.pageListProduct = lst.ToPagedList<Product>(pageIndex,pageSize);
                return View(model);
            }
            catch
            {
                SetAlert("Xẩy ra lỗi, hãy thử lại", "error");
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Uptop(long id)
        {
            var res = daoProduct.Uptop(id);
            return Json(new
            {
                status = res
            });
        }
        public SelectList loadCategory(long? categoryID = 1)
        {
            return new SelectList(daoCategory.getAllProductCategory(), "ID", "Name", categoryID);
        }
        [HttpGet]
        [HasCredential]
        public ActionResult AddNewProduct()
        {
            ViewBag.Status = loadStatus();
            ViewBag.CatagoryID = loadCategory();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                if (daoProduct.checkCode(model.Code))
                {
                    SetAlert("Code này đã được sử dụng cho sản phẩm khác!","warning");
                    ViewBag.Status = loadStatus(model.Status);
                    ViewBag.CatagoryID = loadCategory(model.CatagoryID);
                    return View(model);
                }

                var session = Session[Common.Constans.USER_SESSION] as Common.LoginSession;
                model.CreateBy = session.UserName;
                model.CreateDate = DateTime.Now;
                model.TopHot = model.CreateDate;
                model.MetaTitile = Common.Constans.ToUnsignString(model.Name);
                model.ViewCount = 0;
                model.Price = decimal.Parse(model.Price.ToString());
                model.PromotionPrice = model.PromotionPrice.HasValue ? decimal.Parse(model.PromotionPrice.ToString()) : 0;
                model.Quantity = int.Parse(model.Quantity.ToString());
                if (daoProduct.insertProduct(model))
                {
                    SetAlert("thêm mới sản phẩm thành công!", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("thêm mới sản phẩm thất bại!", "error");
                    return RedirectToAction("Index", "Product");
                }

            }
            ViewBag.Status = loadStatus(model.Status);
            ViewBag.CatagoryID = loadCategory(model.CatagoryID);
            return View(model);
        }

        [HttpGet]
        [HasCredential]
        public ActionResult Update(long ID)
        {
            var product = daoProduct.getProductByID(ID);
            ViewBag.Status = loadStatus(product.Status);
            ViewBag.CatagoryID = loadCategory(product.CatagoryID);
            return View(product);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Product model)
        {
            if (ModelState.IsValid)
            {
                if (daoProduct.checkCode(model.ID,model.Code))
                {
                    SetAlert("Code này đã được sử dụng cho sản phẩm khác!", "warning");
                    ViewBag.Status = loadStatus(model.Status);
                    ViewBag.CatagoryID = loadCategory(model.CatagoryID);
                    return View(model);
                }

                var session = Session[Common.Constans.USER_SESSION] as Common.LoginSession;
                model.ModifiedBy = session.UserName;
                model.ModifiedDate = DateTime.Now;
                model.MetaTitile = Common.Constans.ToUnsignString(model.Name);
                model.Price = decimal.Parse(model.Price.ToString());
                model.PromotionPrice = model.PromotionPrice.HasValue ? decimal.Parse(model.PromotionPrice.ToString()) : 0;
                model.Quantity = int.Parse(model.Quantity.ToString());
                if (daoProduct.updateProduct(model))
                {
                    SetAlert("cập nhật sản phẩm thành công!", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("cập nhật sản phẩm thất bại!", "error");
                    return View(model);
                }

            }
            ViewBag.Status = loadStatus(model.Status);
            ViewBag.CatagoryID = loadCategory(model.CatagoryID);
            return View(model);
        }

        private SelectList loadStatus(bool? StatusID = false)
        {

            return new SelectList(daostatus.getALLStatusName(), "ID", "Content", StatusID);
        }

        [HasCredential]
        public ActionResult Delete(long ID)
        {
            try
            {
                string errorMes = string.Empty;
                if (daoProduct.deleteProduct(ID, out errorMes))
                {
                    SetAlert(errorMes, "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert(errorMes, "error");
                    return RedirectToAction("Index", "Product");
                }
            }
            catch
            {
                SetAlert("Xóa thất bại!", "error");
                return RedirectToAction("Index", "Product");
            }
        }
        public JsonResult ChangeStatus(long id)
        {
            var res = daoProduct.changeStatus(id);
            return Json(new
            {
                status = res
            });
        }

        [HasCredential]
        public ActionResult Detail(long ID)
        {
            var product = daoProduct.getProductByID(ID);

            var categoryOfProduct = daoProduct.getCategory(ID);

            var model = new ProductDetailModel(product, categoryOfProduct);

            return View(model);
        }

        [HttpPost]
        public JsonResult loadCategoryToAlter()
        {
            var lstCategory = daoCategory.getAllProductCategory();

            return Json(new
            {
                data = lstCategory,
                status = true
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.DAO;
using Shopper.Models.EF;
using Shopper.Models.ViewDetailModel;
using PagedList;
using PagedList.Mvc;


namespace Shopper.Controllers
{
    public class ProductController : BaseController
    {
        ProductDAO daoProduct = new ProductDAO();
        CategoryDAO daoCategory = new CategoryDAO();
        //
        // GET: /Product/
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult TiviFeatures()
        {
            var top = daoProduct.checkCountWith4(1);
            ViewBag.CategoryID = daoCategory.getByID(1);
            return PartialView(daoProduct.getAllProduct(1, top));
        }
        [ChildActionOnly]
        public ActionResult AirConditionFeatures()
        {
            var top = daoProduct.checkCountWith4(4);
            ViewBag.CategoryID = daoCategory.getByID(4);
            return PartialView(daoProduct.getAllProduct(4, top));
        }
        [ChildActionOnly]
        public ActionResult WashingMachineFeatures()
        {
            var top = daoProduct.checkCountWith4(3);
            ViewBag.CategoryID = daoCategory.getByID(3);
            return PartialView(daoProduct.getAllProduct(3, top));
        }
        [ChildActionOnly]
        public ActionResult FridgeFeatures()
        {
            var top = daoProduct.checkCountWith4(2);
            ViewBag.CategoryID = daoCategory.getByID(2);
            return PartialView(daoProduct.getAllProduct(2, top));
        }
        public ActionResult CategoryDetail(long cateID, int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 12;
            return View(daoProduct.getProductToView(cateID).ToPagedList(pageIndex, pageSize));
        }

        public ActionResult productViewDetail(long proID)
        {
            ViewBag.CategoryOfProduct = daoProduct.getProductCategory(proID);
            ViewBag.RelatedProduct = daoProduct.lstRelatedProduct(proID);
            return View(daoProduct.viewDetail(proID));
        }

        public JsonResult ListName(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var res = daoProduct.getListName(q);
                return Json(new
                {
                    data = res,
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public ActionResult SearchProductResult(string keyword, int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 12;
            int total = 0;
            List<Product> model = daoProduct.getByFilter(keyword, out total);
            ViewBag.keyword = keyword;
            ViewBag.total = total;
            return View(model.ToPagedList(pageIndex, pageSize));
        }
    }
}
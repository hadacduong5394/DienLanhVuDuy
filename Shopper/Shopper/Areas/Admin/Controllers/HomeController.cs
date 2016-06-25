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
    public class HomeController : BaseController
    {
        MainMenuDAO daoMenu = new MainMenuDAO();
        FooterDAO daoFooter = new FooterDAO();
        OrderDAO daoOrder = new OrderDAO();
        //
        // GET: /Admin/Home/
        public ActionResult Index(string SearchString, int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 10;
            List<Order> model = daoOrder.getOrders(SearchString);
            ViewBag.SearchString = SearchString;
            return View(model.ToPagedList(pageIndex, pageSize));
        }

        [HasCredential]
        public ActionResult DeleteOrder(long id)
        {
            try
            {
                string errorMes = string.Empty;
                if (daoOrder.Delete(id, out errorMes))
                {
                    SetAlert(errorMes, "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert(errorMes, "error");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                SetAlert("Có sự cố xảy ra, hãy thử lại!", "error");
                return RedirectToAction("Index");
            }

        }
        public ActionResult Detail(long id)
        {
            return View(daoOrder.detail(id));
        }
        [HasCredential]
        public ActionResult Success(long id)
        {
            try
            {
                var order = daoOrder.getByID(id);
                if (order.Status == 0)
                {
                    if (daoOrder.Success(id))
                    {
                        SetAlert("Đơn hàng này đã thanh toán hoàn tất!", "success");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        SetAlert("có lỗi xảy ra, hãy thử lại", "error");
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    SetAlert("Đơn hàng này đã thanh toán xong trước đó rồi", "warning");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                SetAlert("có lỗi xảy ra, hãy thử lại", "error");
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public ActionResult ContactHelp()
        {
            return View(daoMenu.getContactAtFirst(4));
        }
        [HttpPost]
        public ActionResult ContactHelp(Menu model)
        {

            if (ModelState.IsValid)
            {
                if (daoMenu.updateContactAtFirstPage(model))
                {
                    SetAlert("Đổi thông tin thành công!", "success");
                    return View(daoMenu.getContactAtFirst(4));
                }
                else
                {
                    SetAlert("Đổi thông tin thất bại!", "warning");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult FooterManager()
        {
            return View(daoFooter.getAllFooter());
        }
        [HttpGet]
        public ActionResult FooterUpdate(string footerID)
        {
            return View(daoFooter.getFooter(footerID));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FooterUpdate(Footer model)
        {
            if (ModelState.IsValid)
            {
                if (daoFooter.updateFooter(model))
                {
                    SetAlert("Đổi thông tin thành công!", "success");
                    return RedirectToAction("FooterManager", "Home");
                }
                else
                {
                    SetAlert("Đổi thông tin thất bại!", "warning");
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult NumberFeedback()
        {
            ViewBag.NumberFeedback = new FeedBackDAO().getNumberFeedBackNotRequest();
            return PartialView();
        }

        public ActionResult NumberOrder()
        {
            ViewBag.NumberOrder = daoOrder.getNumberOrderNotSuccess();
            return PartialView();
        }
    }
}
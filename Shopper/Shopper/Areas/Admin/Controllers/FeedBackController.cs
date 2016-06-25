using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.EF;
using Shopper.Models.DAO;
using PagedList;
using PagedList.Mvc;
using Shopper.Areas.Admin.Models;
using Shopper.Models;

namespace Shopper.Areas.Admin.Controllers
{
    public class FeedBackController : BaseController
    {
        FeedBackDAO daoFeedback = new FeedBackDAO();
        //
        // GET: /Admin/FeedBack/
        public ActionResult Index(string searchString, int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 10;
            ViewBag.searchString = searchString;
            return View(daoFeedback.getByFilter(searchString).ToPagedList(pageIndex, pageSize));
        }

        public JsonResult Detail(int id)
        {
            var res = daoFeedback.getByID(id);
            return Json(new
            {
                data = res,
                status = true
            });
        }

        public ActionResult Delete(int id)
        {
            try
            {
                if (daoFeedback.Delete(id))
                {
                    SetAlert("Xóa phản hồi khách hàng thành công!", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Có sự cố xảy ra, hảy thử lại!", "error");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                SetAlert("Có sự cố xảy ra, hảy thử lại!", "error");
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult RequestFeedback(int id)
        {
            var feedBack = daoFeedback.getByID(id);
            RequestFeedbackModel model = new RequestFeedbackModel();
            model.id = feedBack.ID;
            model.Email = feedBack.Address;
            model.CreateDate = feedBack.CreatedDate.Value.ToString("dd-MM-yyyy");
            model.content = feedBack.Content;
            return View(model);
        }
        [HttpPost]
        public ActionResult RequestFeedback(RequestFeedbackModel model)
        {
            try
            {

                if (daoFeedback.changeStatus(model.id))
                {
                    EmailService sendMail = new EmailService();
                    string sub = "dienlanhvuduy xin trả lời phản hồi của quý khách!";
                    string content = model.myRequest;
                    sendMail.sendEmail(model.Email, sub, content);
                    SetAlert("Đã gửi phản hồi đến khách hàng", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("có sự cố xảy ra, hãy thử lại", "error");
                    return View(model);
                }

            }
            catch
            {
                SetAlert("có sự cố xảy ra, hãy thử lại", "error");
                return View(model);
            }
        }
    }
}
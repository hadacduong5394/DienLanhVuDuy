using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.EF;
using Shopper.Models.DAO;
using Shopper.Models;
namespace Shopper.Controllers
{
    public class ContactController : BaseController
    {
        ContactDAO dao = new ContactDAO();
        //
        // GET: /Contact/
        public ActionResult Index()
        {
            return View(dao.getByID(1));
        }
       
        [HttpPost]
        public ActionResult FeedBack(string txtName, string txtMobile, string txtEmail, string txtContent)
        {
            try
            {
                var model = new FeedBack();
                model.Name = txtName;
                model.DienThoai = txtMobile;
                model.Address = txtEmail;
                model.Content = txtContent;
                model.CreatedDate = DateTime.Now;
                model.Status = false;
                if (dao.CreateNewFeedBack(model))
                {

                    string content = model.Address + "\n" + model.Content;
                    string subj = "phản hồi từ độc giả " + model.Name;
                    EmailService mail = new EmailService();
                    mail.sendEmail(Common.Constans.ADMIN_EMAIL, subj, content);
                    SetAlert("Cảm ơn quý khách đã phản hồi lại cửa hàng\n Chúng tôi sẽ trả lời ý kiến của quý khách sớm nhất có thể!", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Có lỗi xảy ra trong quá trình xử lý, Hãy thử lại!", "error");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                SetAlert("Có lỗi xả ra trong quá trình xử lý, hãy thử lại!", "error");
                return RedirectToAction("Index");
            }
        }

    }
}
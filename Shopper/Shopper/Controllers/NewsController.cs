using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.EF;
using Shopper.Models.DAO;
using PagedList.Mvc;
using PagedList;
using Shopper.Models.ViewDetailModel;

namespace Shopper.Controllers
{
    public class NewsController : BaseController
    {
        ContentDAO daoContent = new ContentDAO();
        //
        // GET: /News/
        public ActionResult Index(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 5;
            List<Content> model = daoContent.getContents();
            return View(model.ToPagedList(pageIndex, pageSize));
        }

        public ActionResult Detail(long id)
        {
            try
            {
                var content = daoContent.getById(id);
                var model = new ContentDetailModel();
                model.content = content;
                model.lstWouldYouLikeView = daoContent.WouldYouLikeView(content);
                return View(model);
            }
            catch
            {
                SetAlert("Có lỗi xả ra, hãy thử lại!", "error");
                return RedirectToAction("Index");
            }
        }
    }
}
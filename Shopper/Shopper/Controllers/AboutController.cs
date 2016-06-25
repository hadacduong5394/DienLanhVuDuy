using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.DAO;
using Shopper.Models.EF;

namespace Shopper.Controllers
{
    public class AboutController : BaseController
    {
        AboutDAO dao = new AboutDAO();
        //
        // GET: /About/
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult FirstChild()
        {
            var model = dao.getFirstChild();
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult SecondChild()
        {
            var model = dao.getSecondChild();
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult ThirdChild()
        {
            var model = dao.getThirdChild();
            return PartialView(model);
        }
    }
}
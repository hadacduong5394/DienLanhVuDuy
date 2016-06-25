using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.DAO;
using Shopper.Models.EF;
using Shopper.Models.ViewDetailModel;

namespace Shopper.Controllers
{
    public class HomeController : BaseController
    {
        MainMenuDAO menuDao = new MainMenuDAO();
        FooterDAO footerDao = new FooterDAO();
        SliderDAO daoSlide = new SliderDAO();
        CategoryDAO daoCategory = new CategoryDAO();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration= 3600 * 24)]
        public ActionResult MainMenu()
        {
            var res = menuDao.getALLMenu(2);
            return PartialView(res);
        }
        [ChildActionOnly]
        public ActionResult MenuTop()
        {
            return PartialView(menuDao.getALLMenu(1));
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult ContactHelp()
        {
            return PartialView(menuDao.getALLMenu(4));
        }
        [ChildActionOnly]
        public ActionResult FooterFirst()
        {
            return PartialView(footerDao.getFooter("First"));
        }
        [ChildActionOnly]
        public ActionResult FooterSecond()
        {
            return PartialView(footerDao.getFooter("Second"));
        }
        [ChildActionOnly]
        public ActionResult FooterThird()
        {
            return PartialView(footerDao.getFooter("Third"));
        }
        [ChildActionOnly]
        public ActionResult FooterFourth()
        {
            return PartialView(footerDao.getFooter("Fourth"));
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult Category()
        {
            return PartialView(daoCategory.getAllProductCategoryOnHome());
        }
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult Slide()
        {
            return PartialView(daoSlide.getSliderShow());
        }

    }
}
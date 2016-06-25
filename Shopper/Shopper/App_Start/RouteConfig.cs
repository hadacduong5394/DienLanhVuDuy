using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shopper
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Category",
                url: "san-pham/{metatitile}-{cateID}",
                defaults: new { controller = "Product", action = "CategoryDetail", id = UrlParameter.Optional },
                namespaces: new[] { "Shopper.Controllers" }
            );
            routes.MapRoute(
                name: "ProductDetail",
                url: "chi-tiet/{metatitile}-{proID}",
                defaults: new { controller = "Product", action = "productViewDetail", id = UrlParameter.Optional },
                namespaces: new[] { "Shopper.Controllers" }
            );
            routes.MapRoute(
                name: "AddCart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "Shopper.Controllers" }
            );
            routes.MapRoute(
               name: "Cart",
               url: "gio-hang",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "Shopper.Controllers" }
           );
            routes.MapRoute(
               name: "Rej",
               url: "xac-nhan",
               defaults: new { controller = "Cart", action = "UpdateCartToPay", id = UrlParameter.Optional },
               namespaces: new[] { "Shopper.Controllers" }
           );
            routes.MapRoute(
              name: "Pay",
              url: "thanh-toan",
              defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
              namespaces: new[] { "Shopper.Controllers" }
          );
            routes.MapRoute(
              name: "Home",
              url: "Trang-chu",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "Shopper.Controllers" }
          );
            routes.MapRoute(
             name: "Success_Pay",
             url: "Loi-cam-on",
             defaults: new { controller = "Cart", action = "SuccessFull", id = UrlParameter.Optional },
             namespaces: new[] { "Shopper.Controllers" }
         );
            routes.MapRoute(
             name: "DetailNews",
             url: "tin-tuc/{metatitile}-{id}",
             defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
             namespaces: new[] { "Shopper.Controllers" }
         );
            routes.MapRoute(
             name: "News",
             url: "tin-tuc",
             defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "Shopper.Controllers" }
         );
            routes.MapRoute(
             name: "About",
             url: "thong-tin",
             defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "Shopper.Controllers" }
         );
            routes.MapRoute(
            name: "Contact",
            url: "lien-he",
            defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "Shopper.Controllers" }
        );
            routes.MapRoute(
            name: "Register",
            url: "dang-ki",
            defaults: new { controller = "User", action = "CreateNew", id = UrlParameter.Optional },
            namespaces: new[] { "Shopper.Controllers" }
        );
            routes.MapRoute(
            name: "login",
            url: "dang-nhap",
            defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
            namespaces: new[] { "Shopper.Controllers" }
        );

            routes.MapRoute(
            name: "ChangePassword",
            url: "doi-mat-khau",
            defaults: new { controller = "User", action = "ChangePassword", id = UrlParameter.Optional },
            namespaces: new[] { "Shopper.Controllers" }
        );
            routes.MapRoute(
            name: "UserInfo",
            url: "thong-tin-nguoi-dung/{UserName}",
            defaults: new { controller = "User", action = "UserInfo", id = UrlParameter.Optional },
            namespaces: new[] { "Shopper.Controllers" }
        );
            routes.MapRoute(
           name: "SearchProduct",
           url: "tim-kiem",
           defaults: new { controller = "Product", action = "SearchProductResult", id = UrlParameter.Optional },
           namespaces: new[] { "Shopper.Controllers" }
       );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Shopper.Controllers" }
            );
        }
    }
}

using System.Web.Mvc;

namespace Shopper.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Login", Controller = "Regestor", id = UrlParameter.Optional },
                namespaces: new[] {"Shopper.Areas.Admin.Controllers"}
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopper.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = HttpContext.Current.Session[Common.Constans.USER_SESSION] as Shopper.Common.LoginSession;
            if (session == null)
            {
                return false;
            }

            if (session.RoleName == Common.Constans.GROUP_ADMIN)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Areas/Admin/Views/Shared/EX401.cshtml"
            };
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;

namespace Shopper.Models.DAO
{
    public class PrivilegeDAO
    {
        OnlineShopDbContext context = null;
        public PrivilegeDAO()
        {
            context = new OnlineShopDbContext();
        }
        public List<Private> getALLPrivilege()
        {
            return context.Privates.OrderByDescending(n => n.ID).ToList();
        }
    }
}
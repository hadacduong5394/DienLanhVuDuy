using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;

namespace Shopper.Models.DAO
{
    public class StatusDAO
    {
        OnlineShopDbContext context = null;
        public StatusDAO()
        {
            context = new OnlineShopDbContext();
        }
        public List<StatusName> getALLStatusName()
        {
            return context.StatusNames.OrderByDescending(n => n.ID).ToList();
        }
    }
}
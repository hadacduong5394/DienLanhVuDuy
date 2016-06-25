using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;
namespace Shopper.Models.DAO
{
    public class FooterDAO
    {
         OnlineShopDbContext context = null;
         public FooterDAO()
        {
            context = new OnlineShopDbContext();
        }

         public Footer getFooter(string Footerth)
         {
             return context.Footers.Find(Footerth);
         }
         public List<Footer> getAllFooter()
         {
             return context.Footers.ToList();
         }

         public bool updateFooter(Footer entity)
         {
             try
             {
                 var footer = context.Footers.Find(entity.ID);
                 footer.content = entity.content;
                 context.SaveChanges();
                 return true;
             }
             catch
             {
                 return false;
             }
         }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;

namespace Shopper.Models.DAO
{
    public class AboutDAO
    {

        OnlineShopDbContext context = null;
        public AboutDAO()
        {
            context = new OnlineShopDbContext();
        }

        public About getByID(long id)
        {
            return context.Abouts.Find(id);
        }

        public bool changeStatus(long id)
        {

            var record = context.Abouts.Find(id);
            record.Status = !record.Status;
            context.SaveChanges();
            return (bool)record.Status;
        }

        public bool edit(About model)
        {
            try
            {
                var rec = context.Abouts.Find(model.ID);
                rec.Name = model.Name;
                rec.Description = model.Description;
                rec.Detail = model.Detail;
                rec.ModifiedBy = model.ModifiedBy;
                rec.ModifiedDate = model.ModifiedDate;
                rec.Status = model.Status;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<About> getAbouts()
        {
            return context.Abouts.OrderBy(n => n.ID).ToList();
        }

        public About getFirstChild()
        {
            return context.Abouts.Find(1);
        }
        public About getSecondChild()
        {
            return context.Abouts.Find(2);
        }
        public About getThirdChild()
        {
            return context.Abouts.Find(3);
        }

    }
}
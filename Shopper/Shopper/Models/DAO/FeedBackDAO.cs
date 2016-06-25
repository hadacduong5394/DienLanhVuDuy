using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;

namespace Shopper.Models.DAO
{
    public class FeedBackDAO
    {
        OnlineShopDbContext context = null;
        public FeedBackDAO()
        {
            context = new OnlineShopDbContext();
        }

        public List<FeedBack> getByFilter(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return context.FeedBacks.Where(n => n.Name.Contains(searchString)).OrderByDescending(n => n.CreatedDate).ToList();
            }

            return context.FeedBacks.OrderByDescending(n => n.CreatedDate).ToList();
        }

        public bool Delete(int id)
        {
            try
            {
                var res = context.FeedBacks.Find(id);
                context.FeedBacks.Remove(res);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public FeedBack getByID(int id)
        {
            return context.FeedBacks.Find(id);
        }

        public int getNumberFeedBackNotRequest()
        {
            return context.FeedBacks.Where(n => n.Status == false).Count();
        }

        public bool changeStatus(int id)
        {
            try
            {
                var f = context.FeedBacks.Find(id);
                f.Status = true;
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
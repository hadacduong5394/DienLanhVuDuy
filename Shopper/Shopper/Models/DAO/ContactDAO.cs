using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;

namespace Shopper.Models.DAO
{
    public class ContactDAO
    {
        OnlineShopDbContext context = new OnlineShopDbContext();
        public ContactDAO()
        {
            context = new OnlineShopDbContext();
        }

        public Contact getByID(long id)
        {
            return context.Contacts.Find(id);
        }
        public bool edit(Contact model)
        {
            try
            {
                var contact = getByID(model.ID);
                contact.Content = model.Content;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateNewFeedBack(FeedBack model)
        {
            try
            {
                context.FeedBacks.Add(model);
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
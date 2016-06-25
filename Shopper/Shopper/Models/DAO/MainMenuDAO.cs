using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;
namespace Shopper.Models.DAO
{
    public class MainMenuDAO
    {

        OnlineShopDbContext context = null;
        public MainMenuDAO()
        {
            context = new OnlineShopDbContext();
        }
        public List<Menu> getALLMenu(int MenuTyeID)
        {
            return context.Menus.Where( n=>n.MenuTypeID == MenuTyeID).OrderBy(n=>n.MenuTypeID).ToList();
        }
        public Menu getContactAtFirst(int MenuTyeID)
        {
            return context.Menus.Where(n => n.MenuTypeID == MenuTyeID).OrderBy(n => n.MenuTypeID).SingleOrDefault();
        }

        public bool updateContactAtFirstPage(Menu entity)
        {
            try
            {
                var menu = context.Menus.Find(entity.ID);
                menu.Text = entity.Text;
                menu.Link = entity.Link;
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
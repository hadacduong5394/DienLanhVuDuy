using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;


namespace Shopper.Models.DAO
{
    public class ContentDAO
    {
        OnlineShopDbContext context = null;
        public ContentDAO()
        {
            context = new OnlineShopDbContext();
        }

        public Content getById(long ContentID)
        {
            return context.Contents.Find(ContentID);
        }

        public bool createNew(Content model)
        {
            try
            {
                context.Contents.Add(model);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool delete(long id)
        {
            var content = getById(id);
            try
            {
                context.Contents.Remove(content);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Content> getByFilter(long categoryID, string name, out int total)
        {
            IEnumerable<Content> query = from a in context.Contents select a;
            if (!string.IsNullOrEmpty(name))
            {
                if (categoryID == 0)
                {
                    query = query.Where(n => n.Name.Contains(name));
                    total = query.Count();
                    query = query.OrderByDescending(n => n.CreateDate);
                    return query.ToList();
                }
                else
                {
                    query = query.Where(n => n.CatagoryID == categoryID && n.Name.Contains(name));
                    total = query.Count();
                    query = query.OrderByDescending(n => n.CreateDate);
                    return query.ToList();
                }
            }
            else
            {
                if (categoryID == 0)
                {
                    total = query.Count();
                    query = query.OrderByDescending(n => n.CreateDate);
                    return query.ToList();
                }
                else
                {
                    query = query.Where(n => n.CatagoryID == categoryID);
                    total = query.Count();
                    query = query.OrderByDescending(n => n.CreateDate);
                    return query.ToList();
                }
            }
        }
        public bool edit(Content model)
        {
            try
            {
                var content = context.Contents.Find(model.ID);
                content.Name = model.Name;
                content.MetaTitile = model.MetaTitile;
                content.ModifiedBy = model.ModifiedBy;
                content.ModifiedDate = model.ModifiedDate;
                content.Image = model.Image;
                content.CatagoryID = model.CatagoryID;
                content.Description = model.Description;
                content.Status = model.Status;
                content.Detail = model.Detail;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool changeStatus(long id)
        {
            var item = context.Contents.Find(id);
            item.Status = !item.Status;
            context.SaveChanges();
            return (bool)item.Status;
        }

        public bool upTop(long id)
        {
            try
            {
                var res = context.Contents.Find(id);
                res.TopHot = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public List<Content> getContents()
        {
            return context.Contents.Where(n => n.Status == true).OrderByDescending(n => n.TopHot).ToList();
        }


        public List<Content> WouldYouLikeView(Content model)
        {
            IQueryable<Content> query = context.Contents.Where(n => n.CatagoryID == model.CatagoryID && n.ID != model.ID && n.Status == true);
            int totalRecord = query.Count();

            if (totalRecord >= 3)
            {
                return query.OrderByDescending(n => n.TopHot).Take(3).ToList();
            }
            else
            {
                return query.OrderByDescending(n => n.TopHot).ToList();
            }
        }
    }
}
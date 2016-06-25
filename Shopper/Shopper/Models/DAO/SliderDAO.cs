using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;
namespace Shopper.Models.DAO
{
    public class SliderDAO
    {
        OnlineShopDbContext context = null;
        public SliderDAO()
        {
            context = new OnlineShopDbContext();
        }

        public List<Slide> getSlider()
        {
            return context.Slides.OrderByDescending(n => n.CreateDate).ToList();
        }
        public List<Slide> getSliderShow()
        {
            return context.Slides.Where(n=>n.Status == true).OrderBy(n => n.DisplayOrder).ToList();
        }
        public Slide getByID(long id)
        {
            return context.Slides.Find(id);
        }
        public bool CreateNew(Slide model)
        {
            try
            {

                context.Slides.Add(model);
                context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var slides = context.Slides.Where(n => n.Status == true).Count();
                if (slides <= 1)
                {
                    return false;
                }
                var entity = context.Slides.Find(id);
                context.Slides.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(Slide model)
        {
            try
            {
                var slide = context.Slides.Find(model.ID);
                slide.Image = model.Image;
                slide.Status = model.Status;
                slide.Description = model.Description;
                slide.DisplayOrder = model.DisplayOrder;
                slide.ModifiedBy = model.ModifiedBy;
                slide.ModifiedDate = model.ModifiedDate;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangeStatus(long id)
        {
            var slide = context.Slides.Find(id);
            slide.Status = !slide.Status;
            context.SaveChanges();
            return (bool)slide.Status;
        }
    }
}
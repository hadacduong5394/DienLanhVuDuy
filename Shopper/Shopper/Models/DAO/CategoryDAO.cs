using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;

namespace Shopper.Models.DAO
{
    public class CategoryDAO
    {
        OnlineShopDbContext context = null;
        public CategoryDAO()
        {
            context = new OnlineShopDbContext();
        }
        public List<ProductCategory> getAllProductCategoryOnHome()
        {
            var res = (from a in context.ProductCategories
                       where a.ShowOnHome == true
                       orderby a.DisplayOrder
                       select a).ToList();
            return res;
        }

        public List<ProductCategory> getAllProductCategory()
        {
            var res = (from a in context.ProductCategories
                       orderby a.DisplayOrder
                       select a).ToList();
            return res;
        }

        public bool insertProductCategory(ProductCategory entity)
        {
            try
            {
                context.ProductCategories.Add(entity);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool checkMetatitle(string metatitle)
        {
            var model = context.ProductCategories.Where(n => n.MetaTitile == metatitle).Count();
            if (model > 0)
                return true;
            return false;
        }

        public bool checkMetatitle(string metatitle, long id)
        {
            var model = context.ProductCategories.Where(n => n.MetaTitile == metatitle && n.ID != id).Count();
            if (model > 0)
                return true;
            return false;
        }

        public bool updateProductCategory(ProductCategory entity)
        {
            try
            {
                var category = context.ProductCategories.Find(entity.ID);
                category.Name = entity.Name;
                category.MetaTitile = entity.MetaTitile;
                category.DisplayOrder = entity.DisplayOrder;
                category.ModifiedBy = entity.ModifiedBy;
                category.ModifiedDate = entity.ModifiedDate;
                category.ShowOnHome = entity.ShowOnHome;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool showOnHome(long id)
        {
            var category = context.ProductCategories.Find(id);
            category.ShowOnHome = !category.ShowOnHome;
            context.SaveChanges();
            return category.ShowOnHome ?? false;
        }

        public ProductCategory getByID(long ID)
        {
            return context.ProductCategories.Find(ID);
        }

        public bool Create(ProductCategory model)
        {
            try
            {
                context.ProductCategories.Add(model);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(long id, out string errorMessage)
        {
            try
            {
                if (id == 1 || id == 2 || id == 3 || id == 4)
                {
                    errorMessage = "Không được xóa danh mục này!";
                    return false;
                }

                var countProduct = context.Products.Where(n => n.CatagoryID == id).Count();
                if (countProduct > 0)
                {
                    errorMessage = "Danh mục này đang tồn tại các mặt hàng quan trọng, Không thể xóa";
                    return false;
                }
                var catagory = context.ProductCategories.Find(id);
                context.ProductCategories.Remove(catagory);
                context.SaveChanges();
                errorMessage = "Xóa thành công!";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Có lỗi xả ra từ hệ thống, hãy thử lại";
                return false;
            }
        }

    }
}
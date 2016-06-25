using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;
namespace Shopper.Models.DAO
{
    public class ProductDAO
    {
        OnlineShopDbContext context = null;
        public ProductDAO()
        {
            context = new OnlineShopDbContext();
        }

        public List<Product> getAllProduct(long categoryID, int top)
        {
            var res = (from a in context.Products
                       where a.CatagoryID == categoryID && a.Status == true
                       orderby a.TopHot descending
                       select a).Take(top).ToList();
            return res;
        }

        public int checkCountWith4(long categoryID)
        {
            int top = context.Products.Where(n => n.CatagoryID == categoryID).Count();
            return (top >= 4) ? 4 : top;
        }

        public List<Product> getProductByCategoryID(long categoryID)
        {
            return context.Products.Where(n => n.CatagoryID == categoryID).OrderByDescending(n => n.CreateDate).ToList();
        }

        public string getCategory(long ID)
        {

            var res = (from a in context.Products
                       join b in context.ProductCategories on a.CatagoryID equals b.ID
                       where a.ID == ID
                       select b).FirstOrDefault().Name;
            return res;
        }


        public List<String> getListName(string keyword)
        {
            return context.Products.Where(n => n.Name.Contains(keyword)).Select(n => n.Name).ToList();
        }

        public List<Product> getPageListProduct(long categoryID, string searchString, out int total)
        {
            IQueryable<Product> query = from a in context.Products select a;
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(n => n.Name.Contains(searchString) || n.Code.Contains(searchString));
            }
            if (categoryID != 0)
            {
                query = query.Where(n => n.CatagoryID == categoryID);
            }
            total = query.Count();
            List<Product> lst = query.OrderBy(n => n.Name).ToList();
            return lst;
        }

        public List<Product> getAllProduct()
        {
            var res = (from a in context.Products
                       orderby a.CreateDate descending
                       select a).ToList();
            return res;
        }

        public List<Product> getAllProductByCategoryID(long categoryID)
        {
            if (categoryID != 0)
            {
                var res = (from a in context.Products
                           where a.CatagoryID == categoryID
                           orderby a.CreateDate descending
                           select a).ToList();
                return res;
            }
            return context.Products.OrderByDescending(n => n.CreateDate).ToList();

        }
        public Product getProductByID(long ID)
        {
            return context.Products.Find(ID);
        }
        public bool insertProduct(Product entity)
        {
            try
            {
                context.Products.Add(entity);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool checkCode(string code)
        {
            var check = context.Products.Where(n => n.Code == code).FirstOrDefault();
            if (check != null)
            {
                return true;
            }
            return false;
        }

        public bool deleteProduct(long ID, out string errorMes)
        {
            try
            {
                var count = context.OrderDetails.Where(n=>n.ProductID == ID).Count();

                if(count > 0){
                    errorMes = "Đang có kiện hàng chứa hàng hóa này, không thể xóa!";
                    return false;
                }

                var product = context.Products.Find(ID);
                context.Products.Remove(product);
                context.SaveChanges();
                errorMes = "Xóa thành công!";
                return true;
            }
            catch
            {
                errorMes = "Có lỗi xả ra, Hãy thử lại!";
                return false;
            }
        }

        public bool checkCode(long id, string code)
        {
            var product = context.Products.Where(n => n.ID != id && n.Code == code).Count();

            return product > 0 ? true : false;
        }

        public bool updateProduct(Product entity)
        {
            try
            {
                var product = context.Products.Find(entity.ID);
                product.Code = entity.Code;
                product.Name = entity.Name;
                product.MetaTitile = entity.MetaTitile;
                product.Price = entity.Price;
                product.PromotionPrice = entity.PromotionPrice;
                product.Quantity = entity.Quantity;
                product.Image = entity.Image;
                product.CatagoryID = entity.CatagoryID;
                product.Detail = entity.Detail;
                product.Status = entity.Status;
                product.ModifiedBy = entity.ModifiedBy;
                product.ModifiedDate = entity.ModifiedDate;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool changeStatus(long productID)
        {
            var product = context.Products.Find(productID);
            product.Status = !product.Status;
            context.SaveChanges();
            return product.Status ?? false;
        }

        public bool Uptop(long id)
        {
            try
            {
                var product = context.Products.Find(id);
                product.TopHot = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public Product viewDetail(long id)
        {
            return context.Products.Find(id);
        }

        public List<Product> lstRelatedProduct(long proID)
        {
            var res = context.Products.Find(proID);
            var lst = context.Products.Where(n => n.ID != proID && n.CatagoryID == res.CatagoryID && n.Status == true).OrderByDescending(n => n.TopHot).ToList();
            if (lst.Count >= 4)
            {
                return lst.Take(4).ToList();
            }
            return lst;
        }

        public ProductCategory getProductCategory(long proID)
        {
            var product = context.Products.Find(proID);
            return context.ProductCategories.Where(n => n.ID == product.CatagoryID).SingleOrDefault();
        }

        public List<Product> getProductToView(long categoryID)
        {
            return context.Products.Where(n => n.CatagoryID == categoryID && n.Status == true).OrderByDescending(n => n.TopHot).ToList();
        }

        public bool checkQuantityToPay(long productID, int quantity)
        {
            var product = context.Products.Find(productID);
            return (product.Quantity >= quantity) ? true : false;
        }

        public List<Product> getByFilter(string productName, out int total)
        {
            IQueryable<Product> query = from a in context.Products where a.Status == true select a;
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(n => n.Name.Contains(productName));
            }
            total = query.Count();
            var lst = query.OrderByDescending(n => n.TopHot).ToList();
            return lst;
        }

    }
}
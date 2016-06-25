using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;
using Shopper.Models.DAO;
using PagedList;
using PagedList.Mvc;

namespace Shopper.Areas.Admin.Models
{
    public class ProductModelDetail
    {
        public string searchString { get; set; }
        public int categoryID { get; set; }

        public List<ProductCategory> lstCategory { get; set; }

        public IPagedList<Product> pageListProduct;
    }
}
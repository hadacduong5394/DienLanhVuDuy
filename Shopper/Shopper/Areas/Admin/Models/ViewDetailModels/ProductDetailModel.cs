using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Shopper.Models.EF;

namespace Shopper.Areas.Admin.Models.ViewDetailModels
{
    public class ProductDetailModel
    {
        public Product entity { get; set; }
        public string category { get; set; }

        public ProductDetailModel(Product obj, string categoryString)
        {
            this.entity = obj;
            this.category = categoryString;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;

namespace Shopper.Areas.Admin.Models.ViewDetailModels
{
    public class ContentDetail
    {
        public Content content { get; set; }
        public ProductCategory category { get; set; }
    }
}
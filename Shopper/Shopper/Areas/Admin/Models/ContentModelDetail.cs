using PagedList;
using Shopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopper.Areas.Admin.Models
{
    public class ContentModelDetail
    {
        public long categoryID { get; set; }
        public string name { get; set; }
        public List<ProductCategory> lstCategory { get; set; }
        public IPagedList<Content> pageList;
    }
}
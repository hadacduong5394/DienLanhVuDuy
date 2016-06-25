using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;
namespace Shopper.Models.ViewDetailModel
{
    public class ContentDetailModel
    {
        public Content content { get; set; }

        public List<Content> lstWouldYouLikeView { get; set; }
    }
}
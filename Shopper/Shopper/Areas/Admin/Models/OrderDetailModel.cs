using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;

namespace Shopper.Areas.Admin.Models
{
    public class OrderDetailModel
    {
        public OrderDetail orderDetail { get; set; }
        public string productName { get; set; }
        public string code { get; set; }
    }
}
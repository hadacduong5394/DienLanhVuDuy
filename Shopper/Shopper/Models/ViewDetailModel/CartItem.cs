using Shopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopper.Models.ViewDetailModel
{
    [Serializable]
    public class CartItem
    {

        public Product product { get; set; }
        public int quantity { get; set; }

    }
}
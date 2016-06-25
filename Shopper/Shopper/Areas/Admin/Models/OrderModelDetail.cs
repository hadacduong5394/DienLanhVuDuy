using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.DAO;
using Shopper.Models.EF;

namespace Shopper.Areas.Admin.Models
{
    public class OrderModelDetail
    {

        public Order order { get; set; }
        public string userName { get; set; }
        public List<OrderDetailModel> lstDetail { get; set; }
        public string total { get; set; }

        public OrderModelDetail(Order model, List<OrderDetailModel> lst, string userName)
        {
            this.order = model;
            this.lstDetail = lst;
            this.userName = userName;
            decimal t = 0;
            foreach (var item in lst)
            {
                t += (item.orderDetail.Price * item.orderDetail.Quantity).Value;
            }

            this.total = t.ToString("N0");
        }

    }
}
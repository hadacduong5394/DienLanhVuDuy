using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;
using System.Transactions;

namespace Shopper.Models.DAO
{
    public class OrderDetailDAO
    {
        OnlineShopDbContext context = null;
        public OrderDetailDAO()
        {
            context = new OnlineShopDbContext();
        }

        //public bool insertOrderDetail(long orderID, List<OrderDetail> lstItem)
        //{
        //    try
        //    {
        //        using (var scope = new System.Transactions.TransactionScope())
        //        {
        //            foreach(var item in lstItem)
        //            {
        //                item.OrderID = orderID;
        //                context.OrderDetails.Add(item);
        //            }
        //            context.SaveChanges();
        //            scope.Complete();
        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
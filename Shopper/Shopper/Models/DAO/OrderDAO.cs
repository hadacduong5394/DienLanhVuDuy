using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;
using System.Transactions;
using Shopper.Areas.Admin.Models;

namespace Shopper.Models.DAO
{
    public class OrderDAO
    {
        OnlineShopDbContext context = null;
        public OrderDAO()
        {
            context = new OnlineShopDbContext();
        }

        public bool createOrder(Order model, List<OrderDetail> lstItem)
        {

            using (var scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var order = new Order();
                    order.CustomerID = model.CustomerID;
                    order.CreateDate = model.CreateDate;
                    order.ShipName = model.ShipName;
                    order.ShipMobile = model.ShipMobile;
                    order.ShipEmail = model.ShipEmail;
                    order.ShipAddress = model.ShipAddress;
                    order.Status = model.Status;
                    order.CustomerID = model.CustomerID;
                    context.Orders.Add(order);
                    context.SaveChanges();
                    foreach (var item in lstItem)
                    {
                        item.OrderID = order.ID;
                        context.OrderDetails.Add(item);
                    }
                    context.SaveChanges();
                    scope.Complete();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public List<Order> getOrders(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return context.Orders.OrderByDescending(n => n.CreateDate).ToList();
            }
            return context.Orders.Where(n => n.ShipName.Contains(searchString)).OrderByDescending(n => n.CreateDate).ToList();
        }

        public Order getByID(long id)
        {
            return context.Orders.Find(id);
        }

        public bool Delete(long id,out string errorMess)
        {
            using (var scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var x = context.Orders.Where(n=>n.ID == id).SingleOrDefault();
                    if(x.Status == 0){
                        errorMess = "Đơn hàng này chưa thanh toán, Hãy thanh toán trước khi xóa đơn hàng!";
                        return false;
                    }

                    var lst = (from a in context.OrderDetails
                               where a.OrderID == id
                               select a).ToList();

                    foreach (var item in lst)
                    {
                        context.OrderDetails.Remove(item);
                    }
                    var order = context.Orders.Find(id);
                    context.Orders.Remove(order);
                    context.SaveChanges();
                    scope.Complete();
                    errorMess = "Thanh toán xong!";
                    return true;
                }
                catch (Exception ex)
                {
                    errorMess = "Lỗi: " + ex;
                    return false;
                }
            }
        }

        public OrderModelDetail detail(long id)
        {
            var order = context.Orders.Find(id);
            var lstDetail = (from a in context.OrderDetails
                             join b in context.Products on a.ProductID equals b.ID
                             where a.OrderID == id
                             select new OrderDetailModel
                             {
                                 orderDetail = a,
                                 productName = b.Name,
                                 code = b.Code
                             }).ToList();
            var user = context.Users.Where(n => n.ID == order.CustomerID).SingleOrDefault();
            return new OrderModelDetail(order, lstDetail, user.UserName);
        }

        public bool Success(long orderID)
        {
            using (var scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var order = context.Orders.Find(orderID);
                    var lstDetail = context.OrderDetails.Where(n => n.OrderID == order.ID).ToList();
                    foreach (var item in lstDetail)
                    {
                        var quantityPay = item.Quantity;
                        var product = context.Products.Where(n => n.ID == item.ProductID).SingleOrDefault();
                        product.Quantity = (product.Quantity - quantityPay);
                    }
                    order.Status = 1;
                    context.SaveChanges();
                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public int getNumberOrderNotSuccess()
        {
            return context.Orders.Where(n => n.Status == 0).Count();
        }
    }
}
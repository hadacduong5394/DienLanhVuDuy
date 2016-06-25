using Shopper.Models.ViewDetailModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopper.Models.EF;
using Shopper.Models.DAO;
using System.Web.Script.Serialization;
using Shopper.Models;
using System.Text;


namespace Shopper.Controllers
{
    public class CartController : BaseController
    {
        public const string CartSession = "CART_SESSION";
        ProductDAO daoproduct = new ProductDAO();
        OrderDAO daoOrder = new OrderDAO();
        UserDAO daoUser = new UserDAO();
        OrderDetailDAO daoOrderDetail = new OrderDetailDAO();
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            var cartSession = Session[CartSession];
            var lstItem = new List<CartItem>();
            if (cartSession != null)
            {
                lstItem = cartSession as List<CartItem>;
            }
            return View(lstItem);
        }

        public ActionResult AddItem(long productID, int quantity)
        {
            var item = daoproduct.getProductByID(productID);
            var cartSession = Session[CartSession];
            if (cartSession != null)
            {
                var lstItem = cartSession as List<CartItem>;
                if (lstItem.Exists(x => x.product.ID == item.ID))
                {
                    foreach (var n in lstItem)
                    {
                        if (n.product.ID == item.ID)
                        {
                            n.quantity += quantity;
                        }
                    }
                }
                else
                {
                    CartItem cItem = new CartItem();
                    cItem.product = item;
                    cItem.quantity = quantity;
                    lstItem.Add(cItem);
                }
                Session[CartSession] = lstItem;
            }
            else
            {
                var cartItem = new CartItem();
                cartItem.product = item;
                cartItem.quantity = quantity;
                var lst = new List<CartItem>();
                lst.Add(cartItem);
                Session[CartSession] = lst;
            }
            SetAlert("Đã thêm sản phẩm vào giỏ hàng", "success");
            return RedirectToAction("Index");
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;

            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long productID)
        {
            var cartSession = Session[CartSession];
            var lstItem = cartSession as List<CartItem>;
            lstItem.RemoveAll(n => n.product.ID == productID);
            Session[CartSession] = lstItem;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult UpdateCart(string model)
        {
            try
            {
                var cartList = new JavaScriptSerializer().Deserialize<List<CartItem>>(model);
                var cartSession = Session[CartSession];
                var lstItem = cartSession as List<CartItem>;

                foreach (var item in lstItem)
                {
                    var jsonItem = cartList.SingleOrDefault(n => n.product.ID == item.product.ID);
                    if (jsonItem != null)
                    {
                        if (!daoproduct.checkQuantityToPay(jsonItem.product.ID, jsonItem.quantity))
                        {
                            return Json(new
                            {
                                data = jsonItem.product.ID,
                                status = 0
                            });
                        }

                        item.quantity = jsonItem.quantity;
                    }
                }
                Session[CartSession] = lstItem;
                return Json(new
                {
                    status = 1
                });
            }
            catch
            {
                SetAlert("Cố lỗi xảy ra, hãy thử lại", "error");
                return null;
            }
        }

        public ActionResult UpdateCartToPay()
        {
            var cartSession = Session[CartSession];
            var lstItem = new List<CartItem>();
            if (cartSession != null)
            {
                lstItem = cartSession as List<CartItem>;
            }
            SetAlert("Đã cập nhật lại thông tin trong giỏ hàng", "success");
            return View(lstItem);
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var user = Session[Common.Constans.SESSION_LOGIN_USER] as UserSession;
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (Session[CartSession] == null)
            {
                SetAlert("Giỏ hàng đang trống", "warning");
                return RedirectToAction("Index","Home");
            }
            SetAlert("Hãy điền đầy đủ thông tin người nhận hàng", "success");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(Order model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var lstItem = Session[CartSession] as List<CartItem>;

                    var lstItemOrderDetail = new List<OrderDetail>();
                    model.CreateDate = DateTime.Now;
                    model.Status = 0;
                    var user = Session[Common.Constans.SESSION_LOGIN_USER] as UserSession;
                    model.CustomerID = daoUser.getByName(user.UserName).ID;
                    decimal total = 0;
                    foreach (var item in lstItem)
                    {
                        OrderDetail detail = new OrderDetail();
                        detail.ProductID = item.product.ID;
                        detail.Quantity = item.quantity;
                        if (item.product.PromotionPrice > 0)
                        {
                            detail.Price = item.product.PromotionPrice;
                        }
                        else
                        {
                            detail.Price = item.product.Price;
                        }
                        lstItemOrderDetail.Add(detail);
                        total += (detail.Price * detail.Quantity).Value;
                    }

                    if (daoOrder.createOrder(model, lstItemOrderDetail))
                    {
                        //send mail
                        EmailService mail = new EmailService();
                        //mail to admin
                        string subj = "Đơn hàng mới từ khách hàng: " + model.ShipName;
                        string contentToAdmin = System.IO.File.ReadAllText(Server.MapPath("~/AssetsClient/PaymentMailToAdmin.html"));
                        contentToAdmin = contentToAdmin.Replace("{{CustomerName}}", model.ShipName);
                        contentToAdmin = contentToAdmin.Replace("{{Phone}}", model.ShipMobile);
                        contentToAdmin = contentToAdmin.Replace("{{Email}}", model.ShipEmail);
                        contentToAdmin = contentToAdmin.Replace("{{Address}}", model.ShipAddress);
                        StringBuilder detail = new StringBuilder();
                        foreach (var item in lstItem)
                        {
                            if (item.product.PromotionPrice > 0)
                            {
                                detail.Append(item.product.Name + "  x  " + item.quantity + " x " + item.product.PromotionPrice.Value + " = " + (item.quantity * item.product.PromotionPrice).Value + "<br/>");
                            }
                            else
                            {
                                detail.Append(item.product.Name + "  x  " + item.quantity + " x " + item.product.Price.Value + " = " + (item.quantity * item.product.Price).Value + "<br/>");
                            }
                        }
                        contentToAdmin = contentToAdmin.Replace("{{Detail}}", detail.ToString());
                        contentToAdmin = contentToAdmin.Replace("{{Total}}", total.ToString("N0"));
                        mail.sendEmail(Common.Constans.ADMIN_EMAIL, subj, contentToAdmin);

                        //mail to customer
                        string subj1 = "thông tin từ DienLanhVuDuy tới khách hàng : " + model.ShipName;
                        string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/AssetsClient/PaymentMailToCutomer.html"));
                        contentCustomer = contentCustomer.Replace("{{CustomerName}}", model.ShipName);
                        contentCustomer = contentCustomer.Replace("{{Phone}}", model.ShipMobile);
                        contentCustomer = contentCustomer.Replace("{{Email}}", model.ShipEmail);
                        contentCustomer = contentCustomer.Replace("{{Address}}", model.ShipAddress);
                        contentCustomer = contentCustomer.Replace("{{Detail}}", detail.ToString());
                        contentCustomer = contentCustomer.Replace("{{Total}}", total.ToString("N0"));
                        mail.sendEmail(model.ShipEmail, subj1, contentCustomer);

                        Session[CartSession] = null;
                        SetAlert("Cảm ơn quý khách đã mua sắm tại cửa hàng!\n Hãy kiểm tra Email để xem lại thông tin các mặt hàng đã mua", "success");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        SetAlert("Có lỗi xảy ra trong quá trình xử, hãy thử lại!", "error");
                        return View(model);
                    }
                }
                catch
                {
                    SetAlert("Có lỗi xảy ra trong quá trình xử lý, hãy thử lại!", "error");
                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult SuccessFull()
        {
            return View();
        }

        public PartialViewResult Total()
        {
            var cartSession = Session[CartSession];
            var lstItem = cartSession as List<CartItem>;
            decimal total = 0;
            if (lstItem != null)
            {
                foreach (var item in lstItem)
                {
                    if (item.product.PromotionPrice > 0)
                    {
                        total += (item.product.PromotionPrice * item.quantity).Value;
                    }
                    else
                    {
                        total += (item.product.Price * item.quantity).Value;
                    }
                }
            }
            CartItemTotal model = new CartItemTotal();
            model.total = total;
            return PartialView(model);
        }
    }
}
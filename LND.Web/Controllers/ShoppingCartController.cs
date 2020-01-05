using AutoMapper;
using LND.Common;
using LND.Model.Models;
using LND.Service;
using LND.Web.App_Start;
using LND.Web.Infrastructure.Extensions;
using LND.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LND.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductService _productService;
        private IOrderService _orderService;
        private ApplicationUserManager _userManager;

        public object ListProduct { get; private set; }

        public ShoppingCartController(IOrderService orderService, IProductService productService, ApplicationUserManager userManager)
        {
            this._productService = productService;
            this._userManager = userManager;
            this._orderService = orderService;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return View();
        }

        //public ActionResult CheckOut()
        //{
        //    if (Session[CommonConstants.SessionCart] == null)
        //    {
        //        return Redirect("/gio-hang");
        //    }
        //    return View();
        //}

        public JsonResult GetAll()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            return Json(new
            {
                data = cart,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;
                var product = _productService.GetById(productId);
                newItem.Product = Mapper.Map<Product, ProductViewModel>(product);
                newItem.Quantity = 1;
                cart.Add(newItem);
            }

            Session[CommonConstants.SessionCart] = cart;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cartSession != null)
            {
                cartSession.RemoveAll(x => x.ProductId == productId);
                Session[CommonConstants.SessionCart] = cartSession;
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);

            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            foreach (var item in cartSession)
            {
                foreach (var jitem in cartViewModel)
                {
                    if (item.ProductId == jitem.ProductId)
                    {
                        item.Quantity = jitem.Quantity;
                    }
                }
            }

            Session[CommonConstants.SessionCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return Json(new
            {
                status = true
            });
        }

        public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindById(userId);
                return Json(new
                {
                    data = user,
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        public JsonResult CreateOrder(string orderViewModel)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            var orderNew = new Order();        
            orderNew.UpdateOrder(order);

            if (Request.IsAuthenticated)
            {
                orderNew.CustomerId = User.Identity.GetUserId();
                orderNew.CreatedBy = User.Identity.GetUserName();
            }

            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            var admin = new OrderAdmin()
            {
                CustomerName = orderNew.CustomerName,
                CustomerMobile = orderNew.CustomerMobile,
                CustomerAddress = orderNew.CustomerAddress,
                CustomerEmail = orderNew.CustomerEmail,
                CreatedDate = orderNew.CreatedDate,
                PaymentMethod = orderNew.PaymentMethod,
                PaymentStatus = "Chưa thanh toán",
                CustomerMessage = orderNew.CustomerMessage,
                OrderStatus = "Chờ duyệt",
                TotalPrice = 0             
            };
            foreach (var item in cart)
            {
                var detail = new OrderDetail();
                detail.ProductID = item.ProductId;
                detail.Quantitty = item.Quantity;
                decimal pricePromotion = Convert.ToDecimal(item.Product.Price * (1 - item.Product.PromotionPrice / 100));
                detail.Price = pricePromotion;
                orderDetails.Add(detail);

                //admin
                admin.ProductName += item.Product.Name + ", ";
                admin.Quantitty += item.Quantity + ", ";
                admin.Price += pricePromotion + ", ";
                admin.TotalPrice += pricePromotion * item.Quantity;
                admin.TotalPriceIn = item.Product.PriceIn * item.Quantity;             
            }
             admin.Income = admin.TotalPrice - admin.TotalPriceIn;
            _orderService.Create(orderNew, orderDetails);
            _orderService.CreateOrderAdmin(admin);

            SendMailCreateOrder(admin);
            return Json(new
            {
                status = true
            });
        }

        public void SendMailCreateOrder(OrderAdmin order)
        {
            string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/mail/OrderSuccess.html"));
            content = content.Replace("{{FullName}}", order.CustomerName);
            content = content.Replace("{{Address}}", order.CustomerAddress);
            content = content.Replace("{{MobilePhone}}", order.CustomerMobile);
            content = content.Replace("{{Products}}", order.ProductName);
            string money = Convert.ToString(order.TotalPrice);
            content = content.Replace("{{Total}}", money);
            string date = Convert.ToString(order.CreatedDate);
            content = content.Replace("{{CreatedDate}}", date);

            MailHelper.SendMail(order.CustomerEmail, "Đặt hàng thành công", content);
        }
    }
}
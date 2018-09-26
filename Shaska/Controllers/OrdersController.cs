using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Shaska.ViewModel;
using System.Data.Entity;
using Shaska.Models;
using ZXing;
using System.IO;

namespace Shaska.Controllers
{
    //  [Authorize(Roles = RoleName.CanManageApplication)]
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;
        private string CartEmptyMsg;
        private string LoginMsg;
        public OrdersController()
        {
            _context = new ApplicationDbContext();
            CartEmptyMsg = "Your cart is Empty please select some items!";
            LoginMsg = "Please login First !";
        }
        // GET: Orders

        public ActionResult Index()
        {
            return View();
        }
        //   iski privelge sirf user ko deni ha annonymous nh rakhni ha
        [Authorize]
        public ActionResult ConfirmOrders()
        {
            var memberId = User.Identity.GetUserId();

            var user = _context.Users.SingleOrDefault(m => m.Id == memberId);
            if (user == null)
            {
                ViewBag.message = LoginMsg;
                return View("Message");
            }

            var carts = _context.Carts.Include(m => m.product).Where(m => m.UserId == memberId).ToList();
            if (carts.Count == 0)
            {
                ViewBag.message = CartEmptyMsg;
                return View("Message");
            }

            float totalprize = 0;
            List<string> Names = new List<string>();

            foreach (var item in carts)
            {
                totalprize = totalprize + item.product.ProductPrice;
                Names.Add(item.product.ProductName);
            }

            var orderCustomerViewModel = new OrderCustomerViewModel()
            {
                Name = user.FirstName + " " + user.LastName,
                Email = user.Email,
                PhoneNumber = user.CellNumber,
                Prize = totalprize,
                ProductNames = Names

            };
            return View(orderCustomerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        public ActionResult Save(OrderCustomerViewModel model)
        {
            var memberId = User.Identity.GetUserId();
            var carts = _context.Carts.Where(m => m.UserId == memberId).ToList();
            if (carts.Count == 0)
            {
                ViewBag.message = CartEmptyMsg;
                return View("Message");
            }
            Order order;
            foreach (var item in carts)
            {
                order = new Order()
                {
                    ApplicationUserId = memberId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    OrderPostDate = DateTime.Now
                };

                _context.Orders.Add(order);
                _context.SaveChanges();
                _context.Carts.Remove(item);
                _context.SaveChanges();

                var OldSearchHistory = _context.Recommendations.SingleOrDefault(c => c.ApplicationUserId == memberId && c.Count == 1);
                if (OldSearchHistory != null)
                {
                    OldSearchHistory.Count = -2;
                    _context.SaveChanges();
                }

                var recommendation = _context.Recommendations.SingleOrDefault(c => c.ProductId == item.ProductId && c.ApplicationUserId == memberId);

                if (recommendation == null)
                {
                    recommendation = new Recommendation()
                    {
                        ApplicationUserId = memberId,
                        ProductId = item.ProductId,
                        Count = 1
                    };
                    _context.Recommendations.Add(recommendation);
                    _context.SaveChanges();
                }
                else
                {
                    recommendation.ProductId = item.ProductId;
                    recommendation.ApplicationUserId = memberId;
                    recommendation.Count = 1;
                    _context.SaveChanges();
                }

            }
            return View();
        }
        //details
        [Authorize]
        public ActionResult Details(string Id)
        {
            ViewBag.id = Id;
            if (User.IsInRole("CanManageApplication"))
                return View("Details");

            return View("OrdersDetailsForUser");
        }

        //OrderConfirmByAdmin
        [Authorize(Roles = RoleName.CanManageApplication)]
        public ActionResult ConfirmOrderByAdmin(string id)
        {
            //Generate barcode
            var barcodeWriter = new BarcodeWriter();

            // set the barcode format
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            //barcodeWriter
            // write text and generate a 2-D barcode as a bitmap
            barcodeWriter.Write(id)
                .Save(Server.MapPath(@"~/Images/Order Barcodes/" + id + ".png"));

            Random RandomKey = new Random();
            int Key = RandomKey.Next(1, 1000);
            
            var ActiveApplicationUser = User.Identity.GetUserId();
            var orders = _context.Orders.Where(m => m.ApplicationUserId == id).ToList();
            foreach (var item in orders)
            {
                item.Status = "Order Confirmed";
                item.OrderConfirmDate = DateTime.Now;
                item.OrderConfirmById = ActiveApplicationUser;
                item.Key = Key;
                _context.SaveChanges();
            }
            ViewBag.Barcode = id + ".png";
            //return View("Index");
            return View("BarcodeImage");
        }

        public ActionResult OrdersForRider(string id)
        {
            return View();
        }
        //OrdersPickedUpByRider
        public ActionResult OrdersPickedUpByRider(string OrdersTaken)
        {
            var CurrentUserId = User.Identity.GetUserId();
            var ApplicationUserIds = OrdersTaken.Split(',');
            if (ApplicationUserIds.Count() > 0)
            {
                foreach (var ApplicationUserId in ApplicationUserIds)
                {
                    if (!String.IsNullOrEmpty(ApplicationUserId))
                    {
                        var Orders = _context.Orders.Where(m => m.ApplicationUserId == ApplicationUserId).ToList();
                        foreach (var order in Orders)
                        {
                            order.OrderAssignToId = CurrentUserId;
                            order.Status = "Picked-Up By Rider";
                            _context.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("OrdersForRider", "Orders");
        }

        //
        public ActionResult QRScanner()
        {
            return View();
        }
        //
        public ActionResult OrderDelivered(int Key , string Id)
        {
            var order = _context.Orders.Where(m=> m.ApplicationUserId==Id && m.Key==Key).ToList();

            if (order.Count() <= 0)
            {
                return View("KeyMisMatchError");
            }

            var Orders = _context.Orders.Where(m => m.ApplicationUserId == Id && m.Key == Key).ToList();
            foreach(var Order in Orders)
            {
                Order.Status = "Delivered";
                _context.SaveChanges();
            }

            return View("OrdersforRider");
        }
    }
}
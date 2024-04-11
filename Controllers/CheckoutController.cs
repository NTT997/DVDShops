using DVDShops.Models;
using DVDShops.Services.Carts;
using DVDShops.Services.Orders;
using DVDShops.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DVDShops.Controllers
{
    [Route("checkout")]
    public class CheckoutController : Controller
    {
        private ICartService cartService;
        private IUserService userService;
        private IOrderService orderService;
        public CheckoutController(ICartService cartService, IUserService userService, IOrderService orderService)
        {
            this.cartService = cartService;
            this.userService = userService;
            this.orderService = orderService;
        }

        [Route("checkout")]
        [Route("")]
        public IActionResult Checkout()
        {
            string email = HttpContext.Session.GetString("email");
            if (email == null)
            {
                TempData["msg"] = "Please Login First!";
                return RedirectToAction("login", "login");
            }

            var user = userService.GetByEmail(email);
            var cart = cartService.GetByUserId(user.UsersId);

            ViewBag.Cart = cart;
            ViewBag.User = user;
            ViewBag.Total = cart.Sum(c => c.Price * c.Quantity);
            return View();
        }

        [Route("placeOrder")]
        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            if (order.Note == "cash")
            {
                order.OrderStatus = true;
            }
            else
            {
                order.OrderStatus = false;
            }

            if (order.OrderStatus)
            {
                var result = orderService.Create(order);

            }

            Debug.WriteLine("=====================");
            return View("checkout");
        }


        [Route("success")]
        [HttpGet]
        public IActionResult Success(string address_street, string address_city, string address_state, string payment_date)
        {
            string email = HttpContext.Session.GetString("email");
            if (email == null)
            {
                TempData["msg"] = "Please Login First!";
                return RedirectToAction("login", "login");
            }

            var user = userService.GetByEmail(email);
            var cart = cartService.GetByUserId(user.UsersId);

            var order = new Order
            {
                UsersId = user.UsersId,
                OrderDate = DateOnly.Parse(payment_date.Substring(0, 10)),
                OrderStatus = true,
                TotalAmount = cart.Sum(c => c.Price * c.Quantity),
                ShipAddress = $"{address_street} - {address_city} - {address_state}",
                Note = "Pay by PayPal"
            };

            if (orderService.Create(order))
            {
                foreach (var item in cart)
                {
                    cartService.Delete(item.CartId);
                }
            }

            TempData["msg"] = "Thank you for your purchase!";
            return RedirectToAction("shop", "shop");
        }
    }
}

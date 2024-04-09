using DVDShops.Models;
using DVDShops.Services.Carts;
using DVDShops.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace DVDShops.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private ICartService cartService;
        private IUserService userService;
        public CartController(ICartService cartService, IUserService userService)
        {
            this.cartService = cartService;
            this.userService = userService;
        }

        [Route("cart")]
        [Route("")]
        public IActionResult Cart()
        {
            var cart = cartService.GetByUserId(userService.GetByEmail(HttpContext.Session.GetString("email")).UsersId);
            ViewBag.Cart = cart;
            return View();
        }

        [Route("addTocart")]
        public IActionResult addTocart(string quantity, int productId)
        {
            int parsedQuantity;
            if (!int.TryParse(quantity, out parsedQuantity))
            {
                TempData["msg"] = "Invalid Quantity";
                return RedirectToAction("shop", "shop");
            };

            var user = userService.GetByEmail(HttpContext.Session.GetString("email"));
            var usersCart = cartService.GetByUserId(user.UsersId);

            if (usersCart.IsNullOrEmpty())
            {
                var cart = new Cart
                {
                    UsersId = user.UsersId,
                    ProductId = productId,
                    Quantity = parsedQuantity,
                };

                cartService.Create(cart);
            }
            else
            {
                var productInCart = usersCart.FirstOrDefault(prod => prod.ProductId == productId);
                productInCart.Quantity += parsedQuantity;
                cartService.Update(productInCart);
            }

            return RedirectToAction("cart");
        }

        [Route("remove")]
        public IActionResult Remove(int cartId)
        {
            cartService.Delete(cartId);
            TempData["msg"] = "Removed Item!";
            return RedirectToAction("cart", "cart");
        }
    }
}

using DVDShops.Models;
using DVDShops.Services.Carts;
using DVDShops.Services.Producers;
using DVDShops.Services.Products;
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
        private IProductService productService;
        public CartController(ICartService cartService, IUserService userService, IProductService productService)
        {
            this.cartService = cartService;
            this.userService = userService;
            this.productService = productService;
        }

        [Route("cart")]
        [Route("")]
        public IActionResult Cart()
        {
            string email = HttpContext.Session.GetString("email");
            if (email == null)
            {
                TempData["msg"] = "Please Login Before Using Cart!";
                return RedirectToAction("login", "login");
            }

            var user = userService.GetByEmail(email);
            var usersCart = cartService.GetByUserId(user.UsersId);
            ViewBag.Cart = usersCart;
            ViewBag.Total = usersCart.Sum(c => c.Price * c.Quantity);
            return View();
        }

        [Route("addTocart")]
        [HttpPost]
        public IActionResult addTocart(int quantity, int productId)
        {
            var user = userService.GetByEmail(HttpContext.Session.GetString("email"));
            var usersCart = cartService.GetByUserId(user.UsersId);
            var product = productService.GetById(productId);
            var price = @Math.Round((double)(product.ProductPrice * (1 - product.Promotion.PromotionPercent / 100)), 2);
            if (usersCart.IsNullOrEmpty())
            {
                var cart = new Cart
                {
                    UsersId = user.UsersId,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = price,
                };

                cartService.Create(cart);
            }
            else
            {
                if (!usersCart.Any(c => c.ProductId == productId))
                {
                    var newProdInCart = new Cart
                    {
                        UsersId = user.UsersId,
                        ProductId = productId,
                        Price = price,
                        Quantity = quantity
                    };
                    cartService.Create(newProdInCart);
                }
                else
                {
                    var existedProd = cartService.GetByProductId(productId);
                    existedProd.Quantity += quantity;
                    cartService.Update(existedProd);
                }
            }

            return RedirectToAction("shop", "shop");
        }

        [Route("remove")]
        public IActionResult Remove(int cartId)
        {
            cartService.Delete(cartId);
            TempData["msg"] = "Removed Item!";
            return RedirectToAction("cart", "cart");
        }

        [Route("empty")]
        public IActionResult Empty()
        {
            string email = HttpContext.Session.GetString("email");
            if (email == null)
            {
                TempData["msg"] = "Please Login Before Using Cart!";
                return RedirectToAction("login", "login");
            }

            var user = userService.GetByEmail(email);
            cartService.EmptyUserCart(user.UsersId);
            TempData["msg"] = "Removed Item!";
            return RedirectToAction("cart", "cart");
        }

        [Route("update")]
        [HttpPost]
        public IActionResult Update(List<int> cartIds, List<int> pQuantities)
        {
            for (int i = 0; i < cartIds.Count; i++)
            {
                var cart = cartService.GetById(cartIds[i]);
                cart.Quantity = pQuantities[i];
                cartService.Update(cart);
            }

            TempData["msg"] = "Cart Updated!";
            return RedirectToAction("cart");
        }
    }
}

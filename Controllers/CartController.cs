using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        [Route("cart")]
        [Route("")]
        public IActionResult Cart(string quantity, int productId)
        {
            int parsedQuantity;
            if (!int.TryParse(quantity, out parsedQuantity))
            {
                return RedirectToAction("detail", "detail", new { productId = productId });
            }
            if (quantity == null) { }
            return View();
        }
    }
}

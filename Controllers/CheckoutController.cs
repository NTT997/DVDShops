using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("checkout")]
    public class CheckoutController : Controller
    {
        [Route("checkout")]
        [Route("")]
        public IActionResult Checkout()
        {
            return View();
        }
    }
}

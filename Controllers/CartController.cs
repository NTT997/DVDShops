using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        [Route("cart")]
        [Route("")]
        public IActionResult Cart()
        {
            return View();
        }
    }
}

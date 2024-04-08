using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("shop")]
    public class ShopController : Controller
    {
        [Route("shop")]
        [Route("")]
        public IActionResult Shop()
        {
            return View();
        }
    }
}

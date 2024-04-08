using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("detail")]
    public class DetailController : Controller
    {
        [Route("detail")]
        [Route("")]
        public IActionResult Detail()
        {
            return View();
        }
    }
}

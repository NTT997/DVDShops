using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("news")]
    public class NewsController : Controller
    {
        [Route("news")]
        [Route("")]
        public IActionResult News()
        {
            return View();
        }
    }
}

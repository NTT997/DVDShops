using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("index")]
    public class IndexController : Controller
    {

        [Route("index")]
        [Route("")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}

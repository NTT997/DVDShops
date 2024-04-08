using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("index")]
    public class IndexController : Controller
    {

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

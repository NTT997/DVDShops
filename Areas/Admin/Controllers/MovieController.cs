using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Areas.Admin.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

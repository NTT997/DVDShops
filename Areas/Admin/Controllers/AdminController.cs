using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/index")]
    public class AdminController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}

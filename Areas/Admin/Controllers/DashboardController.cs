using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/dashboard")]
    public class DashboardController : Controller
    {
        [Route("index")]
        [Route("")]
        public IActionResult Dashboard()
        {
            return View("Dashboard");
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "index", new { area = "" });
        }
    }
}

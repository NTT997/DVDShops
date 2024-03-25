using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class DashboardController : Controller
    {
        [Route("")]
        [Route("dashboard")]
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

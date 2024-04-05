using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

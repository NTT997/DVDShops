using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/user")]
    public class UserController : Controller
    {

        public UserController() { }

        [Route("")]
        [Route("view")]
        public IActionResult UserView()
        {
            return View("UserView");
        }
    }
}

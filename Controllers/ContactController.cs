using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        [Route("contact")]
        [Route("")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}

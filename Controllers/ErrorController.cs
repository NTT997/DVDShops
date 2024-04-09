using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("PageNotFound")]
        [Route("")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("movies")]
    public class MoviesController : Controller
    {
        [Route("movies")]
        [Route("")]
        public IActionResult Movies()
        {
            return View();
        }
    }
}

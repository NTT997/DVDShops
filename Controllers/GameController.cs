using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("game")]
    public class GameController : Controller
    {
        [Route("game")]
        [Route("")]
        public IActionResult Game()
        {
            return View();
        }
    }
}

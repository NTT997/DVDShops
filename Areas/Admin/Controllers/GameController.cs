using DVDShops.Services.Games;
using DVDShops.Services.GamesGenres;
using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/game")]
    public class GameController : Controller
    {
        private IGameService gameService;
        private IGameGenreService ggService;

        public GameController(IGameGenreService ggService, IGameService gameService)
        {
            this.gameService = gameService;
            this.ggService = ggService;
        }
        private void SetTempData(bool status, string title, string array)
        {
            TempData["status"] = status.ToString();
            TempData["title"] = title;
            TempData["message"] = array;
        }

        [Route("")]
        [Route("view")]
        public IActionResult GameView()
        {
            return View("GameView");
        }
    }
}

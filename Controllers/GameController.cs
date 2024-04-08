using DVDShops.Models;
using DVDShops.Services.Games;
using DVDShops.Services.Producers;
using DVDShops.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DVDShops.Controllers
{
    [Route("game")]
    public class GameController : Controller
    {
        private IGameService gameService;
        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [Route("game")]
        [Route("")]
        public IActionResult Game()
        {
            ViewBag.GameList = gameService.GetAll();
            return View();
        }

        [Route("sort")]
        public IActionResult Sort(string type)
        {
            var list = new List<Game>();
            if (type == "latest")
            {
                list = gameService.GetAll().OrderByDescending(list => list.GameTitle).ToList();
            }
            if (type == "popularity")
            {
                list = gameService.GetAll().OrderByDescending(list => list.GameTrailer).ToList();
            }
            if (type == "rating")
            {
                list = gameService.GetAll().OrderByDescending(list => list.GameId).ToList();
            }
            ViewBag.GameList = list;
            return View("game");
        }

        [Route("search")]
        public IActionResult Search(string keyword)
        {
            var list = gameService.GetAll();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                list = gameService.GetAll().Where(p => p.GameTitle.ToLower().Contains(keyword)).ToList();
            }

            ViewBag.GameList = list;
            return View("game");
        }
    }
}

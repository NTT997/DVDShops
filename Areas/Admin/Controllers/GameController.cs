using DVDShops.Helpers;
using DVDShops.Models;
using DVDShops.Services.Games;
using DVDShops.Services.GamesGenres;
using DVDShops.Services.SongsGenres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text.RegularExpressions;

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

        [Route("addGame")]
        [HttpGet]
        public IActionResult GameAdd()
        {
            return View("GameAdd");
        }

        [Route("addGame")]
        [HttpPost]
        public IActionResult GameAdd(Game game, List<string> genres)
        {
            if (string.IsNullOrWhiteSpace(game.GameTitle) || string.IsNullOrWhiteSpace(game.GameDescription) ||
                string.IsNullOrWhiteSpace(game.GameTrailer) || string.IsNullOrWhiteSpace(game.DownloadLink))
            {
                SetTempData(false, "Create Game Failed!", "Some Input Field Is White Space Only!");
                return View("GameAdd", game);
            }

            if (!CheckRegex.CheckLink(game.GameTrailer) || !CheckRegex.CheckLink(game.DownloadLink))
            {
                
                SetTempData(false, "Create Game Failed!", "Url is Invalid!");
                return View("GameAdd", game);
            }

            if (genres.Count() < 1)
            {
                SetTempData(false, "Create Game Failed!", "Choose Atleast 1 Genre");
                return View("GameAdd", game);
            }

            if (game.ProducerId < 0)
            {
                SetTempData(false, "Create Game Failed!", "Please Choose a Producer!");
                return View("GameAdd", game);
            }

            game.CategoryId = 4;
            var result = gameService.Create(game);

            if (!result)
            {
                SetTempData(false, "Create Game Failed!", "Something Wrong, Could Not Create Game!");
                return View("GameAdd", game);
            }

            var newGame = gameService.GetByTitle(game.GameTitle);
            int[] genreIds = genres.Select(int.Parse).ToArray();
            foreach (var genreId in genreIds)
            {
                var newGg = new GamesGenre
                {
                    GameId = newGame.GameId,
                    GenreId = genreId,
                };
                ggService.Create(newGg);
            }

            SetTempData(true, "Create game Success!", $"{game.GameTitle} Added!");
            return RedirectToAction("view");
        }

        [Route("editGame")]
        [HttpGet]
        public IActionResult GameEdit(int gameId)
        {
            var game = gameService.GetById(gameId);
            return View("GameEdit", game);
        }

        [Route("editGame")]
        [HttpPost]
        public IActionResult GameEdit(Game game, string newTrailer, string newLink, List<string> genres)
        {
            if (!string.IsNullOrWhiteSpace(newTrailer))
            {
                if (newTrailer.Trim() == game.GameTrailer)
                {
                    SetTempData(false, "Update Game Failed!", "New Trailer Cannot be The Same As Current Trailer!");
                    return View("GameEdit", game);
                }

                if (!CheckRegex.CheckLink(newTrailer))
                {
                    SetTempData(false, "Update Game Failed!", "Trailer Link Invalid!");
                    return View("GameEdit", game);
                }

                if (!string.IsNullOrEmpty(newTrailer))
                {
                    game.GameTrailer = newTrailer;
                }
            }

            if (!string.IsNullOrWhiteSpace(newLink))
            {
                if (newLink.Trim() == game.DownloadLink)
                {
                    SetTempData(false, "Update Game Failed!", "New Download Link Cannot be The Same As Current Link!");
                    return View("GameEdit", game);
                }

                if (!CheckRegex.CheckLink(newLink))
                {
                    SetTempData(false, "Update Game Failed!", "Download Link Invalid!");
                    return View("GameEdit", game);
                }

                if (!string.IsNullOrEmpty(newLink))
                {
                    game.DownloadLink = newLink;
                }
            }

            if (string.IsNullOrWhiteSpace(game.GameTitle) || string.IsNullOrWhiteSpace(game.GameDescription))
            {
                SetTempData(false, "Update Game Failed!", "Some Input Field Is White Space Only!");
                return View("GameEdit", game);
            }

            if (genres.Count() < 1)
            {
                SetTempData(false, "Update Game Failed!", "Choose Atleast 1 Genre");
                return View("GameEdit", game);
            }

            if (game.ProducerId < 0)
            {
                SetTempData(false, "Update Game Failed!", "Please Choose a Producer!");
                return View("GameEdit", game);
            }

            var result = gameService.Update(game);
            if (!result)
            {
                SetTempData(false, "Update Game Failed!", "Something Wrong, Could Not Update Game!");
                return View("GameEdit", game);
            }

            var updateGenres = ggService.GetByGameId(game.GameId);
            foreach (var item in updateGenres)
            {
                ggService.Delete(item.Id);
            }

            int[] genreIds = genres.Select(int.Parse).ToArray();
            foreach (var genreId in genreIds)
            {
                var newGg = new GamesGenre
                {
                    GameId = game.GameId,
                    GenreId = genreId,
                };
                ggService.Create(newGg);
            }

            SetTempData(true, "Update Game Success!", $"{game.GameTitle} Updated!");
            return RedirectToAction("view");
        }

        [Route("deleteGame")]
        [HttpGet]
        public IActionResult GameDelete(int gameId)
        {
            var game = gameService.GetById(gameId);
            if (gameService.Delete(gameId))
            {
                SetTempData(true, "Delete Game Success!", $"Bye {game.GameTitle}!");
            }
            else
            {
                SetTempData(false, "Delete Game Failed!", "This Game Cannot be Deleted This Way!");
            }
            return RedirectToAction("view");
        }
    }
}

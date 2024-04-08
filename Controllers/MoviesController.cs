using DVDShops.Models;
using DVDShops.Services.Movies;
using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("movies")]
    public class MoviesController : Controller
    {
        private IMovieService movieService;
        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [Route("movies")]
        [Route("")]
        public IActionResult Movies()
        {
            ViewBag.MovieList = movieService.GetAll();
            return View();
        }

        [Route("sort")]
        public IActionResult Sort(string type)
        {
            var list = new List<Movie>();
            if (type == "latest")
            {
                list = movieService.GetAll().OrderByDescending(list => list.MovieTitle).ToList();
            }
            if (type == "popularity")
            {
                list = movieService.GetAll().OrderByDescending(list => list.MovieTrailer).ToList();
            }
            if (type == "rating")
            {
                list = movieService.GetAll().OrderByDescending(list => list.MovieId).ToList();
            }
            ViewBag.MovieList = list;

            return View("movies");
        }

        [Route("search")]
        public IActionResult Search(string keyword)
        {
            var list = movieService.GetAll();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                list = movieService.GetAll().Where(p => p.MovieTitle.ToLower().Contains(keyword)).ToList();
            }

            ViewBag.MovieList = list;
            return View("movies");
        }
    }
}

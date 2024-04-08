using DVDShops.Helpers;
using DVDShops.Models;
using DVDShops.Services.Advertises;
using DVDShops.Services.Games;
using DVDShops.Services.Movies;
using DVDShops.Services.Moviesgenres;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/movie")]
    public class MovieController : Controller
    {
        private IMovieService movieService;
        private IMovieGenreService mgService;
        private IWebHostEnvironment env;
        public MovieController(IMovieService movieService, IMovieGenreService mgService, IWebHostEnvironment env)
        {
            this.movieService = movieService;
            this.mgService = mgService;
            this.env = env;
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
            return View("MovieView");
        }

        [Route("addMovie")]
        [HttpGet]
        public IActionResult MovieAdd()
        {
            return View("MovieAdd");
        }

        [Route("addMovie")]
        [HttpPost]
        public IActionResult MovieAdd(Movie movie, List<string> genres, IFormFile coverImage)
        {
            if (string.IsNullOrWhiteSpace(movie.MovieTitle) || string.IsNullOrWhiteSpace(movie.MovieDescription) ||
                string.IsNullOrWhiteSpace(movie.MovieTrailer) || string.IsNullOrWhiteSpace(movie.DownloadLink))
            {
                SetTempData(false, "Create Game Failed!", "Some Input Field Is White Space Only!");
                return View("MovieAdd", movie);
            }

            if (!CheckRegex.CheckLink(movie.MovieTrailer) || !CheckRegex.CheckLink(movie.DownloadLink))
            {

                SetTempData(false, "Create Movie Failed!", "Url is Invalid!");
                return View("MovieAdd", movie);
            }

            if (genres.Count() < 1)
            {
                SetTempData(false, "Create Movie Failed!", "Choose Atleast 1 Genre");
                return View("MovieAdd", movie);
            }

            if (movie.ProducerId < 0)
            {
                SetTempData(false, "Create Movie Failed!", "Please Choose a Producer!");
                return View("MovieAdd", movie);
            }

            if (coverImage != null && coverImage.Length > 0)
            {
                string fileExtension = Path.GetExtension(coverImage.FileName);
                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".ico")
                {
                    SetTempData(false, "Create Movie Failed!", "Please Choose Correct File Extension!");
                    return View("MovieAdd", movie);
                }

                movie.MovieCover = Guid.NewGuid().ToString().Replace("-", "") + "_" + coverImage.FileName;
                var path = Path.Combine(env.WebRootPath, "images/movie", movie.MovieCover);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    coverImage.CopyTo(stream);
                }
            }
            else
            {
                movie.MovieCover = "no_image.jpg";
            }

            movie.CategoryId = 5;
            var result = movieService.Create(movie);

            if (!result)
            {
                SetTempData(false, "Create Movie Failed!", "Something Wrong, Could Not Create Movie!");
                return View("MovieAdd", movie);
            }

            var newMovie = movieService.GetByTitle(movie.MovieTitle);
            int[] genreIds = genres.Select(int.Parse).ToArray();
            foreach (var genreId in genreIds)
            {
                var newGg = new MoviesGenre
                {
                    MovieId = newMovie.MovieId,
                    GenreId = genreId,
                };
                mgService.Create(newGg);
            }

            SetTempData(true, "Create Movie Success!", $"{movie.MovieTitle} Added!");
            return RedirectToAction("view");
        }

        [Route("editMovie")]
        [HttpGet]
        public IActionResult MovieEdit(int movieId)
        {
            var movie = movieService.GetById(movieId);
            Debug.WriteLine("==========================");
            Debug.WriteLine(movie.MovieId);
            return View("MovieEdit", movie);
        }

        [Route("editMovie")]
        [HttpPost]
        public IActionResult MovieEdit(Movie movie, string newTrailer, string newLink, List<string> genres, IFormFile coverImage)
        {
            if (!string.IsNullOrWhiteSpace(newTrailer))
            {
                if (newTrailer.Trim() == movie.MovieTrailer)
                {
                    SetTempData(false, "Update Game Failed!", "New Trailer Cannot be The Same As Current Trailer!");
                    return View("MovieEdit", movie);
                }

                if (!CheckRegex.CheckLink(newTrailer))
                {
                    SetTempData(false, "Update Game Failed!", "Trailer Link Invalid!");
                    return View("MovieEdit", movie);
                }

                if (!string.IsNullOrEmpty(newTrailer))
                {
                    movie.MovieTrailer = newTrailer;
                }
            }

            if (!string.IsNullOrWhiteSpace(newLink))
            {
                if (newLink.Trim() == movie.DownloadLink)
                {
                    SetTempData(false, "Update Movie Failed!", "New Download Link Cannot be The Same As Current Link!");
                    return View("MovieEdit", movie);
                }

                if (!CheckRegex.CheckLink(newLink))
                {
                    SetTempData(false, "Update Movie Failed!", "Download Link Invalid!");
                    return View("MovieEdit", movie);
                }

                if (!string.IsNullOrEmpty(newLink))
                {
                    movie.DownloadLink = newLink;
                }
            }

            if (string.IsNullOrWhiteSpace(movie.MovieTitle) || string.IsNullOrWhiteSpace(movie.MovieDescription))
            {
                SetTempData(false, "Update Movie Failed!", "Some Input Field Is White Space Only!");
                return View("MovieEdit", movie);
            }

            if (genres.Count() < 1)
            {
                SetTempData(false, "Update Movie Failed!", "Choose Atleast 1 Genre");
                return View("MovieEdit", movie);
            }

            if (movie.ProducerId < 0)
            {
                SetTempData(false, "Update Movie Failed!", "Please Choose a Producer!");
                return View("MovieEdit", movie);
            }

            if (coverImage != null && coverImage.Length > 0)
            {
                string fileExtension = Path.GetExtension(coverImage.FileName);
                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".ico")
                {
                    SetTempData(false, "Update Movie Failed!", "Please Choose Correct File Extension!");
                    return View("MovieEdit", movie);
                }

                if (movie.MovieCover != "no_image.jpg")
                {
                    var oldFile = Path.Combine(env.WebRootPath, "images/movie", movie.MovieCover);
                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                }

                movie.MovieCover = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16) + "_" + coverImage.FileName;
                var path = Path.Combine(env.WebRootPath, "images/movie", movie.MovieCover);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    coverImage.CopyTo(stream);
                }
            }

            var result = movieService.Update(movie);
            if (!result)
            {
                SetTempData(false, "Update Movie Failed!", "Something Wrong, Could Not Update Movie!");
                return View("MovieEdit", movie);
            }

            var updateGenres = mgService.GetByMovieId(movie.MovieId);
            foreach (var item in updateGenres)
            {
                mgService.Delete(item.Id);
            }

            int[] genreIds = genres.Select(int.Parse).ToArray();
            foreach (var genreId in genreIds)
            {
                var newMg = new MoviesGenre
                {
                    MovieId = movie.MovieId,
                    GenreId = genreId,
                };
                mgService.Create(newMg);
            }

            SetTempData(true, "Update Movie Success!", $"{movie.MovieTitle} Updated!");
            return RedirectToAction("view");
        }

        [Route("deleteMovie")]
        [HttpGet]
        public IActionResult MovieDelete(int movieId)
        {
            var movie = movieService.GetById(movieId);
            if (movieService.Delete(movieId))
            {
                SetTempData(true, "Delete Movie Success!", $"Bye {movie.MovieTitle}!");
            }
            else
            {
                SetTempData(false, "Delete Movie Failed!", "This Movie Cannot be Deleted This Way!");
            }
            return RedirectToAction("view");
        }
    }
}

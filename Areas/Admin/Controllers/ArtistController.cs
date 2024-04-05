using DVDShops.Models;
using DVDShops.Services.Artists;
using DVDShops.Services.ArtistsGenres;
using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/artist")]
    public class ArtistController : Controller
    {
        private IArtistService artistService;
        private IArtistGenreService artistGenreService;
        private IWebHostEnvironment env;
        public ArtistController(IArtistService artistService, IArtistGenreService artistGenreService, IWebHostEnvironment env)
        {
            this.artistService = artistService;
            this.artistGenreService = artistGenreService;
            this.env = env;
        }

        [Route("view")]
        [Route("")]
        public IActionResult ArtistView(Artist artist)
        {
            return View("ArtistView", artist);
        }

        private void SetTempData(bool status, string title, string array)
        {
            TempData["status"] = status.ToString();
            TempData["title"] = title;
            TempData["message"] = array;
        }

        [Route("createArtist")]
        [HttpPost]
        public IActionResult CreateArtist(Artist artist, List<string> genres, IFormFile artistPhoto)
        {
            if (string.IsNullOrWhiteSpace(artist.ArtistName))
            {
                SetTempData(false, "Create Artist Failed!", "Artist's Name Is Must Have!");
                return RedirectToAction("view", artist);
            }

            if (artistService.GetByName(artist.ArtistName) != null)
            {
                SetTempData(false, "Create Artist Failed!", "Artist Already Existed!");
                return RedirectToAction("view", artist);
            }

            if (genres.Count() < 1)
            {
                SetTempData(false, "Create Artist Failed!", "Choose Atleast 1 Genre");
                return RedirectToAction("view", artist);
            }

            if (artistPhoto != null && artistPhoto.Length > 0)
            {
                string fileExtension = Path.GetExtension(artistPhoto.FileName);
                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".ico")
                {
                    SetTempData(false, "Update Artist Failed!", "Please Choose Correct File Extension!");
                    return View("view", artist);
                }

                artist.ArtistPhoto = Guid.NewGuid().ToString() + "_" + artistPhoto.FileName;
                var path = Path.Combine(env.WebRootPath, "/images/artist", artist.ArtistPhoto);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    artistPhoto.CopyTo(stream);
                }
            }
            else
            {
                artist.ArtistPhoto = "no-image.jpg";
            }

            var result = artistService.Create(artist);
            if (!result)
            {
                SetTempData(false, "Create Artist Failed!", "Something Wrong, Could Not Create Artist!");
                return RedirectToAction("view");
            }

            var newArtist = artistService.GetByName(artist.ArtistName);
            int[] genreIds = genres.Select(int.Parse).ToArray();
            foreach (var genreId in genreIds)
            {
                var newAg = new ArtistsGenre
                {
                    ArtistId = newArtist.ArtistId,
                    GenreId = genreId,
                };
                artistGenreService.Create(newAg);
            }

            SetTempData(true, "Create Artist Success!", "New Artist Has Been Added!");
            return RedirectToAction("view");
        }

        [Route("editArtist")]
        [HttpGet]
        public IActionResult EditArtist(int artistId)
        {
            var artist = artistService.GetById(artistId);
            return View("ArtistProfile", artist);
        }

        [Route("editArtist")]
        [HttpPost]
        public IActionResult EditArtist(Artist artist, List<string> genres, IFormFile artistPhoto, string oldName)
        {
            if (string.IsNullOrWhiteSpace(artist.ArtistName))
            {
                SetTempData(false, "Update Artist Failed!", "Artist's Name Is Must Have!");
                return View("ArtistProfile", artist);
            }

            if (artist.ArtistName != oldName)
            {
                if (artistService.GetByName(artist.ArtistName) != null)
                {
                    SetTempData(false, "Update Artist Failed!", "Artist Already Existed!");
                    return View("ArtistProfile", artist);
                }
            }

            if (genres.Count() < 1)
            {
                SetTempData(false, "Update Artist Failed!", "Choose Atleast 1 Genre");
                return View("ArtistProfile", artist);
            }

            if (artistPhoto != null && artistPhoto.Length > 0)
            {
                string fileExtension = Path.GetExtension(artistPhoto.FileName);
                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".ico")
                {
                    SetTempData(false, "Update Artist Failed!", "Please Choose Correct File Extension!");
                    return View("view", artist);
                }

                var oldFile = Path.Combine(env.WebRootPath, "/images/artist", artist.ArtistPhoto);
                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }

                artist.ArtistPhoto = Guid.NewGuid().ToString() + "_" + artistPhoto.FileName;
                var path = Path.Combine(env.WebRootPath, "/images/artist", artist.ArtistPhoto);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    artistPhoto.CopyTo(stream);
                }
            }

            var result = artistService.Update(artist);
            if (!result)
            {
                SetTempData(false, "Update Artist Failed!", "Cannot Update Now!");
                return View("ArtistProfile", artist);
            }

            var ag = artistGenreService.GetByArtistId(artist.ArtistId);
            foreach (var item in ag)
            {
                artistGenreService.Delete(item.Id);
            }

            int[] genreIds = genres.Select(int.Parse).ToArray();
            foreach (var genreId in genreIds)
            {
                var newAg = new ArtistsGenre
                {
                    ArtistId = artist.ArtistId,
                    GenreId = genreId,
                };
                artistGenreService.Create(newAg);
            }

            SetTempData(true, "Update Artist Success!", "Artist's Profile Updated!");
            return RedirectToAction("view");
        }

        [Route("deleteArtist")]
        public IActionResult DeleteArtist(int artistId)
        {
            var deleteArtist = artistService.GetById(artistId);

            if (artistService.Delete(artistId))
            {
                SetTempData(true, "Delete Artist Success!", $"BYE Artist {deleteArtist.ArtistName}!");
            }
            else
            {
                SetTempData(false, "Delete Artist Failed!", $"Cannot Delete Artist {deleteArtist.ArtistName}!");
            }

            return RedirectToAction("view");
        }
    }
}

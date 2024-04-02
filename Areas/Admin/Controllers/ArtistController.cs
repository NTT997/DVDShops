using DVDShops.Models;
using DVDShops.Services.Artists;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/artist")]
    public class ArtistController : Controller
    {
        private IArtistService artistService;
        public ArtistController(IArtistService artistService)
        {
            this.artistService = artistService;
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
        public IActionResult CreateArtist(Artist artist, List<string> genres)
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

            int[] genreIds = genres.Select(int.Parse).ToArray();
            foreach (var genreId in genreIds)
            {
                var newArtist = new Artist
                {
                    ArtistName = artist.ArtistName,
                    Bio = artist.Bio,
                    GenreId = genreId,
                };

                artistService.Create(newArtist);
            }

            SetTempData(true, "Create Artist Success!", "New Artist Has Been Added!");

            return RedirectToAction("view");
        }

        [Route("FindArtistByName")]
        [HttpGet]
        public IActionResult FindArtistByName(string name)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var artists = artistService.GetArtistsByName(name);

            return new JsonResult(artists, options);
        }

        [Route("editArtist")]
        [HttpPost]
        public IActionResult EditArtist(Artist artist, List<string> editGenres, string hiddenName)
        {
            var delArtList = artistService.GetArtistsByName(hiddenName);

            if (string.IsNullOrWhiteSpace(artist.ArtistName))
            {
                SetTempData(false, "Update Artist Failed!", "Artist's Name Is Must Have!");
                return RedirectToAction("view", artist);
            }

            if (hiddenName != artist.ArtistName.Trim())
            {
                if (artistService.GetByName(artist.ArtistName) != null)
                {
                    SetTempData(false, "Update Artist Failed!", "Artist Already Existed!");
                    return RedirectToAction("view", artist);
                }
            }

            if (editGenres.Count() < 1)
            {
                SetTempData(false, "Update Artist Failed!", "Choose Atleast 1 Genre");
                return RedirectToAction("view", artist);
            }

            foreach (var item in delArtList)
            {
                var res = artistService.Delete(item);
            }

            int[] genreIds = editGenres.Select(int.Parse).ToArray();
            foreach (var genreId in genreIds)
            {
                var newArtist = new Artist
                {
                    ArtistName = artist.ArtistName,
                    Bio = artist.Bio,
                    GenreId = genreId,
                };

                artistService.Create(newArtist);
            }

            SetTempData(true, "Update Artist Success!", "Artist's Profile Updated!");

            return RedirectToAction("view");
        }

        [Route("deleteArtist")]
        public IActionResult DeleteArtist(string name)
        {
            var deleteStatus = false;
            var deleteArtistName = artistService.GetArtistsByName(name);

            foreach (var item in deleteArtistName)
            {
                deleteStatus = artistService.Delete(item);
                if (!deleteStatus)
                {
                    SetTempData(false, "Delete Artist Failed!", "Something Wrong!");
                    break;
                }
            }

            if (deleteStatus)
            {
                SetTempData(deleteStatus, "Delete Artist Success!", $"BYE Artist {name}!");
            }

            return RedirectToAction("view");
        }
    }
}

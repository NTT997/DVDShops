using DVDShops.Models;
using DVDShops.Services.Albums;
using DVDShops.Services.Artists;
using DVDShops.Services.Genres;
using DVDShops.Services.Songs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/music")]
    public class MusicController : Controller
    {
        private IArtistService artistService;
        private ISongService songService;
        private IAlbumService albumService;
        private IGenreService genreService;
        public MusicController(IArtistService artistService, ISongService songService,
                               IAlbumService albumService, IGenreService genreService)
        {
            this.artistService = artistService;
            this.songService = songService;
            this.albumService = albumService;
            this.genreService = genreService;
        }

        [Route("view")]
        [Route("")]
        public IActionResult MusicDashboard()
        {
            return View("MusicView");
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(Artist artist, List<string> genres)
        {
            if (artistService.GetByName(artist.ArtistName) != null)
            {
                //popup confirm artist already existed => yes => update
            }

            if (genres.Count() < 1)
            {
                TempData["message"] = "At least 1 Genre must be chosen";
                return View("MusicView", artist);
            }

            int[] genreIds = genres.Select(int.Parse).ToArray();
            foreach (var genreId in genreIds)
            {
                artist.GenreId = genreId;

                Debug.WriteLine("================");
                Debug.WriteLine($"id : {artist.ArtistId}");
                Debug.WriteLine($"name : {artist.ArtistName}");
                Debug.WriteLine($"bio : {artist.Bio}");
                Debug.WriteLine($"genre : {artist.GenreId}");
                Debug.WriteLine("================");
                //artistService.Create(artist);
            }
            return RedirectToAction("view", "music", new { area = "admin" });
        }

        [Route("edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {

            return View("EditArtist");
        }

        [Route("edit/{id}")]
        [HttpPost]
        public IActionResult Edit(Artist artist)
        {

            return RedirectToAction("view", "artist", new { area = "admin" });
        }

        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {

            return RedirectToAction("view", "artist", new { area = "admin" });
        }
    }
}

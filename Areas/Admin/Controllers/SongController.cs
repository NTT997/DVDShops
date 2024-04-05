using DVDShops.Models;
using DVDShops.Services.Albums;
using DVDShops.Services.AlbumsGenres;
using DVDShops.Services.Artists;
using DVDShops.Services.ArtistsGenres;
using DVDShops.Services.Producers;
using DVDShops.Services.Songs;
using DVDShops.Services.SongsGenres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/song")]
    public class SongController : Controller
    {
        private ISongService songService;
        private ISongGenreService songGenreService;
        private IWebHostEnvironment env;
        public SongController(ISongService songService, ISongGenreService songGenreService, IWebHostEnvironment env)
        {
            this.songService = songService;
            this.songGenreService = songGenreService;
            this.env = env;
        }
        private void SetTempData(bool status, string title, string array)
        {
            TempData["status"] = status.ToString();
            TempData["title"] = title;
            TempData["message"] = array;
        }

        [Route("view")]
        [Route("")]
        public IActionResult SongView()
        {
            return View("SongView");
        }

        [Route("addSong")]
        [HttpGet]
        public IActionResult AddSong()
        {
            ViewBag.ArtistId = 0;
            if (HttpContext.Request.Query.ContainsKey("artistId"))
            {
                var artistId = HttpContext.Request.Query["artistId"];
                ViewBag.ArtistId = int.Parse(artistId);
            }

            return View("SongAdd");
        }

        [Route("addSong")]
        [HttpPost]
        public IActionResult AddSong(Song song, IFormFile songFile, List<string> genres)
        {            
            if (string.IsNullOrWhiteSpace(song.SongName) || string.IsNullOrWhiteSpace(song.SongIntroduction))
            {
                SetTempData(false, "Create Song Failed!", "Some Input Field Is White Space Only!");
                return View("SongAdd", song);
            }

            if (genres.Count() < 1)
            {
                SetTempData(false, "Create Song Failed!", "Choose Atleast 1 Genre");
                return View("SongAdd", song);
            }

            if (song.ProducerId < 0)
            {
                SetTempData(false, "Create Song Failed!", "Please Choose a Producer!");
                return View("SongAdd", song);
            }

            if (song.ArtistId < 0)
            {
                SetTempData(false, "Create Song Failed!", "Please Choose an Artist!");
                return View("SongAdd", song);
            }

            if (songFile != null && songFile.Length > 0)
            {
                string fileExtension = Path.GetExtension(songFile.FileName);
                if (fileExtension != ".mp3" && fileExtension != ".wav" && fileExtension != ".ogg")
                {
                    SetTempData(false, "Create Song Failed!", "Please Choose Correct File Extension!");
                    return View("SongAdd", song);
                }
                song.DownloadLink = Guid.NewGuid().ToString() + "_" + songFile.FileName;
                var path = Path.Combine(env.WebRootPath, "/download/song", song.DownloadLink);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    songFile.CopyTo(stream);
                }
            }
            else
            {
                SetTempData(false, "Create Song Failed!", "Please Upload The Song!");
                return View("SongAdd", song);
            }

            song.CategoryId = 3;
            var result = songService.Create(song);

            if (!result)
            {
                SetTempData(false, "Create Song Failed!", "Something Wrong, Could Not Create Song!");
                return View("SongAdd", song);
            }

            var newSong = songService.GetByName(song.SongName);
            int[] genreIds = genres.Select(int.Parse).ToArray();
            foreach (var genreId in genreIds)
            {
                var newSg = new SongsGenre
                {
                    SongId = newSong.SongId,
                    GenreId = genreId,
                };
                songGenreService.Create(newSg);
            }
            SetTempData(true, "Create Song Success!", $"{song.SongName} Created!");
            return RedirectToAction("view");
        }

        [Route("deleteSong")]
        [HttpGet]
        public IActionResult DeleteSong(int songId)
        {
            var song = songService.GetById(songId);

            if (songService.Delete(songId))
            {
                SetTempData(true, "Delete Song Success!", $"Bye {song.SongName}!");
            }
            else
            {
                SetTempData(false, "Delete Song Failed!", "This Song Cannot be Deleted This Way!");
            }
            return RedirectToAction("view");
        }

        [Route("editSong")]
        [HttpGet]
        public IActionResult EditSong(int songId)
        {
            var song = songService.GetById(songId);
            return View("SongEdit", song);
        }

        [Route("editSong")]
        [HttpPost]
        public IActionResult EditSong(Song song, IFormFile songFile, List<string> genres)
        {
            if (string.IsNullOrWhiteSpace(song.SongName) || string.IsNullOrWhiteSpace(song.SongIntroduction))
            {
                SetTempData(false, "Update Song Failed!", "Some Input Field Is White Space Only!");
                return View("SongEdit", song);
            }

            if (genres.Count() < 1)
            {
                SetTempData(false, "Update Song Failed!", "Choose Atleast 1 Genre");
                return View("SongEdit", song);
            }

            if (song.ProducerId < 0)
            {
                SetTempData(false, "Update Song Failed!", "Please Choose a Producer!");
                return View("SongEdit", song);
            }

            if (song.ArtistId < 0)
            {
                SetTempData(false, "Update Song Failed!", "Please Choose an Artist!");
                return View("SongEdit", song);
            }

            if (songFile != null && songFile.Length > 0)
            {
                string fileExtension = Path.GetExtension(songFile.FileName);
                if (fileExtension != ".mp3" && fileExtension != ".wav" && fileExtension != ".ogg")
                {
                    SetTempData(false, "Update Song Failed!", "Please Choose Correct File Extension!");
                    return View("SongEdit", song);
                }

                var oldFile = Path.Combine(env.WebRootPath, "/download/song", song.DownloadLink);
                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }

                song.DownloadLink = Guid.NewGuid().ToString() + "_" + songFile.FileName;
                var path = Path.Combine(env.WebRootPath, "/download/song", song.DownloadLink);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    songFile.CopyTo(stream);
                }
            }

            var result = songService.Update(song);
            if (!result)
            {
                SetTempData(false, "Update Song Failed!", "Something Wrong, Could Not Create Song!");
                return View("SongEdit", song);
            }

            var updateGenres = songGenreService.GetBySongId(song.SongId);
            foreach (var item in updateGenres)
            {
                songGenreService.Delete(item.Id);
            }

            int[] genreIds = genres.Select(int.Parse).ToArray();
            foreach (var genreId in genreIds)
            {
                var newSg = new SongsGenre
                {
                    SongId = song.SongId,
                    GenreId = genreId,
                };
                songGenreService.Create(newSg);
            }

            SetTempData(true, "Update Song Success!", $"{song.SongName} Updated!");
            return RedirectToAction("view");
        }
    }
}

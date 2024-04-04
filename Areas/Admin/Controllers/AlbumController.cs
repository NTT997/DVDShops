using DVDShops.Models;
using DVDShops.Services.Albums;
using DVDShops.Services.Artists;
using DVDShops.Services.Producers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/album")]
    public class AlbumController : Controller
    {
        private IAlbumService albumService;
        private IArtistService artistService;
        private IProducerService producerService;
        public AlbumController(IAlbumService albumService, IArtistService artistService, IProducerService producerService)
        {
            this.albumService = albumService;
            this.artistService = artistService;
            this.producerService = producerService;
        }
        private void SetTempData(bool status, string title, string array)
        {
            TempData["status"] = status.ToString();
            TempData["title"] = title;
            TempData["message"] = array;
        }

        [Route("view")]
        [Route("")]
        public IActionResult AlbumView()
        {
            return View("AlbumView");
        }

        [Route("deleteAlbum")]
        [HttpGet]
        public IActionResult DeleteAlbum(int albumId)
        {
            var album = albumService.GetById(albumId);

            if (albumService.Delete(albumId))
            {
                SetTempData(true, "Delete Album Success!", $"Bye {album.AlbumName}!");
            }
            else
            {
                SetTempData(false, "Delete Album Failed!", "This Album Cannot be Deleted This Way!");
            }
            return RedirectToAction("view");
        }

        [Route("addAlbum")]
        [HttpGet]
        public IActionResult AddAlbum()
        {
            return View("AlbumAdd");
        }

        [Route("addAlbum")]
        [HttpPost]
        public IActionResult AddAlbum(Album album, string issueDate)
        {
            if (string.IsNullOrEmpty(issueDate))
            {
                SetTempData(false, "Create Album Failed!", "Please Choose Issue Date!");
                TempData["issue"] = issueDate;
                return View("AlbumAdd", album);
            }
            album.IssueDate = DateOnly.Parse(issueDate);

            if (string.IsNullOrWhiteSpace(album.AlbumName) || string.IsNullOrWhiteSpace(album.AlbumIntroduction))
            {
                SetTempData(false, "Create Album Failed!", "Some Input Field Is White Space Only!");
                TempData["issue"] = issueDate;
                return View("AlbumAdd", album);
            }

            if (album.ProducerId < 0)
            {
                SetTempData(false, "Create Album Failed!", "Please Choose a Producer!");
                TempData["issue"] = issueDate;
                return View("AlbumAdd", album);
            }

            if (album.ArtistId < 0)
            {
                SetTempData(false, "Create Album Failed!", "Please Choose an Artist!");
                TempData["issue"] = issueDate;
                return View("AlbumAdd", album);
            }

            album.CategoryId = 2;
            if (albumService.Create(album))
            {
                SetTempData(true, "Create Album Success!", "New Album Just Added!");
            }
            else
            {
                SetTempData(false, "Create Album Failed!", "Something Wrong!");
            }
            return RedirectToAction("view");
        }

        [Route("editAlbum")]
        [HttpGet]
        public IActionResult EditAlbum(int albumId)
        {
            var album = albumService.GetById(albumId);
            TempData["issue"] = $"{album.IssueDate:yyyy-MM-dd}";
            return View("AlbumEdit", album);
        }

        [Route("editAlbum")]
        [HttpPost]
        public IActionResult EditAlbum(Album album, string issueDate)
        {
            if (string.IsNullOrEmpty(issueDate))
            {
                SetTempData(false, "Update Album Failed!", "Please Choose Issue Date!");
                TempData["issue"] = issueDate;
                return View("AlbumEdit", album.AlbumId);
            }
            album.IssueDate = DateOnly.Parse(issueDate);

            if (string.IsNullOrWhiteSpace(album.AlbumName) || string.IsNullOrWhiteSpace(album.AlbumIntroduction))
            {
                SetTempData(false, "Update Album Failed!", "Some Input Field Is White Space Only!");
                TempData["issue"] = issueDate;
                return View("AlbumEdit", album.AlbumId);
            }

            if (album.ProducerId < 0)
            {
                SetTempData(false, "Update Album Failed!", "Please Choose a Producer!");
                TempData["issue"] = issueDate;
                return View("AlbumEdit", album.AlbumId);
            }

            if (album.ArtistId < 0)
            {
                SetTempData(false, "Update Album Failed!", "Please Choose an Artist!");
                TempData["issue"] = issueDate;
                return View("AlbumEdit", album.AlbumId);
            }

            if (albumService.Update(album))
            {
                SetTempData(true, "Update Album Success!", $"Album {album.AlbumName} Just Updated!");
            }
            else
            {
                SetTempData(false, "Update Album Failed!", "Something Wrong!");
            }
            return RedirectToAction("view");
        }
    }
}

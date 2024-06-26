﻿using DVDShops.Models;
using DVDShops.Services.Albums;
using DVDShops.Services.AlbumsSongs;
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
        private IAlbumsSongsService albumsSongsService;
        private IWebHostEnvironment env;
        public AlbumController(IAlbumService albumService,
            IAlbumsSongsService albumsSongsService, IWebHostEnvironment env)
        {
            this.albumService = albumService;
            this.albumsSongsService = albumsSongsService;
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
                SetTempData(true, "Delete Album Success!", $"{album.AlbumName} Already Delete!");
            }
            else
            {
                SetTempData(false, "Delete Album Failed!", "Cannot Delete Albums Being Sold!");
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
        public IActionResult AddAlbum(Album album, string issueDate, List<string> songs, IFormFile coverImage)
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

            if (songs.Count() < 1)
            {
                SetTempData(false, "Create Album Failed!", "Choose Atleast 1 Song");
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

            if (coverImage != null && coverImage.Length > 0)
            {
                string fileExtension = Path.GetExtension(coverImage.FileName);
                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".ico")
                {
                    SetTempData(false, "Create Album Failed!", "Please Choose Correct File Extension!");
                    TempData["issue"] = issueDate;
                    return View("AlbumAdd", album);
                }

                album.AlbumCover = Guid.NewGuid().ToString().Replace("-", "") + "_" + coverImage.FileName;
                var path = Path.Combine(env.WebRootPath, "images/album", album.AlbumCover);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    coverImage.CopyTo(stream);
                }
            }
            else
            {
                album.AlbumCover = "no_image.jpg";
            }

            album.CategoryId = 2;
            var result = albumService.Create(album);

            if (!result)
            {
                SetTempData(false, "Create Album Failed!", "Something Wrong!");
                TempData["issue"] = issueDate;
                return View("AlbumAdd", album);
            }

            var newAlbum = albumService.GetByName(album.AlbumName);
            int[] songIds = songs.Select(int.Parse).ToArray();
            foreach (var songId in songIds)
            {
                var newAs = new AlbumsSong
                {
                    SongId = songId,
                    AlbumId = newAlbum.AlbumId,
                };
                albumsSongsService.Create(newAs);
            }

            SetTempData(true, "Create Album Success!", $"Album {album.AlbumName} Just Added!");
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
        public IActionResult EditAlbum(Album album, string issueDate, List<string> songs, IFormFile coverImage)
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

            if (songs.Count() < 1)
            {
                SetTempData(false, "Create Album Failed!", "Choose Atleast 1 Song");
                return View("AlbumAdd", album.AlbumId);
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

            if (coverImage != null && coverImage.Length > 0)
            {
                string fileExtension = Path.GetExtension(coverImage.FileName);
                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".ico")
                {
                    SetTempData(false, "Update Album Failed!", "Please Choose Correct File Extension!");
                    TempData["issue"] = issueDate;
                    return View("AlbumEdit", album.AlbumId);
                }

                if (album.AlbumCover != "no_image.jpg")
                {
                    var oldFile = Path.Combine(env.WebRootPath, "images/album", album.AlbumCover);
                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                }

                album.AlbumCover = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16) + "_" + coverImage.FileName;
                var path = Path.Combine(env.WebRootPath, "images/album", album.AlbumCover);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    coverImage.CopyTo(stream);
                }
            }

            var result = albumService.Update(album);
            if (!result)
            {
                SetTempData(false, "Update Album Failed!", "Cannot Update Now!");
                TempData["issue"] = issueDate;
                return View("AlbumEdit", album.AlbumId);
            }

            var updateSong = albumsSongsService.GetByAlbumId(album.AlbumId);
            foreach (var song in updateSong)
            {
                albumsSongsService.Delete(song.Id);
            }

            int[] songIds = songs.Select(int.Parse).ToArray();
            foreach (var songId in songIds)
            {
                var newAs = new AlbumsSong
                {
                    SongId = songId,
                    AlbumId = album.AlbumId,
                };
                albumsSongsService.Create(newAs);
            }
            SetTempData(true, "Update Album Success!", $"Album {album.AlbumName} Just Updated!");
            return RedirectToAction("view");
        }
    }
}

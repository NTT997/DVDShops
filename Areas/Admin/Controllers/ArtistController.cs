using DVDShops.Models;
using DVDShops.Services.Artists;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ArtistView()
        {
            ViewBag.artistsList = artistService.GetAll();
            return View("ViewArtist");
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View("CreateArtist");
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(Artist artist)
        {
            return RedirectToAction("view", "artist", new { area = "admin" });
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

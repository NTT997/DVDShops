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
    }
}

using DVDShops.Services.Albums;
using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/album")]
    public class AlbumController : Controller
    {
        private IAlbumService albumService;
        public AlbumController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        [Route("view")]
        [Route("")]
        public IActionResult AlbumView()
        {
            return View("AlbumView");
        }
    }
}

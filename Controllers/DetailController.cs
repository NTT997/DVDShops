using DVDShops.Services.Advertises;
using DVDShops.Services.Albums;
using DVDShops.Services.AlbumsSongs;
using DVDShops.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DVDShops.Controllers
{
    [Route("detail")]
    public class DetailController : Controller
    {
        private IProductService productService;
        private IAlbumService albumService;
        private IAlbumsSongsService alsService;
        public DetailController(IProductService productService, IAlbumService albumService, IAlbumsSongsService alsService)
        {
            this.productService = productService;
            this.albumService = albumService;
            this.alsService = alsService;
        }

        [Route("detail")]
        [Route("")]
        public IActionResult Detail(int productId)
        {
            var detail = productService.GetById(productId);
            return View("detail", detail);
        }
    }
}

using DVDShops.Models;
using DVDShops.Services.ArtistsGenres;
using DVDShops.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DVDShops.Controllers
{
    [Route("shop")]
    public class ShopController : Controller
    {
        private IProductService productService;
        public ShopController(IProductService productService)
        {
            this.productService = productService;
        }

        [Route("shop")]
        [Route("")]
        public IActionResult Shop()
        {
            ViewBag.Products = productService.GetAll();
            return View();
        }

        [Route("sort")]
        public IActionResult Sort(string type)
        {
            var list = new List<Product>();
            if (type == "latest")
            {
                list = productService.GetAll().OrderByDescending(list => list.CreatedAt).ToList();
            }
            if (type == "popularity")
            {
                list = productService.GetAll().OrderByDescending(list => list.SoldUnit).ToList();
            }
            if (type == "rating")
            {
                list = productService.GetAll().OrderByDescending(list => list.ProductRate).ToList();
            }
            ViewBag.Products = list;

            return View("shop");
        }

        [Route("search")]
        public IActionResult Search(string keyword)
        {
            var list = productService.GetAll();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                list = productService.GetAll().Where(p => p.ProductTitle.ToLower().Contains(keyword)).ToList();
            }

            ViewBag.Products = list;
            return View("shop");
        }

        [Route("searchByPrice")]
        public IActionResult SearchByPrice(List<string> prices)
        {
            int[] priceTag = prices.Select(int.Parse).ToArray();
            var list = productService.GetAll();



            ViewBag.Products = list;
            return View("shop");
        }
    }
}

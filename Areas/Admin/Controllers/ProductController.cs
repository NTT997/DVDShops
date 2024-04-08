using DVDShops.Models;
using DVDShops.Services.Games;
using DVDShops.Services.Producers;
using DVDShops.Services.Products;
using DVDShops.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        private void SetTempData(bool status, string title, string array)
        {
            TempData["status"] = status.ToString();
            TempData["title"] = title;
            TempData["message"] = array;
        }

        [Route("")]
        [Route("view")]
        public IActionResult ProductView()
        {
            return View("ProductView");
        }

        [Route("addProduct")]
        [HttpGet]
        public IActionResult ProductAdd()
        {
            return View("ProductAdd");
        }

        [Route("addProduct")]
        [HttpPost]
        public IActionResult ProductAdd(Product product)
        {
            if (product.AlbumId < 0)
            {
                SetTempData(false, "Create Product Failed!", "Must Choose an Album!");
                return View("ProductAdd", product);
            }

            if (string.IsNullOrWhiteSpace(product.ProductTitle))
            {
                SetTempData(false, "Create Product Failed!", "Product Need a Title!");
                return View("ProductAdd", product);
            }

            if (product.ProductQuantity <= 0 || product.ProductQuantity <= 0)
            {
                SetTempData(false, "Create Product Failed!", "Quantity & Price Must > 0!");
                return View("ProductAdd", product);
            }

            if (product.SupplierId < 0 || product.PromotionId < 0)
            {
                SetTempData(false, "Create Product Failed!", "Must Choose Both Supplier & Promotion!");
                return View("ProductAdd", product);
            }

            var newProd = new Product
            {
                ProductTitle = product.ProductTitle,
                ProductPrice = product.ProductPrice,
                ProductQuantity = product.ProductQuantity,
                SoldUnit = 0,
                ProductRate = 0,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                SupplierId = product.SupplierId,
                AlbumId = product.AlbumId,
                PromotionId = product.PromotionId,
            };

            var result = productService.Create(newProd);
            if (!result)
            {
                SetTempData(false, "Create Product Failed!", "Something Wrong!");
            }

            SetTempData(true, "Create Product Success!", $"{product.ProductTitle} Just Added!");
            return RedirectToAction("view");
        }

        [Route("editProduct")]
        [HttpGet]
        public IActionResult ProductEdit(int productId)
        {
            var product = productService.GetById(productId);
            return View("ProductEdit", product);
        }

        [Route("editProduct")]
        [HttpPost]
        public IActionResult ProductEdit(Product product)
        {
            if (product.AlbumId < 0)
            {
                SetTempData(false, "Update Product Failed!", "Must Choose an Album!");
                return View("ProductAdd", product);
            }

            if (string.IsNullOrWhiteSpace(product.ProductTitle))
            {
                SetTempData(false, "Update Product Failed!", "Product Need a Title!");
                return View("ProductAdd", product);
            }

            if (product.ProductQuantity <= 0 || product.ProductQuantity <= 0)
            {
                SetTempData(false, "Update Product Failed!", "Quantity & Price Must > 0!");
                return View("ProductAdd", product);
            }

            if (product.SupplierId < 0 || product.PromotionId < 0)
            {
                SetTempData(false, "Update Product Failed!", "Must Choose Both Supplier & Promotion!");
                return View("ProductAdd", product);
            }

            var result = productService.Update(product);
            if (!result)
            {
                SetTempData(false, "Update Product Failed!", "Something Wrong!");
            }

            SetTempData(true, "Update Product Success!", $"{product.ProductTitle} Updated!");
            return RedirectToAction("view");
        }

        [Route("deleteProduct")]
        [HttpGet]
        public IActionResult DeleteProduct(int productId)
        {
            var product = productService.GetById(productId);
            product.IsDelete = !product.IsDelete;

            if (productService.Update(product))
            {
                if (product.IsDelete)
                {
                    SetTempData(true, "Delete Product Success!", $"{product.ProductTitle} Deleted!");
                }
                else
                {
                    SetTempData(true, "Recover Product Success!", $"{product.ProductTitle} Recoverd!");
                }
            }
            else
            {
                SetTempData(false, "Delete Product Failed!", "Something Wrong!");
            }

            return RedirectToAction("view");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using DVDShops.Models;
using System.Linq;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/news")]
    public class NewsController : Controller
    {
        private readonly DvdshopContext _context;

        public NewsController(DvdshopContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            var newsList = _context.News.ToList();
            return View(newsList);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                _context.News.Add(news);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var news = _context.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, News news)
        {
            if (ModelState.IsValid)
            {
                _context.News.Update(news);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var news = _context.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var news = _context.News.Find(id);
            if (news != null)
            {
                _context.News.Remove(news);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

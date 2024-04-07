﻿using Microsoft.AspNetCore.Mvc;
using DVDShops.Models;
using System.Linq;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/feedback")]
    public class FeedbackController : Controller
    {
        private readonly DvdshopContext _context;

        public FeedbackController(DvdshopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var feedbackList = _context.Feedback.ToList();
            return View(feedbackList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Feedback.Add(feedback);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var feedback = _context.Feedback.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        [HttpPost]
        public IActionResult Edit(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Feedback.Update(feedback);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var feedback = _context.Feedback.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var feedback = _context.Feedback.Find(id);
            if (feedback != null)
            {
                _context.Feedback.Remove(feedback);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

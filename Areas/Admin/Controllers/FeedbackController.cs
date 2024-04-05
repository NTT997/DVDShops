using Microsoft.AspNetCore.Mvc;
using DVDShops.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/feedback")]
    public class FeedbackController : Controller
    {
        private readonly DvdshopContext _dvdshopContext;

        public FeedbackController(DvdshopContext dvdshopContext)
        {
            _dvdshopContext = dvdshopContext;
        }

        [Route("view")]
        [Route("")]
        public IActionResult FeedbackView()
        {
            var feedbacks = _dvdshopContext.Feedbacks.ToList();
            return View("FeedbackView");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var feedback = _dvdshopContext.Feedbacks.Find(id);
            if (feedback != null)
            {
                _dvdshopContext.Feedbacks.Remove(feedback);
                _dvdshopContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reply(int id, string reply)
        {
            var feedback = _dvdshopContext.Feedbacks.Find(id);
            if (feedback != null)
            {
                feedback.FeedbackReply = reply;
                _dvdshopContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("search")]
        public IActionResult Search(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index");
            }

            var feedbacks = _dvdshopContext.Feedbacks.Where(f => f.Users.UserName.Contains(user)).ToList();
            return View("Index", feedbacks);
        }

    }
}

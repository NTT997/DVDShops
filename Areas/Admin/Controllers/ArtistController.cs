using DVDShops.Models;
using DVDShops.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            var feedbacks = _feedbackService.GetAllFeedbacks();
            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public ActionResult<Feedback> GetFeedbackById(int id)
        {
            var feedback = _feedbackService.GetFeedbackById(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return Ok(feedback);
        }

        [HttpPost]
        public IActionResult AddFeedback(Feedback feedback)
        {
            _feedbackService.AddFeedback(feedback);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = feedback.FeedbackId }, feedback);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFeedback(int id, Feedback feedback)
        {
            if (id != feedback.FeedbackId)
            {
                return BadRequest();
            }

            _feedbackService.UpdateFeedback(feedback);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeedback(int id)
        {
            _feedbackService.DeleteFeedback(id);
            return NoContent();
        }
    }
}

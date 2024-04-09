using DVDShops.Models;
using DVDShops.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DVDShops.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly DvdshopContext _dbContext;

        public FeedbackService(DvdshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Feedback> GetAllFeedbacks()
        {
            return _dbContext.Feedbacks.ToList();
        }

        public Feedback GetFeedbackById(int feedbackId)
        {
            return _dbContext.Feedbacks.Find(feedbackId);
        }

        public void AddFeedback(Feedback feedback)
        {
            _dbContext.Feedbacks.Add(feedback);
            _dbContext.SaveChanges();
        }

        public void UpdateFeedback(Feedback feedback)
        {
            _dbContext.Feedbacks.Update(feedback);
            _dbContext.SaveChanges();
        }

        public void DeleteFeedback(int feedbackId)
        {
            var feedback = _dbContext.Feedbacks.Find(feedbackId);
            if (feedback != null)
            {
                _dbContext.Feedbacks.Remove(feedback);
                _dbContext.SaveChanges();
            }
        }
    }
}

using DVDShops.Models;
using System.Collections.Generic;

namespace DVDShops.Services
{
    public interface IFeedbackService
    {
        IEnumerable<Feedback> GetAllFeedbacks();
        Feedback GetFeedbackById(int feedbackId);
        void AddFeedback(Feedback feedback);
        void UpdateFeedback(Feedback feedback);
        void DeleteFeedback(int feedbackId);
    }
}

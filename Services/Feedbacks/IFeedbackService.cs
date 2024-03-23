using DVDShops.Models;

namespace DVDShops.Services.Feedbacks
{
    public interface IFeedbackService
    {
        bool Delete(int feedbackId);
        bool Create(Feedback feedback);
        Feedback GetById(int id);
        List<Feedback> GetAll();
        List<Feedback> GetByMemberId(int memberId);
        List<Feedback> GetByProductId(int productId);
    }
}

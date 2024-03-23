using DVDShops.Models;

namespace DVDShops.Services.Feedbacks
{
    public class FeedbackService : IFeedbackService
    {

        private DvdshopContext dbContext;
        public FeedbackService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(Feedback feedback)
        {
            try
            {
                dbContext.Feedbacks.Add(feedback);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int feedbackId)
        {
            var feedback = GetById(feedbackId);
            try
            {
                dbContext.Feedbacks.Remove(feedback);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Feedback GetById(int feedbackId)
        {
            return dbContext.Feedbacks.Find(feedbackId);
        }

        public List<Feedback> GetAll()
        {
            return dbContext.Feedbacks.ToList();
        }

        public List<Feedback> GetByMemberId(int memberId)
        {
            return dbContext.Feedbacks.Where(f => f.UsersId == memberId).ToList();
        }

        public List<Feedback> GetByProductId(int productId)
        {
            return dbContext.Feedbacks.Where(f => f.PId == productId).ToList();
        }

        Feedback IFeedbackService.GetById(int id)
        {
            return dbContext.Feedbacks.Find(id);
        }
    }
}

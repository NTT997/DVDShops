using DVDShops.Models;

namespace DVDShops.Services.Promotions
{
    public class PromotionService : IPromotionService
    {
        private DvdshopContext dbContext;
        public PromotionService(DvdshopContext dbContext) { this.dbContext = dbContext; }
        public bool Create(Promotion promotion)
        {
            try
            {
                dbContext.Promotions.Add(promotion);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int promoId)
        {
            try
            {
                dbContext.Promotions.Remove(GetById(promoId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Promotion> GetAll()
        {
            return dbContext.Promotions.ToList();
        }

        public Promotion GetById(int promoId)
        {
            return dbContext.Promotions.Find(promoId);
        }

        public bool Update(Promotion promotion)
        {
            try
            {
                dbContext.Entry(promotion).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

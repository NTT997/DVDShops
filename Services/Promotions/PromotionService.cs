using System;
using System.Collections.Generic;
using System.Linq;
using DVDShops.Models;

namespace DVDShops.Services.Promotions
{
    public class PromotionService : IPromotionService
    {
        private readonly DvdshopContext _dbContext;

        public PromotionService(DvdshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(Promotion promotion)
        {
            try
            {
                _dbContext.Promotions.Add(promotion);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Promotion promotion)
        {
            try
            {
                _dbContext.Promotions.Update(promotion);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int promotionId)
        {
            var promotion = GetById(promotionId);
            if (promotion != null)
            {
                try
                {
                    _dbContext.Promotions.Remove(promotion);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public Promotion GetById(int promotionId)
        {
            return _dbContext.Promotions.Find(promotionId);
        }

        public List<Promotion> GetAll()
        {
            return _dbContext.Promotions.ToList();
        }
    }
}

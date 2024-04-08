using DVDShops.Models;
using System.Collections.Generic;

namespace DVDShops.Services.Promotions
{
    public interface IPromotionService
    {
        bool Create(Promotion promotion);
        bool Update(Promotion promotion);
        bool Delete(int promotionId);
        Promotion GetById(int promotionId);
        List<Promotion> GetAll();
    }
}

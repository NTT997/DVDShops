using DVDShops.Models;

namespace DVDShops.Services.Promotions
{
    public interface IPromotionService
    {
        List<Promotion> GetAll();
        Promotion GetById(int promoId);
        bool Create(Promotion promotion);
        bool Update(Promotion promotion);
        bool Delete(int promoId);
    }
}

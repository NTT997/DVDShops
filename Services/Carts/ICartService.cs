using DVDShops.Models;

namespace DVDShops.Services.Carts
{
    public interface ICartService
    {
        List<Cart> GetByUserId(int userId);
        Cart GetById(int cartId);
        bool Create(Cart cart);
        bool Update(Cart cart);
        bool Delete(int cartId);
    }
}

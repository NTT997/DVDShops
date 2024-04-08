using DVDShops.Models;

namespace DVDShops.Services.Products
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> SearchByPTitle(string pTitle);
        Product GetById(int pId);
        Product GetByPTitle(string pTitle);
        bool Create(Product product);
        bool Update(Product product);
        bool Delete(int productId);
    }
}

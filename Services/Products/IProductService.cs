using DVDShops.Models;

namespace DVDShops.Services.Products
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        bool Create(Product product);
        bool Update(Product product);
        bool Delete(int productId);
    }
}

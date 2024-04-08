using DVDShops.Models;
using System.Security.Cryptography;

namespace DVDShops.Services.Products
{
    public class Productservice : IProductService
    {
        private DvdshopContext dbContext;
        public Productservice(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(Product product)
        {
            try
            {
                dbContext.Products.Add(product);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int productId)
        {
            try
            {
                dbContext.Products.Remove(GetById(productId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetAll()
        {
            return dbContext.Products.ToList();
        }

        public Product GetById(int pId)
        {
            return dbContext.Products.Find(pId);
        }

        public Product GetByPTitle(string pTitle)
        {
            pTitle = pTitle.Trim();
            return dbContext.Products.Where(p => p.ProductTitle.ToLower() == pTitle.ToLower()).FirstOrDefault();
        }

        public List<Product> SearchByPTitle(string pTitle)
        {
            pTitle = pTitle.Trim();
            return dbContext.Products.Where(p => p.ProductTitle.ToLower().Contains(pTitle.ToLower())).ToList();
        }

        public bool Update(Product product)
        {
            try
            {
                dbContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

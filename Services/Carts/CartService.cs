﻿using DVDShops.Models;

namespace DVDShops.Services.Carts
{
    public class CartService : ICartService
    {
        private DvdshopContext dbContext;
        public CartService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(Cart cart)
        {
            try
            {
                dbContext.Carts.Add(cart);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int cartId)
        {
            try
            {
                dbContext.Carts.Remove(GetById(cartId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EmptyUserCart(int userId)
        {
            var deleteList = GetByUserId(userId);
            try
            {
                foreach (var item in deleteList)
                {
                    dbContext.Carts.Remove(item);
                }
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Cart GetById(int cartId)
        {
            return dbContext.Carts.Find(cartId);
        }

        public Cart GetByProductId(int productId)
        {
            return dbContext.Carts.SingleOrDefault(c => c.ProductId == productId);
        }

        public List<Cart> GetByUserId(int userId)
        {
            return dbContext.Carts.Where(cart => cart.UsersId == userId).ToList();
        }

        public bool Update(Cart cart)
        {
            try
            {
                dbContext.Entry(cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

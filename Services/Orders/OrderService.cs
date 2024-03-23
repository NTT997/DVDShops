using DVDShops.Models;

namespace DVDShops.Services.Orders
{
    public class OrderService : IOrderService
    {

        private DvdshopContext dbContext;
        public OrderService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(Order order)
        {
            try
            {
                dbContext.Orders.Add(order);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Order> GetAllOrders()
        {
            return dbContext.Orders.ToList();
        }

        public Order GetById(int orderId)
        {
            return dbContext.Orders.Find(orderId);
        }
        public bool Delete(int orderId)
        {
            try
            {
                dbContext.Orders.Remove(GetById(orderId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Order> GetOrdersByUserId(int userId)
        {
            return dbContext.Orders.Where(o => o.UsersId == userId).ToList();
        }

        public bool Update(Order order)
        {
            try
            {
                dbContext.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

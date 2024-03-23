using DVDShops.Models;

namespace DVDShops.Services.Orders
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetById(int orderId);
        List<Order> GetOrdersByUserId(int userId);
        bool Create(Order order);
        bool Update(Order order);
        bool Delete(int orderId);
    }
}

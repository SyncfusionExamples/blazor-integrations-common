using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(Order order);
        Task<List<Order>> GetOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int orderId);
    }
}
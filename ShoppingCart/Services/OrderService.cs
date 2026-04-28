using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new();
        
        public Task<Order> PlaceOrderAsync(Order order)
        {
            order.OrderId = _orders.Count + 1;
            order.Status = "Confirmed";
            _orders.Add(order);
            return Task.FromResult(order);
        }

        public Task<List<Order>> GetOrdersAsync() => Task.FromResult(_orders);

        public Task<Order?> GetOrderByIdAsync(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            return Task.FromResult(order);
        }
    }
}
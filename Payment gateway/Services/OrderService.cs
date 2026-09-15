using System.Collections.Concurrent;
using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class OrderService : IOrderService
    {
        private readonly ConcurrentDictionary<int, Order> _orders = new();
        private int _nextId = 1;

        public Task<Order> PlaceOrderAsync(Order order)
        {
            order.OrderId = Interlocked.Increment(ref _nextId) - 1;
            // Status will move to "Paid" once the PaymentIntent is confirmed by Stripe
            order.Status = "Awaiting Payment";
            order.PaymentStatus = "requires_payment_method";
            _orders[order.OrderId] = order;
            return Task.FromResult(order);
        }

        public Task<Order?> UpdateAsync(Order order)
        {
            if (order == null || order.OrderId == 0) return Task.FromResult<Order?>(null);
            _orders[order.OrderId] = order;
            return Task.FromResult<Order?>(order);
        }

        public Task<List<Order>> GetOrdersAsync()
        {
            var list = _orders.Values.OrderByDescending(o => o.OrderDate).ToList();
            return Task.FromResult(list);
        }

        public Task<Order?> GetOrderByIdAsync(int orderId)
        {
            _orders.TryGetValue(orderId, out var order);
            return Task.FromResult(order);
        }
    }
}

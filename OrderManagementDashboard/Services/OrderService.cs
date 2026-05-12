using OrderManagementDashboard.Models;

namespace OrderManagementDashboard.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders;

        public OrderService()
        {
            _orders = GenerateSampleOrders();
        }

        public Task<List<Order>> GetOrdersAsync(int pageNumber, int pageSize, string searchTerm = "")
        {
            var query = _orders.AsEnumerable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(o => o.OrderNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || 
                                        o.CustomerName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                        o.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            var result = query
                .OrderByDescending(o => o.OrderDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<int> GetTotalOrderCountAsync() => Task.FromResult(_orders.Count);

        public Task<DashboardKpi> GetKpiDataAsync()
        {
            return Task.FromResult(new DashboardKpi
            {
                TotalOrders = 3823,
                PendingPayment = 934,
                Processing = 993,
                Shipped = 536,
                Delivered = 24392,
                Cancelled = 9372,
                Returned = 434,
                Failed = 938
            });
        }

        private List<Order> GenerateSampleOrders()
        {
            var orders = new List<Order>();
            var random = new Random(42);
            var customers = new[] { "Alexa Smith", "John Doe", "Sarah Johnson", "Michael Brown", "Emma Wilson", "James Davis", "Olivia Martinez", "William Garcia", "Sophia Anderson", "Robert Taylor" };
            var reasons = new[] { "Product does not match the description", "Damaged product", "Wrong item shipped", "Quality issues", "Changed mind" };
            
            var paymentStatuses = new[] { PaymentStatus.Paid, PaymentStatus.Pending, PaymentStatus.Unpaid };
            var receivedStatuses = new[] { ReceivedStatus.Delivered, ReceivedStatus.Processing, ReceivedStatus.Shipped };
            
            for (int i = 1; i <= 200; i++)
            {
                var daysAgo = random.Next(0, 90);
                var orderDate = new DateTime(2027, 9, 12).AddDays(-daysAgo);
                var customerName = customers[random.Next(customers.Length)];
                
                orders.Add(new Order
                {
                    Id = $"#{73423 + i}",
                    OrderNumber = $"#{803 + i}",
                    Amount = random.Next(100, 1000),
                    OrderDate = orderDate,
                    Email = $"{customerName.ToLower().Replace(" ", ".")}@gmail.com",
                    Reason = reasons[random.Next(reasons.Length)],
                    PaymentStatus = paymentStatuses[random.Next(paymentStatuses.Length)],
                    ReceivedStatus = receivedStatuses[random.Next(receivedStatuses.Length)],
                    Date = orderDate,
                    CustomerId = $"CUST{i:000}",
                    CustomerName = customerName,
                    Items = random.Next(1, 20)
                });
            }
            return orders.OrderByDescending(o => o.OrderDate).ToList();
        }
    }
}
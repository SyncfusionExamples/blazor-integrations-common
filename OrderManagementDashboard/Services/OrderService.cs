using OrderManagementDashboard.Models;

namespace OrderManagementDashboard.Services
{
    public class OrderService : IOrderService
    {
        private static List<Order> _orders = new List<Order>();
        private static readonly Random _random = new Random(42);
        private static bool _isInitialized = false;
        private static readonly object _lock = new object();

        public OrderService()
        {
            lock (_lock)
            {
                if (!_isInitialized)
                {
                    GenerateRealisticOrders();
                    _isInitialized = true;
                }
            }
        }

        private void GenerateRealisticOrders()
        {
            var products = new[] { "Laptop", "Smartphone", "Tablet", "Headphones", "Smartwatch", 
                "Camera", "Speaker", "Monitor", "Keyboard", "Mouse", "Printer", "Router", 
                "Hard Drive", "USB Cable", "Power Bank", "Webcam", "Microphone", "Desk Lamp", 
                "Office Chair", "Standing Desk" };

            var reasons = new[] { "Standard purchase", "Bulk order", "Corporate purchase", 
                "Promotional offer", "Repeat customer", "New customer", "Seasonal sale", 
                "Holiday special", "Clearance item", "Premium product", "Gift purchase", 
                "Business requirement" };

            var statusDistribution = new Dictionary<string, int>
            {
                { "Paid_Delivered", 85 },
                { "Paid_Shipped", 42 },
                { "Paid_Processing", 28 },
                { "Pending_Processing", 18 },
                { "Unpaid_Processing", 12 },
                { "Paid_Cancelled", 8 },
                { "Paid_Returned", 7 }
            };

            int orderId = 1;
            var startDate = DateTime.Now.AddMonths(-12);

            foreach (var status in statusDistribution)
            {
                for (int i = 0; i < status.Value; i++)
                {
                    var parts = status.Key.Split('_');
                    var paymentStatus = Enum.Parse<PaymentStatus>(parts[0]);
                    var receivedStatus = Enum.Parse<ReceivedStatus>(parts[1]);

                    var firstName = SharedDataConstants.FirstNames[_random.Next(SharedDataConstants.FirstNames.Length)];
                    var lastName = SharedDataConstants.LastNames[_random.Next(SharedDataConstants.LastNames.Length)];
                    var customerName = $"{firstName} {lastName}";
                    var email = SharedDataConstants.GenerateEmail(firstName, lastName, orderId);

                    var daysAgo = _random.Next(0, 365);
                    var orderDate = startDate.AddDays(daysAgo);

                    var itemCount = _random.Next(1, 4);
                    var itemsList = new List<string>();
                    var totalAmount = 0m;

                    while (itemsList.Count < itemCount)
                    {
                        var product = products[_random.Next(products.Length)];
                        if (!itemsList.Contains(product))
                        {
                            itemsList.Add(product);
                            var price = _random.Next(20, 800) + (decimal)_random.NextDouble();
                            totalAmount += Math.Round(price, 2);
                        }
                    }

                    var order = new Order
                    {
                        Id = $"ORD{orderId:D6}",
                        OrderNumber = $"#{10000 + orderId}",
                        OrderDate = orderDate,
                        CustomerName = customerName,
                        Email = email,
                        Amount = Math.Round(totalAmount, 2),
                        PaymentStatus = paymentStatus,
                        ReceivedStatus = receivedStatus,
                        Items = string.Join(", ", itemsList),
                        Reason = reasons[_random.Next(reasons.Length)],
                        ShippingAddress = $"{_random.Next(100, 9999)} Main St, City, ST {_random.Next(10000, 99999)}"
                    };

                    _orders.Add(order);
                    orderId++;
                }
            }

            _orders = _orders.OrderByDescending(o => o.OrderDate).ToList();
        }

        public Task<List<Order>> GetOrdersAsync(int pageNumber, int pageSize, string searchTerm = "")
        {
            var query = _orders.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(o =>
                    o.OrderNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    o.CustomerName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    o.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    o.Id.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            var result = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<List<Order>> GetAllOrdersAsync()
        {
            return Task.FromResult(_orders.ToList());
        }

        public Task<int> GetTotalOrderCountAsync()
        {
            return Task.FromResult(_orders.Count);
        }

        public Task<DashboardKpi> GetKpiDataAsync()
        {
            var kpi = new DashboardKpi
            {
                TotalOrders = _orders.Count,
                PendingPayment = _orders.Count(o => o.PaymentStatus == PaymentStatus.Pending),
                Processing = _orders.Count(o => o.ReceivedStatus == ReceivedStatus.Processing),
                Shipped = _orders.Count(o => o.ReceivedStatus == ReceivedStatus.Shipped),
                Delivered = _orders.Count(o => o.ReceivedStatus == ReceivedStatus.Delivered),
                Cancelled = _orders.Count(o => o.ReceivedStatus == ReceivedStatus.Cancelled),
                Returned = _orders.Count(o => o.ReceivedStatus == ReceivedStatus.Returned),
                Failed = _orders.Count(o => o.PaymentStatus == PaymentStatus.Unpaid && 
                                           o.ReceivedStatus == ReceivedStatus.Cancelled)
            };

            return Task.FromResult(kpi);
        }

        public Task<Order?> GetOrderByIdAsync(string orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            return Task.FromResult(order);
        }

        public Task<Order?> GetOrderByOrderNumberAsync(string orderNumber)
        {
            var order = _orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            return Task.FromResult(order);
        }

        public Task<List<ProfitMarginData>> GetProfitMarginDataAsync(int months = 12)
        {
            var profitData = new List<ProfitMarginData>();
            var startDate = DateTime.Now.AddMonths(-months);
            
            for (int i = 0; i < months; i++)
            {
                var monthStart = startDate.AddMonths(i);
                var monthEnd = monthStart.AddMonths(1);
                
                var ordersInMonth = _orders.Where(o => 
                    o.OrderDate >= monthStart && 
                    o.OrderDate < monthEnd &&
                    o.PaymentStatus == PaymentStatus.Paid).ToList();
                
                var totalRevenue = ordersInMonth.Sum(o => o.Amount);
                var costOfGoods = totalRevenue * 0.65m; // 65% COGS
                var totalProfit = totalRevenue - costOfGoods;
                
                profitData.Add(new ProfitMarginData
                {
                    Month = monthStart.ToString("MMM yyyy"),
                    Earnings = totalRevenue,
                    TotalProfits = totalProfit
                });
            }
            
            return Task.FromResult(profitData);
        }
    }
}
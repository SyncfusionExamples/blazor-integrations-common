using OrderManagementDashboard.Models;

namespace OrderManagementDashboard.Services
{
    public class AbandonedCartService : IAbandonedCartService
    {
        private readonly List<AbandonedCart> _items;
        private readonly IOrderService _orderService;

        public AbandonedCartService(IOrderService orderService)
        {
            _orderService = orderService;
            _items = GenerateSampleData();
        }

        public Task<List<AbandonedCart>> GetAbandonedCartsAsync(int pageNumber, int pageSize, string searchTerm = "")
        {
            var query = _items.AsEnumerable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(x => x.PlacedBy.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                        x.Id.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            var result = query
                .OrderByDescending(x => x.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<int> GetTotalCountAsync() => Task.FromResult(_items.Count);

        private List<AbandonedCart> GenerateSampleData()
        {
            var data = new List<AbandonedCart>();
            var random = new Random(123);
            var baseDate = DateTime.Now.AddDays(-90);
            
            var existingOrders = _orderService.GetAllOrdersAsync().Result;
            var customerEmails = existingOrders
                .Select(o => o.Email)
                .Distinct()
                .ToList();

            for (int i = 1; i <= 100; i++)
            {
                var abandonDate = baseDate.AddDays(random.Next(0, 90));
                var cartAmount = Math.Round((decimal)(random.Next(15, 750) + random.NextDouble()), 2);
                
                string email;
                if (random.NextDouble() < 0.7 && customerEmails.Count > 0)
                {
                    email = customerEmails[random.Next(customerEmails.Count)];
                }
                else
                {
                    var firstName = SharedDataConstants.FirstNames[random.Next(SharedDataConstants.FirstNames.Length)];
                    var lastName = SharedDataConstants.LastNames[random.Next(SharedDataConstants.LastNames.Length)];
                    email = SharedDataConstants.GenerateEmail(firstName, lastName, 5000 + i);
                }

                data.Add(new AbandonedCart
                {
                    Id = $"CART{500000 + i}",
                    Date = abandonDate,
                    PlacedBy = email,
                    Amount = cartAmount
                });
            }
            return data.OrderByDescending(x => x.Date).ToList();
        }
    }
}
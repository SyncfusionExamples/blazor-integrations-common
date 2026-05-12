using OrderManagementDashboard.Models;

namespace OrderManagementDashboard.Services
{
    public class AbandonedCartService : IAbandonedCartService
    {
        private readonly List<AbandonedCart> _items;

        public AbandonedCartService()
        {
            _items = GenerateSampleData();
        }

        public Task<List<AbandonedCart>> GetAbandonedCartsAsync(int pageNumber, int pageSize, string searchTerm = "")
        {
            var query = _items.AsEnumerable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(x => x.PlacedBy.Contains(searchTerm));

            var result = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<int> GetTotalCountAsync() => Task.FromResult(_items.Count);

        private List<AbandonedCart> GenerateSampleData()
        {
            var data = new List<AbandonedCart>();
            for (int i = 1; i <= 100; i++)
            {
                data.Add(new AbandonedCart
                {
                    Id = $"#{73423 + i}",
                    Date = new DateTime(2027, 9, 12),
                    PlacedBy = "example@gmail.com",
                    Amount = 400
                });
            }
            return data;
        }
    }
}
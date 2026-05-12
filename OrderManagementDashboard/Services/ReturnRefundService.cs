using OrderManagementDashboard.Models;

namespace OrderManagementDashboard.Services
{
    public class ReturnRefundService : IReturnRefundService
    {
        private readonly List<ReturnRefund> _items;

        public ReturnRefundService()
        {
            _items = GenerateSampleData();
        }

        public Task<List<ReturnRefund>> GetReturnRefundsAsync(int pageNumber, int pageSize, string searchTerm = "")
        {
            var query = _items.AsEnumerable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(x => x.OrderNumber.Contains(searchTerm) || x.Email.Contains(searchTerm));

            var result = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<int> GetTotalCountAsync() => Task.FromResult(_items.Count);

        private List<ReturnRefund> GenerateSampleData()
        {
            var data = new List<ReturnRefund>();
            for (int i = 1; i <= 100; i++)
            {
                data.Add(new ReturnRefund
                {
                    Id = $"#{73423 + i}",
                    OrderNumber = $"#{803 + i}",
                    Amount = 400,
                    OrderDate = new DateTime(2027, 9, 12),
                    Email = "example@gmail.com",
                    Reason = "Product does not match the description",
                    Status = "Paid",
                    Date = new DateTime(2027, 9, 12)
                });
            }
            return data;
        }
    }
}
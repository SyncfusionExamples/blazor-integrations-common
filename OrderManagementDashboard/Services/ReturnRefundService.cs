using OrderManagementDashboard.Models;

namespace OrderManagementDashboard.Services
{
    public class ReturnRefundService : IReturnRefundService
    {
        private readonly List<ReturnRefund> _items;
        private readonly IOrderService _orderService;
        
        private readonly string[] _reasons = new[]
        {
            "Product does not match the description",
            "Item arrived damaged",
            "Wrong item received",
            "Found better price elsewhere",
            "Product quality below expectations",
            "Changed mind about purchase",
            "Defective upon arrival",
            "Size/fit not suitable",
            "Duplicate order by mistake",
            "Product no longer needed"
        };

        private readonly string[] _statuses = new[] { "Pending", "Approved", "Rejected", "Processing", "Refunded" };

        public ReturnRefundService(IOrderService orderService)
        {
            _orderService = orderService;
            _items = GenerateSampleData();
        }

        public Task<List<ReturnRefund>> GetReturnRefundsAsync(int pageNumber, int pageSize, string searchTerm = "")
        {
            var query = _items.AsEnumerable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(x => x.OrderNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || 
                                        x.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                        x.Id.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            var result = query
                .OrderByDescending(x => x.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<int> GetTotalCountAsync() => Task.FromResult(_items.Count);

        public Task<bool> ApproveRefundAsync(string refundId)
        {
            var item = _items.FirstOrDefault(x => x.Id == refundId);
            if (item != null && item.Status != "Refunded")
            {
                item.Status = "Approved";
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> RejectRefundAsync(string refundId)
        {
            var item = _items.FirstOrDefault(x => x.Id == refundId);
            if (item != null && item.Status != "Refunded")
            {
                item.Status = "Rejected";
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        private List<ReturnRefund> GenerateSampleData()
        {
            var data = new List<ReturnRefund>();
            var random = new Random(42);
            
            // Get delivered orders that can be returned
            var eligibleOrders = _orderService.GetAllOrdersAsync().Result
                .Where(o => o.PaymentStatus == PaymentStatus.Paid && 
                           (o.ReceivedStatus == ReceivedStatus.Delivered || 
                            o.ReceivedStatus == ReceivedStatus.Returned))
                .OrderByDescending(o => o.OrderDate)
                .Take(100)
                .ToList();

            for (int i = 0; i < Math.Min(100, eligibleOrders.Count); i++)
            {
                var order = eligibleOrders[i];
                var returnDate = order.OrderDate.AddDays(random.Next(2, 20));
                
                // Ensure return date is not in the future
                if (returnDate > DateTime.Now)
                    returnDate = DateTime.Now.AddDays(-random.Next(1, 10));

                data.Add(new ReturnRefund
                {
                    Id = $"RET{100000 + i}",
                    OrderNumber = order.OrderNumber,
                    Amount = order.Amount,
                    OrderDate = order.OrderDate,
                    Email = order.Email,
                    Reason = _reasons[random.Next(_reasons.Length)],
                    Status = _statuses[random.Next(_statuses.Length)],
                    Date = returnDate
                });
            }
            
            return data.OrderByDescending(x => x.Date).ToList();
        }
    }
}
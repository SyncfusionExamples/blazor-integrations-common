using OrderManagementDashboard.Models;

namespace OrderManagementDashboard.Services
{
    public class OrderService : IOrderService
    {
        private static List<Order> _orders = new List<Order>();
        private static readonly Random _random = new Random(42); // Fixed seed for consistency

        public OrderService()
        {
            if (_orders.Count == 0)
            {
                GenerateRealisticOrders();
            }
        }

        private void GenerateRealisticOrders()
        {
            var firstNames = new[] { "John", "Emma", "Michael", "Sophia", "William", "Olivia", "James", "Ava", 
                "Robert", "Isabella", "David", "Mia", "Richard", "Charlotte", "Joseph", "Amelia", "Thomas", "Harper",
                "Daniel", "Evelyn", "Matthew", "Abigail", "Christopher", "Emily", "Andrew", "Elizabeth", "Joshua", 
                "Sofia", "Kevin", "Avery", "Brian", "Ella", "George", "Scarlett", "Timothy", "Grace", "Ronald", 
                "Chloe", "Jason", "Victoria", "Jeffrey", "Riley", "Ryan", "Aria", "Jacob", "Lily", "Gary", "Aubrey",
                "Nicholas", "Zoey", "Eric", "Penelope", "Jonathan", "Lillian", "Stephen", "Addison", "Larry", "Layla",
                "Justin", "Natalie", "Scott", "Camila", "Brandon", "Hannah", "Benjamin", "Brooklyn", "Samuel", "Zoe",
                "Raymond", "Nora", "Gregory", "Leah", "Alexander", "Savannah", "Patrick", "Audrey", "Frank", "Claire",
                "Dennis", "Eleanor", "Jerry", "Skylar", "Tyler", "Ellie", "Aaron", "Samantha", "Jose", "Stella",
                "Adam", "Paisley", "Nathan", "Violet", "Douglas", "Mila", "Zachary", "Allison", "Peter", "Alexa"};
            
            var lastNames = new[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis",
                "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor",
                "Moore", "Jackson", "Martin", "Lee", "Thompson", "White", "Harris", "Clark", "Lewis", "Robinson",
                "Walker", "Hall", "Allen", "Young", "King", "Wright", "Scott", "Green", "Baker", "Adams", "Nelson",
                "Carter", "Mitchell", "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards",
                "Collins", "Stewart", "Morris", "Rogers", "Reed", "Cook", "Morgan", "Bell", "Murphy", "Bailey",
                "Rivera", "Cooper", "Richardson", "Cox", "Howard", "Ward", "Torres", "Peterson", "Gray", "Ramirez",
                "James", "Watson", "Brooks", "Kelly", "Sanders", "Price", "Bennett", "Wood", "Barnes", "Ross",
                "Henderson", "Coleman", "Jenkins", "Perry", "Powell", "Long", "Patterson", "Hughes", "Flores",
                "Washington", "Butler", "Simmons", "Foster", "Gonzales", "Bryant", "Alexander", "Russell", "Griffin"};

            var products = new[] { "Laptop", "Smartphone", "Tablet", "Headphones", "Smartwatch", "Camera", 
                "Speaker", "Monitor", "Keyboard", "Mouse", "Printer", "Router", "Hard Drive", "USB Cable",
                "Power Bank", "Webcam", "Microphone", "Desk Lamp", "Office Chair", "Standing Desk" };

            var reasons = new[] { "Standard purchase", "Bulk order", "Corporate purchase", "Promotional offer",
                "Repeat customer", "New customer", "Seasonal sale", "Holiday special", "Clearance item",
                "Premium product", "Gift purchase", "Business requirement" };

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
                    var firstName = firstNames[_random.Next(firstNames.Length)];
                    var lastName = lastNames[_random.Next(lastNames.Length)];
                    var customerName = $"{firstName} {lastName}";
                    var email = $"{firstName.ToLower()}.{lastName.ToLower()}{_random.Next(1, 999)}@email.com";
                    
                    var daysAgo = _random.Next(0, 365);
                    var orderDate = startDate.AddDays(daysAgo);
                    
                    var itemCount = _random.Next(1, 8);
                    var itemsList = new HashSet<string>();
                    while (itemsList.Count < itemCount)
                    {
                        itemsList.Add(products[_random.Next(products.Length)]);
                    }
                    
                    var amount = _random.Next(50, 3500);
                    
                    var statusParts = status.Key.Split('_');
                    var paymentStatus = Enum.Parse<PaymentStatus>(statusParts[0]);
                    var receivedStatus = Enum.Parse<ReceivedStatus>(statusParts[1]);

                    _orders.Add(new Order
                    {
                        Id = $"ORD{orderId:D6}",
                        OrderNumber = $"ON-{orderDate:yyyyMM}-{orderId:D4}",
                        CustomerId = $"CUST{_random.Next(1000, 9999)}",
                        CustomerName = customerName,
                        Email = email,
                        Items = itemCount,
                        Amount = amount,
                        PaymentStatus = paymentStatus,
                        ReceivedStatus = receivedStatus,
                        OrderDate = orderDate,
                        Date = orderDate,
                        Reason = reasons[_random.Next(reasons.Length)]
                    });

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
                Failed = _orders.Count(o => o.PaymentStatus == PaymentStatus.Unpaid)
            };

            return Task.FromResult(kpi);
        }

        public Task<Order?> GetOrderByIdAsync(string orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            return Task.FromResult(order);
        }
    }
}
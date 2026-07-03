using FluxorBlazorDataGrid.Models;

namespace FluxorBlazorDataGrid.Services
{
    public class OrderService
    {
        private static int _nextId = 1;

        static OrderService()
        {
            var seedData = GetSeedData();
            if (seedData.Any())
                _nextId = seedData.Max(o => o.Id);
        }

        private static List<Order> GetSeedData()
        {
            return new List<Order>
            {
                new Order { Id =  1, CustomerName = "John Doe",        ProductName = "Laptop",              Quantity = 1, Price = 1200.00m, OrderDate = DateTime.Now.AddDays(-20), Status = "Completed"  },
                new Order { Id =  2, CustomerName = "Jane Smith",      ProductName = "Mouse",               Quantity = 2, Price = 25.50m,   OrderDate = DateTime.Now.AddDays(-19), Status = "Pending"    },
                new Order { Id =  3, CustomerName = "Bob Johnson",     ProductName = "Keyboard",            Quantity = 1, Price = 75.00m,   OrderDate = DateTime.Now.AddDays(-18), Status = "Processing" },
                new Order { Id =  4, CustomerName = "Alice Brown",     ProductName = "Monitor",             Quantity = 2, Price = 350.00m,  OrderDate = DateTime.Now.AddDays(-17), Status = "Shipped"    },
                new Order { Id =  5, CustomerName = "Charlie Wilson",  ProductName = "Webcam",              Quantity = 1, Price = 85.00m,   OrderDate = DateTime.Now.AddDays(-16), Status = "Pending"    },
                new Order { Id =  6, CustomerName = "Diana Prince",    ProductName = "Headphones",          Quantity = 3, Price = 120.00m,  OrderDate = DateTime.Now.AddDays(-15), Status = "Shipped"    },
                new Order { Id =  7, CustomerName = "Ethan Hunt",      ProductName = "USB Hub",             Quantity = 2, Price = 45.00m,   OrderDate = DateTime.Now.AddDays(-14), Status = "Completed"  },
                new Order { Id =  8, CustomerName = "Fiona Green",     ProductName = "Desk Chair",          Quantity = 1, Price = 299.00m,  OrderDate = DateTime.Now.AddDays(-13), Status = "Processing" },
                new Order { Id =  9, CustomerName = "George Harris",   ProductName = "Standing Desk",       Quantity = 1, Price = 499.00m,  OrderDate = DateTime.Now.AddDays(-12), Status = "Pending"    },
                new Order { Id = 10, CustomerName = "Hannah Lee",      ProductName = "Laptop Stand",        Quantity = 2, Price = 60.00m,   OrderDate = DateTime.Now.AddDays(-11), Status = "Shipped"    },
                new Order { Id = 11, CustomerName = "Ivan Petrov",     ProductName = "SSD Drive",           Quantity = 1, Price = 110.00m,  OrderDate = DateTime.Now.AddDays(-10), Status = "Completed"  },
                new Order { Id = 12, CustomerName = "Julia Roberts",   ProductName = "Printer",             Quantity = 1, Price = 220.00m,  OrderDate = DateTime.Now.AddDays(-9),  Status = "Cancelled"  },
                new Order { Id = 13, CustomerName = "Kevin Nguyen",    ProductName = "Ink Cartridge",       Quantity = 5, Price = 18.00m,   OrderDate = DateTime.Now.AddDays(-8),  Status = "Completed"  },
                new Order { Id = 14, CustomerName = "Laura Martinez",  ProductName = "Ethernet Cable",      Quantity = 3, Price = 12.00m,   OrderDate = DateTime.Now.AddDays(-7),  Status = "Shipped"    },
                new Order { Id = 15, CustomerName = "Mark Thompson",   ProductName = "Graphics Card",       Quantity = 1, Price = 650.00m,  OrderDate = DateTime.Now.AddDays(-6),  Status = "Processing" },
                new Order { Id = 16, CustomerName = "Nina Patel",      ProductName = "RAM Module",          Quantity = 2, Price = 95.00m,   OrderDate = DateTime.Now.AddDays(-5),  Status = "Pending"    },
                new Order { Id = 17, CustomerName = "Oscar Wilde",     ProductName = "Power Supply",        Quantity = 1, Price = 130.00m,  OrderDate = DateTime.Now.AddDays(-4),  Status = "Shipped"    },
                new Order { Id = 18, CustomerName = "Paula Scott",     ProductName = "Cooling Pad",         Quantity = 2, Price = 40.00m,   OrderDate = DateTime.Now.AddDays(-3),  Status = "Completed"  },
                new Order { Id = 19, CustomerName = "Quinn Adams",     ProductName = "Mechanical Keyboard", Quantity = 1, Price = 145.00m,  OrderDate = DateTime.Now.AddDays(-2),  Status = "Processing" },
                new Order { Id = 20, CustomerName = "Rachel Kim",      ProductName = "Wireless Mouse",      Quantity = 2, Price = 55.00m,   OrderDate = DateTime.Now.AddDays(-1),  Status = "Pending"    }
            };
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            await Task.Delay(500);
            return GetSeedData();
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await Task.Delay(300);
            order.Id = Interlocked.Increment(ref _nextId);
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            await Task.Delay(300);
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            await Task.Delay(300);
            return true;
        }
    }
}
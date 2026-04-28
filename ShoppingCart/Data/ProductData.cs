using ShoppingCart.Models;

namespace ShoppingCart.Data
{
    public static class ProductData
    {
        public static List<Product> Products => new()
        {
            new() { Id = 1, Name = "Wireless Headphones", Description = "Premium noise-canceling headphones", 
                    Price = 299.99m, Category = "Electronics", 
                    ImageUrl = "https://via.placeholder.com/300x300?text=Headphones",
                    Rating = 4.8, ReviewCount = 256, IsInStock = true },
            new() { Id = 2, Name = "Smart Watch", Description = "Feature-rich smartwatch", 
                    Price = 199.99m, Category = "Electronics", 
                    ImageUrl = "https://via.placeholder.com/300x300?text=Watch",
                    Rating = 4.6, ReviewCount = 189, IsInStock = true },
            new() { Id = 3, Name = "Running Shoes", Description = "Lightweight running shoes", 
                    Price = 129.99m, Category = "Footwear", 
                    ImageUrl = "https://via.placeholder.com/300x300?text=Shoes",
                    Rating = 4.7, ReviewCount = 423, IsInStock = true },
            new() { Id = 4, Name = "Leather Backpack", Description = "Durable leather backpack", 
                    Price = 89.99m, Category = "Accessories", 
                    ImageUrl = "https://via.placeholder.com/300x300?text=Backpack",
                    Rating = 4.5, ReviewCount = 156, IsInStock = true },
            new() { Id = 5, Name = "USB-C Cable", Description = "High-speed USB-C charging cable", 
                    Price = 19.99m, Category = "Accessories", 
                    ImageUrl = "https://via.placeholder.com/300x300?text=Cable",
                    Rating = 4.4, ReviewCount = 512, IsInStock = true },
            new() { Id = 6, Name = "Portable Charger", Description = "20000mAh portable power bank", 
                    Price = 49.99m, Category = "Electronics", 
                    ImageUrl = "https://via.placeholder.com/300x300?text=Charger",
                    Rating = 4.7, ReviewCount = 301, IsInStock = true },
        };

        public static List<string> Categories => Products
            .Select(p => p.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }
}
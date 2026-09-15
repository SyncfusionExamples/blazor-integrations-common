using ShoppingCart.Models;

namespace ShoppingCart.Data
{
    public static class ProductData
    {
        public static List<Product> Products => new()
        {
            new() { Id = 1, Name = "Wireless Headphones", Description = "Premium noise-canceling headphones", Price = 299.99m, Category = "Electronics", ImageUrl = "/images/product1.jpg", Rating = 4.8, ReviewCount = 256, IsInStock = true },
            new() { Id = 2, Name = "Smart Watch", Description = "Feature-rich smartwatch", Price = 199.99m, Category = "Electronics", ImageUrl = "/images/product2.jpg", Rating = 4.6, ReviewCount = 189, IsInStock = true },
            new() { Id = 3, Name = "Running Shoes", Description = "Lightweight running shoes", Price = 129.99m, Category = "Footwear", ImageUrl = "/images/product3.jpg", Rating = 4.7, ReviewCount = 423, IsInStock = true },
            new() { Id = 4, Name = "Leather Backpack", Description = "Durable leather backpack", Price = 89.99m, Category = "Accessories", ImageUrl = "/images/product4.jpg", Rating = 4.5, ReviewCount = 156, IsInStock = true },
            new() { Id = 5, Name = "USB-C Cable", Description = "High-speed USB-C charging cable", Price = 19.99m, Category = "Accessories", ImageUrl = "/images/product5.jpg", Rating = 4.4, ReviewCount = 512, IsInStock = true },
            new() { Id = 6, Name = "Portable Charger", Description = "20000mAh portable power bank", Price = 49.99m, Category = "Electronics", ImageUrl = "/images/product6.jpg", Rating = 4.7, ReviewCount = 301, IsInStock = true },
            new() { Id = 7, Name = "Bluetooth Speaker", Description = "Loud portable speaker", Price = 39.99m, Category = "Electronics", ImageUrl = "/images/product7.jpg", Rating = 4.5, ReviewCount = 210, IsInStock = true },
            new() { Id = 8, Name = "Smartphone", Description = "Latest model smartphone", Price = 499.99m, Category = "Electronics", ImageUrl = "/images/product9.png", Rating = 4.6, ReviewCount = 320, IsInStock = true },
            new() { Id = 9, Name = "Wireless Mouse", Description = "Ergonomic wireless mouse", Price = 29.99m, Category = "Accessories", ImageUrl = "/images/product10.jpg", Rating = 4.3, ReviewCount = 110, IsInStock = true },
            new() { Id = 10, Name = "Mechanical Keyboard", Description = "Tactile mechanical keyboard", Price = 89.99m, Category = "Accessories", ImageUrl = "/images/product2.jpg", Rating = 4.7, ReviewCount = 89, IsInStock = true },
            new() { Id = 11, Name = "Action Camera", Description = "Waterproof action camera", Price = 149.99m, Category = "Electronics", ImageUrl = "/images/product9.png", Rating = 4.2, ReviewCount = 67, IsInStock = true },
            new() { Id = 12, Name = "Noise Cancelling Earbuds", Description = "Compact true wireless earbuds", Price = 129.99m, Category = "Electronics", ImageUrl = "/images/product1.jpg", Rating = 4.4, ReviewCount = 240, IsInStock = true },
            new() { Id = 13, Name = "Gaming Headset", Description = "Surround sound gaming headset", Price = 79.99m, Category = "Electronics", ImageUrl = "/images/headphones.png", Rating = 4.1, ReviewCount = 98, IsInStock = true },
            new() { Id = 14, Name = "4K Monitor", Description = "Ultra HD 4K monitor", Price = 349.99m, Category = "Electronics", ImageUrl = "/images/product9.png", Rating = 4.6, ReviewCount = 45, IsInStock = true },
            new() { Id = 15, Name = "Fitness Band", Description = "Track your activity and sleep", Price = 59.99m, Category = "Electronics", ImageUrl = "/images/product8.jpg", Rating = 4.0, ReviewCount = 75, IsInStock = true },
            new() { Id = 16, Name = "Portable SSD", Description = "Fast external SSD drive", Price = 119.99m, Category = "Storage", ImageUrl = "/images/product6.jpg", Rating = 4.8, ReviewCount = 53, IsInStock = true },
            new() { Id = 17, Name = "Wireless Charger", Description = "Fast wireless charging pad", Price = 24.99m, Category = "Accessories", ImageUrl = "/images/product6.jpg", Rating = 4.2, ReviewCount = 33, IsInStock = true },
            new() { Id = 18, Name = "Tripod", Description = "Lightweight camera tripod", Price = 34.99m, Category = "Accessories", ImageUrl = "/images/product9.png", Rating = 4.1, ReviewCount = 22, IsInStock = true },
            new() { Id = 19, Name = "Desk Lamp", Description = "LED desk lamp with dimmer", Price = 39.99m, Category = "Home", ImageUrl = "/images/product7.jpg", Rating = 4.3, ReviewCount = 18, IsInStock = true },
            new() { Id = 20, Name = "Backpack Cover", Description = "Waterproof backpack cover", Price = 14.99m, Category = "Accessories", ImageUrl = "/images/product4.jpg", Rating = 4.0, ReviewCount = 12, IsInStock = true },
        };

        public static List<string> Categories => Products
            .Select(p => p.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }
}
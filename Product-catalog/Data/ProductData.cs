using BlazorProductGrid.Models;

namespace BlazorProductGrid.Data;

public class ProductData
{
    public static List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Wireless Bluetooth Headphones",
                Category = "Electronics",
                Price = 79.99m,
                Stock = 4,
                ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=300",
                Rating = 4.5,
                Description = "Premium wireless headphones with noise cancellation"
            },
            new Product
            {
                Id = 2,
                Name = "Smart Watch Series 5",
                Category = "Electronics",
                Price = 299.99m,
                Stock = 30,
                ImageUrl = "https://images.unsplash.com/photo-1546868871-7041f2a55e12?w=300",
                Rating = 4.8,
                Description = "Advanced smartwatch with health monitoring"
            },
            new Product
            {
                Id = 3,
                Name = "Leather Messenger Bag",
                Category = "Accessories",
                Price = 149.99m,
                Stock = 25,
                ImageUrl = "https://images.unsplash.com/photo-1548036328-c9fa89d128fa?w=300",
                Rating = 4.6,
                Description = "Genuine leather messenger bag for professionals"
            },
            new Product
            {
                Id = 4,
                Name = "Running Shoes Pro",
                Category = "Footwear",
                Price = 129.99m,
                Stock = 60,
                ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=300",
                Rating = 4.7,
                Description = "High-performance running shoes for athletes"
            },
            new Product
            {
                Id = 5,
                Name = "Stainless Steel Water Bottle",
                Category = "Home & Kitchen",
                Price = 34.99m,
                Stock = 100,
                ImageUrl = "https://images.unsplash.com/photo-1602143407151-7111542de6e8?w=300",
                Rating = 4.4,
                Description = "Insulated water bottle keeps drinks cold 24hrs"
            },
            new Product
            {
                Id = 6,
                Name = "Mechanical Keyboard RGB",
                Category = "Electronics",
                Price = 89.99m,
                Stock = 40,
                ImageUrl = "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=300",
                Rating = 4.9,
                Description = "Professional mechanical keyboard with RGB lighting"
            },
            new Product
            {
                Id = 7,
                Name = "Polarized Sunglasses",
                Category = "Accessories",
                Price = 59.99m,
                Stock = 55,
                ImageUrl = "https://images.unsplash.com/photo-1572635196237-14b3f281503f?w=300",
                Rating = 4.3,
                Description = "UV protection polarized sunglasses for men"
            },
            new Product
            {
                Id = 8,
                Name = "Ceramic Coffee Mug Set",
                Category = "Home & Kitchen",
                Price = 45.99m,
                Stock = 70,
                ImageUrl = "https://images.unsplash.com/photo-1514228742587-6b1558fcca3d?w=300",
                Rating = 4.5,
                Description = "Set of 4 premium ceramic coffee mugs"
            },
            new Product
            {
                Id = 9,
                Name = "Wireless Mouse Ergonomic",
                Category = "Electronics",
                Price = 49.99m,
                Stock = 80,
                ImageUrl = "https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=300",
                Rating = 4.6,
                Description = "Ergonomic wireless mouse for comfortable use"
            },
            new Product
            {
                Id = 10,
                Name = "Canvas Backpack",
                Category = "Accessories",
                Price = 79.99m,
                Stock = 35,
                ImageUrl = "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=300",
                Rating = 4.7,
                Description = "Durable canvas backpack with laptop compartment"
            },
            new Product
            {
                Id = 11,
                Name = "Yoga Mat Premium",
                Category = "Sports",
                Price = 39.99m,
                Stock = 90,
                ImageUrl = "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=300",
                Rating = 4.8,
                Description = "Non-slip premium yoga mat for fitness"
            },
            new Product
            {
                Id = 12,
                Name = "Portable Charger 20000mAh",
                Category = "Electronics",
                Price = 49.99m,
                Stock = 65,
                ImageUrl = "https://images.unsplash.com/photo-1609091839311-d5365f9ff1c5?w=300",
                Rating = 4.4,
                Description = "High capacity portable charger for all devices"
            },
            new Product
            {
                Id = 13,
                Name = "Wireless Earbuds Pro",
                Category = "Electronics",
                Price = 149.99m,
                Stock = 50,
                ImageUrl = "https://images.unsplash.com/photo-1484704849700-f032a568e944?w=300",
                Rating = 4.7,
                Description = "True wireless earbuds with active noise cancellation"
            },
            new Product
            {
                Id = 14,
                Name = "Smart Home Hub",
                Category = "Electronics",
                Price = 89.99m,
                Stock = 40,
                ImageUrl = "https://images.unsplash.com/photo-1558089687-f282ffcbc126?w=300",
                Rating = 4.5,
                Description = "Central hub for all your smart home devices"
            },
            new Product
            {
                Id = 15,
                Name = "Fitness Tracker Band",
                Category = "Electronics",
                Price = 59.99m,
                Stock = 75,
                ImageUrl = "https://images.unsplash.com/photo-1575311373937-040b8e1fd5b6?w=300",
                Rating = 4.4,
                Description = "Lightweight fitness tracker with heart rate monitor"
            },
            new Product
            {
                Id = 16,
                Name = "Leather Wallet",
                Category = "Accessories",
                Price = 69.99m,
                Stock = 45,
                ImageUrl = "https://images.unsplash.com/photo-1627123424574-724758594e93?w=300",
                Rating = 4.6,
                Description = "Genuine leather wallet with card organizer"
            },
            new Product
            {
                Id = 17,
                Name = "Desk Lamp LED",
                Category = "Home & Kitchen",
                Price = 44.99m,
                Stock = 85,
                ImageUrl = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=300",
                Rating = 4.3,
                Description = "Adjustable LED desk lamp with multiple brightness levels"
            },
            new Product
            {
                Id = 18,
                Name = "Hiking Boots",
                Category = "Footwear",
                Price = 159.99m,
                Stock = 30,
                ImageUrl = "https://images.unsplash.com/photo-1551107696-a4b0c5a0d9a2?w=300",
                Rating = 4.8,
                Description = "Waterproof hiking boots for all terrains"
            },
            new Product
            {
                Id = 19,
                Name = "Noise Canceling Earmuffs",
                Category = "Electronics",
                Price = 39.99m,
                Stock = 60,
                ImageUrl = "https://images.unsplash.com/photo-1583394838336-acd977736f90?w=300",
                Rating = 4.2,
                Description = "Comfortable earmuffs for focused work and travel"
            },
            new Product
            {
                Id = 20,
                Name = "Insulated Lunch Bag",
                Category = "Home & Kitchen",
                Price = 29.99m,
                Stock = 95,
                ImageUrl = "https://images.unsplash.com/photo-1567620905732-2d1ec7ab7445?w=300",
                Rating = 4.5,
                Description = "Reusable insulated lunch bag with multiple compartments"
            }
        };
    }
}
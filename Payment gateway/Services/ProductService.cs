using ShoppingCart.Models;
using ShoppingCart.Data;

namespace ShoppingCart.Services
{
    public class ProductService : IProductService
    {
        public Task<List<Product>> GetProductsAsync()
        {
            return Task.FromResult(ProductData.Products);
        }

        public Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            var products = ProductData.Products
                .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Task.FromResult(products);
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            var product = ProductData.Products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task<List<string>> GetCategoriesAsync()
        {
            return Task.FromResult(ProductData.Categories);
        }
    }
}
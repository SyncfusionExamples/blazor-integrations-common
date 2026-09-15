using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<List<Product>> GetProductsByCategoryAsync(string category);
        Task<Product?> GetProductByIdAsync(int id);
        Task<List<string>> GetCategoriesAsync();
    }
}
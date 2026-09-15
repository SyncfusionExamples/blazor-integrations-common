using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public interface ICartService
    {
        event Action? OnCartChanged;
        List<CartItem> Items { get; }
        int ItemCount { get; }
        decimal Total { get; }
        
        void AddItem(Product product);
        void RemoveItem(int productId);
        void UpdateQuantity(int productId, int quantity);
        void ClearCart();
    }
}
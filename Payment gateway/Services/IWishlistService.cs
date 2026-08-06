using ShoppingCart;
using ShoppingCart.Models;

public interface IWishlistService
{
    event Action? OnChange;

    void Toggle(Product product);
    bool Contains(Product product);
    IReadOnlyList<Product> GetItems();
    void Remove(Product product);
}

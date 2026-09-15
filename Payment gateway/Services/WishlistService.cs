using ShoppingCart;
using ShoppingCart.Models;

public class WishlistService : IWishlistService
{
    private readonly List<Product> _items = new();

    public event Action? OnChange;

    public void Toggle(Product product)
    {
        if (Contains(product))
            _items.RemoveAll(p => p.Id == product.Id);
        else
            _items.Add(product);

        OnChange?.Invoke();
    }

    public bool Contains(Product product)
        => _items.Any(p => p.Id == product.Id);

    public IReadOnlyList<Product> GetItems()
        => _items.AsReadOnly();

    public void Remove(Product product)
    {
        _items.RemoveAll(p => p.Id == product.Id);
        OnChange?.Invoke();
    }
}

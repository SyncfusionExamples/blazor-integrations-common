using BlazorProductGrid.Models;

namespace BlazorProductGrid.Services;

public class WishlistService
{
    private List<Product> _wishlistItems = new();

    public IReadOnlyList<Product> WishlistItems => _wishlistItems.AsReadOnly();
    public int Count => _wishlistItems.Count;

    public void AddToWishlist(Product product)
    {
        if (!_wishlistItems.Any(p => p.Id == product.Id))
        {
            _wishlistItems.Add(product);
        }
    }

    public void RemoveFromWishlist(Product product)
    {
        var item = _wishlistItems.FirstOrDefault(p => p.Id == product.Id);
        if (item != null)
        {
            _wishlistItems.Remove(item);
        }
    }

    public bool IsInWishlist(Product product)
    {
        return _wishlistItems.Any(p => p.Id == product.Id);
    }

    public void ClearWishlist()
    {
        _wishlistItems.Clear();
    }
}
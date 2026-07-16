using BlazorProductGrid.Models;

namespace BlazorProductGrid.Services;

public class CartService
{
    private List<CartItem> _cartItems = new();

    public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();
    public int TotalItems => _cartItems.Sum(c => c.Quantity);
    public decimal TotalAmount => _cartItems.Sum(c => c.Product.Price * c.Quantity);

    public void AddToCart(Product product)
    {
        var existingItem = _cartItems.FirstOrDefault(c => c.Product.Id == product.Id);
        if (existingItem != null)
        {
            if (existingItem.Quantity < product.Stock)
            {
                existingItem.Quantity++;
            }
        }
        else
        {
            _cartItems.Add(new CartItem { Product = product, Quantity = 1 });
        }
    }

    public void RemoveFromCart(Product product)
    {
        var item = _cartItems.FirstOrDefault(c => c.Product.Id == product.Id);
        if (item != null)
        {
            _cartItems.Remove(item);
        }
    }

    public void IncreaseQty(CartItem item)
    {
        var existing = _cartItems.FirstOrDefault(c => c.Product.Id == item.Product.Id);
        if (existing != null && existing.Quantity < item.Product.Stock)
        {
            existing.Quantity++;
        }
    }

    public void DecreaseQty(CartItem item)
    {
        var existing = _cartItems.FirstOrDefault(c => c.Product.Id == item.Product.Id);
        if (existing != null)
        {
            if (existing.Quantity > 1)
            {
                existing.Quantity--;
            }
            else
            {
                RemoveFromCart(item.Product);
            }
        }
    }

    public void ClearCart()
    {
        _cartItems.Clear();
    }

    public class CartItem
    {
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
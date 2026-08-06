using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class CartService : ICartService
    {
        private List<CartItem> _items = new();
        
        public event Action? OnCartChanged;
        
        public List<CartItem> Items => _items;
        
        public int ItemCount => _items.Sum(i => i.Quantity);
        
        public decimal Total => _items.Sum(i => i.Subtotal);

        public void AddItem(Product product)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductId == product.Id);
            
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                _items.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = 1,
                    ImageUrl = product.ImageUrl
                });
            }
            
            OnCartChanged?.Invoke();
        }

        public void RemoveItem(int productId)
        {
            _items.RemoveAll(i => i.ProductId == productId);
            OnCartChanged?.Invoke();
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (quantity <= 0)
                    RemoveItem(productId);
                else
                    item.Quantity = quantity;
                OnCartChanged?.Invoke();
            }
        }

        public void ClearCart()
        {
            _items.Clear();
            OnCartChanged?.Invoke();
        }
    }
}
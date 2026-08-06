using System.ComponentModel.DataAnnotations;
namespace ShoppingCart.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";

        // Stripe payment references (NEVER raw card data)
        public string? PaymentIntentId { get; set; }
        public string? PaymentStatus { get; set; }   // requires_payment_method | processing | succeeded | canceled
        public string? PaymentMethodId { get; set; }
        public string Currency { get; set; } = "usd";

        public ShippingInfo Shipping { get; set; } = new();
        public PaymentInfo Payment { get; set; } = new();
    }

    public class ShippingInfo
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "ZIP code is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Enter a valid ZIP code")]
        public string ZipCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Enter a valid phone number")]
        public string Phone { get; set; } = string.Empty;

        // Optional: separate billing email, used by Stripe receipt
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string? Email { get; set; }
    }

    // Holds NON-SENSITIVE billing metadata for display only.
    // Real card data is captured by Stripe Elements and never touches this object.
    public class PaymentInfo
    {
        public string? CardBrand { get; set; }
        public string? CardLast4 { get; set; }
        public int? CardExpMonth { get; set; }
        public int? CardExpYear { get; set; }
        public string? BillingName { get; set; }
        public string? BillingEmail { get; set; }
    }
}

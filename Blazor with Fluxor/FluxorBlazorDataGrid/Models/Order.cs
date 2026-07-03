using System.ComponentModel.DataAnnotations;

namespace FluxorBlazorDataGrid.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string? ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime OrderDate { get; set; }

        public string? Status { get; set; }

        public Order()
        {
            OrderDate = DateTime.UtcNow;
            Status = "Pending";
        }
    }
}

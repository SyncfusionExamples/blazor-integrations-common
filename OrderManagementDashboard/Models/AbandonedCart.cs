namespace OrderManagementDashboard.Models
{
    public class AbandonedCart
    {
        public string Id { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string PlacedBy { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
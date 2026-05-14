namespace OrderManagementDashboard.Models
{
    public class Order
    {
        public string Id { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public PaymentStatus PaymentStatus { get; set; }
        public ReceivedStatus ReceivedStatus { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string Items { get; set; } = string.Empty;  // Changed from int to string
        public string ShippingAddress { get; set; } = string.Empty;  // Added missing property
    }

    public enum PaymentStatus
    {
        Paid,
        Pending,
        Unpaid
    }

    public enum ReceivedStatus
    {
        Delivered,
        Processing,
        Shipped,
        Cancelled,
        Returned
    }
}
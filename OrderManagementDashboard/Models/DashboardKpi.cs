namespace OrderManagementDashboard.Models
{
    public class DashboardKpi
    {
        public int TotalOrders { get; set; }
        public int Processing { get; set; }
        public int Shipped { get; set; }
        public int Delivered { get; set; }
        public int Cancelled { get; set; }
        public int Returned { get; set; }
        public int Failed { get; set; }
    }

    public class ProfitMarginData
    {
        public string Month { get; set; } = string.Empty;
        public decimal Earnings { get; set; }
        public decimal TotalProfits { get; set; }
    }
}
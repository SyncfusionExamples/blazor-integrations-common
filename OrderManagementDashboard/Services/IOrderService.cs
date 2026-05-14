using OrderManagementDashboard.Models;

namespace OrderManagementDashboard.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync(int pageNumber, int pageSize, string searchTerm = "");
        Task<List<Order>> GetAllOrdersAsync();
        Task<int> GetTotalOrderCountAsync();
        Task<DashboardKpi> GetKpiDataAsync();
        Task<Order?> GetOrderByIdAsync(string orderId);
        Task<Order?> GetOrderByOrderNumberAsync(string orderNumber);
        Task<List<ProfitMarginData>> GetProfitMarginDataAsync(int months = 12);
    }
}
using OrderManagementDashboard.Models;

namespace OrderManagementDashboard.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync(int pageNumber, int pageSize, string searchTerm = "");
        Task<int> GetTotalOrderCountAsync();
        Task<DashboardKpi> GetKpiDataAsync();
    }
}
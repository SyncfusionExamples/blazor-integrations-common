using OrderManagementDashboard.Models;

namespace OrderManagementDashboard.Services
{
    public interface IAbandonedCartService
    {
        Task<List<AbandonedCart>> GetAbandonedCartsAsync(int pageNumber, int pageSize, string searchTerm = "");
        Task<int> GetTotalCountAsync();
    }
}
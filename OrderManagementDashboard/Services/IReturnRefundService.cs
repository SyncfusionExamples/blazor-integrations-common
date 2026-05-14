using OrderManagementDashboard.Models;

namespace OrderManagementDashboard.Services
{
    public interface IReturnRefundService
    {
        Task<List<ReturnRefund>> GetReturnRefundsAsync(int pageNumber, int pageSize, string searchTerm = "");
        Task<int> GetTotalCountAsync();
        Task<bool> ApproveRefundAsync(string refundId);
        Task<bool> RejectRefundAsync(string refundId);
    }
}
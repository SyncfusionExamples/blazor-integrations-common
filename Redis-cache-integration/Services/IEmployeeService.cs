using BlazorRedisServer.Models;

namespace BlazorRedisServer.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetEmployeesAsync(CancellationToken cancellationToken = default);
    Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default);
    Task RefreshCacheAsync(CancellationToken cancellationToken = default);
}

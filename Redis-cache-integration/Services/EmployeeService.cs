using BlazorRedisServer.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BlazorRedisServer.Services;

public class EmployeeService : IEmployeeService
{
    private const string EmployeesListCacheKey = "employees:all";
    private const string EmployeeByIdCacheKeyPrefix = "employees:byId:";

    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly IDistributedCache _cache;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(IDistributedCache cache, ILogger<EmployeeService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<List<Employee>> GetEmployeesAsync(CancellationToken cancellationToken = default)
    {
        var cached = await _cache.GetAsync(EmployeesListCacheKey, cancellationToken);
        if (cached is not null)
        {
            _logger.LogInformation("Employees retrieved from Redis cache.");
            var fromCache = JsonSerializer.Deserialize<List<Employee>>(cached, JsonOptions);
            return fromCache ?? new List<Employee>();
        }

        _logger.LogInformation("Cache miss - generating fresh employee data and storing in Redis.");
        var employees = SeedEmployees();

        var payload = JsonSerializer.SerializeToUtf8Bytes(employees, JsonOptions);
        await _cache.SetAsync(
            EmployeesListCacheKey,
            payload,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = CacheDuration
            },
            cancellationToken);

        return employees;
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var key = EmployeeByIdCacheKeyPrefix + id;
        var cached = await _cache.GetAsync(key, cancellationToken);
        if (cached is not null)
        {
            _logger.LogInformation("Employee {Id} retrieved from Redis cache.", id);
            return JsonSerializer.Deserialize<Employee>(cached, JsonOptions);
        }

        var employees = await GetEmployeesAsync(cancellationToken);
        var match = employees.FirstOrDefault(e => e.EmployeeId == id);
        if (match is not null)
        {
            var payload = JsonSerializer.SerializeToUtf8Bytes(match, JsonOptions);
            await _cache.SetAsync(
                key,
                payload,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheDuration
                },
                cancellationToken);
        }

        return match;
    }

    public async Task RefreshCacheAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Refreshing Redis cache for employees.");
        await _cache.RemoveAsync(EmployeesListCacheKey, cancellationToken);
        await GetEmployeesAsync(cancellationToken);
    }

    private static List<Employee> SeedEmployees()
    {
        return new List<Employee>
        {
            new() { EmployeeId = 1, Name = "Alice Johnson",  Designation = "Senior Developer", Department = "Engineering", Location = "Seattle",  JoinDate = new DateTime(2019, 4, 12), Salary = 120000m },
            new() { EmployeeId = 2, Name = "Bob Smith",      Designation = "Project Manager",  Department = "Delivery",    Location = "Austin",    JoinDate = new DateTime(2017, 9, 3),  Salary = 105000m },
            new() { EmployeeId = 3, Name = "Carol Davis",    Designation = "UX Designer",      Department = "Design",      Location = "Boston",    JoinDate = new DateTime(2021, 1, 20), Salary = 95000m  },
            new() { EmployeeId = 4, Name = "David Wilson",   Designation = "DevOps Engineer",  Department = "Operations",  Location = "Denver",    JoinDate = new DateTime(2020, 6, 5),  Salary = 115000m },
            new() { EmployeeId = 5, Name = "Eve Martinez",   Designation = "QA Lead",          Department = "Quality",     Location = "Chicago",   JoinDate = new DateTime(2018, 11, 15),Salary = 98000m  },
            new() { EmployeeId = 6, Name = "Frank Brown",    Designation = "Data Analyst",     Department = "Analytics",   Location = "New York",  JoinDate = new DateTime(2022, 3, 8),  Salary = 88000m  },
            new() { EmployeeId = 7, Name = "Grace Lee",      Designation = "Product Owner",    Department = "Product",     Location = "San Diego", JoinDate = new DateTime(2016, 7, 22), Salary = 130000m },
            new() { EmployeeId = 8, Name = "Hank Garcia",    Designation = "Tech Lead",        Department = "Engineering", Location = "Seattle",   JoinDate = new DateTime(2015, 2, 10), Salary = 145000m },
            new() { EmployeeId = 9, Name = "Ivy Robinson",   Designation = "Scrum Master",     Department = "Delivery",    Location = "Remote",    JoinDate = new DateTime(2019, 10, 1), Salary = 102000m },
            new() { EmployeeId = 10,Name = "Jack Walker",    Designation = "Junior Developer", Department = "Engineering", Location = "Austin",    JoinDate = new DateTime(2023, 5, 14), Salary = 72000m  }
        };
    }
}

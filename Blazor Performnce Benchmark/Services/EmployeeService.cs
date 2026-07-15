using PerformanceBenchmark.Models;

namespace PerformanceBenchmark.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GenerateLocalDataAsync(int count);
    Task<List<Employee>> FetchRemoteDataAsync(int count);
}

public class EmployeeService : IEmployeeService
{
    private static readonly string[] FirstNames =
    {
        "Michael", "Kathryn", "Tamer", "Martin", "Nancy", "Fuller", "Leverling",
        "Peacock", "Margaret", "Buchanan", "Janet", "Andrew", "Laura", "Anne",
        "Robert", "Emily", "David", "Sarah", "James", "Jessica", "Daniel", "Ashley"
    };

    private static readonly string[] LastNames =
    {
        "Davolio", "Callahan", "Dodsworth", "Bergs", "Vinet", "Anton", "Fleet",
        "Zachery", "King", "Jack", "Rose", "Smith", "Johnson", "Williams",
        "Brown", "Jones", "Garcia", "Miller", "Wilson", "Taylor"
    };

    private static readonly string[] Designations =
    {
        "Senior Engineer", "Tech Lead", "Solution Architect", "DevOps Engineer",
        "Frontend Developer", "Backend Developer", "Full Stack Developer",
        "QA Engineer", "Data Engineer", "Cloud Architect"
    };

    private static readonly string[] Departments =
        { "Engineering", "Cloud", "DevOps", "Frontend", "Backend", "QA", "Data" };

    private static readonly string[] Locations =
        { "UK", "USA", "Sweden", "France", "Canada", "Germany", "India", "Australia" };

    private static readonly string[] Statuses = { "Active", "Inactive" };

    private static readonly string[] Managers =
        { "John Doe", "Jane Smith", "Robert Brown", "Emily Davis", "Chris Lee" };

    private static readonly string[] Skills =
    {
        "Blazor", "C#", ".NET", "Azure", "MAUI", "ASP.NET Core",
        "Entity Framework", "SignalR", "gRPC", "WASM"
    };

    private static readonly string[] Domains =
        { "syncfusion.com", "company.com", "enterprise.io", "techcorp.net" };

    public Task<List<Employee>> GenerateLocalDataAsync(int count)
    {
        // Server-side: stay on the current thread; the request thread is off the UI thread anyway.
        var list = new List<Employee>(count);
        for (int i = 1; i <= count; i++)
        {
            int hireYear = 2010 + (i % 10);
            int hireMonth = 1 + (i % 12);
            int hireDay = 1 + (i % 28);
            string firstName = FirstNames[i % FirstNames.Length];
            string lastName = LastNames[(i + 3) % LastNames.Length];

            list.Add(new Employee
            {
                EmployeeID = 10000 + i,
                EmployeeName = $"{firstName} {lastName}",
                Designation = Designations[i % Designations.Length],
                Department = Departments[i % Departments.Length],
                Location = Locations[i % Locations.Length],
                Status = Statuses[i % 2],
                Salary = 50000m + (i * 800m) + ((i % 100) * 50m),
                HireDate = new DateTime(hireYear, hireMonth, hireDay),
                Experience = DateTime.Now.Year - hireYear,
                Rating = 1 + (i % 5),
                Email = $"{firstName.ToLower()}.{lastName.ToLower()}{i % 100}@{Domains[i % Domains.Length]}",
                Manager = Managers[i % Managers.Length],
                ProjectCount = 1 + (i % 20),
                SkillSet = Skills[i % Skills.Length],
            });
        }
        return Task.FromResult(list);
    }

    public async Task<List<Employee>> FetchRemoteDataAsync(int count)
    {
        // Simulated remote service (in real apps, call an HTTP API here)
        await Task.Delay(300);
        return await GenerateLocalDataAsync(count);
    }
}

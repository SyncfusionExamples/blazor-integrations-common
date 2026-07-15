namespace PerformanceBenchmark.Models;

public class Employee
{
    public int EmployeeID { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Status { get; set; } = "Active";
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
    public int Experience { get; set; }
    public int Rating { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Manager { get; set; } = string.Empty;
    public int ProjectCount { get; set; }
    public string SkillSet { get; set; } = string.Empty;
}

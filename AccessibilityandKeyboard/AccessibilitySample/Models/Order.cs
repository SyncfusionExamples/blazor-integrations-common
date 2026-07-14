namespace AccessibilitySample.Models;

public class Order
{
    public int OrderID { get; set; }
    public string CustomerID { get; set; } = string.Empty;
    public int EmployeeID { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShipName { get; set; } = string.Empty;
    public string ShipCity { get; set; } = string.Empty;
    public string ShipAddress { get; set; } = string.Empty;
    public string ShipRegion { get; set; } = string.Empty;
    public string ShipPostalCode { get; set; } = string.Empty;
    public string ShipCountry { get; set; } = string.Empty;
    public double Freight { get; set; }
    public bool Verified { get; set; }
}
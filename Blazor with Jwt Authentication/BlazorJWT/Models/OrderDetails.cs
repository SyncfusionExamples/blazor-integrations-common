namespace BlazorJWT.Models;

public class OrdersDetails
{
  public int OrderID { get; set; }
  public string? CustomerID { get; set; }
  public string? ShipCity { get; set; }
  public string? ShipCountry { get; set; }

  private static List<OrdersDetails>? _data;
  public static List<OrdersDetails> GetAllRecords()
  {
    if (_data is null)
    {
      _data = Enumerable.Range(1, 50).Select(i => new OrdersDetails
      {
        OrderID = i,
        CustomerID = $"CUST-{i:000}",
        ShipCity = i % 2 == 0 ? "Chennai" : "Bengaluru",
        ShipCountry = "India"
      }).ToList();
    }
    return _data;
  }
}

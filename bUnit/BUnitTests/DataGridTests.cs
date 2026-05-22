using BlazorApp.Components.Pages;
using Xunit;
using System.Linq;

public class DataGridTests : TestBase
{

    [Fact]
    public void DataGrid_DataSource_Count()
    {
        var comp = Render<Home>();

        // Access component instance
        var instance = comp.Instance;

        Assert.Equal(75, instance.Orders.Count);
    }

    [Fact]
    public void DataGrid_Paging_Is_Configured()
    {
        var comp = Render<Home>();

        // Pager exists
        var pager = comp.Find(".e-pager");

        Assert.NotNull(pager);

        // Validate first page row count (PageSize = 12)
        var rows = comp.FindAll(".e-row");

        Assert.Equal(12, rows.Count);
    }


    [Fact]
    public void DataGrid_Renders_Rows()
    {
        var comp = Render<Home>();

        var rows = comp.FindAll(".e-row");

        Assert.Equal(12, rows.Count);
    }

      [Fact]
    public void DataGrid_Column_Definition_Check()
    {
        var comp = Render<Home>();

        var headers = comp.FindAll(".e-headercell");

        Assert.Equal(5, headers.Count);

        Assert.Equal("Order ID", headers[0].TextContent.Trim());
        Assert.Equal("Customer Name", headers[1].TextContent.Trim());
        Assert.Equal("Order Date", headers[2].TextContent.Trim());
        Assert.Equal("Freight", headers[3].TextContent.Trim());
        Assert.Equal("Ship Country", headers[4].TextContent.Trim());
    }

    [Fact]
     public void DataGrid_Field_Value_Check()
    {
        var comp = Render<Home>();

        var instance = comp.Instance;

        var firstData = instance.Orders.First();

        var firstRowCells = comp.Find(".e-row").Children;

        // OrderID
        Assert.Equal(firstData.OrderID.ToString(), firstRowCells[0].TextContent.Trim());

        // CustomerID
        Assert.Equal(firstData.CustomerID, firstRowCells[1].TextContent.Trim());

        // ShipCountry
        Assert.Equal(firstData.ShipCountry, firstRowCells[4].TextContent.Trim());
    }
}

using NUnit.Framework;
using System.Linq;
using Bunit;
using BlazorApp.Components.Pages;

public class DataGridTests : TestBase
{
    [Test]
    public void DataGrid_DataSource_Count()
    {
        // ✅ Use RenderComponent (correct API)
        var comp = Render<Home>();

        var instance = comp.Instance;

        // ✅ FIX: Use NUnit fluent assertion
        Assert.That(instance.Orders.Count, Is.EqualTo(75));
    }

    [Test]
    public void DataGrid_Paging_Working()
    {
        var comp = Render<Home>();

        // ✅ Pager exists
        var pager = comp.Find(".e-pager");
        Assert.That(pager, Is.Not.Null);

        // ✅ Page size validation
        var rows = comp.FindAll(".e-row");
        Assert.That(rows.Count, Is.EqualTo(12));
    }

    [Test]
    public void DataGrid_Column_Definition_Check()
    {
        var comp = Render<Home>();

        var headers = comp.FindAll(".e-headercell");

        // ✅ Column count
        Assert.That(headers.Count, Is.EqualTo(5));

        // ✅ Column text validation
        Assert.That(headers[0].TextContent.Trim(), Is.EqualTo("Order ID"));
        Assert.That(headers[1].TextContent.Trim(), Is.EqualTo("Customer Name"));
        Assert.That(headers[2].TextContent.Trim(), Is.EqualTo("Order Date"));
        Assert.That(headers[3].TextContent.Trim(), Is.EqualTo("Freight"));
        Assert.That(headers[4].TextContent.Trim(), Is.EqualTo("Ship Country"));
    }

    [Test]
    public void DataGrid_Field_Value_Check()
    {
        var comp = Render<Home>();

        var instance = comp.Instance;
        var firstData = instance.Orders.First();

        var firstRowCells = comp.Find(".e-row").Children;

        // ✅ Field value validation
        Assert.That(firstRowCells[0].TextContent.Trim(), Is.EqualTo(firstData.OrderID.ToString()));
        Assert.That(firstRowCells[1].TextContent.Trim(), Is.EqualTo(firstData.CustomerID));
        Assert.That(firstRowCells[4].TextContent.Trim(), Is.EqualTo(firstData.ShipCountry));
    }
}
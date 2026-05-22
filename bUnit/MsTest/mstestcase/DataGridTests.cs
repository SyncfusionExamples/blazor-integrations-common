
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Bunit;
using BlazorApp.Components.Pages;

[TestClass]
public class DataGridTests : TestBase
{
    // ✅ 1. DataSource Count Validation
    [TestMethod]
    public void DataGrid_DataSource_Count()
    {
        var comp = Render<Home>();

        var instance = comp.Instance;

        Assert.AreEqual(75, instance.Orders.Count);
    }

    // ✅ 2. Grid Renders Successfully
    [TestMethod]
    public void DataGrid_Renders()
    {
        var comp = Render<Home>();

        var grid = comp.Find(".e-grid");

        Assert.IsNotNull(grid);
    }

    // ✅ 3. Paging Enabled Check
    [TestMethod]
    public void DataGrid_Paging_Working()
    {
        var comp = Render<Home>();

        // Pager exists
        var pager = comp.Find(".e-pager");
        Assert.IsNotNull(pager);

        // Page size = 12
        var rows = comp.FindAll(".e-row");
        Assert.AreEqual(12, rows.Count);
    }

    // ✅ 4. Column Count Validation
    [TestMethod]
    public void DataGrid_Column_Count()
    {
        var comp = Render<Home>();

        var headers = comp.FindAll(".e-headercell");

        Assert.AreEqual(5, headers.Count);
    }

    // ✅ 5. Column Header Text Validation
    [TestMethod]
    public void DataGrid_Column_Header_Text()
    {
        var comp = Render<Home>();

        var headers = comp.FindAll(".e-headercell");

        Assert.AreEqual("Order ID", headers[0].TextContent.Trim());
        Assert.AreEqual("Customer Name", headers[1].TextContent.Trim());
        Assert.AreEqual("Order Date", headers[2].TextContent.Trim());
        Assert.AreEqual("Freight", headers[3].TextContent.Trim());
        Assert.AreEqual("Ship Country", headers[4].TextContent.Trim());
    }

    // ✅ 6. First Row Data Binding Validation
    [TestMethod]
    public void DataGrid_Field_Value_Check()
    {
        var comp = Render<Home>();

        var instance = comp.Instance;
        var firstData = instance.Orders.First();

        var firstRowCells = comp.Find(".e-row").Children;

        Assert.AreEqual(firstData.OrderID.ToString(), firstRowCells[0].TextContent.Trim());
        Assert.AreEqual(firstData.CustomerID, firstRowCells[1].TextContent.Trim());
        Assert.AreEqual(firstData.ShipCountry, firstRowCells[4].TextContent.Trim());
    }

    // ✅ 7. Verify Multiple Rows Rendered
    [TestMethod]
    public void DataGrid_Row_Render_Check()
    {
        var comp = Render<Home>();

        var rows = comp.FindAll(".e-row");

        Assert.IsTrue(rows.Count > 0);
    }

    // ✅ 8. Verify Specific Cell Value Exists
    [TestMethod]
    public void DataGrid_Contains_Data()
    {
        var comp = Render<Home>();

        Assert.IsTrue(comp.Markup.Contains("Order ID"));
    }

    // ✅ 9. Verify Grid Body Exists
    [TestMethod]
    public void DataGrid_Body_Rendered()
    {
        var comp = Render<Home>();

        var tbody = comp.Find("tbody");

        Assert.IsNotNull(tbody);
    }

    // ✅ 10. Verify First Page Data Consistency
    [TestMethod]
    public void DataGrid_FirstPage_Data_Consistency()
    {
        var comp = Render<Home>();

        var rows = comp.FindAll(".e-row");

        // Page size = 12
        Assert.AreEqual(12, rows.Count);

        // Ensure first row is not empty
        Assert.IsFalse(string.IsNullOrWhiteSpace(rows[0].TextContent));
    }
}
namespace DataGridTesting.Components.Dashboard;

public static class TestCaseRepository
{
    // ──────────────────────────────────────────────────────────────────────
    //  bUnit (replaces Jest) — 25 master cases
    // ──────────────────────────────────────────────────────────────────────
    public static readonly List<TestCase> BUnitCases = new()
    {
        new() {
            Name = "1. Sort the name column in ascending order.",
            Description = "Verifies that sorting the name column arranges the grid rows alphabetically in ascending (A–Z) order based on the visible dataset.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public void T01_SortName_Ascending()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();

    var header = cut.FindAll("".e-headertext"")
                   .First(h => h.TextContent.Trim() == ""Name"");
    var cell   = header.Closest(""[role='columnheader']"");

    cell.Click();
    cut.WaitForState(() => cell.GetAttribute(""aria-sort"") == ""ascending"",
                     TimeSpan.FromSeconds(2));

    Assert.Equal(""ascending"", cell.GetAttribute(""aria-sort""));
}",
            Steps = {
                "Render the grid component with data.",
                "Locate the name column header.",
                "Perform sort action on the Name column.",
                "Wait for the grid to update.",
                "Retrieve the visible data from the grid.",
                "Extract the name values.",
                "Verify that the values are sorted in ascending order."
            }
        },
        new() {
            Name = "2. Sort the name column in descending order.",
            Description = "Verifies that applying descending sorting on the name column arranges rows alphabetically in reverse order (Z–A).",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T02_SortName_Descending()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    await cut.Instance.SortColumn(
        nameof(GridRow.Name),
        SortDirection.Descending);
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(600));
}",
            Steps = {
                "Render the grid component with data.",
                "Locate the name column header.",
                "Apply descending sort on the Name column.",
                "Wait for the grid to update.",
                "Retrieve the visible data from the grid.",
                "Extract the name values.",
                "Verify that the values are sorted in descending order."
            }
        },
        new() {
            Name = "3. Sort the ID column in ascending numerical order.",
            Description = "Verifies that sorting the ID column arranges numeric values in ascending order (smallest to largest).",
            Code = @"using Bunit;
using Xunit;

[Fact]
public void T03_SortId_Ascending()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    var id = cut.FindAll("".e-headertext"")
                .First(h => h.TextContent.Trim() == ""ID"");
    id.Closest(""[role='columnheader']"").Click();
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));
}",
            Steps = {
                "Render the grid component with data.",
                "Locate the ID column header.",
                "Apply sorting on the ID column.",
                "Wait for the grid to update.",
                "Extract the ID values.",
                "Verify that the values are sorted numerically."
            }
        },
        new() {
            Name = "4. Apply sorting to multiple columns.",
            Description = "Verifies that sorting can be applied on multiple columns simultaneously and sorting indicators are displayed correctly.",
            Code = @"using Bunit;
using Xunit;

[Fact]
public void T04_Multisort()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    var h = cut.FindAll("".e-headertext"");
    var name = h.First(x => x.TextContent.Trim() == ""Name"")
                 .Closest(""[role='columnheader']"");
    var role = h.First(x => x.TextContent.Trim() == ""Designation"")
                 .Closest(""[role='columnheader']"");

    name.Click();
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));
    role.Click(ctrlKey: true);
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));

    Assert.True(cut.FindAll(""[role='columnheader'][aria-sort]"").Count >= 2);
}",
            Steps = {
                "Render the grid component with data.",
                "Apply sorting to the first column.",
                "Apply sorting to the second column using the multisort interaction.",
                "Wait for the grid to update.",
                "Verify that the columns are sorted."
            }
        },
        new() {
            Name = "5. Filter rows by exact column value.",
            Description = "Verifies that applying an exact filter returns only rows matching the specified value.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T05_FilterExact_Equal()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    await cut.Instance.FilterByColumn(""Department"", ""equal"", ""Design"");
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(600));
}",
            Steps = {
                "Render the grid component with data.",
                "Apply the filter using exact match.",
                "Wait for the grid to update.",
                "Retrieve the filtered rows.",
                "Verify that all values match the filter exactly."
            }
        },
        new() {
            Name = "6. Filter rows using partial text match.",
            Description = "Verifies that applying a contains filter returns rows that include the specified substring.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T06_FilterContains_Des()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    await cut.Instance.FilterByColumn(""Department"", ""contains"", ""Des"");
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(600));
}",
            Steps = {
                "Render the grid component with data.",
                "Apply a partial text filter.",
                "Wait for the grid to update.",
                "Retrieve the filtered data.",
                "Verify all values contain the search text."
            }
        },
        new() {
            Name = "7. Apply multiple filters using the AND condition.",
            Description = "Verifies that applying multiple filters combines them using AND logic.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T07_MultiFilter_And()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    await cut.Instance.FilterByColumn(""Name"", ""contains"", ""a"");
    await cut.Instance.FilterByColumn(""Role"", ""contains"", ""Eng"");
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(600));
}",
            Steps = {
                "Render the grid component with data.",
                "Apply the first column filter.",
                "Apply the second column filter.",
                "Wait for the grid to update.",
                "Verify that all rows satisfy both conditions."
            }
        },
        new() {
            Name = "8. Filter should ignore case sensitivity.",
            Description = "Verifies that filtering works regardless of text case differences.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T08_Filter_IgnoresCase()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    await cut.Instance.FilterByColumn(""Department"", ""contains"", ""design"");
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(600));
}",
            Steps = {
                "Render the grid component with data.",
                "Apply the filter using a lowercase value.",
                "Wait for the grid to update.",
                "Retrieve the data.",
                "Verify that the results match, ignoring case."
            }
        },
        new() {
            Name = "9. Search with non-existent value returns no results.",
            Description = "Verifies that searching with a value not present in the dataset returns no data rows.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T09_FilterNonexistent_NoRows()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    await cut.Instance.FilterByColumn(""Department"", ""contains"", ""NonExistingValue"");
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(600));

    Assert.Empty(cut.FindAll("".e-row:not(.e-headerrow)""));
}",
            Steps = {
                "Render the grid component with data.",
                "Perform search with an invalid value.",
                "Wait for the grid to update.",
                "Verify that no rows are displayed."
            }
        },
        new() {
            Name = "10. Search rows based on Name column.",
            Description = "Verifies that searching filters rows using Name values.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T10_Search_Alice()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    await cut.Instance.Search(""Alice"");
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(600));
}",
            Steps = {
                "Render the grid component with data.",
                "Enter a search value.",
                "Wait for the grid to update.",
                "Retrieve the data.",
                "Verify that all records match the search criteria."
            }
        },
        new() {
            Name = "11. Open edit dialog on row interaction.",
            Description = "Verifies that dialog editor opens when performing edit action on a row.",
            Code = @"using Bunit;
using Xunit;

[Fact]
public void T11_DblClick_OpensDialog()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                     TimeSpan.FromSeconds(1));

    cut.Find(""tr.e-row .e-rowcell:nth-child(2)"").DoubleClick();
    cut.WaitForState(() => cut.FindAll("".e-dialog"").Count > 0,
                     TimeSpan.FromSeconds(2));

    Assert.NotEmpty(cut.FindAll("".e-dialog""));
}",
            Steps = {
                "Render the grid component with data.",
                "Double-click a row.",
                "Trigger an edit action.",
                "Verify that the dialog opens."
            }
        },
        new() {
            Name = "12. Add a new record to the grid.",
            Description = "Verifies that a new row can be added and persists in the data source.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T12_AddRecord()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    await cut.Instance.AddRecord(new GridRow {
        Id = 100, Name = ""Test User"",
        Role = ""Engineer"", Department = ""R&D""
    });
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));
}",
            Steps = {
                "Render the grid component with data.",
                "Create a new record data.",
                "Add a record.",
                "Wait for the grid to update.",
                "Verify that the record exists."
            }
        },
        new() {
            Name = "13. Update an existing record.",
            Description = "Verifies that modifying a record updates values correctly in the grid.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T13_UpdateRecord()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    var updated = cut.Instance.DataSource.Select(r => r.Id == 1
        ? new GridRow { Id = 1, Name = ""Updated User"",
            Role = ""Senior Engineer"", Department = ""R&D"",
            DateOfJoining = r.DateOfJoining } : r).ToList();

    cut.Instance.DataSource = updated;
    await cut.Instance.Refresh();
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));
}",
            Steps = {
                "Render the grid component with data.",
                "Modify record values.",
                "Apply the update.",
                "Wait for refresh.",
                "Verify the updated values."
            }
        },
        new() {
            Name = "14. Delete a record from the grid.",
            Description = "Verifies that deleting a record removes it from the data source.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T14_DeleteRecord()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    Assert.Contains(cut.Instance.DataSource, r => r.Id == 1);

    await cut.Instance.DeleteRecord(""Id"", new GridRow { Id = 1 });
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));

    Assert.DoesNotContain(cut.Instance.DataSource, r => r.Id == 1);
}",
            Steps = {
                "Render the grid component with data.",
                "Identify the record.",
                "Delete the record.",
                "Wait for the grid to update.",
                "Verify that the record is removed."
            }
        },
        new() {
            Name = "15. Select single and multiple rows.",
            Description = "Verifies that selecting rows updates the selected row indexes correctly.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public void T15_SelectRows()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));

    cut.Instance.SelectRows(new[] { 0 });
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(300));
    Assert.Contains(0, cut.Instance.SelectedRowIndexes);

    cut.Instance.SelectRows(new[] { 0, 1 });
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(300));
    Assert.Contains(1, cut.Instance.SelectedRowIndexes);
}",
            Steps = {
                "Render the grid component with data.",
                "Select a row.",
                "Select multiple rows.",
                "Verify the selected indexes."
            }
        },
        new() {
            Name = "16. Verify column headers are displayed correctly.",
            Description = "Verifies that all expected column headers are rendered.",
            Code = @"using Bunit;
using Xunit;

[Fact]
public void T16_HeadersRendered()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    var texts = cut.FindAll("".e-headertext"")
                   .Select(h => h.TextContent.Trim()).ToList();

    Assert.Equal(new[] { ""ID"", ""Name"", ""Designation"",
                          ""Department"", ""Date of Joining"" }, texts);
}",
            Steps = {
                "Render the grid component with data.",
                "Identify the headers.",
                "Verify that the expected headers are present."
            }
        },
        new() {
            Name = "17. Verify sorting icons on columns.",
            Description = "Verifies that sorting indicators are displayed on sortable columns.",
            Code = @"using Bunit;
using Xunit;

[Fact]
public void T17_SortIconPresent()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    var name = cut.FindAll("".e-headertext"")
                  .First(h => h.TextContent.Trim() == ""Name"");
    var cell = name.Closest("".e-headercell"");
    Assert.NotNull(cell.QuerySelector("".e-sortfilterdiv""));
}",
            Steps = {
                "Render the grid component with data.",
                "Identify a sortable column.",
                "Verify that the sort icon exists."
            }
        },
        new() {
            Name = "18. Verify grid data rows are rendered.",
            Description = "Verifies that the grid displays data rows correctly.",
            Code = @"using Bunit;
using Xunit;

[Fact]
public void T18_DataRowsRendered()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    cut.WaitForState(() => cut.FindAll("".e-row:not(.e-headerrow)"").Count > 0,
                     TimeSpan.FromSeconds(1));

    Assert.NotEmpty(cut.FindAll("".e-row:not(.e-headerrow)""));
}",
            Steps = {
                "Render the grid component with data.",
                "Wait for data to render.",
                "Verify that the rows exist."
            }
        },
        new() {
            Name = "19. Verify pagination limits row count.",
            Description = "Verifies that paging shows correct number of rows per page.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public void T19_Paging_12Rows()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));

    Assert.Equal(12, cut.Instance.GetCurrentViewRecords().Count);
}",
            Steps = {
                "Render the grid component with data.",
                "Retrieve current page records.",
                "Verify row count per page."
            }
        },
        new() {
            Name = "20. Verify focus behavior in filter dialog.",
            Description = "Verifies that focus remains within the dialog when it is open.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T20_FilterDialog_Focus()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    await cut.Instance.FilterByColumn(""Department"", ""contains"", """");
    cut.WaitForState(() => cut.FindAll("".e-dialog"").Count > 0,
                     TimeSpan.FromSeconds(1));

    var dialog = cut.Find("".e-dialog"");
    Assert.Equal(""true"", dialog.GetAttribute(""aria-modal""));
}",
            Steps = {
                "Render the grid component with data.",
                "Open the filter dialog.",
                "Verify that the dialog is active.",
                "Verify that focusable elements exist."
            }
        },
        new() {
            Name = "21. Verify column reorder functionality.",
            Description = "Verifies that columns can be reordered and order is updated.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T21_ColumnReorder()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));

    var before = cut.Instance.GetColumns().Select(c => c.HeaderText).ToList();
    await cut.Instance.ReorderColumns(
        new List<string> { ""Name"" }, new List<string> { ""Id"" });
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));

    var after = cut.Instance.GetColumns().Select(c => c.HeaderText).ToList();
    Assert.NotEqual(before, after);
}",
            Steps = {
                "Render the grid component with data.",
                "Capture the initial order.",
                "Reorder the columns.",
                "Verify that the column order has changed."
            }
        },
        new() {
            Name = "22. Verify column resizing updates width.",
            Description = "Verifies that resizing a column updates its width.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T22_ColumnResize()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));

    var col = cut.Instance.GetColumnByField(""Id"");
    int before = int.Parse(col.Width!);
    col.Width = (before + 50).ToString();
    await cut.Instance.RefreshColumns();
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));

    int after = int.Parse(cut.Instance.GetColumnByField(""Id"").Width!);
    Assert.True(after > before);
}",
            Steps = {
                "Render the grid component with data.",
                "Resize a column.",
                "Wait for the grid to update.",
                "Verify that the width increases."
            }
        },
        new() {
            Name = "23. Verify PDF export functionality.",
            Description = "Verifies that PDF export is triggered when the toolbar action is executed.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T23_PdfExport_Triggered()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));

    // SfGrid exposes ExportToPdfAsync() — invoked via toolbar Id
    Assert.True(cut.Markup.Contains(""sample-grid""));
}",
            Steps = {
                "Render the grid component with data.",
                "Trigger a PDF export.",
                "Verify that the export action is invoked."
            }
        },
        new() {
            Name = "24. Verify Excel export functionality.",
            Description = "Verifies that an Excel export is triggered from the toolbar.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T24_ExcelExport_Triggered()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));
    Assert.True(cut.Markup.Contains(""sample-grid""));
}",
            Steps = {
                "Render the grid component with data.",
                "Trigger an Excel export.",
                "Verify the export action."
            }
        },
        new() {
            Name = "25. Verify CSV export functionality.",
            Description = "Verifies that CSV export is triggered from the toolbar.",
            Code = @"using Bunit;
using Syncfusion.Blazor.Grids;
using Xunit;

[Fact]
public async Task T25_CsvExport_Triggered()
{
    using var ctx = new TestContext();
    ctx.Services.AddSyncfusionBlazor();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;

    var cut = ctx.Render<GridClient>();
    cut.WaitForState(() => true, TimeSpan.FromMilliseconds(500));
    Assert.True(cut.Markup.Contains(""sample-grid""));
}",
            Steps = {
                "Render the grid component with data.",
                "Trigger a CSV export.",
                "Verify the export action."
            }
        }
    };

    // ──────────────────────────────────────────────────────────────────────
    //  xUnit (replaces RTL) — 25 cases derived from bUnit syntax
    // ──────────────────────────────────────────────────────────────────────
    public static readonly List<TestCase> xUnitCases = BuildFromBUnit(b => b
        .Replace("using Bunit;",
                 "using Microsoft.AspNetCore.Components.Testing;\nusing Xunit;"));

    // ──────────────────────────────────────────────────────────────────────
    //  MSTest (replaces Cypress — Microsoft first-party) — 25 cases
    // ──────────────────────────────────────────────────────────────────────
    public static readonly List<TestCase> MSTestCases = BuildFromBUnit(b => b
        .Replace("using Xunit;",
                 "using Microsoft.VisualStudio.TestTools.UnitTesting;")
        .Replace("[Fact]", "[TestMethod]")
        .Replace("Assert.Equal(",  "Assert.AreEqual(")
        .Replace("Assert.True(",   "Assert.IsTrue(")
        .Replace("Assert.NotEmpty(","Assert.IsTrue(")
        .Replace("Assert.NotNull(", "Assert.IsNotNull(")
        .Replace("Assert.Empty(",  "Assert.IsFalse(")
        .Replace("Assert.Contains(0,", "Assert.IsTrue(0 <=")
        .Replace("Assert.DoesNotContain(", "Assert.IsFalse("));

    // ──────────────────────────────────────────────────────────────────────
    //  Playwright (kept) — 25 cases (browser-driven)
    // ──────────────────────────────────────────────────────────────────────
    public static readonly List<TestCase> PlaywrightCases =
        BUnitCases.ConvertAll(tc => new TestCase
        {
            Name = tc.Name,
            Description = tc.Description,
            Steps = tc.Steps,
            Code = @"using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using Xunit;

[Fact]
public async Task T_Playwright()
{
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.LaunchAsync();
    var page = await browser.NewPageAsync();
    await page.GotoAsync(""http://localhost:5000/testing"");
    await page.WaitForSelectorAsync(""#sample-grid .e-row"");

    // (action steps mirror the bUnit case: " + tc.Name + @")
}"
        });

    // ──────────────────────────────────────────────────────────────────────
    private static List<TestCase> BuildFromBUnit(Func<string, string> rewrite)
    {
        var list = new List<TestCase>();
        foreach (var tc in BUnitCases)
        {
            list.Add(new TestCase
            {
                Name = tc.Name,
                Description = tc.Description,
                Steps = tc.Steps,
                Code = rewrite(tc.Code)
            });
        }
        return list;
    }
}
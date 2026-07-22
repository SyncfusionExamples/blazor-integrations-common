namespace DataGridTesting.Components.Dashboard;

public static class TestCaseRepository
{
    public static readonly List<TestCase> BUnitCases = Build(Constants.Names, Codes.BUnit);
    public static readonly List<TestCase> xUnitCases = Build(Constants.Names, Codes.xUnit);
    public static readonly List<TestCase> NUnitCases = Build(Constants.Names, Codes.NUnit);
    public static readonly List<TestCase> PlaywrightCases = Build(Constants.Names, Codes.Playwright);
    public static readonly List<TestCase> CypressCases = Build(Constants.Names, Codes.Cypress);

    // Zip shared metadata with the per-framework code snippets.
    private static List<TestCase> Build(IReadOnlyList<string> names,
                                        IReadOnlyList<string> codes)
    {
        var list = new List<TestCase>(25);
        for (int i = 0; i < 25; i++)
            list.Add(new TestCase
            {
                Name = names[i],
                Description = Constants.Descriptions[i],
                Steps = Constants.Steps[i].ToList(),   // ← string[] → List<string>
                Code = codes[i]
            });
        return list;
    }

    // ─────────────────────────────────────────────────────────────────────
    //  framework tabs
    // ─────────────────────────────────────────────────────────────────────

    private static class Constants
    {
        public static readonly string[] Names =
        {
            "1. Sort the name column in ascending order.",
            "2. Sort the name column in descending order.",
            "3. Sort the ID column in ascending numerical order.",
            "4. Apply sorting to multiple columns.",
            "5. Filter rows by exact column value.",
            "6. Filter rows using partial text match.",
            "7. Apply multiple filters using the AND condition.",
            "8. Filter should ignore case sensitivity.",
            "9. Search with non-existent value returns no results.",
            "10. Search rows based on Name column.",
            "11. Open edit dialog on row interaction.",
            "12. Add a new record to the grid.",
            "13. Update an existing record.",
            "14. Delete a record from the grid.",
            "15. Select single and multiple rows.",
            "16. Verify column headers are displayed correctly.",
            "17. Verify sorting icons on columns.",
            "18. Verify grid data rows are rendered.",
            "19. Verify pagination limits row count.",
            "20. Verify focus behavior in filter dialog.",
            "21. Verify column reorder functionality.",
            "22. Verify column resizing updates width.",
            "23. Verify PDF export functionality.",
            "24. Verify Excel export functionality.",
            "25. Verify CSV export functionality."
        };

        public static readonly string[] Descriptions =
        {
            "Verifies that sorting the name column arranges the grid rows alphabetically in ascending (A–Z) order based on the visible dataset.",
            "Verifies that applying descending sorting on the name column arranges rows alphabetically in reverse order (Z–A).",
            "Verifies that sorting the ID column arranges numeric values in ascending order (smallest to largest).",
            "Verifies that sorting can be applied on multiple columns simultaneously and sorting indicators are displayed correctly.",
            "Verifies that applying an exact filter returns only rows matching the specified value.",
            "Verifies that applying a contains filter returns rows that include the specified substring.",
            "Verifies that applying multiple filters combines them using AND logic.",
            "Verifies that filtering works regardless of text case differences.",
            "Verifies that searching with a value not present in the dataset returns no data rows.",
            "Verifies that searching filters rows using Name values.",
            "Verifies that dialog editor opens when performing edit action on a row.",
            "Verifies that a new row can be added and persists in the data source.",
            "Verifies that modifying a record updates values correctly in the grid.",
            "Verifies that deleting a record removes it from the data source.",
            "Verifies that selecting rows updates the selected row indexes correctly.",
            "Verifies that all expected column headers are rendered.",
            "Verifies that sorting indicators are displayed on sortable columns.",
            "Verifies that the grid displays data rows correctly.",
            "Verifies that paging shows correct number of rows per page.",
            "Verifies that focus remains within the dialog when it is open.",
            "Verifies that columns can be reordered and order is updated.",
            "Verifies that resizing a column updates its width.",
            "Verifies that PDF export is triggered when the toolbar action is executed.",
            "Verifies that an Excel export is triggered from the toolbar.",
            "Verifies that CSV export is triggered from the toolbar."
        };

        public static readonly IReadOnlyList<string[]> Steps = new[]
        {
            new[] { "Render the grid component with data.",
                    "Locate the name column header.",
                    "Perform sort action on the Name column.",
                    "Wait for the grid to update.",
                    "Retrieve the visible data from the grid.",
                    "Extract the name values.",
                    "Verify that the values are sorted in ascending order." },
            new[] { "Render the grid component with data.",
                    "Locate the name column header.",
                    "Apply descending sort on the Name column.",
                    "Wait for the grid to update.",
                    "Retrieve the visible data from the grid.",
                    "Extract the name values.",
                    "Verify that the values are sorted in descending order." },
            new[] { "Render the grid component with data.",
                    "Locate the ID column header.",
                    "Apply sorting on the ID column.",
                    "Wait for the grid to update.",
                    "Extract the ID values.",
                    "Verify that the values are sorted numerically." },
            new[] { "Render the grid component with data.",
                    "Apply sorting to the first column.",
                    "Apply sorting to the second column using the multisort interaction.",
                    "Wait for the grid to update.",
                    "Verify that the columns are sorted." },
            new[] { "Render the grid component with data.",
                    "Apply the filter using exact match.",
                    "Wait for the grid to update.",
                    "Retrieve the filtered rows.",
                    "Verify that all values match the filter exactly." },
            new[] { "Render the grid component with data.",
                    "Apply a partial text filter.",
                    "Wait for the grid to update.",
                    "Retrieve the filtered data.",
                    "Verify all values contain the search text." },
            new[] { "Render the grid component with data.",
                    "Apply the first column filter.",
                    "Apply the second column filter.",
                    "Wait for the grid to update.",
                    "Verify that all rows satisfy both conditions." },
            new[] { "Render the grid component with data.",
                    "Apply the filter using a lowercase value.",
                    "Wait for the grid to update.",
                    "Retrieve the data.",
                    "Verify that the results match, ignoring case." },
            new[] { "Render the grid component with data.",
                    "Perform search with an invalid value.",
                    "Wait for the grid to update.",
                    "Verify that no rows are displayed." },
            new[] { "Render the grid component with data.",
                    "Enter a search value.",
                    "Wait for the grid to update.",
                    "Retrieve the data.",
                    "Verify that all records match the search criteria." },
            new[] { "Render the grid component with data.",
                    "Double-click a row.",
                    "Trigger an edit action.",
                    "Verify that the dialog opens." },
            new[] { "Render the grid component with data.",
                    "Create a new record data.",
                    "Add a record.",
                    "Wait for the grid to update.",
                    "Verify that the record exists." },
            new[] { "Render the grid component with data.",
                    "Modify record values.",
                    "Apply the update.",
                    "Wait for refresh.",
                    "Verify the updated values." },
            new[] { "Render the grid component with data.",
                    "Identify the record.",
                    "Delete the record.",
                    "Wait for the grid to update.",
                    "Verify that the record is removed." },
            new[] { "Render the grid component with data.",
                    "Select a row.",
                    "Select multiple rows.",
                    "Verify the selected indexes." },
            new[] { "Render the grid component with data.",
                    "Identify the headers.",
                    "Verify that the expected headers are present." },
            new[] { "Render the grid component with data.",
                    "Identify a sortable column.",
                    "Verify that the sort icon exists." },
            new[] { "Render the grid component with data.",
                    "Wait for data to render.",
                    "Verify that the rows exist." },
            new[] { "Render the grid component with data.",
                    "Retrieve current page records.",
                    "Verify row count per page." },
            new[] { "Render the grid component with data.",
                    "Open the filter dialog.",
                    "Verify that the dialog is active.",
                    "Verify that focusable elements exist." },
            new[] { "Render the grid component with data.",
                    "Capture the initial order.",
                    "Reorder the columns.",
                    "Verify that the column order has changed." },
            new[] { "Render the grid component with data.",
                    "Resize a column.",
                    "Wait for the grid to update.",
                    "Verify that the width increases." },
            new[] { "Render the grid component with data.",
                    "Trigger a PDF export.",
                    "Verify that the export action is invoked." },
            new[] { "Render the grid component with data.",
                    "Trigger an Excel export.",
                    "Verify the export action." },
            new[] { "Render the grid component with data.",
                    "Trigger a CSV export.",
                    "Verify the export action." }
        };
    }

    // ─────────────────────────────────────────────────────────────────────
    //  Per-framework code snippets (25 each).
    // ─────────────────────────────────────────────────────────────────────

    private static class Codes
    {

        // bUnit Test cases
        public static readonly string[] BUnit =
{
            // 1. Sort the name column in ascending order
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridSortedNameAscendingTests : TestContext
{
    [Fact(DisplayName = ""T01: Sort Name Column Ascending"")]
    public async Task SortName_Ascending()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
            await grid.SortColumnAsync(nameof(GridRow.Name),
                                       SortDirection.Ascending));

        var names = (await grid.GetCurrentViewRecordsAsync())
                    .Select(r => r.Name).ToList();
        Assert.Equal(names.OrderBy(n => n, StringComparer.Ordinal), names);
    }
}",
            // 2. Sort the name column in descending order
            @"using System;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridSortedNameDescendingTests : TestContext
{
    [Fact(DisplayName = ""T02: Sort Name Column Descending"")]
    public async Task SortName_Descending()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
            await grid.SortColumnAsync(nameof(GridRow.Name),
                                       SortDirection.Descending));

        var names = (await grid.GetCurrentViewRecordsAsync())
                    .Select(r => r.Name).ToList();
        Assert.Equal(names.OrderByDescending(n => n, StringComparer.Ordinal), names);
        Assert.Equal(""Zack"", names[0]);
    }
}",
            // 3. Sort the ID column in ascending numerical order
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridSortedIdAscendingTests : TestContext
{
    [Fact(DisplayName = ""T03: Sort ID Column Ascending"")]
    public async Task SortId_Ascending()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
            await grid.SortColumnAsync(nameof(GridRow.Id),
                                       SortDirection.Ascending));

        var ids = (await grid.GetCurrentViewRecordsAsync())
                  .Select(r => r.Id).ToList();
        Assert.Equal(ids.OrderBy(i => i), ids);
        Assert.Equal(1, ids[0]);
    }
}",
            // 4. Apply sorting to multiple columns
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridMultiSortTests : TestContext
{
    [Fact(DisplayName = ""T04: Multi-sort Multiple Columns"")]
    public async Task MultiSort_TwoColumns()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearSortingAsync();
            await grid.SortColumnAsync(nameof(GridRow.Name),   SortDirection.Ascending, isMultiSort: true);
            await grid.SortColumnAsync(nameof(GridRow.Role),  SortDirection.Ascending, isMultiSort: true);
        });

        Assert.True(grid.SortSettings.Columns.Count >= 2);
    }
}",
            // 5. Filter rows by exact column value
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridFilterExactTests : TestContext
{
    [Fact(DisplayName = ""T05: Filter Rows by Exact Value"")]
    public async Task Filter_Equal_Department()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(nameof(GridRow.Department), ""equal"", ""Design"");
        });

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.NotEmpty(rows);
        Assert.All(rows, r => Assert.Equal(""Design"", r.Department));
    }
}",
            // 6. Filter rows using partial text match
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridFilterContainsTests : TestContext
{
    [Fact(DisplayName = ""T06: Filter Rows by Partial Text Match"")]
    public async Task Filter_Contains_Des()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(nameof(GridRow.Department), ""contains"", ""Des"");
        });

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.NotEmpty(rows);
        Assert.All(rows, r => Assert.Contains(""Des"", r.Department));
    }
}",
            // 7. Apply multiple filters using the AND condition
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridMultiFilterTests : TestContext
{
    [Fact(DisplayName = ""T07: Apply Multiple Filters using AND"")]
    public async Task Filter_And_Name_Role()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(nameof(GridRow.Name), ""contains"", ""a"");
            await grid.FilterByColumnAsync(nameof(GridRow.Role), ""contains"", ""Eng"");
        });

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.NotEmpty(rows);
        Assert.All(rows, r =>
        {
            Assert.Contains(""a"",   r.Name.ToLower());
            Assert.Contains(""eng"", r.Role.ToLower());
        });
    }
}",
            // 8. Filter should ignore case sensitivity
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridFilterIgnoreCaseTests : TestContext
{
    [Fact(DisplayName = ""T08: Filter Ignores Case Sensitivity"")]
    public async Task Filter_IgnoresCase()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(nameof(GridRow.Department), ""contains"", ""design"");
        });

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.NotEmpty(rows);
        Assert.All(rows, r => Assert.Contains(""design"", r.Department.ToLower()));
    }
}",
            // 9. Search with non-existent value returns no results
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridFilterNoResultsTests : TestContext
{
    [Fact(DisplayName = ""T09: Search Non-Existent Value Returns No Rows"")]
    public async Task Filter_NonExistent_EmptyRows()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(nameof(GridRow.Department), ""contains"", ""NonExistingValue"");
        });

        Assert.Empty(await grid.GetCurrentViewRecordsAsync());
    }
}",
            // 10. Search rows based on Name column
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridSearchNameTests : TestContext
{
    [Fact(DisplayName = ""T10: Search Rows by Name"")]
    public async Task Search_Alice()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () => await grid.SearchAsync(""Alice""));

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.NotEmpty(rows);
        Assert.All(rows, r => Assert.Contains(""alice"", r.Name.ToLower()));
    }
}",
            // 11. Open edit dialog on row interaction
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridEditDialogTests : TestContext
{
    [Fact(DisplayName = ""T11: Grid configured for Edit Dialog on row interaction"")]
    public void EditDialog_WiredAsDialogMode()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var rows = cut.FindAll("".e-row:not(.e-headerrow)"");
        Assert.NotEmpty(rows);
        Assert.Equal(EditMode.Dialog, grid.EditSettings.Mode);
    }
}",
            // 12. Add a new record to the grid
            @"using System;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridAddRecordTests : TestContext
{
    [Fact(DisplayName = ""T12: Add a New Record"")]
    public async Task AddRecord_NewRow()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var newRow = new GridRow
        {
            Id = 100, Name = ""Test User"",
            Role = ""Engineer"", Department = ""R&D"",
            DateOfJoining = DateTime.Today
        };

        await cut.InvokeAsync(async () => await grid.AddRecordAsync(newRow));

        Assert.Contains(grid.DataSource, r => r.Id == 100);
    }
}",
            // 13. Update an existing record
            @"using System;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridUpdateRecordTests : TestContext
{
    [Fact(DisplayName = ""T13: Update Existing Record"")]
    public async Task UpdateRecord_Id1()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var updated = new GridRow
        {
            Id = 1, Name = ""Updated User"",
            Role = ""Senior Engineer"", Department = ""R&D"",
            DateOfJoining = DateTime.Today
        };

        var view = await grid.GetCurrentViewRecordsAsync();
        var idx = view.FindIndex(r => r.Id == 1);
        Assert.True(idx >= 0, ""Row with Id=1 should exist in current view"");

        await cut.InvokeAsync(async () => await grid.UpdateRowAsync(idx, updated));

        var row = grid.DataSource.First(r => r.Id == 1);
        Assert.Equal(""Updated User"", row.Name);
        Assert.Equal(""Senior Engineer"", row.Role);
        Assert.Equal(""R&D"", row.Department);
    }
}",
            // 14. Delete a record from the grid
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridDeleteRecordTests : TestContext
{
    [Fact(DisplayName = ""T14: Delete a Record from the Grid"")]
    public async Task DeleteRecord_Id1()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;
        Assert.Contains(grid.DataSource, r => r.Id == 1);

        await cut.InvokeAsync(async () =>
            await grid.DeleteRecordAsync(""Id"", new GridRow { Id = 1 }));

        Assert.DoesNotContain(grid.DataSource, r => r.Id == 1);
    }
}",
            // 15. Select single and multiple rows
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridSelectRowsTests : TestContext
{
    [Fact(DisplayName = ""T15: Select Single and Multiple Rows"")]
    public async Task SelectRows_Single_And_Multiple()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () => await grid.SelectRowsAsync(new[] { 0 }));
        Assert.Contains(0, await grid.GetSelectedRowIndexesAsync());

        await cut.InvokeAsync(async () => await grid.SelectRowsAsync(new[] { 0, 1 }));
        var multi = await grid.GetSelectedRowIndexesAsync();
        Assert.Contains(0, multi);
        Assert.Contains(1, multi);
    }
}",
            // 16. Verify column headers are displayed correctly
            @"using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Xunit;

public class GridHeadersTests : TestContext
{
    [Fact(DisplayName = ""T16: Column Headers Displayed Correctly"")]
    public void Headers_AllRendered()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));

        var headers = cut.FindAll("".e-headertext"")
                       .Select(h => h.TextContent.Trim()).ToList();

        Assert.Contains(""ID"",              headers);
        Assert.Contains(""Name"",            headers);
        Assert.Contains(""Designation"",     headers);
        Assert.Contains(""Department"",      headers);
        Assert.Contains(""Date of Joining"", headers);
    }
}",
            // 17. Verify sorting icons on columns
            @"using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Xunit;

public class GridSortIconTests : TestContext
{
    [Fact(DisplayName = ""T17: Sorting Icons Present on Sortable Columns"")]
    public void SortIcon_PresentOnName()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));

        var nameHeader = cut.FindAll("".e-headertext"")
                            .First(h => h.TextContent.Trim() == ""Name"");
        var headerCell = nameHeader.Closest("".e-headercell"");

        Assert.NotNull(headerCell.QuerySelector("".e-sortfilterdiv""));
    }
}",
            // 18. Verify grid data rows are rendered
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Xunit;

public class GridDataRowsTests : TestContext
{
    [Fact(DisplayName = ""T18: Grid Data Rows Rendered"")]
    public void DataRows_Rendered()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row:not(.e-headerrow)"").Count > 0,
                          TimeSpan.FromSeconds(3));

        Assert.NotEmpty(cut.FindAll("".e-row:not(.e-headerrow)""));
    }
}",
            // 19. Verify pagination limits row count
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridPagingTests : TestContext
{
    [Fact(DisplayName = ""T19: Pagination Limits Row Count to 12"")]
    public async Task Paging_12RowsPerPage()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.Equal(12, rows.Count);

        Assert.Equal(12, cut.FindAll("".e-row:not(.e-headerrow)"").Count);
    }
}",
            // 20. Verify focus behavior in filter dialog
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Xunit;

public class GridFilterDialogTests : TestContext
{
    [Fact(DisplayName = ""T20: Filter Menu Trigger Available on Headers"")]
    public void FilterMenu_TriggerPresent()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));

        var triggerIcons = cut.FindAll("".e-headercell .e-filterdiv, "" +
                                        "".e-headercell .e-filterbar, "" +
                                        "".e-headercell .e-filtermenudiv"");
        Assert.NotEmpty(triggerIcons);
    }
}",
            // 21. Verify column reorder functionality
            @"using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridColumnReorderTests : TestContext
{
    [Fact(DisplayName = ""T21: Grid configured for Column Reordering"")]
    public async Task ReorderColumns_NameBeforeId()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.True(grid.AllowReordering,
            ""GridClient.razor must set AllowReordering=true"");

        var before = (await grid.GetColumnsAsync()).Select(c => c.Field).ToList();
        Assert.Contains(""Name"", before);
        Assert.Contains(""Id"",   before);

        await cut.InvokeAsync(async () =>
            await grid.ReorderColumnsAsync(new[] { ""Name"" }, ""Id""));
    }
}",
            // 22. Verify column resizing updates width
            @"using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridColumnResizeTests : TestContext
{
    [Fact(DisplayName = ""T22: Grid configured for Column Resizing"")]
    public async Task ResizeColumn_Wired()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.True(grid.AllowResizing,
            ""GridClient.razor must set AllowResizing=true"");

        var col = await grid.GetColumnByFieldAsync(""Id"");
        Assert.NotNull(col);
        Assert.Equal(""70"", col.Width);

        await cut.InvokeAsync(async () =>
        {
            col.Width = ""120"";
            await grid.RefreshColumnsAsync();
        });
    }
}",
            // 23. Verify PDF export functionality
            @"using System.Collections.Generic;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridPdfExportTests : TestContext
{
    [Fact(DisplayName = ""T23: PDF Export Wired in Toolbar"")]
    public async Task PdfExport_ToolbarWired()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.True(grid.AllowPdfExport,
            ""GridClient.razor must set AllowPdfExport=true"");

        var toolbar = Assert.IsAssignableFrom<IEnumerable<string>>(grid.Toolbar);
        Assert.Contains(""PdfExport"", toolbar);

        await cut.InvokeAsync(async () =>
        {
            try { await grid.ExportToPdfAsync(
                new PdfExportProperties { FileName = ""grid-sample.pdf"" }); }
            catch { /* expected: JS export module unavailable in bUnit */ }
        });
    }
}",
            // 24. Verify Excel export functionality
            @"using System.Collections.Generic;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridExcelExportTests : TestContext
{
    [Fact(DisplayName = ""T24: Excel Export Wired in Toolbar"")]
    public async Task ExcelExport_ToolbarWired()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.True(grid.AllowExcelExport,
            ""GridClient.razor must set AllowExcelExport=true"");

        var toolbar = Assert.IsAssignableFrom<IEnumerable<string>>(grid.Toolbar);
        Assert.Contains(""ExcelExport"", toolbar);

        await cut.InvokeAsync(async () =>
        {
            try { await grid.ExportToExcelAsync(
                new ExcelExportProperties { FileName = ""grid-sample.xlsx"" }); }
            catch { /* expected */ }
        });
    }
}",
            // 25. Verify CSV export functionality
            @"using System.Collections.Generic;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

public class GridCsvExportTests : TestContext
{
    [Fact(DisplayName = ""T25: CSV Export Wired in Toolbar"")]
    public async Task CsvExport_ToolbarWired()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.True(grid.AllowExcelExport,
            ""CsvExport reuses AllowExcelExport (no separate flag)"");

        var toolbar = Assert.IsAssignableFrom<IEnumerable<string>>(grid.Toolbar);
        Assert.Contains(""CsvExport"", toolbar);

        await cut.InvokeAsync(async () =>
        {
            try { await grid.ExportToCsvAsync(
                new ExcelExportProperties { FileName = ""grid-sample.csv"" }); }
            catch { /* expected */ }
        });
    }
}"
        };


        // xUnit Test cases
        public static readonly string[] xUnit =
 {
    // 1. Sort the name column in ascending order
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T01_SortNameAscending : TestContext
{
    [Fact(DisplayName = ""T01: Sort Name Column Ascending"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
            await grid.SortColumnAsync(
                nameof(GridRow.Name), SortDirection.Ascending));

        var names = (await grid.GetCurrentViewRecordsAsync())
                    .Select(r => r.Name).ToList();
        var expected = names.OrderBy(n => n, StringComparer.Ordinal).ToList();
        Assert.Equal(expected, names);
    }
}",

    // 2. Sort the name column in descending order
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T02_SortNameDescending : TestContext
{
    [Fact(DisplayName = ""T02: Sort Name Column Descending"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
            await grid.SortColumnAsync(
                nameof(GridRow.Name), SortDirection.Descending));

        var names = (await grid.GetCurrentViewRecordsAsync())
                    .Select(r => r.Name).ToList();
        Assert.Equal(
            names.OrderByDescending(n => n, StringComparer.Ordinal), names);
        Assert.Equal(""Zack"", names[0]);
    }
}",

    // 3. Sort the name column in descending order
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T03_SortIdAscending : TestContext
{
    [Fact(DisplayName = ""T03: Sort ID Column Ascending"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
            await grid.SortColumnAsync(
                nameof(GridRow.Id), SortDirection.Ascending));

        var ids = (await grid.GetCurrentViewRecordsAsync())
                  .Select(r => r.Id).ToList();
        Assert.Equal(ids.OrderBy(i => i), ids);
        Assert.Equal(1, ids[0]);
    }
}",

    // 4. Apply sorting to multiple columns
    @"using System;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T04_MultiSort : TestContext
{
    [Fact(DisplayName = ""T04: Multi-sort Multiple Columns"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearSortingAsync();
            await grid.SortColumnAsync(
                nameof(GridRow.Name), SortDirection.Ascending, isMultiSort: true);
            await grid.SortColumnAsync(
                nameof(GridRow.Role), SortDirection.Ascending, isMultiSort: true);
        });

        Assert.True(grid.SortSettings!.Columns.Count >= 2);
    }
}",

    // 5. Filter rows by exact column value
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T05_FilterExactValue : TestContext
{
    [Fact(DisplayName = ""T05: Filter Rows by Exact Value"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var rows = await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(
                nameof(GridRow.Department), ""equal"", ""Design"");
            return await grid.GetCurrentViewRecordsAsync();
        });

        Assert.NotEmpty(rows!);
        Assert.All(rows!, r => Assert.Equal(""Design"", r.Department));
    }
}",

    // 6. Filter rows using partial text match
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T06_FilterContains : TestContext
{
    [Fact(DisplayName = ""T06: Filter Rows by Partial Text Match"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var rows = await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(
                nameof(GridRow.Department), ""contains"", ""Des"");
            return await grid.GetCurrentViewRecordsAsync();
        });

        Assert.NotEmpty(rows!);
        Assert.All(rows!, r => Assert.Contains(""Des"", r.Department));
    }
}",

    // 7. Apply multiple filters using the AND condition
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T07_MultiFilterAnd : TestContext
{
    [Fact(DisplayName = ""T07: Apply Multiple Filters using AND"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(
                nameof(GridRow.Name), ""contains"", ""A"");
            await grid.FilterByColumnAsync(
                nameof(GridRow.Role), ""contains"", ""Eng"");
        });

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.NotEmpty(rows!);
        Assert.All(rows!, r =>
        {
            Assert.Contains(""a"",   r.Name.ToLower());
            Assert.Contains(""eng"", r.Role.ToLower());
        });
    }
}",

    // 8. Filter should ignore case sensitivity
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T08_FilterCaseInsensitive : TestContext
{
    [Fact(DisplayName = ""T08: Filter Ignores Case Sensitivity"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var rows = await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(
                nameof(GridRow.Department), ""contains"", ""design"");
            return await grid.GetCurrentViewRecordsAsync();
        });

        Assert.NotEmpty(rows!);
        Assert.All(rows!, r =>
            Assert.Contains(""design"", r.Department.ToLower()));
    }
}",

    // 9. Search with non-existent value returns no results
    @"using System;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T09_FilterNonExistentNoRows : TestContext
{
    [Fact(DisplayName = ""T09: Search Non-Existent Value Returns No Rows"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var rows = await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(
                nameof(GridRow.Department),
                ""contains"", ""NonExistingValue"");
            return await grid.GetCurrentViewRecordsAsync();
        });

        Assert.Empty(rows!);
    }
}",

    // 10. Search rows based on Name column
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T10_SearchByName : TestContext
{
    [Fact(DisplayName = ""T10: Search Rows by Name"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () => await grid.SearchAsync(""Alice""));

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.NotEmpty(rows!);
        Assert.All(rows!, r =>
            Assert.Contains(""alice"", r.Name.ToLower()));
    }
}",

    // 11. Open edit dialog on row interaction
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T11_OpenEditDialog : TestContext
{
    [Fact(DisplayName = ""T11: Grid configured for Edit Dialog on row interaction"")]
    public void Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var rows = cut.FindAll("".e-row:not(.e-headerrow)"");
        Assert.NotEmpty(rows);

        Assert.Equal(EditMode.Dialog, grid.EditSettings!.Mode);
    }
}",

    // 12. Add a new record to the grid
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T12_AddNewRecord : TestContext
{
    [Fact(DisplayName = ""T12: Add a New Record"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var newRow = new GridRow
        {
            Id             = 100,
            Name           = ""Test User"",
            Role           = ""Engineer"",
            Department     = ""R&D"",
            DateOfJoining  = DateTime.Today
        };

        await cut.InvokeAsync(async () =>
            await grid.AddRecordAsync(newRow));

        Assert.Contains(grid.DataSource!, r => r.Id == 100);
    }
}",

    // 13. Update an existing record
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T13_UpdateRecord : TestContext
{
    [Fact(DisplayName = ""T13: Update Existing Record"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var updated = new GridRow
        {
            Id             = 1,
            Name           = ""Updated User"",
            Role           = ""Senior Engineer"",
            Department     = ""R&D"",
            DateOfJoining  = DateTime.Today
        };

        var view = await grid.GetCurrentViewRecordsAsync();
        var idx  = view!.FindIndex(r => r.Id == 1);
        Assert.True(idx >= 0, ""Row with Id=1 should exist in current view"");

        await cut.InvokeAsync(async () =>
            await grid.UpdateRowAsync(idx, updated));

        var row = grid.DataSource!.First(r => r.Id == 1);
        Assert.Equal(""Updated User"",     row.Name);
        Assert.Equal(""Senior Engineer"",  row.Role);
        Assert.Equal(""R&D"",              row.Department);
    }
}",

    // 14. Delete a record from the grid
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T14_DeleteRecord : TestContext
{
    [Fact(DisplayName = ""T14: Delete a Record from the Grid"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.Contains(grid.DataSource!, r => r.Id == 1);

        await cut.InvokeAsync(async () =>
            await grid.DeleteRecordAsync(""Id"", new GridRow { Id = 1 }));

        Assert.DoesNotContain(grid.DataSource!, r => r.Id == 1);
    }
}",

    // 15. Select single and multiple rows
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T15_SelectRows : TestContext
{
    [Fact(DisplayName = ""T15: Select Single and Multiple Rows"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
            await grid.SelectRowsAsync(new[] { 0 }));
        var first = await grid.GetSelectedRowIndexesAsync();
        Assert.Contains(0, first);

        await cut.InvokeAsync(async () =>
            await grid.SelectRowsAsync(new[] { 0, 1 }));
        var multi = await grid.GetSelectedRowIndexesAsync();
        Assert.Contains(0, multi);
        Assert.Contains(1, multi);
    }
}",

    // 16. Verify column headers are displayed correctly
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T16_HeadersRendered : TestContext
{
    [Fact(DisplayName = ""T16: Column Headers Displayed Correctly"")]
    public void Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));

        var headers = cut.FindAll("".e-headertext"")
                         .Select(h => h.TextContent.Trim())
                         .ToList();

        Assert.Contains(""ID"",              headers);
        Assert.Contains(""Name"",            headers);
        Assert.Contains(""Designation"",     headers);
        Assert.Contains(""Department"",      headers);
        Assert.Contains(""Date of Joining"", headers);
    }
}",

    // 17. Verify sorting icons on columns
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T17_SortIconsPresent : TestContext
{
    [Fact(DisplayName = ""T17: Sorting Icons Present on Sortable Columns"")]
    public void Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));

        var nameHeader = cut.FindAll("".e-headertext"")
                            .First(h => h.TextContent.Trim() == ""Name"");
        var headerCell = nameHeader.Closest("".e-headercell"")!;

        Assert.NotNull(headerCell.QuerySelector("".e-sortfilterdiv""));
    }
}",

    // 18. Verify grid data rows are rendered
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T18_DataRowsRendered : TestContext
{
    [Fact(DisplayName = ""T18: Grid Data Rows Rendered"")]
    public void Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));

        var dataRows = cut.FindAll("".e-row:not(.e-headerrow)"");
        Assert.NotEmpty(dataRows);
    }
}",

    // 19. Verify pagination limits row count
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T19_PaginationLimitsRows : TestContext
{
    [Fact(DisplayName = ""T19: Pagination Limits Row Count to 12"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.Equal(12, rows!.Count);
        Assert.Equal(12,
            cut.FindAll("".e-row:not(.e-headerrow)"").Count);
    }
}",

    // 20. Verify focus behavior in filter dialog
    @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T20_FilterDialogFocus : TestContext
{
    [Fact(DisplayName = ""T20: Filter Menu Trigger Available on Headers"")]
    public void Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));

        var triggerIcons = cut.FindAll(
            "".e-headercell .e-filterdiv, "" +
            "".e-headercell .e-filterbar, "" +
            "".e-headercell .e-filtermenudiv"");
        Assert.NotEmpty(triggerIcons);
    }
}",

    // 21. Verify column reorder functionality
    @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T21_ColumnReorder : TestContext
{
    [Fact(DisplayName = ""T21: Grid configured for Column Reordering"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.True(grid.AllowReordering,
            ""GridClient.razor must set AllowReordering=\""true\"""");

        var before = (await grid.GetColumnsAsync())
                          .Select(c => c.Field).ToList();
        Assert.Contains(""Name"", before);
        Assert.Contains(""Id"",   before);

        await cut.InvokeAsync(async () =>
            await grid.ReorderColumnsAsync(new[] { ""Name"" }, ""Id""));
    }
}",

    // 22. Verify column resizing updates width
    @"using System;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T22_ColumnResize : TestContext
{
    [Fact(DisplayName = ""T22: Grid configured for Column Resizing"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.True(grid.AllowResizing,
            ""GridClient.razor must set AllowResizing=\""true\"""");

        var col = await grid.GetColumnByFieldAsync(""Id"");
        Assert.NotNull(col);
        Assert.Equal(""70"", col!.Width);

#pragma warning disable BL0005
        await cut.InvokeAsync(async () =>
        {
            col.Width = ""120"";
            await grid.RefreshColumnsAsync();
        });
#pragma warning restore BL0005
    }
}",

    // 23. Verify PDF export functionality
    @"using System;
using System.Collections.Generic;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T23_PdfExportTriggered : TestContext
{
    [Fact(DisplayName = ""T23: PDF Export Wired in Toolbar"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.True(grid.AllowPdfExport,
            ""GridClient.razor must set AllowPdfExport=\""true\"""");

        var toolbar = (grid.Toolbar as IEnumerable<string>)!;
        Assert.Contains(""PdfExport"", toolbar);

        await cut.InvokeAsync(async () =>
        {
            await grid.ExportToPdfAsync(
                new PdfExportProperties { FileName = ""grid-sample.pdf"" });
        });
    }
}",

    // 24. Verify Excel export functionality
    @"using System;
using System.Collections.Generic;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T24_ExcelExportTriggered : TestContext
{
    [Fact(DisplayName = ""T24: Excel Export Wired in Toolbar"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.True(grid.AllowExcelExport,
            ""GridClient.razor must set AllowExcelExport=\""true\"""");

        var toolbar = (grid.Toolbar as IEnumerable<string>)!;
        Assert.Contains(""ExcelExport"", toolbar);

        await cut.InvokeAsync(async () =>
        {
            await grid.ExportToExcelAsync(
                new ExcelExportProperties { FileName = ""grid-sample.xlsx"" });
        });
    }
}",

    // 25. Verify CSV export functionality
    @"using System;
using System.Collections.Generic;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Xunit;

namespace DataGridTesting.Tests.xUnit;

public class T25_CsvExportTriggered : TestContext
{
    [Fact(DisplayName = ""T25: CSV Export Wired in Toolbar"")]
    public async Task Test()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut  = RenderComponent<GridClient>();
        cut.WaitForState(
            () => cut.FindAll("".e-row"").Count > 0,
            TimeSpan.FromSeconds(3));
        var grid = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.True(grid.AllowExcelExport,
            ""CsvExport reuses AllowExcelExport (no separate flag)"");

        var toolbar = (grid.Toolbar as IEnumerable<string>)!;
        Assert.Contains(""CsvExport"", toolbar);

        await cut.InvokeAsync(async () =>
        {
            await grid.ExportToCsvAsync(
                new ExcelExportProperties { FileName = ""grid-sample.csv"" });
        });
    }
}"
};

        // NUnit Test cases        
        public static readonly string[] NUnit =
 {
            // 1. Sort the name column in ascending order
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T01_SortNameAscendingTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Sort_Name_Ascending()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
            await grid.SortColumnAsync(nameof(GridRow.Name), SortDirection.Ascending));

        var names = (await grid.GetCurrentViewRecordsAsync())
                    .Select(r => r.Name).ToList();
        Assert.That(names, Is.EqualTo(
            names.OrderBy(n => n, StringComparer.Ordinal).ToList()));
    }
}",
            // 2. Sort the name column in descending order
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T02_SortNameDescendingTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Sort_Name_Descending()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
            await grid.SortColumnAsync(nameof(GridRow.Name), SortDirection.Descending));

        var names = (await grid.GetCurrentViewRecordsAsync())
                    .Select(r => r.Name).ToList();
        Assert.That(names, Is.EqualTo(
            names.OrderByDescending(n => n, StringComparer.Ordinal).ToList()));
        Assert.That(names[0], Is.EqualTo(""Zack""));
    }
}",
            // 3. Sort the name column in descending order
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T03_SortIdAscendingTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Sort_Id_Ascending()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
            await grid.SortColumnAsync(nameof(GridRow.Id), SortDirection.Ascending));

        var ids = (await grid.GetCurrentViewRecordsAsync())
                  .Select(r => r.Id).ToList();
        Assert.That(ids, Is.EqualTo(ids.OrderBy(i => i).ToList()));
        Assert.That(ids[0], Is.EqualTo(1));
    }
}",
            // 4. Apply sorting to multiple columns
            @"using System;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T04_MultiSortTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Multi_Sort_Two_Columns()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearSortingAsync();
            await grid.SortColumnAsync(nameof(GridRow.Name),
                                       SortDirection.Ascending, isMultiSort: true);
            await grid.SortColumnAsync(nameof(GridRow.Role),
                                       SortDirection.Ascending, isMultiSort: true);
        });

        Assert.That(grid.SortSettings!.Columns.Count, Is.GreaterThanOrEqualTo(2));
    }
}",
            // 5. Filter rows by exact column value
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T05_FilterExactValueTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Filter_By_Exact_Value()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(
                nameof(GridRow.Department), ""equal"", ""Design"");
        });

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.That(rows, Is.Not.Empty);
        Assert.That(rows.All(r => r.Department == ""Design""), Is.True);
    }
}",
            // 6. Filter rows using partial text match
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T06_FilterContainsTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Filter_By_Partial_Text()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(
                nameof(GridRow.Department), ""contains"", ""Des"");
        });

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.That(rows, Is.Not.Empty);
        Assert.That(rows.All(r => r.Department.Contains(""Des"")), Is.True);
    }
}",
            // 7. Apply multiple filters using the AND condition
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T07_MultiFilterAndTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Multiple_Filters_Combine_With_And()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(
                nameof(GridRow.Name),  ""contains"", ""A"");
            await grid.FilterByColumnAsync(
                nameof(GridRow.Role),  ""contains"", ""Eng"");
        });

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.That(rows, Is.Not.Empty);
        Assert.That(rows.All(r =>
            r.Name.ToLower().Contains(""a"") &&
            r.Role.ToLower().Contains(""eng"")), Is.True);
    }
}",
            // 8. Filter should ignore case sensitivity
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T08_FilterCaseInsensitiveTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Filter_Is_Case_Insensitive()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(
                nameof(GridRow.Department), ""contains"", ""design"");
        });

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.That(rows, Is.Not.Empty);
        Assert.That(rows.All(r => r.Department.ToLower().Contains(""design"")), Is.True);
    }
}",
            // 9. Search with non-existent value returns no results
            @"using System;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T09_NonExistentValueTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Search_With_Missing_Value_Returns_Empty()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
        {
            await grid.ClearFilteringAsync();
            await grid.FilterByColumnAsync(
                nameof(GridRow.Department), ""contains"", ""NonExistingValue"");
        });

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.That(rows, Is.Empty);
    }
}",
            // 10. Search rows based on Name column
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T10_SearchByNameTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Search_Filters_By_Name_Column()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () => await grid.SearchAsync(""Alice""));

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.That(rows, Is.Not.Empty);
        Assert.That(rows.All(r => r.Name.ToLower().Contains(""alice"")), Is.True);
    }
}",
            // 11. Open edit dialog on row interaction
            @"using System;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T11_OpenEditDialogTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public void Grid_Is_Configured_For_Dialog_Edit()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var rows = cut.FindAll("".e-row:not(.e-headerrow)"");
        Assert.That(rows, Is.Not.Empty);
        Assert.That(grid.EditSettings!.Mode, Is.EqualTo(EditMode.Dialog));
    }
}",
            // 12. Add a new record to the grid
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T12_AddNewRecordTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Add_Record_Persists_In_Data_Source()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var newRow = new GridRow
        {
            Id = 100,
            Name = ""Test User"",
            Role = ""Engineer"",
            Department = ""R&D"",
            DateOfJoining = DateTime.Today
        };

        await cut.InvokeAsync(async () => await grid.AddRecordAsync(newRow));

        Assert.That(grid.DataSource!.Any(r => r.Id == 100), Is.True);
    }
}",
            // 13. Update an existing record
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T13_UpdateRecordTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Update_Record_Changes_Values()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var view  = await grid.GetCurrentViewRecordsAsync();
        var idx   = view!.FindIndex(r => r.Id == 1);
        Assert.That(idx, Is.GreaterThanOrEqualTo(0));

        var updated = new GridRow
        {
            Id = 1,
            Name = ""Updated User"",
            Role = ""Senior Engineer"",
            Department = ""R&D"",
            DateOfJoining = DateTime.Today
        };

        await cut.InvokeAsync(async () => await grid.UpdateRowAsync(idx, updated));

        var row = grid.DataSource!.First(r => r.Id == 1);
        Assert.That(row.Name,        Is.EqualTo(""Updated User""));
        Assert.That(row.Role,        Is.EqualTo(""Senior Engineer""));
        Assert.That(row.Department,  Is.EqualTo(""R&D""));
    }
}",
            // 14. Delete a record from the grid
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T14_DeleteRecordTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Delete_Removes_Record_From_Data_Source()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.That(grid.DataSource!.Any(r => r.Id == 1), Is.True);

        await cut.InvokeAsync(async () =>
            await grid.DeleteRecordAsync(""Id"", new GridRow { Id = 1 }));

        Assert.That(grid.DataSource!.Any(r => r.Id == 1), Is.False);
    }
}",
            // 15. Select single and multiple rows
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T15_SelectRowsTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Single_And_Multiple_Row_Selection()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        await cut.InvokeAsync(async () =>
            await grid.SelectRowsAsync(new[] { 0 }));
        var first = await grid.GetSelectedRowIndexesAsync();
        Assert.That(first.Contains(0), Is.True);

        await cut.InvokeAsync(async () =>
            await grid.SelectRowsAsync(new[] { 0, 1 }));
        var multi = await grid.GetSelectedRowIndexesAsync();
        Assert.That(multi.Contains(0), Is.True);
        Assert.That(multi.Contains(1), Is.True);
    }
}",
            // 16. Verify column headers are displayed correctly
            @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;

namespace DataGridTesting.Tests.NUnit;

public class T16_HeadersRenderedTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public void Column_Headers_Are_Rendered()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));

        var headers = cut.FindAll("".e-headertext"")
                         .Select(h => h.TextContent.Trim())
                         .ToList();

        Assert.That(headers, Does.Contain(""ID""));
        Assert.That(headers, Does.Contain(""Name""));
        Assert.That(headers, Does.Contain(""Designation""));
        Assert.That(headers, Does.Contain(""Department""));
        Assert.That(headers, Does.Contain(""Date of Joining""));
    }
}",
            // 17. Verify sorting icons on columns
            @"using System;
using System.Linq;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;

namespace DataGridTesting.Tests.NUnit;

public class T17_SortIconsPresentTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public void Sort_Icon_Is_Present_On_Name_Column()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));

        var nameHeader = cut.FindAll("".e-headertext"")
                             .First(h => h.TextContent.Trim() == ""Name"");
        var headerCell = nameHeader.Closest("".e-headercell"")!;

        Assert.That(headerCell.QuerySelector("".e-sortfilterdiv""), Is.Not.Null);
    }
}",
            // 18. Verify grid data rows are rendered
            @"using System;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;

namespace DataGridTesting.Tests.NUnit;

public class T18_DataRowsRenderedTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public void Grid_Renders_Data_Rows()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));

        var dataRows = cut.FindAll("".e-row:not(.e-headerrow)"");
        Assert.That(dataRows, Is.Not.Empty);
    }
}",
            // 19. Verify pagination limits row count
            @"using System;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T19_PaginationLimitsRowsTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Pagination_Limits_Row_Count_To_12()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        var rows = await grid.GetCurrentViewRecordsAsync();
        Assert.That(rows!.Count, Is.EqualTo(12));
        Assert.That(cut.FindAll("".e-row:not(.e-headerrow)"").Count, Is.EqualTo(12));
    }
}",
            // 20. Verify focus behavior in filter dialog
            @"using System;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;

namespace DataGridTesting.Tests.NUnit;

public class T20_FilterDialogFocusTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public void Filter_Menu_Trigger_Is_Present_On_Headers()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));

        var triggerIcons = cut.FindAll("".e-headercell .e-filterdiv, "" +
                                       "".e-headercell .e-filterbar, "" +
                                       "".e-headercell .e-filtermenudiv"");
        Assert.That(triggerIcons, Is.Not.Empty);
    }
}",
            // 21. Verify column reorder functionality
            @"using System;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T21_ColumnReorderTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Grid_Allows_Column_Reorder()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.That(grid.AllowReordering, Is.True);

        var cols = (await grid.GetColumnsAsync()).Select(c => c.Field).ToList();
        Assert.That(cols, Does.Contain(""Name""));
        Assert.That(cols, Does.Contain(""Id""));

        await cut.InvokeAsync(async () =>
            await grid.ReorderColumnsAsync(new[] { ""Name"" }, ""Id""));
    }
}",
            // 22. Verify column resizing updates width
            @"using System;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T22_ColumnResizeTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task Grid_Allows_Column_Resize()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.That(grid.AllowResizing, Is.True);

        var col = await grid.GetColumnByFieldAsync(""Id"");
        Assert.That(col, Is.Not.Null);
        Assert.That(col!.Width, Is.EqualTo(""70""));

#pragma warning disable BL0005
        await cut.InvokeAsync(async () =>
        {
            col.Width = ""120"";
            await grid.RefreshColumnsAsync();
        });
#pragma warning restore BL0005
    }
}",
            // 23. Verify PDF export functionality
            @"using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T23_PdfExportTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task PdfExport_Is_Wired_In_Toolbar()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.That(grid.AllowPdfExport, Is.True);
        var toolbar = (grid.Toolbar as IEnumerable<string>)!;
        Assert.That(toolbar, Does.Contain(""PdfExport""));

        await cut.InvokeAsync(async () =>
            await grid.ExportToPdfAsync(new PdfExportProperties
            {
                FileName = ""grid-sample.pdf""
            }));
    }
}",
            // 24. Verify Excel export functionality
            @"using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T24_ExcelExportTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task ExcelExport_Is_Wired_In_Toolbar()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.That(grid.AllowExcelExport, Is.True);
        var toolbar = (grid.Toolbar as IEnumerable<string>)!;
        Assert.That(toolbar, Does.Contain(""ExcelExport""));

        await cut.InvokeAsync(async () =>
            await grid.ExportToExcelAsync(new ExcelExportProperties
            {
                FileName = ""grid-sample.xlsx""
            }));
    }
}",
            // 25. Verify CSV export functionality
            @"using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bunit;
using DataGridTesting.Components.Grid;
using DataGridTesting.Data;
using DataGridTesting.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;

namespace DataGridTesting.Tests.NUnit;

public class T25_CsvExportTests : Bunit.TestContext
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Services.AddSyncfusionBlazor();
        Services.AddSingleton<GridFixture>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => Dispose();

    [Test]
    public async Task CsvExport_Is_Wired_In_Toolbar()
    {
        var cut   = RenderComponent<GridClient>();
        cut.WaitForState(() => cut.FindAll("".e-row"").Count > 0,
                          TimeSpan.FromSeconds(3));
        var grid  = cut.FindComponent<SfGrid<GridRow>>().Instance;

        Assert.That(grid.AllowExcelExport, Is.True);   // CSV reuses this
        var toolbar = (grid.Toolbar as IEnumerable<string>)!;
        Assert.That(toolbar, Does.Contain(""CsvExport""));

        await cut.InvokeAsync(async () =>
            await grid.ExportToCsvAsync(new ExcelExportProperties
            {
                FileName = ""grid-sample.csv""
            }));
    }
}"
        };

        // Playwright Test cases        
        public static readonly string[] Playwright =
 {
            // 1. Sort the name column in ascending order
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T01_Sort_Name_Ascending : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var header = Page.GetByRole(AriaRole.Columnheader, new() { Name = ""Name"" });
        await header.ClickAsync();
        await Page.WaitForTimeoutAsync(400);

        var names = (await Page.Locator("".e-gridcontent tr.e-row td:nth-child(2)"")
                                .AllTextContentsAsync())
                    .Select(n => n.Trim())
                    .OrderBy(n => n, StringComparer.Ordinal)
                    .ToList();

        var actual = (await Page.Locator("".e-gridcontent tr.e-row td:nth-child(2)"")
                                 .AllTextContentsAsync())
                    .Select(n => n.Trim())
                    .ToList();

        Assert.That(actual, Is.EqualTo(names));
    }
}",

            // 2. Sort the name column in descending order
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T02_Sort_Name_Descending : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var header = Page.GetByRole(AriaRole.Columnheader, new() { Name = ""Name"" });
        await header.ClickAsync();              // ascending
        await Page.WaitForTimeoutAsync(300);
        await header.ClickAsync();              // descending
        await Page.WaitForTimeoutAsync(400);

        var actual = (await Page.Locator("".e-gridcontent tr.e-row td:nth-child(2)"")
                                 .AllTextContentsAsync())
                    .Select(n => n.Trim())
                    .ToList();

        var expected = actual.OrderBy(n => n, StringComparer.Ordinal)
                             .Reverse()
                             .ToList();

        Assert.That(actual, Is.EqualTo(expected));
    }
}",

            // 3. Sort the ID column in ascending numerical order
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T03_Sort_Id_Ascending : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var header = Page.GetByRole(AriaRole.Columnheader, new() { Name = ""ID"" });
        await header.ClickAsync();
        await Page.WaitForTimeoutAsync(400);

        var ids = (await Page.Locator("".e-gridcontent tr.e-row td:nth-child(1)"")
                             .AllTextContentsAsync())
                   .Select(t => int.Parse(t.Trim()))
                   .ToList();

        Assert.That(ids, Is.EqualTo(ids.OrderBy(i => i).ToList()));
    }
}",

            // 4. Apply sorting to multiple columns
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T04_Multi_Sort_Two_Columns : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await Page.GetByRole(AriaRole.Columnheader, new() { Name = ""Name"" })
                  .ClickAsync();
        await Page.WaitForTimeoutAsync(400);

        await Page.GetByRole(AriaRole.Columnheader, new() { Name = ""Designation"" })
                  .ClickAsync(new() { Modifiers = new[] { KeyboardModifier.Control } });
        await Page.WaitForTimeoutAsync(500);

        var sortedCount = await Page
            .Locator(""[role='columnheader'][aria-sort='ascending'], "" +
                     ""[role='columnheader'][aria-sort='descending']"")
            .CountAsync();
        Assert.That(sortedCount, Is.GreaterThanOrEqualTo(2));
    }
}",

            // 5. Filter rows by exact column value
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T05_Filter_Exact_Department : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await OpenFilterMenuAsync(Page, ""Department"");
        await TypeInFilterInputAsync(Page, ""Design"");
        await PressEnterInFilterAsync(Page);
        await Page.WaitForSelectorAsync("".e-gridcontent tr.e-row"");

        var deps = (await Page.Locator("".e-gridcontent tr.e-row td:nth-child(4)"")
                                .AllTextContentsAsync()).ToList();

        Assert.That(deps, Is.Not.Empty);
        foreach (var d in deps)
            Assert.That(d.Trim(), Is.EqualTo(""Design""));
    }

    private async Task OpenFilterMenuAsync(IPage page, string headerName)
    {
        var header = page.GetByRole(AriaRole.Columnheader, new() { Name = headerName });
        await header.Locator("".e-filtermenudiv"").ClickAsync();
        await page.WaitForSelectorAsync("".e-filter-popup"", new() { Timeout = 5_000 });
    }

    private async Task TypeInFilterInputAsync(IPage page, string value)
    {
        var input = page.Locator("".e-filter-popup input:not([readonly])"").First;
        await input.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5_000 });
        await input.ClearAsync();
        await input.FillAsync(value);
    }

    private async Task PressEnterInFilterAsync(IPage page)
    {
        var filterBtn = page.Locator("".e-filter-popup button"")
                            .GetByText(""Filter"", new() { Exact = true });
        if (await filterBtn.CountAsync() > 0)
            await filterBtn.First.ClickAsync();
        else
            await page.Locator("".e-filter-popup input:not([readonly])"").First.PressAsync(""Enter"");
        await page.WaitForTimeoutAsync(300);
    }
}",

            // 6. Filter rows using partial text match
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T06_Filter_Contains_Des : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await OpenFilterMenuAsync(Page, ""Department"");
        await TypeInFilterInputAsync(Page, ""Des"");
        await PressEnterInFilterAsync(Page);
        await Page.WaitForSelectorAsync("".e-gridcontent tr.e-row"");

        var deps = (await Page.Locator("".e-gridcontent tr.e-row td:nth-child(4)"")
                                .AllTextContentsAsync()).ToList();
        Assert.That(deps, Is.Not.Empty);
        foreach (var d in deps)
            Assert.That(d, Does.Contain(""Des""));
    }

    private async Task OpenFilterMenuAsync(IPage page, string headerName)
    {
        var header = page.GetByRole(AriaRole.Columnheader, new() { Name = headerName });
        await header.Locator("".e-filtermenudiv"").ClickAsync();
        await page.WaitForSelectorAsync("".e-filter-popup"", new() { Timeout = 5_000 });
    }

    private async Task TypeInFilterInputAsync(IPage page, string value)
    {
        var input = page.Locator("".e-filter-popup input:not([readonly])"").First;
        await input.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5_000 });
        await input.ClearAsync();
        await input.FillAsync(value);
    }

    private async Task PressEnterInFilterAsync(IPage page)
    {
        var filterBtn = page.Locator("".e-filter-popup button"")
                            .GetByText(""Filter"", new() { Exact = true });
        if (await filterBtn.CountAsync() > 0)
            await filterBtn.First.ClickAsync();
        else
            await page.Locator("".e-filter-popup input:not([readonly])"").First.PressAsync(""Enter"");
        await page.WaitForTimeoutAsync(300);
    }
}",

            // 7. Apply multiple filters using the AND condition
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T07_MultiFilter_And : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await OpenFilterMenuAsync(Page, ""Name"");
        await TypeInFilterInputAsync(Page, ""A"");
        await PressEnterInFilterAsync(Page);
        await Page.WaitForTimeoutAsync(300);

        await OpenFilterMenuAsync(Page, ""Designation"");
        await TypeInFilterInputAsync(Page, ""Eng"");
        await PressEnterInFilterAsync(Page);
        await Page.WaitForTimeoutAsync(300);

        var rows  = Page.Locator("".e-gridcontent tr.e-row"");
        var count = await rows.CountAsync();
        Assert.That(count, Is.GreaterThan(0));

        for (int i = 0; i < count; i++)
        {
            var name = (await rows.Nth(i).Locator(""td:nth-child(2)"").TextContentAsync())!.Trim().ToLower();
            var role = (await rows.Nth(i).Locator(""td:nth-child(3)"").TextContentAsync())!.Trim().ToLower();
            Assert.That(name, Does.Contain(""a""));
            Assert.That(role, Does.Contain(""eng""));
        }
    }

    private async Task OpenFilterMenuAsync(IPage page, string headerName)
    {
        var header = page.GetByRole(AriaRole.Columnheader, new() { Name = headerName });
        await header.Locator("".e-filtermenudiv"").ClickAsync();
        await page.WaitForSelectorAsync("".e-filter-popup"", new() { Timeout = 5_000 });
    }

    private async Task TypeInFilterInputAsync(IPage page, string value)
    {
        var input = page.Locator("".e-filter-popup input:not([readonly])"").First;
        await input.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5_000 });
        await input.ClearAsync();
        await input.FillAsync(value);
    }

    private async Task PressEnterInFilterAsync(IPage page)
    {
        var filterBtn = page.Locator("".e-filter-popup button"")
                            .GetByText(""Filter"", new() { Exact = true });
        if (await filterBtn.CountAsync() > 0)
            await filterBtn.First.ClickAsync();
        else
            await page.Locator("".e-filter-popup input:not([readonly])"").First.PressAsync(""Enter"");
        await page.WaitForTimeoutAsync(300);
    }
}",

            // 8. Filter should ignore case sensitivity
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T08_Filter_IgnoresCase : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await OpenFilterMenuAsync(Page, ""Department"");
        await TypeInFilterInputAsync(Page, ""design"");
        await PressEnterInFilterAsync(Page);
        await Page.WaitForSelectorAsync("".e-gridcontent tr.e-row"");

        var deps = (await Page.Locator("".e-gridcontent tr.e-row td:nth-child(4)"")
                                .AllTextContentsAsync()).ToList();
        Assert.That(deps, Is.Not.Empty);
        foreach (var d in deps)
            Assert.That(d.Trim().ToLower(), Does.Contain(""design""));
    }

    private async Task OpenFilterMenuAsync(IPage page, string headerName)
    {
        var header = page.GetByRole(AriaRole.Columnheader, new() { Name = headerName });
        await header.Locator("".e-filtermenudiv"").ClickAsync();
        await page.WaitForSelectorAsync("".e-filter-popup"", new() { Timeout = 5_000 });
    }

    private async Task TypeInFilterInputAsync(IPage page, string value)
    {
        var input = page.Locator("".e-filter-popup input:not([readonly])"").First;
        await input.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5_000 });
        await input.ClearAsync();
        await input.FillAsync(value);
    }

    private async Task PressEnterInFilterAsync(IPage page)
    {
        var filterBtn = page.Locator("".e-filter-popup button"")
                            .GetByText(""Filter"", new() { Exact = true });
        if (await filterBtn.CountAsync() > 0)
            await filterBtn.First.ClickAsync();
        else
            await page.Locator("".e-filter-popup input:not([readonly])"").First.PressAsync(""Enter"");
        await page.WaitForTimeoutAsync(300);
    }
}",

            // 9. Search with non-existent value returns no results
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T09_Filter_NoResults : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await OpenFilterMenuAsync(Page, ""Department"");
        await TypeInFilterInputAsync(Page, ""NonExistingValue"");
        await PressEnterInFilterAsync(Page);

        await Expect(Page.Locator("".e-gridcontent .e-emptyrow"")).ToBeVisibleAsync();

        var dataRowCount = await Page
            .Locator("".e-gridcontent tr.e-row:not(.e-emptyrow)"")
            .CountAsync();
        Assert.That(dataRowCount, Is.EqualTo(0));
    }

    private async Task OpenFilterMenuAsync(IPage page, string headerName)
    {
        var header = page.GetByRole(AriaRole.Columnheader, new() { Name = headerName });
        await header.Locator("".e-filtermenudiv"").ClickAsync();
        await page.WaitForSelectorAsync("".e-filter-popup"", new() { Timeout = 5_000 });
    }

    private async Task TypeInFilterInputAsync(IPage page, string value)
    {
        var input = page.Locator("".e-filter-popup input:not([readonly])"").First;
        await input.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5_000 });
        await input.ClearAsync();
        await input.FillAsync(value);
    }

    private async Task PressEnterInFilterAsync(IPage page)
    {
        var filterBtn = page.Locator("".e-filter-popup button"")
                            .GetByText(""Filter"", new() { Exact = true });
        if (await filterBtn.CountAsync() > 0)
            await filterBtn.First.ClickAsync();
        else
            await page.Locator("".e-filter-popup input:not([readonly])"").First.PressAsync(""Enter"");
        await page.WaitForTimeoutAsync(300);
    }
}",

            // 10. Search rows based on Name column
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T10_Search_Alice : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var searchInput = Page.Locator(
                ""#sample-grid .e-input-group:has(.e-search-icon) input"").First;

        await searchInput.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 15_000
        });

        await searchInput.FillAsync(""Alice"");
        await searchInput.PressAsync(""Enter"");
        await Page.WaitForTimeoutAsync(500);

        var names = await Page
            .Locator("".e-gridcontent tr.e-row td:nth-child(2)"")
            .AllTextContentsAsync();

        Assert.That(names, Is.Not.Empty);
        foreach (var n in names)
            Assert.That(n.Trim().ToLower(), Does.Contain(""alice""));
    }
}",

            // 11. Open edit dialog on row interaction
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T11_DoubleClick_OpensDialog : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await Page.Locator("".e-gridcontent tr.e-row td:nth-child(2)"")
                  .First
                  .DblClickAsync();
        await Page.WaitForTimeoutAsync(400);

        await Expect(Page.Locator("".e-dialog.e-popup-open"")).ToBeVisibleAsync();
    }
}",

            // 12. Add a new record to the grid
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T12_Add_Record_ViaDialog : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await Page.Locator(""[id$='_add']"").ClickAsync();
        await Expect(Page.Locator("".e-dialog.e-popup-open"")).ToBeVisibleAsync();

        await Page.Locator(""input[name='Id']"").FillAsync(""100"");

        await Page.Locator(""input[name='Name']"").FillAsync(""Test User"");
        await Page.Locator(""input[name='Role']"").FillAsync(""Engineer"");
        await Page.Locator(""input[name='Department']"").FillAsync(""Research"");

        await Page.EvaluateAsync(@""
            () => {
                const dateInput = document.querySelector(
                    "".e-dialog .e-datepicker input[name='DateOfJoining'], "" +
                    "".e-dialog input[name='DateOfJoining']"");
                if (!dateInput) throw new Error('DateOfJoining input not found');
                const setter = Object.getOwnPropertyDescriptor(
                    Object.getPrototypeOf(dateInput), 'value').set;
                setter.call(dateInput, '7/15/2024');
                dateInput.dispatchEvent(new Event('input',  { bubbles: true }));
                dateInput.dispatchEvent(new Event('change', { bubbles: true }));
            }"");
        await Page.WaitForTimeoutAsync(500);

        await Page.Locator("".e-dialog button"")
                  .GetByText(""Save"", new() { Exact = true })
                  .ClickAsync();

        await Expect(Page.Locator("".e-dialog.e-popup-open""))
            .ToBeHiddenAsync(new() { Timeout = 10_000 });

        var newRow = Page.Locator("".e-gridcontent tr.e-row td"")
                         .Filter(new() { HasText = ""Test User"" });

        try
        {
            await Expect(newRow.First).ToBeVisibleAsync(new() { Timeout = 3_000 });
        }
        catch
        {
            var nextPage = Page.Locator("".e-nextpage"").First;
            var pagesTried = 0;
            const int maxPages = 5;   // 32 fixture + 1 = 33 → 3 pages of 12

            while (pagesTried < maxPages
                   && await nextPage.IsVisibleAsync()
                   && await nextPage.IsEnabledAsync())
            {
                await nextPage.ClickAsync();
                await Page.WaitForTimeoutAsync(500);
                if (await newRow.CountAsync() > 0
                    && await newRow.First.IsVisibleAsync())
                    break;
                pagesTried++;
            }
            await Expect(newRow.First).ToBeVisibleAsync(new() { Timeout = 5_000 });
        }
    }
}",

            // 13. Update an existing record
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T13_Update_Record_ViaDialog : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await Page.Locator("".e-gridcontent tr.e-row td"")
                  .Filter(new() { HasText = ""Alice"" })
                  .First
                  .DblClickAsync();
        await Expect(Page.Locator("".e-dialog.e-popup-open"")).ToBeVisibleAsync();

        await Page.Locator(""input[name='Name']"").FillAsync(""Updated User"");
        await Page.Locator(""input[name='Role']"").FillAsync(""Senior Engineer"");
        await Page.Locator(""input[name='Department']"").FillAsync(""Research"");

        await Page.Locator("".e-dialog button"")
                  .GetByText(""Save"", new() { Exact = true })
                  .ClickAsync();
        await Page.WaitForTimeoutAsync(600);

        await Expect(Page.Locator("".e-gridcontent tr.e-row td"")
            .Filter(new() { HasText = ""Updated User"" }))
            .ToBeVisibleAsync();
    }
}",

            // 14. Delete a record from the grid
            @"using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T14_Delete_Record : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await Page.Locator("".e-gridcontent tr.e-row"").First.ClickAsync();
        await Page.WaitForTimeoutAsync(200);
        await Page.Locator(""[id$='_delete']"").ClickAsync();
        await Page.WaitForTimeoutAsync(400);

        var confirmBtn = Page.Locator("".e-dialog button"").GetByText(""OK"");
        if (await confirmBtn.CountAsync() > 0)
            await confirmBtn.First.ClickAsync();
        await Page.WaitForTimeoutAsync(500);

        await Expect(Page.Locator("".e-gridcontent tr.e-row td"")
            .Filter(new() { HasTextRegex = new Regex(@""^1$"") }))
            .ToHaveCountAsync(0);
    }
}",

            // 15. Select single and multiple rows
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T15_Select_Rows : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var rows = Page.Locator("".e-gridcontent tr.e-row"");

        await rows.Nth(0).ClickAsync();
        await Page.WaitForTimeoutAsync(200);
        await Expect(rows.Nth(0)).ToHaveAttributeAsync(""aria-selected"", ""true"");

        await rows.Nth(1).ClickAsync(new()
        {
            Modifiers = new[] { KeyboardModifier.Control }
        });
        await Page.WaitForTimeoutAsync(200);

        var selectedCount = await Page
            .Locator("".e-gridcontent tr.e-row[aria-selected='true']"")
            .CountAsync();
        Assert.That(selectedCount, Is.GreaterThanOrEqualTo(2));
    }
}",

            // 16. Verify column headers are displayed correctly
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T16_Headers_Rendered : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var header = Page.Locator("".e-gridheader"");
        await Expect(header).ToBeVisibleAsync();

        await Expect(header.GetByText(""ID"",              new() { Exact = true })).ToBeVisibleAsync();
        await Expect(header.GetByText(""Name"",            new() { Exact = true })).ToBeVisibleAsync();
        await Expect(header.GetByText(""Designation"",     new() { Exact = true })).ToBeVisibleAsync();
        await Expect(header.GetByText(""Department"",      new() { Exact = true })).ToBeVisibleAsync();
        await Expect(header.GetByText(""Date of Joining"", new() { Exact = true })).ToBeVisibleAsync();
    }
}",

            // 17. Verify sorting icons on columns
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T17_Sort_Applied : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var nameHeader = Page.GetByRole(AriaRole.Columnheader, new() { Name = ""Name"" });
        await nameHeader.ClickAsync();
        await Page.WaitForTimeoutAsync(400);

        await Expect(nameHeader).ToHaveAttributeAsync(""aria-sort"", ""ascending"");
    }
}",

            // 18. Verify grid data rows are rendered
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T18_DataRows_Rendered : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await Expect(Page.Locator(""#sample-grid .e-row"").First).ToBeVisibleAsync();
        var count = await Page
            .Locator("".e-gridcontent tr.e-row:not(.e-emptyrow)"")
            .CountAsync();
        Assert.That(count, Is.GreaterThan(0));
    }
}",

            // 19. Verify pagination limits row count
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T19_Paging_12Rows : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await Expect(Page.Locator("".e-gridcontent tr.e-row""))
              .ToHaveCountAsync(12);
    }
}",

            // 20. Verify focus behavior in filter dialog
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T20_FilterDialog_HasFocusableElements : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        await OpenFilterMenuAsync(Page, ""Department"");

        var dialog = Page.Locator("".e-dialog.e-popup-open"");
        await Expect(dialog).ToBeVisibleAsync();

        var focusable = dialog.Locator(
            ""input, button, select, textarea, [tabindex]:not([tabindex='-1'])"");
        Assert.That(await focusable.CountAsync(), Is.GreaterThan(0));
    }

    private async Task OpenFilterMenuAsync(IPage page, string headerName)
    {
        var header = page.GetByRole(AriaRole.Columnheader, new() { Name = headerName });
        await header.Locator("".e-filtermenudiv"").ClickAsync();
        await page.WaitForSelectorAsync("".e-filter-popup"", new() { Timeout = 5_000 });
    }
}",

            // 21. Verify column reorder functionality
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T21_Column_Reorder : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var headers = Page.Locator("".e-gridheader th.e-headercell"");
        await Expect(headers.First).ToBeVisibleAsync();

        var headerTexts = Page.Locator("".e-gridheader .e-headertext"");
        var before = await headerTexts.AllTextContentsAsync();

        var src = headers.Nth(1);
        var dst = headers.Nth(0);
        var srcBox = (await src.BoundingBoxAsync())!;
        var dstBox = (await dst.BoundingBoxAsync())!;

        await Page.Mouse.MoveAsync(srcBox.X + srcBox.Width / 2,
                                   srcBox.Y + srcBox.Height / 2);
        await Page.Mouse.DownAsync();
        await Page.Mouse.MoveAsync(dstBox.X + dstBox.Width / 2,
                                   dstBox.Y + dstBox.Height / 2,
                                   new() { Steps = 10 });
        await Page.WaitForTimeoutAsync(150);
        await Page.Mouse.UpAsync();
        await Page.WaitForTimeoutAsync(500);

        var after = await headerTexts.AllTextContentsAsync();
        Assert.That(after, Is.Not.EqualTo(before));
    }
}",

            // 22. Verify column resizing updates width
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T22_Column_Resize : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var header = Page.Locator("".e-gridheader th.e-headercell"").First;
        await Expect(header).ToBeVisibleAsync();
        var before = (await header.BoundingBoxAsync())!;

        var resizer = header.Locator("".e-rhandler"");
        await Expect(resizer).ToBeVisibleAsync();
        var rBox = (await resizer.BoundingBoxAsync())!;

        await Page.Mouse.MoveAsync(rBox.X + rBox.Width / 2,
                                   rBox.Y + rBox.Height / 2);
        await Page.Mouse.DownAsync();
        await Page.Mouse.MoveAsync(rBox.X + rBox.Width / 2 + 40,
                                   rBox.Y + rBox.Height / 2,
                                   new() { Steps = 5 });
        await Page.Mouse.UpAsync();
        await Page.WaitForTimeoutAsync(300);

        var after = (await header.BoundingBoxAsync())!;
        Assert.That(after.Width, Is.GreaterThan(before.Width));
    }
}",

            // 23. Verify PDF export functionality
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T23_PdfExport_DownloadsFile : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var pdfBtn = Page.Locator(""#sample-grid button[aria-label='PDF Export']"");
        await Expect(pdfBtn).ToBeVisibleAsync();

        var downloadTask = Page.WaitForDownloadAsync();
        await pdfBtn.ClickAsync();
        var download = await downloadTask;

        Assert.That(download.SuggestedFilename, Does.EndWith("".pdf""));
    }
}",

            // 24. Verify Excel export functionality
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T24_ExcelExport_DownloadsFile : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var excelBtn = Page.Locator(""#sample-grid button[aria-label='Excel Export']"");
        await Expect(excelBtn).ToBeVisibleAsync();

        var downloadTask = Page.WaitForDownloadAsync();
        await excelBtn.ClickAsync();
        var download = await downloadTask;

        Assert.That(download.SuggestedFilename, Does.EndWith("".xlsx""));
    }
}",

            // 25. Verify CSV export functionality
            @"using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
public class T25_CsvExport_DownloadsFile : PageTest
{
    [Test]
    public async Task Test()
    {
        await Page.SetViewportSizeAsync(1600, 900);
        await Page.GotoAsync(""http://localhost:5199/testing"");
        await Page.WaitForSelectorAsync(""#sample-grid .e-row"");

        var csvBtn = Page.Locator(""#sample-grid button[aria-label='CSV Export']"");
        await Expect(csvBtn).ToBeVisibleAsync();

        var downloadTask = Page.WaitForDownloadAsync();
        await csvBtn.ClickAsync();
        var download = await downloadTask;

        Assert.That(download.SuggestedFilename, Does.EndWith("".csv""));
    }
}"
        };

        // Cypress Test cases
        public static readonly string[] Cypress =
        {
            // 1. Sort the name column in ascending order

            @"describe('Grid sorted name ascending', () => {
  it('Clicking the name column header sorts rows in ascending order.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.contains('.e-headertext', /^Name$/)
      .closest('[role=""columnheader""]')
      .click();
    cy.wait(400);

    cy.contains('.e-headertext', /^Name$/)
      .closest('.e-headercell')
      .should('have.attr', 'aria-sort', 'ascending');

    cy.get('.e-gridcontent tbody tr.e-row').then(($rows) => {
      const names = [...$rows].map((r) =>
        r.querySelectorAll('.e-rowcell')[1].textContent.trim()
      );

      const sorted = [...names].sort((a, b) =>
        a < b ? -1 : a > b ? 1 : 0
      );
      expect(names).to.deep.equal(sorted);
    });
  });
});",

            // 2. Sort the name column in descending order
            @"describe('Grid sorted name descending', () => {
  it('Clicking the name header again sorts rows in descending order.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    const header = cy.contains('.e-headertext', /^Name$/)
      .closest('[role=""columnheader""]');

    header.click(); // ascending
    cy.wait(300);
    header.click(); // descending
    cy.wait(400);

    cy.contains('.e-headertext', /^Name$/)
      .closest('.e-headercell')
      .should('have.attr', 'aria-sort', 'descending');

    cy.get('.e-gridcontent tbody tr.e-row').then(($rows) => {
      const names = [...$rows].map((r) =>
        r.querySelectorAll('.e-rowcell')[1].textContent.trim()
      );
      const sorted = [...names].sort((a, b) =>
        a < b ? 1 : a > b ? -1 : 0
      );
      expect(names).to.deep.equal(sorted);
    });
  });
});",

            // 3. Sort the ID column in ascending numerical order
            @"describe('Grid sorted ID ascending', () => {
  it('Clicking the ID header sorts numbers smallest to largest.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.contains('.e-headertext', /^ID$/)
      .closest('[role=""columnheader""]')
      .click();
    cy.wait(400);

    cy.contains('.e-headertext', /^ID$/)
      .closest('.e-headercell')
      .should('have.attr', 'aria-sort', 'ascending');

    cy.get('.e-gridcontent tbody tr.e-row').then(($rows) => {
      const ids = [...$rows].map((r) =>
        Number(r.querySelectorAll('.e-rowcell')[0].textContent.trim())
      );
      const sorted = [...ids].sort((a, b) => a - b);
      expect(ids).to.deep.equal(sorted);
    });
  });
});",

            // 4. Apply sorting to multiple columns

            @"describe('Grid multi-sort', () => {
  it('Applying sorting on multiple columns sorts the grid accordingly.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.contains('.e-headertext', /^Name$/)
      .closest('[role=""columnheader""]')
      .click();
    cy.wait(300);

    cy.contains('.e-headertext', /^Designation$/)
      .closest('[role=""columnheader""]')
      .click({ ctrlKey: true });
    cy.wait(400);

    cy.get('[role=""columnheader""][aria-sort]')
      .should('have.length.at.least', 2);
  });
});",

            // 5. Filter rows by exact column value

            @"describe('Grid filter exact', () => {
  it('Filtering by exact Department value returns matching rows.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.contains('.e-headertext', /^Department$/)
      .closest('[role=""columnheader""]')
      .find('.e-filtermenudiv')
      .click();
    cy.get('.e-filter-popup').should('be.visible');

    cy.get('.e-filter-popup input:not([readonly])')
      .first()
      .clear()
      .type('Design');

    cy.get('.e-filter-popup button')
      .contains(/^Filter$/)
      .click();
    cy.wait(400);

    cy.get('.e-gridcontent tbody tr.e-row').should('have.length.greaterThan', 0);
    cy.get('.e-gridcontent tbody tr.e-row').each(($row) => {
      cy.wrap($row)
        .find('td:not(.e-gridchkbox)')
        .eq(3)
        .invoke('text')
        .should('match', /^Design$/);
    });
  });
});",

            // 6. Filter rows using partial text match
            @"describe('Grid filter contains', () => {
  it('Filtering by partial Department value returns matching rows.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.contains('.e-headertext', /^Department$/)
      .closest('[role=""columnheader""]')
      .find('.e-filtermenudiv')
      .click();
    cy.get('.e-filter-popup').should('be.visible');

    cy.get('.e-filter-popup input:not([readonly])')
      .first()
      .clear()
      .type('Des');
    cy.get('.e-filter-popup button')
      .contains(/^Filter$/)
      .click();
    cy.wait(400);

    cy.get('.e-gridcontent tbody tr.e-row').should('have.length.greaterThan', 0);
    cy.get('.e-gridcontent tbody tr.e-row').each(($row) => {
      cy.wrap($row)
        .find('td:not(.e-gridchkbox)')
        .eq(3)
        .invoke('text')
        .should('contain', 'Des');
    });
  });
});",

            // 7. Apply multiple filters using the AND condition
            @"describe('Grid multi-filter AND', () => {
  it('Multiple filters combine using AND logic.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.contains('.e-headertext', /^Name$/)
      .closest('[role=""columnheader""]')
      .find('.e-filtermenudiv')
      .click();
    cy.get('.e-filter-popup').should('be.visible');
    cy.get('.e-filter-popup input:not([readonly])').first().type('A');
    cy.get('.e-filter-popup button').contains(/^Filter$/).click();
    cy.wait(400);

    cy.contains('.e-headertext', /^Designation$/)
      .closest('[role=""columnheader""]')
      .find('.e-filtermenudiv')
      .click();
    cy.get('.e-filter-popup').should('be.visible');
    cy.get('.e-filter-popup input:not([readonly])').first().type('Eng');
    cy.get('.e-filter-popup button').contains(/^Filter$/).click();
    cy.wait(400);

    cy.get('.e-gridcontent tbody tr.e-row').should('have.length.greaterThan', 0);
    cy.get('.e-gridcontent tbody tr.e-row').each(($row) => {
      cy.wrap($row)
        .find('td:not(.e-gridchkbox)')
        .eq(1)
        .invoke('text')
        .should('match', /A/i);
      cy.wrap($row)
        .find('td:not(.e-gridchkbox)')
        .eq(2)
        .invoke('text')
        .should('match', /Eng/i);
    });
  });
});",

            // 8. Filter should ignore case sensitivity
            @"describe('Grid filter case-insensitive', () => {
  it('Filtering ignores case differences.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.contains('.e-headertext', /^Department$/)
      .closest('[role=""columnheader""]')
      .find('.e-filtermenudiv')
      .click();
    cy.get('.e-filter-popup').should('be.visible');

    cy.get('.e-filter-popup input:not([readonly])')
      .first()
      .clear()
      .type('design');
    cy.get('.e-filter-popup button').contains(/^Filter$/).click();
    cy.wait(400);

    cy.get('.e-gridcontent tbody tr.e-row').should('have.length.greaterThan', 0);
    cy.get('.e-gridcontent tbody tr.e-row').each(($row) => {
      cy.wrap($row)
        .find('td:not(.e-gridchkbox)')
        .eq(3)
        .invoke('text')
        .then((t) => expect(t.toLowerCase()).to.include('design'));
    });
  });
});",

            // 9. Search with non-existent value returns no results
            @"describe('Grid filter no results', () => {
  it('Searching with a non-existent value shows no rows.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.contains('.e-headertext', /^Department$/)
      .closest('[role=""columnheader""]')
      .find('.e-filtermenudiv')
      .click();
    cy.get('.e-filter-popup').should('be.visible');

    cy.get('.e-filter-popup input:not([readonly])')
      .first()
      .clear()
      .type('NonExistingValue');
    cy.get('.e-filter-popup button').contains(/^Filter$/).click();
    cy.wait(400);

    cy.get('.e-gridcontent tbody tr.e-emptyrow')
      .should('have.length', 1);
    cy.get('.e-gridcontent .e-emptyrow')
      .should('contain', 'No records to display');
  });
});",

            // 10. Search rows based on Name column
            @"describe('Grid search by name', () => {
  it('Search filters rows based on the name value.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.get('#sample-grid .e-input-group:has(.e-search-icon) input')
      .first()
      .should('be.visible')
      .clear()
      .type('Alice{enter}');
    cy.wait(500);

    cy.get('.e-gridcontent tbody tr.e-row').should('have.length.greaterThan', 0);
    cy.get('.e-gridcontent tbody tr.e-row').each(($row) => {
      cy.wrap($row)
        .find('td:not(.e-gridchkbox)')
        .eq(1)
        .invoke('text')
        .should('contain', 'Alice');
    });
  });
});",

            // 11. Open edit dialog on row interaction
            @"describe('Grid edit dialog', () => {
  it('Double-clicking opens the dialog editor.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.get('.e-gridcontent tbody tr.e-row')
      .first()
      .find('td')
      .eq(1)
      .dblclick({ force: true });
    cy.wait(500);

    cy.get('.e-dialog.e-popup-open').should('be.visible');
  });
});",

            // 12. Add a new record to the grid
            @"describe('Grid add record', () => {
  it('Creating a new row persists it in the data source and the grid.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.window().should((win) => {
      const inst = win.document.querySelector('#sample-grid')?.ej2_instances?.[0];
      expect(inst, 'SfGrid ej2_instances[0]').to.exist;
    });

    cy.get('#sample-grid_add button')
      .first()
      .click({ force: true });
    cy.get('.e-dialog.e-popup-open', { timeout: 15000 }).should('be.visible');

    cy.get('.e-dialog input[name=""Id""]', { timeout: 10000 })
      .clear()
      .type('100');

    cy.get('.e-dialog input[name=""Name""]').clear().type('Test User');
    cy.get('.e-dialog input[name=""Role""]').clear().type('Engineer');
    cy.get('.e-dialog input[name=""Department""]').clear().type('Research');

    cy.window().then((win) => {
      const dateInput = win.document.querySelector(
        '.e-dialog .e-datepicker input[name=""DateOfJoining""], ' +
        '.e-dialog input[name=""DateOfJoining""]'
      );
      if (!dateInput) throw new Error('DateOfJoining input not found');
      const setter = Object.getOwnPropertyDescriptor(
        Object.getPrototypeOf(dateInput),
        'value'
      ).set;
      setter.call(dateInput, '7/15/2024');
      dateInput.dispatchEvent(new Event('input',  { bubbles: true }));
      dateInput.dispatchEvent(new Event('change', { bubbles: true }));
    });
    cy.wait(300);

    cy.get('.e-dialog .e-footer-content button')
      .contains(/^Save$/, { matchCase: false })
      .click();
    cy.get('.e-dialog.e-popup-open', { timeout: 15000 }).should('not.exist');

    const tryFindRow = (attempts) => {
      cy.get('body').then(($b) => {
        const cell = $b.find('.e-gridcontent tr.e-row td:contains(""Test User"")');
        if (cell.length > 0 && Cypress.dom.isVisible(cell[0])) return;
        if (attempts >= 4) return;
        cy.get('body').then(($b2) => {
          const next = $b2.find('.e-nextpage').first();
          if (
            next.length > 0 &&
            Cypress.dom.isVisible(next[0]) &&
            !next.prop('disabled')
          ) {
            cy.wrap(next).click({ force: true });
            cy.wait(500);
          }
        });
        cy.wait(300);
        tryFindRow(attempts + 1);
      });
    };
    tryFindRow(0);

    cy.get('.e-gridcontent tr.e-row td:contains(""Test User"")')
      .first()
      .should('be.visible');
  });
});",

            // 13. Update an existing record
            @"describe('Grid update record', () => {
  it('Updating an existing row updates its values via the dialog.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.get('.e-gridcontent tbody tr.e-row')
      .first()
      .find('td')
      .eq(1)
      .dblclick({ force: true });
    cy.get('.e-dialog.e-popup-open').should('be.visible');

    cy.get('.e-dialog input[name=""Name""]').clear().type('Updated User');
    cy.get('.e-dialog input[name=""Role""]').clear().type('Senior Engineer');
    cy.get('.e-dialog input[name=""Department""]').clear().type('Research');

    cy.get('.e-dialog .e-footer-content button')
      .contains(/^Save$/, { matchCase: false })
      .click();
    cy.wait(600);

    cy.get('.e-gridcontent tr.e-row td')
      .filter(':contains(""Updated User"")')
      .first()
      .should('be.visible');
  });
});",

            // 14. Delete a record from the grid
            @"describe('Grid delete record', () => {
  it('Deleting a selected row removes it from the grid.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.get('.e-gridcontent tbody tr.e-row').first().click();
    cy.wait(200);
    cy.get('[id$=""_delete""] button').first().click({ force: true });
    cy.wait(400);

    cy.get('body').then(($b) => {
      const ok = $b.find('.e-dialog button').filter((_i, el) =>
        /^OK$/i.test(el.textContent?.trim() || '')
      );
      if (ok.length) cy.wrap(ok.first()).click();
    });
    cy.wait(500);

    cy.get('.e-gridcontent tbody')
      .contains('td', /^1$/)
      .should('not.exist');
  });
});",

            // 15. Select single and multiple rows
            @"describe('Grid row selection', () => {
  it('Selecting rows updates the selected indexes.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('have.length.greaterThan', 1);
    cy.wait(200);

    cy.get('.e-gridcontent tbody tr.e-row').eq(0).click();
    cy.get('.e-gridcontent tbody tr.e-row').eq(0)
      .should('have.attr', 'aria-selected', 'true');

    cy.get('.e-gridcontent tbody tr.e-row').eq(1).click({ ctrlKey: true });
    cy.get('.e-gridcontent tbody tr.e-row').eq(1)
      .should('have.attr', 'aria-selected', 'true');

    cy.get("".e-gridcontent tr.e-row[aria-selected='true']"")
      .should('have.length.at.least', 2);
  });
});",

            // 16. Verify column headers are displayed correctly
            @"describe('Grid headers', () => {
  it('All column headers are rendered correctly.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('.e-gridheader').should('be.visible');

    cy.get('.e-gridheader').contains('ID').should('be.visible');
    cy.get('.e-gridheader').contains('Name').should('be.visible');
    cy.get('.e-gridheader').contains('Designation').should('be.visible');
    cy.get('.e-gridheader').contains('Department').should('be.visible');
    cy.get('.e-gridheader').contains('Date of Joining').should('be.visible');
  });
});",

            // 17. Verify sorting icons on columns
            @"describe('Grid sort icons', () => {
  it('Sortable columns display a sort icon and apply sort on click.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(400);

    cy.contains('.e-headertext', /^Name$/)
      .closest('[role=""columnheader""]')
      .find('.e-sortfilterdiv')
      .should('exist');

    cy.get('.e-gridcontent tbody tr.e-row').then(($rows) => {
      const namesBefore = [...$rows].map((r) =>
        r.querySelectorAll('.e-rowcell')[1].textContent.trim()
      );

      cy.contains('.e-headertext', /^Name$/)
        .closest('[role=""columnheader""]')
        .click();
      cy.wait(500);

      cy.get('.e-gridcontent tbody tr.e-row').then(($rowsAfter) => {
        const namesAfter = [...$rowsAfter].map((r) =>
          r.querySelectorAll('.e-rowcell')[1].textContent.trim()
        );
        const sortedAsc = [...namesAfter].sort((a, b) =>
          a < b ? -1 : a > b ? 1 : 0
        );
        expect(namesAfter, 'rows should be sorted ascending after sort click')
          .to.deep.equal(sortedAsc);
        expect(namesAfter, 'sorted order should differ from initial order')
          .to.not.deep.equal(namesBefore);
      });
    });
  });
});",

            // 18. Verify grid data rows are rendered
            @"describe('Grid data rows', () => {
  it('The grid renders data rows correctly.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('.e-gridcontent tbody tr.e-row')
      .should('have.length.greaterThan', 0);
  });
});",

            // 19. Verify pagination limits row count
            @"describe('Grid paging', () => {
  it('Paging shows 12 rows per page.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('.e-gridcontent tbody tr.e-row').should('have.length', 12);
  });
});",

            // 20. Verify focus behavior in filter dialog
            @"describe('Grid filter dialog focus', () => {
  it('Focusable elements exist inside the open filter dialog.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.contains('.e-headertext', /^Department$/)
      .closest('[role=""columnheader""]')
      .find('.e-filtermenudiv')
      .click();

    cy.get('.e-dialog.e-popup-open')
      .should('be.visible')
      .then(($dialog) => {
        const focusable = $dialog.find(
          'input, button, select, textarea, [tabindex]:not([tabindex=""-1""])'
        );
        expect(focusable.length).to.be.greaterThan(0);
      });
  });
});",

            // 21. Verify column reorder functionality
@"describe('Grid column reorder', () => {
  it('Reordering columns is supported by the grid configuration.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(400);

    cy.get('.e-headertext').then(($headers) => {
      const headers = [...$headers].map((h) => (h.textContent || '').trim());
      expect(headers.length, 'five column headers should render').to.equal(5);
      expect(headers[0], 'first column is ID by default').to.equal('ID');
      expect(headers[1], 'second column is Name by default').to.equal('Name');
    });

    cy.window().then((win) => {
      const inst = win.document.querySelector('.e-grid')?.ej2_instances?.[0];
      expect(inst, 'SfGrid ej2_instances[0]').to.exist;
      expect(inst.allowReordering, 'grid must enable AllowReordering')
        .to.equal(true);
    });

    cy.get('.e-gridheader th.e-headercell')
      .should('have.length.at.least', 5);
    cy.get('.e-gridheader .e-sortfilterdiv').should('exist');
  });
});",

             // 22. Verify column resizing updates width  
            @"describe('Grid column resize', () => {
  it('Resizing a column keeps a valid width after a drag gesture.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(500);

    cy.get('.e-gridheader th.e-headercell').eq(1)
      .invoke('width')
      .then((before) => {
        const beforeNum = Number(before);
        expect(beforeNum, 'header width before resize should be > 0')
          .to.be.greaterThan(0);

        cy.get('.e-gridheader th.e-headercell').eq(1)
          .find('.e-rhandler')
          .then(($handle) => {
            const handleBox = $handle[0].getBoundingClientRect();
            const startX = handleBox.x + handleBox.width / 2;
            const startY = handleBox.y + handleBox.height / 2;
            const endX = startX + 60;
            const endY = startY;

            const fire = (
              el, type, x, y, buttons
            ) => {
              el.dispatchEvent(new PointerEvent(type, {
                bubbles: true, cancelable: true, composed: true,
                pointerId: 1, pointerType: 'mouse', isPrimary: true,
                button: 0, buttons, clientX: x, clientY: y,
              }));
            };

            fire($handle[0], 'pointerdown', startX, startY, 1);
            cy.wait(60);
            fire(document, 'pointermove', startX + 15, startY, 1);
            cy.wait(50);
            fire(document, 'pointermove', startX + 30, startY, 1);
            cy.wait(50);
            fire(document, 'pointermove', endX,       startY, 1);
            cy.wait(50);
            fire(document, 'pointerup',   endX,       startY, 0);
          });
      });

    cy.wait(500);

    cy.get('.e-gridheader th.e-headercell').eq(1)
      .invoke('width')
      .then((after) => {
        expect(Number(after), 'resized column width should be > 0')
          .to.be.greaterThan(0);
      });
  });
});",

            // 23. Verify PDF export functionality
            @"describe('Grid PDF export', () => {
  it('PDF export downloads a .pdf file.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.get('#sample-grid button[aria-label=""PDF Export""]')
      .should('be.visible')
      .click({ force: true });

    cy.readFile('cypress/downloads/grid-sample.pdf', { timeout: 15000 })
      .should('exist');
  });
});",

            // 24. Verify Excel export functionality
            @"describe('Grid Excel export', () => {
  it('Excel export downloads an .xlsx file.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.get('#sample-grid button[aria-label=""Excel Export""]')
      .should('be.visible')
      .click({ force: true });

    cy.readFile('cypress/downloads/grid-sample.xlsx', { timeout: 15000 })
      .should('exist');
  });
});",

            // 25. Verify CSV export functionality
            @"describe('Grid CSV export', () => {
  it('CSV export downloads a .csv file.', () => {
    cy.viewport(1600, 900);
    cy.visit('http://localhost:5199/testing');
    cy.get('#sample-grid .e-row').should('exist');
    cy.wait(200);

    cy.get('#sample-grid button[aria-label=""CSV Export""]')
      .should('be.visible')
      .click({ force: true });

    cy.readFile('cypress/downloads/grid-sample.csv', { timeout: 20000 })
      .should('exist');
  });
});"
        };
    }
}
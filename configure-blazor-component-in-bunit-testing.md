---
layout: post
title: Configure Blazor components in bUnit tests | Syncfusion
description: Learn how to configure Blazor components in bUnit using xUnit or NUnit, register required services, write effective unit tests, and explore advanced testing patterns.
platform: Blazor
control: Common
documentation: ug
---

# Configure Blazor components in bUnit testing

This guide explains how to configure [Blazor components](https://www.syncfusion.com/blazor-components) for unit testing with [bUnit](https://bunit.dev/docs/getting-started/index.html) in a modern .NET 10 environment using Visual Studio Code.

## Prerequisites

* **.NET 10 SDK** or later
* **Visual Studio Code** with the **.NET Extension Pack**
* A Syncfusion Blazor application to test

## Configure bUnit with xUnit Test Project

### Create xUnit Test Project

1. Open Visual Studio Code and create a new xUnit test project using the terminal:

    ```bash
    dotnet new xunit -n BlazorXUnitTesting -f net10.0
    cd BlazorXUnitTesting
    ```

2. Add the bUnit and Syncfusion Blazor NuGet packages:

    ```bash
    dotnet add package bunit --version 2.4.3
    dotnet add package Microsoft.AspNetCore.Components.Web --version 10.0.0
    dotnet add package Syncfusion.Blazor --version 28.1.33
    ```

3. Add a reference to your Blazor project:

    ```bash
    dotnet add reference ../path/to/YourBlazorApp/YourBlazorApp.csproj
    ```

### Write a Basic bUnit Test

Add the following test component to your Blazor project in `~/Pages/Home.razor`:

```cshtml
@using Syncfusion.Blazor.Buttons

<SfButton @onclick="OnButtonClick">My Button</SfButton>
<span class="status">Count: @clickCount</span>

@code {
    private int clickCount = 0;

    [Parameter]
    public int Step { get; set; } = 1;

    private void OnButtonClick()
    {
        clickCount += Step;
    }
}
```

Add the bUnit test in `~/UnitTest1.cs`:

```csharp
using Xunit;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorXUnitTesting;

public class ButtonComponentTests
{
    [Fact]
    public void ButtonClick_IncrementsCounter()
    {
        // Arrange
        using var testContext = new TestContext();
        testContext.Services.AddSyncfusionBlazor()
            .Replace(ServiceDescriptor.Transient<IComponentActivator, SfComponentActivator>());
        testContext.Services.AddOptions();

        // Act
        var component = testContext.RenderComponent<Home>();
        var button = component.FindComponent<SfButton>();
        var status = component.Find("span.status");

        // Assert - Initial state
        status.MarkupMatches("<span class=\"status\">Count: 0</span>");

        // Act - Trigger click
        button.Find(".e-btn").Click();

        // Assert - After click
        status.MarkupMatches("<span class=\"status\">Count: 1</span>");
    }

    [Fact]
    public void SetParameters_ConfiguresStepValue()
    {
        using var testContext = new TestContext();
        testContext.Services.AddSyncfusionBlazor()
            .Replace(ServiceDescriptor.Transient<IComponentActivator, SfComponentActivator>());
        testContext.Services.AddOptions();

        var component = testContext.RenderComponent<Home>();
        component.SetParametersAndRender(p => p.Add(h => h.Step, 5));

        var button = component.FindComponent<SfButton>();
        button.Find(".e-btn").Click();

        var status = component.Find("span.status");
        status.MarkupMatches("<span class=\"status\">Count: 5</span>");
    }
}
```

Run tests with:

```bash
dotnet test
```

## Configure bUnit with NUnit Test Project

### Create NUnit Test Project

1. Create the NUnit project:

    ```bash
    dotnet new nunit -n BlazorNUnitTesting -f net10.0
    cd BlazorNUnitTesting
    ```

2. Add the required packages:

    ```bash
    dotnet add package bunit --version 2.4.3
    dotnet add package Microsoft.AspNetCore.Components.Web --version 10.0.0
    dotnet add package Syncfusion.Blazor --version 28.1.33
    ```

3. Add a reference to your Blazor project:

    ```bash
    dotnet add reference ../path/to/YourBlazorApp/YourBlazorApp.csproj
    ```

### Write a Basic bUnit Test

```csharp
using Bunit;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorNUnitTesting;

public class ButtonComponentTests
{
    [Test]
    public void ButtonClick_IncrementsCounter()
    {
        using var testContext = new TestContext();
        testContext.Services.AddSyncfusionBlazor()
            .Replace(ServiceDescriptor.Transient<IComponentActivator, SfComponentActivator>());
        testContext.Services.AddOptions();

        var component = testContext.RenderComponent<Home>();
        var button = component.FindComponent<SfButton>();
        var status = component.Find("span.status");

        status.MarkupMatches("<span class=\"status\">Count: 0</span>");
        button.Find(".e-btn").Click();
        status.MarkupMatches("<span class=\"status\">Count: 1</span>");
    }
}
```

Run tests with:

```bash
dotnet test
```

## Testing DataGrid Component

The **DataGrid** is a high-usage component that requires comprehensive testing coverage. This section demonstrates how to test DataGrid interactions including data binding, sorting, filtering, and row selection.

### DataGrid Test Setup

First, create a data model and service for testing:

```csharp
// Models/Employee.cs
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public int Age { get; set; }
}
```

Add the DataGrid component to `~/Pages/DataGridDemo.razor`:

```cshtml
@using Syncfusion.Blazor.Grids
@using Syncfusion.Blazor.Buttons

<SfGrid DataSource="@Employees" AllowSorting="true" AllowFiltering="true">
    <GridColumns>
        <GridColumn Field="@nameof(Employee.Id)" HeaderText="ID" Width="100" />
        <GridColumn Field="@nameof(Employee.Name)" HeaderText="Name" Width="150" />
        <GridColumn Field="@nameof(Employee.Department)" HeaderText="Department" Width="150" />
        <GridColumn Field="@nameof(Employee.Age)" HeaderText="Age" Width="100" />
    </GridColumns>
</SfGrid>
<span class="selection-info">Selected: @SelectedEmployee</span>

<button @onclick="ClearSelection">Clear Selection</button>

@code {
    private List<Employee> Employees { get; set; } = new()
    {
        new Employee { Id = 1, Name = "John Doe", Department = "Engineering", Age = 32 },
        new Employee { Id = 2, Name = "Jane Smith", Department = "Marketing", Age = 28 },
        new Employee { Id = 3, Name = "Bob Wilson", Department = "Engineering", Age = 45 }
    };

    [Parameter]
    public string? SelectedEmployee { get; set; }

    private void ClearSelection()
    {
        SelectedEmployee = null;
    }
}
```

### DataGrid Test Cases

```csharp
[Fact]
public void DataGrid_RendersWithCorrectRowCount()
{
    using var testContext = new TestContext();
    testContext.Services.AddSyncfusionBlazor()
        .Replace(ServiceDescriptor.Transient<IComponentActivator, SfComponentActivator>());
    testContext.Services.AddOptions();

    var component = testContext.RenderComponent<DataGridDemo>();

    // Verify grid renders with data rows
    var gridRows = component.FindAll(".e-row");
    Assert.Equal(3, gridRows.Count);
}

[Fact]
public void DataGrid_ColumnHeaders_AreCorrect()
{
    using var testContext = new TestContext();
    testContext.Services.AddSyncfusionBlazor()
        .Replace(ServiceDescriptor.Transient<IComponentActivator, SfComponentActivator>());
    testContext.Services.AddOptions();

    var component = testContext.RenderComponent<DataGridDemo>();

    // Verify column headers
    var headers = component.FindAll(".e-headercell");
    Assert.Equal(4, headers.Count);

    var headerTexts = headers.Select(h => h.TextContent.Trim()).ToList();
    Assert.Contains("ID", headerTexts);
    Assert.Contains("Name", headerTexts);
    Assert.Contains("Department", headerTexts);
    Assert.Contains("Age", headerTexts);
}

[Fact]
public void DataGrid_SortButton_Click_SortsData()
{
    using var testContext = new TestContext();
    testContext.Services.AddSyncfusionBlazor()
        .Replace(ServiceDescriptor.Transient<IComponentActivator, SfComponentActivator>());
    testContext.Services.AddOptions();

    var component = testContext.RenderComponent<DataGridDemo>();

    // Find and click the sort button for Name column
    var sortButtons = component.FindAll(".e-sortfilter");
    Assert.NotEmpty(sortButtons);
    sortButtons[1].Click();

    // Verify sort icon state changes
    var sortedColumn = component.Find(".e-sorticon");
    Assert.NotNull(sortedColumn);
}

[Fact]
public void DataGrid_Filterrow_AcceptsInput()
{
    using var testContext = new TestContext();
    testContext.Services.AddSyncfusionBlazor()
        .Replace(ServiceDescriptor.Transient<IComponentActivator, SfComponentActivator>());
    testContext.Services.AddOptions();

    var component = testContext.RenderComponent<DataGridDemo>();

    // Find the filter input
    var filterInput = component.Find(".e-filterbar .e-input");
    filterInput.Change("Engineering");

    // Verify filtering is applied
    var gridRows = component.FindAll(".e-row");
    Assert.NotEmpty(gridRows);
}
```

## Advanced Testing Patterns

### Mocking Services

Mock external services to isolate component behavior:

```csharp
[Fact]
public void Component_WithMockedService_DisplaysData()
{
    using var testContext = new TestContext();
    testContext.Services.AddSyncfusionBlazor()
        .Replace(ServiceDescriptor.Transient<IComponentActivator, SfComponentActivator>());
    testContext.Services.AddOptions();

    // Mock a service
    var mockDataService = new Mock<IDataService>();
    mockDataService.Setup(s => s.GetEmployeesAsync())
        .ReturnsAsync(new List<Employee>
        {
            new Employee { Id = 1, Name = "Test User", Department = "QA", Age = 30 }
        });
    testContext.Services.AddScoped(_ => mockDataService.Object);

    var component = testContext.RenderComponent<MyComponent>();
    Assert.Contains("Test User", component.Markup);
}
```

### Cascading Values Testing

Test components that use cascading parameters:

```csharp
[Fact]
public void Component_WithCascadingValue_ReceivesCorrectData()
{
    using var testContext = new TestContext();
    testContext.Services.AddSyncfusionBlazor()
        .Replace(ServiceDescriptor.Transient<IComponentActivator, SfComponentActivator>());
    testContext.Services.AddOptions();

    var wrapper = testContext.RenderComponent<CascadingWrapper>(
        parameters => parameters.Add(p => p.Value, "Test Theme"));

    var child = wrapper.FindComponent<ChildComponent>();
    Assert.Contains("Test Theme", child.Markup);
}
```

### JS Interop Mocking

Mock JavaScript interop for browser-dependent components:

```csharp
[Fact]
public async Task Component_TriggersJsInterop_CallsConfirm()
{
    using var testContext = new TestContext();
    testContext.Services.AddSyncfusionBlazor()
        .Replace(ServiceDescriptor.Transient<IComponentActivator, SfComponentActivator>());
    testContext.Services.AddOptions();

    var mockJsRuntime = new BunitJsRuntime();
    testContext.Services.AddSingleton<Microsoft.JSInterop.IJSRuntime>(mockJsRuntime);

    var component = testContext.RenderComponent<DialogComponent>();
    component.Find(".confirm-btn").Click();

    // Verify interop was called
    mockJsRuntime.VerifyInvoke("confirm", "Are you sure?", Times.Once);
}
```

## See Also

* [bUnit Official Documentation](https://bunit.dev/docs/getting-started/)
* [bUnit GitHub Repository](https://github.com/bUnit-dev/bUnit)
* [Test Blazor components](https://learn.microsoft.com/en-us/aspnet/core/blazor/test)
* [Syncfusion Blazor DataGrid](https://blazor.syncfusion.com/documentation/datagrid/)

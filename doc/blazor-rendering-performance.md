# Blazor Rendering Performance with Popular Syncfusion Components

The Blazor rendering engine efficiently constructs, diffs, and applies UI updates. This guide explains the core rendering mechanics and demonstrates performance optimization patterns using the **most demanded Syncfusion components**: `SfGrid`, `SfChart`, `SfDropDownList`, `SfDatePicker`, `SfSpinner`, and `SfToast`.

---

## Prerequisites

Ensure your project has Syncfusion Blazor configured:

```bash
dotnet add package Syncfusion.Blazor
dotnet add package Syncfusion.Blazor.Themes
```

Add to `_Imports.razor`:

```razor
@using Syncfusion.Blazor
@using Syncfusion.Blazor.Grids
@using Syncfusion.Blazor.Charts
@using Syncfusion.Blazor.Dropdowns
@using Syncfusion.Blazor.Calendars
@using Syncfusion.Blazor.Spinner
@using Syncfusion.Blazor.Notifications
```

Register in `Program.cs`:

```csharp
builder.Services.AddSyncfusionBlazor();
```

---

## Overview

- [The Render Tree](#the-render-tree)
- [Diffing Algorithm](#diffing-algorithm)
- [Update Batching](#update-batching)
- [Reducing Render Costs](#reducing-render-costs)
  - [ShouldRender with SfGrid](#shouldrender-with-sfgrid)
  - [Stable Parameters with SfChart](#stable-parameters-with-sfchart)
  - [Grid Virtualization](#grid-virtualization)
  - [Async Loading States](#async-loading-states)
  - [Component Decomposition](#component-decomposition)
  - [Avoid Lambda Captures](#avoid-lambda-captures)
  - [Background Updates with SfToast](#background-updates-with-sftoast)
- [Best Practices](#best-practices)

---

## The Render Tree

Blazor maintains an in-memory **render tree** — a lightweight representation of the UI. Each `.razor` file is compiled into a `BuildRenderTree()` method that emits `RenderTreeFrame` structures.

| Frame Type | Example |
|---|---|
| `Component` | `SfGrid`, `SfChart`, `SfDropDownList` |
| `Element` | `<div>`, `<table>`, `<form>` |
| `Attribute` | `Value="@_selectedId"`, `@onclick="Handle"` |
| `Text` | Literal text nodes |

On the **first render**, the full tree is recorded. On **subsequent renders**, only the diff is applied — minimizing mutations.

> **Note:**  
> `BuildRenderTree` is compiler-generated and automatic. Never call it directly.

---

## Diffing Algorithm

Blazor compares the previous render tree to the new one and applies only the changes.

### Type Identity

If a component type changes at the same position, Blazor destroys and recreates the entire subtree.

```razor
@* Bad: conditionally swapping component types *@
@if (_useGrid)
{
    <SfGrid DataSource="@_data" />
}
else
{
    <SfChart Title="Data Chart" />  @* Full teardown/rebuild on toggle *@
}
```

### The `@key` Directive

Within `@foreach` loops, use `@key` to enable identity-based matching instead of positional matching. This prevents unnecessary re-renders when items are added, removed, or reordered.

```razor
@* Correct: use @key for stable identity *@
@foreach (var order in Orders)
{
    @* With @key, only the affected row re-renders when list changes *@
    <OrderRow @key="order.OrderID" Order="order" />
}
```

> **Important:**  
> The `@key` must be unique and stable. Never use array indexes as keys.

---

## Update Batching

Blazor batches all state mutations within a single event handler or async continuation into **one render pass**.

```csharp
private void RefreshDashboard()
{
    _title = "Q1 Report";           // mutation 1
    _recordCount = _orders.Count;   // mutation 2
    _lastRefresh = DateTime.Now;    // mutation 3
    // All three → ONE render, NOT three
}
```

### Async Rendering

When `await` is used in a lifecycle method, Blazor may render **twice**: once at the suspension point and once on continuation. This is intentional — it enables loading states.

```razor
@if (_isLoading)
{
    <SfSpinner Visible="true" Label="Loading…" Type="SpinnerType.Fluent2" />
}
else
{
    @* This content shown after async work completes *@
    <SfGrid DataSource="@_orders" AllowPaging="true" Height="400px">
        <GridColumns>
            <GridColumn Field="OrderID"    HeaderText="Order ID"   IsPrimaryKey="true" Width="120" />
            <GridColumn Field="CustomerID" HeaderText="Customer"   Width="150" />
            <GridColumn Field="Freight"    HeaderText="Freight"    Format="C2" Width="120" />
        </GridColumns>
    </SfGrid>
}

@code {
    private bool _isLoading = true;
    private List<Order> _orders = [];

    protected override async Task OnInitializedAsync()
    {
        // FIRST render: _isLoading=true (spinner shown)
        _orders    = await OrderService.GetAllAsync();
        _isLoading = false;
        // SECOND render: grid shown
    }
}
```

> **Tip:**  
> Show a loading spinner during async initialization for a polished UX.

---

## Reducing Render Costs

### ShouldRender with SfGrid

Override `ShouldRender()` to skip re-renders when output hasn't changed. Useful for grids that update infrequently.

```razor
@* SalesGrid.razor — skip re-render if data period hasn't changed *@

<SfGrid DataSource="@GridData" AllowPaging="true" Height="400px">
    <GridColumns>
        <GridColumn Field="Month"     HeaderText="Month"   Width="120" />
        <GridColumn Field="Sales"     HeaderText="Sales"   Format="C2" Width="120" />
        <GridColumn Field="Quantity"  HeaderText="Qty"     Width="100" />
    </GridColumns>
</SfGrid>

@code {
    [Parameter] public string Period { get; set; } = string.Empty;
    [Parameter] public List<SalesData> GridData { get; set; } = [];

    private string _previousPeriod = string.Empty;

    protected override void OnParametersSet()
    {
        _previousPeriod = Period;
    }

    protected override bool ShouldRender()
    {
        // Re-render only when Period actually changes
        return Period != _previousPeriod;
    }

    public class SalesData
    {
        public string Month    { get; set; } = string.Empty;
        public decimal Sales   { get; set; }
        public int Quantity    { get; set; }
    }
}
```

> **Important:**  
> `ShouldRender` is NOT called on first render. Only use it to suppress re-renders on known unchanged state.

---

### Stable Parameters with SfChart

Blazor compares `[Parameter]` values by reference for objects. Passing a new object on every render forces a re-render, even if content is identical.

**Anti-pattern:**

```razor
@* Allocates new ChartData object on EVERY parent render *@
<ChartWrapper ChartData="new() { Title = "Sales", Values = _values }" />
```

**Correct:**

```razor
@* Allocates ChartData once, reuses the same reference *@
<ChartWrapper ChartData="_chartConfig" />

@code {
    // Allocated once in initialization
    private readonly ChartConfig _chartConfig = new()
    {
        Title = "Sales",
        Threshold = 1000
    };

    private record ChartConfig(string Title, int Threshold);
}
```

> **Tip:**  
> Use C# `record` types for complex parameters. Records provide structural equality by default.

---

### Grid Virtualization

For large datasets (100+ rows), enable **virtualization** on `SfGrid` to render only visible rows. This dramatically reduces render cost regardless of dataset size.

```razor
@* LargeOrderGrid.razor — virtualized grid for 10,000+ rows *@

<SfGrid DataSource="@_orders"
        EnableVirtualization="true"
        Height="500px"
        AllowPaging="true">

    <GridPageSettings PageSize="50" />

    <GridColumns>
        <GridColumn Field="OrderID"    HeaderText="Order ID"   IsPrimaryKey="true" Width="120" />
        <GridColumn Field="CustomerID" HeaderText="Customer"   Width="150" />
        <GridColumn Field="OrderDate"  HeaderText="Date"       Format="d"
                    Type="ColumnType.Date" Width="120" />
        <GridColumn Field="Freight"    HeaderText="Freight"    Format="C2" Width="100" />
    </GridColumns>

</SfGrid>

@code {
    private List<Order> _orders = [];

    protected override void OnInitialized()
    {
        // Generate large dataset
        _orders = Enumerable.Range(1, 50_000).Select(i => new Order
        {
            OrderID    = i,
            CustomerID = $"CUST{i:D5}",
            OrderDate  = DateTime.Today.AddDays(-i % 365),
            Freight    = Math.Round(i * 0.37 % 500, 2)
        }).ToList();
    }

    public class Order
    {
        public int      OrderID    { get; set; }
        public string   CustomerID { get; set; } = string.Empty;
        public DateTime OrderDate  { get; set; }
        public double   Freight    { get; set; }
    }
}
```

> **Note:**  
> `EnableVirtualization="true"` requires a fixed `Height` on `SfGrid`.

---

### Async Loading States

Use `SfSpinner` for full-area loading and conditional rendering to handle async initialization gracefully.

```razor
@* OrderDashboard.razor — two-phase async load *@

<div style="position: relative; min-height: 300px;">

    <SfSpinner @bind-Visible="_isLoading"
               Label="Fetching orders…"
               Type="SpinnerType.Fluent2"
               Size="50px" />

    @if (!_isLoading && _orders != null)
    {
        <SfGrid DataSource="@_orders"
                AllowPaging="true"
                AllowSorting="true"
                AllowFiltering="true"
                Height="400px">

            <GridColumns>
                <GridColumn Field="OrderID"     HeaderText="Order ID"  IsPrimaryKey="true" Width="120" />
                <GridColumn Field="CustomerID"  HeaderText="Customer"  Width="150" />
                <GridColumn Field="OrderDate"   HeaderText="Date"      Format="d" Width="120" />
                <GridColumn Field="Freight"     HeaderText="Freight"   Format="C2" Width="120" />
            </GridColumns>

            <GridPageSettings PageSize="20" />

        </SfGrid>
    }

</div>

@code {
    private bool _isLoading = true;
    private List<Order>? _orders;

    protected override async Task OnInitializedAsync()
    {
        // Spinner shown during fetch
        _orders    = await OrderService.GetAllAsync();
        _isLoading = false;
        // Grid rendered after await
    }

    public class Order
    {
        public int      OrderID    { get; set; }
        public string   CustomerID { get; set; } = string.Empty;
        public DateTime OrderDate  { get; set; }
        public double   Freight    { get; set; }
    }
}
```

---

### Component Decomposition

Large monolithic pages re-render their entire subtree on ANY state change. Decompose into focused sub-components so only affected sections re-render.

**Monolithic (bad):**

```razor
@* Everything in one component — any state change → full re-render *@
<SfGrid DataSource="@_orders" />
<SfDropDownList DataSource="@_customers" @bind-Value="_selectedCustomer" />
<SfChart Title="Sales" @bind-SelectedData="_selectedChartData" />
```

**Decomposed (good):**

```razor
@* OrderPage.razor — orchestrates independent components *@

@* Grid updates don't affect dropdown or chart *@
<OrderGridWrapper Orders="_orders" OnOrderSelect="HandleOrderSelect" />

@* Dropdown updates don't affect grid or chart *@
<CustomerFilterWrapper Customers="_customers" 
                       OnSelectionChanged="HandleCustomerFilter" />

@* Chart updates independently *@
<SalesChartWrapper Data="_chartData" />

@code {
    private List<Order> _orders = [];
    private List<Customer> _customers = [];
    private List<ChartData> _chartData = [];

    private void HandleOrderSelect(int orderId) { /* ... */ }
    private void HandleCustomerFilter(int customerId) { /* ... */ }
}
```

> **Best Practice:**  
> Keep pages under 200 lines. Host each major Syncfusion component (SfGrid, SfChart, SfDropDownList) in its own wrapper component.

---

### Avoid Lambda Captures

Inline lambdas create a new delegate on every parent render, forcing child component re-renders even when parameters haven't changed.

**Anti-pattern (allocates new lambda each render):**

```razor
@foreach (var order in _orders)
{
    <OrderRow Order="order"
              OnDelete="async () => await DeleteOrderAsync(order.OrderID)" />
}
```

**Correct (stable method reference):**

```razor
@foreach (var order in _orders)
{
    @* @key + method reference = stable delegate across renders *@
    <OrderRow @key="order.OrderID"
              Order="order"
              OnDelete="DeleteOrderAsync" />
}

@code {
    // Child component passes the ID to this method
    private async Task DeleteOrderAsync(int orderId)
    {
        _orders.RemoveAll(o => o.OrderID == orderId);
        
        // Show confirmation toast
        await _toastRef?.ShowAsync(new ToastModel
        {
            Title   = "Order Deleted",
            Content = $"Order #{orderId} removed successfully."
        });
    }

    private SfToast? _toastRef;
}
```

> **Note:**  
> `EventCallback<T>` compares delegates by target and method pointer. Method groups are stable; lambdas are not.

---

### Background Updates with SfToast

When state changes occur **outside** Blazor's event pipeline (background timers, external service callbacks), use `InvokeAsync(StateHasChanged)` to marshal the update back to the render thread.

```razor
@* OrderNotifier.razor — shows incoming orders with SfToast *@
@implements IDisposable

<SfToast @ref="_toastRef" AutoClose="true" ShowCloseButton="true">
</SfToast>

<div>
    <p>Monitored orders: @_notificationCount</p>
</div>

@code {
    private int _notificationCount = 0;
    private SfToast? _toastRef;

    protected override void OnInitialized()
    {
        // Subscribe to external order service events
        // (fires on a background thread)
        OrderNotificationService.OnNewOrder += HandleNewOrder;
    }

    private void HandleNewOrder(Order order)
    {
        _notificationCount++;

        // InvokeAsync marshals to Blazor thread, schedules one render
        InvokeAsync(async () =>
        {
            if (_toastRef != null)
            {
                await _toastRef.ShowAsync(new ToastModel
                {
                    Title = "New Order",
                    Content = $"Order #{order.OrderID} from {order.CustomerID}"
                });
            }
        });
    }

    public void Dispose()
    {
        // Always unsubscribe to prevent memory leaks
        OrderNotificationService.OnNewOrder -= HandleNewOrder;
    }
}
```

> **Critical:**  
> Always use `InvokeAsync(StateHasChanged)` when updates come from background threads. Calling `StateHasChanged` directly from a non-Blazor thread throws `SynchronizationContextException` on Blazor Server.

---

## Best Practices

| Technique | Impact | When to Use |
|---|---|---|
| Use `@key` in `@foreach` | **High** | Any loop over mutable collections |
| Override `ShouldRender` | **High** | Grids/charts with stable data |
| Stable parameter refs / records | **High** | Parent → child parameters (objects) |
| Enable `SfGrid` virtualization | **Very High** | Datasets > 50–100 rows |
| Decompose large pages | **High** | Pages hosting multiple Syncfusion controls |
| Avoid inline lambda handlers | **Medium** | Event handlers in loops |
| Use `InvokeAsync(StateHasChanged)` | **Critical** | Background thread updates |
| Show loading spinner during async | **Medium** | Any `OnInitializedAsync` with delays |

> **Tip:**  
> Profile first! Use Blazor WebAssembly DevTools (Chrome) or `dotnet-trace` (Blazor Server) to identify slow components before optimizing.

---

## See Also

- [Syncfusion Blazor Grid — Virtualization](https://blazor.syncfusion.com/documentation/datagrid/virtualization)
- [Syncfusion Blazor Chart](https://blazor.syncfusion.com/documentation/chart/getting-started)
- [Syncfusion Blazor DropDownList](https://blazor.syncfusion.com/documentation/dropdown-list/getting-started)
- [Syncfusion Blazor DatePicker](https://blazor.syncfusion.com/documentation/date-picker/getting-started)
- [Syncfusion Blazor Spinner](https://blazor.syncfusion.com/documentation/spinner/getting-started)
- [Syncfusion Blazor Toast](https://blazor.syncfusion.com/documentation/toast/getting-started)
- [ASP.NET Core Blazor Rendering — Microsoft Docs](https://learn.microsoft.com/aspnet/core/blazor/components/rendering)
- [ASP.NET Core Blazor Performance — Microsoft Docs](https://learn.microsoft.com/aspnet/core/blazor/performance)

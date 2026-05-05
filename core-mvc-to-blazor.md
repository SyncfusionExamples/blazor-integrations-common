# Migrating Common Syncfusion ASP.NET Core MVC Components to Blazor

## Overview

Migrating enterprise applications from ASP.NET Core MVC to Blazor represents a transition from a controller‑driven, request–response framework to a component‑based web UI model built entirely on .NET. Blazor enables developers to build interactive user interfaces using reusable Razor components with automatic UI updates and minimal JavaScript dependency.

This documentation explains how commonly used Syncfusion ASP.NET Core MVC components can be migrated to their Blazor equivalents, while preserving existing business logic and data models. The migration helps improve application maintainability, responsiveness, and long‑term scalability.

## Why migrate from ASP.NET Core MVC to Blazor?

ASP.NET Core MVC applications rely on **server‑side request handling**, **partial views**, and **AJAX callbacks** to update UI content. While this architecture is robust and well‑established, it can become increasingly complex to manage as applications grow more interactive and UI logic becomes distributed across controllers, views, and client scripts.

Blazor replaces the traditional request‑response interaction model with **event‑driven UI updates**, enables **reusable and composable components**, and aligns closely with modern .NET development practices. This makes Blazor the recommended migration path for long‑term application modernization and maintainability.

| Dimension                 | ASP.NET Core MVC                    | Blazor                                        |
| ------------------------- | ----------------------------------- | --------------------------------------------- |
| **Execution model**       | Stateless request / response        | Blazor Server (SignalR) or Blazor WebAssembly |
| **Hosting & deployment**  | IIS, containers, cloud              | Cloud, containers, IIS, or static hosting     |
| **UI technology**         | Razor Views with HTML / Tag Helpers | Razor components (HTML + C#)                  |
| **State management**      | TempData, ViewData, Session         | Component‑based state (in‑memory)             |
| **User interaction**      | Full page reload or AJAX callback   | Event‑driven UI updates                       |
| **Tooling & IDE support** | Visual Studio / VS Code             | Visual Studio / VS Code                       |
| **Scalability**           | Depends on server request handling  | Cloud‑native and horizontally scalable        |
| **Application updates**   | Requires redeploy for UI changes    | Blazor Server: immediate; WebAssembly: cached |

N> Blazor Server is commonly recommended for migrating existing ASP.NET Core MVC applications, as it preserves server‑side execution while enabling real‑time UI updates.

## Key architectural differences

| Concept      | ASP.NET Core MVC        | Blazor          |
| --------- | ----------- | ------------ |
| **UI definition**        | `.cshtml` Razor views            | `.razor` component files                                                                                         |
| **Code‑behind pattern**  | Controllers and ViewModels       | `@code {}` block or `.razor.cs` file                                                                             |
| **Lifecycle model**      | Request‑based (per HTTP request) | Component lifecycle methods: `OnInitialized{Async}`, `OnParametersSet{Async}`, `OnAfterRender{Async}`, `Dispose` |
| **State handling**       | TempData, ViewData, Session      | In‑memory component state                                                                                        |
| **Event handling**       | Form post / AJAX callbacks       | `EventCallback<T>` / delegates                                                                                   |
| **Dependency injection** | Built‑in but controller‑centric  | Built‑in and component‑centric                                                                                   |
| **Navigation model**     | Controller‑based routing         | SPA‑style routing using `@page` directive                                                                        |

## Development Environment Setup

### Prerequisites

* [.NET 8 SDK or later](https://dotnet.microsoft.com/en-us/download/dotnet)
* [Visual Studio](https://visualstudio.microsoft.com/downloads/) 2022 or later or [Visual Studio Code](https://code.visualstudio.com/) with [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) extension

Verify installation using the following .NET CLI command. Verify that the .NET version is 8.0.0 or later.

{% tabs %}
{% highlight bash tabtitle=".NET CLI" %}

dotnet --version
dotnet --info

{% endhighlight %}
{% endtabs %}

### Project structure comparison

The following table maps common **ASP.NET Core MVC application artifacts** to their **Blazor equivalents**, highlighting how application structure transitions from a controller‑centric model to a component‑based architecture.

| Concept                 | ASP.NET Core MVC artifact      | Blazor equivalent                          |
| ----------------------- | ------------------------------ | ------------------------------------------ |
| **UI definition**       | Razor Views (`Views/*.cshtml`) | Razor components (`Pages/*.razor`)         |
| **Controller logic**    | `Controllers/*.cs`             | Component code (`@code {}` or `.razor.cs`) |
| **Application startup** | `Program.cs` (MVC middleware)  | `Program.cs` (Razor components)            |
| **Layout**              | `_Layout.cshtml`               | `MainLayout.razor`                         |
| **Reusable UI**         | Partial Views                  | Razor child components                     |
| **Static assets**       | `wwwroot`                      | `wwwroot`                                  |
| **State handling**      | TempData / ViewData / Session  | Component fields and injected services     |

N> Existing domain models, data access layers, and business services used in ASP.NET Core MVC applications can be reused directly in Blazor without modification.

## Creating a Blazor project

### Creating a Blazor Web App with Interactive Server

For Web Forms migrations, create a **Blazor Web App with Interactive Server** option, which runs server-side and preserves the familiar server-hosted execution model with real-time interactivity via SignalR:

{% tabs %}
{% highlight bash tabtitle=".NET CLI" %}

dotnet new blazor -n MyBlazorApp --interactivity Server
cd MyBlazorApp
dotnet watch   # Hot reload during development

{% endhighlight %}
{% endtabs %}

N> The `--interactivity Server` flag configures SignalR-based interactivity, providing immediate UI updates similar to Web Forms postback behavior, but over a persistent connection instead of full page reloads.

## Migrating Syncfusion Components from ASP.NET Core MVC to Blazor

This section provides **step-by-step migration guidance** for the following Syncfusion components:

*   **DataGrid**
*   **Scheduler**
*   **Rich Text Editor**

Each component includes package installation, configuration, and side-by-side comparisons.

## DataGrid

### Migration Overview

In ASP.NET Core MVC, the Syncfusion DataGrid relies on **server‑side processing** and UI refresh through page loads or AJAX calls.

In Blazor, the DataGrid is a **stateful Razor component** that reacts automatically to data changes, eliminating the need for explicit refresh logic.



### Feature Comparison

| Feature     | MVC Grid                  | Blazor Grid            |
| ----------- | ------------------------- | ---------------------- |
| Package     | Syncfusion.AspNetCore.Mvc | Syncfusion.Blazor.Grid |
| Declaration | HTML Helper               | `<SfGrid>`             |
| Data Source | ViewModel                 | Component State        |
| Events      | HTTP callbacks            | EventCallback          |
| Refresh     | Manual                    | Automatic              |



### Step‑by‑Step Migration

**MVC**

```cshtml
@Html.EJS().Grid("OrdersGrid")
    .DataSource(Model.Orders)
    .AllowPaging()
    .Render()
```

**Blazor**

```razor
<SfGrid DataSource="@Orders" AllowPaging="true">
    <GridColumns>
        <GridColumn Field="OrderID" HeaderText="Order ID" />
        <GridColumn Field="CustomerName" HeaderText="Customer" />
    </GridColumns>
</SfGrid>

@code {
    List<Order> Orders = new();
}
```

> **Best Practice:** Load large datasets using async services and paging or virtualization.



## Charts

### Migration Overview

MVC Charts depend on JavaScript rendering with data refreshed through controller actions.

Blazor Charts bind directly to component data and update automatically when the underlying collection changes.



### Feature Comparison

| Feature      | MVC Chart                 | Blazor Chart             |
| ------------ | ------------------------- | ------------------------ |
| Package      | Syncfusion.AspNetCore.Mvc | Syncfusion.Blazor.Charts |
| Rendering    | JS‑based                  | Component‑based          |
| Data Updates | Server refresh            | State change             |



### Step‑by‑Step Migration

**MVC**

```cshtml
@Html.EJS().Chart("SalesChart")
    .Series(s => s.DataSource(Model.Sales).Add())
    .Render()
```

**Blazor**

```razor
<SfChart>
    <ChartSeriesCollection>
        <ChartSeries DataSource="@Sales"
                     XName="Month"
                     YName="Amount"
                     Type="ChartSeriesType.Column" />
    </ChartSeriesCollection>
</SfChart>

@code {
    List<SalesData> Sales = new();
}
```



## Scheduler

### Migration Overview

In MVC, Scheduler views and appointments are refreshed using postbacks or AJAX callbacks.

The Blazor Scheduler maintains appointments in memory and rerenders instantly when state changes.



### Feature Comparison

| Feature    | MVC Scheduler             | Blazor Scheduler           |
| ---------- | ------------------------- | -------------------------- |
| Package    | Syncfusion.AspNetCore.Mvc | Syncfusion.Blazor.Schedule |
| Data Flow  | Server‑centric            | Component‑centric          |
| Navigation | AJAX                      | Instant                    |



### Step‑by‑Step Migration

**MVC**

```cshtml
@Html.EJS().Schedule("Schedule")
    .EventSettings(e => e.DataSource(Model.Events))
    .Render()
```

**Blazor**

```razor
<SfSchedule TValue="Meeting">
    <ScheduleEventSettings DataSource="@Meetings" />
</SfSchedule>

@code {
    List<Meeting> Meetings = new();
}
```

> **Note:** Ensure meeting models match the Scheduler’s default field mapping.



## Rich Text Editor (RTE)

### Migration Overview

MVC RTE content is typically reloaded on each request and processed server‑side.

Blazor RTE uses **two‑way data binding**, enabling live editing without round trips.



### Feature Comparison

| Feature          | MVC RTE                   | Blazor RTE                       |
| ---------------- | ------------------------- | -------------------------------- |
| Package          | Syncfusion.AspNetCore.Mvc | Syncfusion.Blazor.RichTextEditor |
| Content Handling | Server lifecycle          | @bind‑Value                      |
| User Experience  | Reload‑based              | Live                             |



### Step‑by‑Step Migration

**MVC**

```cshtml
@Html.EJS().RichTextEditor("Editor")
    .Value(Model.Content)
    .Render()
```

**Blazor**

```razor
<SfRichTextEditor @bind-Value="Content" Height="300px" />

@code {
    string Content = "Welcome to Syncfusion";
}
```



## Common Migration Scenarios

*   Partial Views → Razor Components
*   AJAX calls → Async EventCallback
*   ViewData/ViewBag → Component Parameters
*   JavaScript‑heavy UI → Native Blazor events

> **Important:** Migrate **Grid‑centric and form‑heavy pages first** to maximize ROI.



## See Also

*   Blazor Component Lifecycle
*   Syncfusion Blazor Component Library
*   Blazor Server Architecture
*   State Management in Blazor


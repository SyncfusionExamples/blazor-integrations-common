# Integrating Syncfusion DataGrid with Bold Reports (Step‑by‑Step)

This guide shows a minimal, Syncfusion‑aligned approach to pass SfGrid data into a Bold Reports (RDLC) viewer in a Blazor Server app. It follows the pattern and tone used in the project documentation and includes exact code snippets and PowerShell commands to reproduce the sample workspace state.

> Prerequisites
- .NET SDK installed
- Syncfusion and Bold Reports NuGet packages available (see csproj sections below)
- Working Blazor Server app

---

## 1. Project packages
Add the required NuGet packages in your .csproj:

```xml
<ItemGroup>
  <PackageReference Include="BoldReports.AspNet.Core" Version="12.2.6" />
  <PackageReference Include="BoldReports.Net.Core" Version="12.2.6" />
  <PackageReference Include="Microsoft.AspNetCore.Mvc.NewtonsoftJson" Version="10.0.3" />
  <PackageReference Include="Syncfusion.Blazor.Grid" Version="32.2.3" />
  <PackageReference Include="Syncfusion.Blazor.Buttons" Version="32.2.3" />
  <PackageReference Include="Syncfusion.Blazor.Themes" Version="32.2.3" />
</ItemGroup>
```

Run (PowerShell):

```powershell
dotnet restore; dotnet build
```

---

## 2. Services and app startup (Program.cs)
Register required services and enable interactive server render mode.

Key points:
- Add Syncfusion services
- Add controllers with NewtonsoftJson for Bold Reports
- Add memory cache and HttpClient
- Map controllers and Razor components

Example excerpt:

```csharp
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.MapControllers();
```

Note: stop any running process before rebuild to avoid file locks:

```powershell
Stop-Process -Name "BlazorApp" -Force; dotnet clean; dotnet build; dotnet run
```

---

## 3. App shell and scripts (Components/App.razor)
Ensure the shell loads Syncfusion and Bold Reports resources and uses InteractiveServer render mode.

- Use `<Routes @rendermode="InteractiveServer" />`
- Include Syncfusion CSS and Bold Reports CSS/JS (exact CDN links)
- Reference a small interop JS (`wwwroot/scripts/boldreports-interop.js`)

Example lines:

```html
<link href="_content/Syncfusion.Blazor.Themes/bootstrap5.css" rel="stylesheet" />
<link href="https://cdn.boldreports.com/12.2.6/content/v2.0/tailwind-light/bold.report-viewer.min.css" rel="stylesheet" />

<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
<script src="https://cdn.boldreports.com/12.2.6/scripts/v2.0/common/bold.reports.common.min.js"></script>
<script src="https://cdn.boldreports.com/12.2.6/scripts/v2.0/common/bold.reports.widgets.min.js"></script>
<script src="https://cdn.boldreports.com/12.2.6/scripts/v2.0/bold.report-viewer.min.js"></script>

<script src="@Assets["scripts/boldreports-interop.js"]"></script>
```

---

## 4. Interop (wwwroot/scripts/boldreports-interop.js)
Create a small interop used from Blazor to initialize the viewer. Example:

```javascript
window.BoldReports = {
  renderViewer: function (elementId, options) {
    console.log('[BoldReports] renderViewer called', options);
    $("#" + elementId).boldReportViewer({
      reportServiceUrl: options.serviceUrl,
      processingMode: "Local",
      reportPath: options.reportPath
    });
  },

  postDataAndRender: async function (orders, serviceUrl, reportPath, elementId) {
    // Optional: POST from browser so you can see Network calls
    const resp = await fetch('/api/BoldReportsAPI/SetReportData', {
      method: 'POST', headers: {'Content-Type':'application/json'}, body: JSON.stringify({ dataSources: orders })
    });
    if (!resp.ok) throw new Error('POST failed: ' + resp.status);
    $("#" + elementId).empty();
    $("#" + elementId).boldReportViewer({ reportServiceUrl: serviceUrl, processingMode: 'Local', reportPath: reportPath });
  }
};
```

---

## 5. Component UI (Home.razor)
Use Syncfusion components and a Syncfusion button. Use NavigationManager to build absolute URIs for server HttpClient.

Key points:
- Render SfGrid bound to Orders list
- Use SfButton with OnClick="@OpenReport"
- In OpenReport build absolute URL with NavigationManager.BaseUri and POST using HttpClient
- Then call JS interop to render viewer

Example (important parts):

```razor
@page "/"
@using Syncfusion.Blazor.Grids
@inject IJSRuntime JS
@inject HttpClient Http
@inject NavigationManager Nav

<SfGrid DataSource="@Orders" />
<SfButton OnClick="@OpenReport">Open RDLC Report</SfButton>

@code {
  public List<Order> Orders { get; set; } = new();

  private async Task OpenReport() {
    var dataModel = new { DataSources = Orders };
    var baseUrl = Nav.BaseUri.TrimEnd('/');
    var url = $"{baseUrl}/api/BoldReportsAPI/SetReportData".Replace("//api","/api");
    var response = await Http.PostAsJsonAsync(url, dataModel);
    response.EnsureSuccessStatusCode();

    var viewerOptions = new { serviceUrl = "/api/BoldReportsAPI", reportPath = "Orders.rdlc" };
    await JS.InvokeVoidAsync("BoldReports.renderViewer", "viewer", viewerOptions);
  }
}
```

Notes
- Server‑side HttpClient requests aren’t visible in browser DevTools. Use `postDataAndRender` to POST from browser and inspect Network.

---

## 6. Bold Reports API controller (Controllers/BoldReportsAPIController.cs)
Implement IReportController, convert incoming list to a DataSet, cache it, load RDLC, and bind the DataTable on OnReportLoaded.

Important behavior:
- `SetReportData` accepts posted JSON, converts to DataTable named exactly as RDLC DataSet (OrdersDataSet)
- `OnInitReportOptions` loads the .rdlc stream from `wwwroot/Reports/Orders.rdlc`
- `OnReportLoaded` sets `options.ReportModel.DataSources = new ReportDataSourceCollection { new ReportDataSource("OrdersDataSet", table) };`

Why this matters
- RDLC tablix uses DataSet name; the name must match exactly. Also include required tablix hierarchies in RDLC (see next section).

---

## 7. RDLC file requirements (wwwroot/Reports/Orders.rdlc)
Ensure dataset and tablix definitions match the runtime data:
- DataSet Name must equal the controller-bound name: `OrdersDataSet`
- Fields: OrderID (Int32), CustomerID (String), OrderDate (DateTime), Freight (Double)
- Tablix must include `TablixColumnHierarchy` and `TablixRowHierarchy` nodes (missing these causes parsing exceptions)

Example fragment (Tablix row/column hierarchy present):

```xml
<Tablix ...>
  <TablixBody> ... </TablixBody>
  <TablixColumnHierarchy>
    <TablixMembers>
      <TablixMember />
      <TablixMember />
      <TablixMember />
      <TablixMember />
    </TablixMembers>
  </TablixColumnHierarchy>
  <TablixRowHierarchy>
    <TablixMembers>
      <TablixMember>
        <KeepWithGroup>After</KeepWithGroup>
        <RepeatOnNewPage>true</RepeatOnNewPage>
      </TablixMember>
      <TablixMember>
        <Group Name="Detail" />
      </TablixMember>
    </TablixMembers>
  </TablixRowHierarchy>
  <DataSetName>OrdersDataSet</DataSetName>
</Tablix>
```

---

## 8. Troubleshooting checklist
- Button click not firing
  - Ensure `<Routes @rendermode="InteractiveServer" />` and `OnClick="@OpenReport"` on SfButton.
- No network entry for POST
  - Server HttpClient posts run server‑side (not visible in browser). Use `postDataAndRender` for browser POSTs.
- Invalid request URI
  - Use NavigationManager.BaseUri to build an absolute URL or register HttpClient with BaseAddress.
- RDLC parsing error for tablix
  - Ensure the RDLC tablix includes both Column and Row hierarchies and DataSetName matches.
- Antiforgery / 403 on POST
  - Add `[IgnoreAntiforgeryToken]` to SetReportData if you post from browser without antiforgery token.

---

## 9. Example terminal sequence (PowerShell)

```powershell
Stop-Process -Name "BlazorApp" -Force; dotnet clean; dotnet build; dotnet run
# open the URL printed by dotnet run
```

---

## 10. Next steps and recommendations
- Add better error UI in the Blazor page to surface API errors returned by SetReportData.
- If you need client-side visibility, prefer `BoldReports.postDataAndRender` (browser POST path).
- Keep RDLC dataset names and tablix metadata in sync with the DataTable you produce in the controller.

---

If you want, I can commit this file into the repository (blazor‑boldreports‑syncfusion.md).
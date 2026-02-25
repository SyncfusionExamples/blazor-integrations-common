using MyBlazorApp.Components;
using Syncfusion.Blazor;
using ElectronNET.API;
using ElectronNET.API.Entities;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSyncfusionBlazor();
builder.Services.AddElectron();

builder.UseElectron(args, async () =>
{
    var options = new BrowserWindowOptions
    {
        Show = false,
        IsRunningBlazor = true,
        Width = 1200,
        Height = 800
    };

    if (OperatingSystem.IsWindows() || OperatingSystem.IsLinux())
    {
        options.AutoHideMenuBar = true;
    }

    var window = await Electron.WindowManager.CreateWindowAsync(options);
    window.OnReadyToShow += () => window.Show();

    window.OnClosed += () =>
    {
        Electron.App.Quit();
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Comment out for Electron dev to avoid mixed-content issues
// app.UseHttpsRedirection();

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<MyBlazorApp.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
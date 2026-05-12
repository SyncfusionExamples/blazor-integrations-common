using OrderManagementDashboard.Components;
using OrderManagementDashboard.Services;
using OrderManagementDashboard.Models;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Syncfusion Configuration
builder.Services.AddSyncfusionBlazor();


builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReturnRefundService, ReturnRefundService>();
builder.Services.AddScoped<IAbandonedCartService, AbandonedCartService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
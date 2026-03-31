using BlazorApp.Components;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
var builder = WebApplication.CreateBuilder(args);

// Register Syncfusion license key
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JHaF5cWWdCekx0Q3xbf1x2ZFdMYVRbQXNPMyBoS35RcEVgW3hecnVcR2dUVkVwVEFe");
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Configure SignalR to support large PDF file transfers
builder.Services.AddSignalR(o => { o.MaximumReceiveMessageSize = 102400000; });
//Add Syncfusion Blazor service to the container.
builder.Services.AddMemoryCache();
builder.Services.AddSyncfusionBlazor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

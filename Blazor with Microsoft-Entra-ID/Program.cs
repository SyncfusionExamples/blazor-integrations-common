using Microsoft_Entra_ID.Components;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Configure authentication with Microsoft Entra ID (Azure AD)
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
  .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization();

// Register Syncfusion Blazor services
builder.Services.AddSyncfusionBlazor();

// Add Razor Components (Blazor Server interactive)
builder.Services.AddRazorComponents()
  .AddInteractiveServerComponents();

// Add controllers with UI endpoints for Microsoft Identity (SignIn/SignOut)
builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
  .AddInteractiveServerRenderMode();

app.Run();

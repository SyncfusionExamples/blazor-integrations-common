using Microsoft_Entra_ID.Components;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Configure authentication with Microsoft Entra ID (Azure AD)
// Ensure configuration contains a path-only CallbackPath (avoid full-URL overrides)
builder.Configuration["AzureAd:CallbackPath"] = "/signin-oidc";

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
  .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

// Ensure the OIDC callback path is set to a path-only value to avoid config binding errors
builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.CallbackPath = new PathString("/signin-oidc");
});

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
app.MapRazorComponents<App>()
  .AddInteractiveServerRenderMode();

app.Run();

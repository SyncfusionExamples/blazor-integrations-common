using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR_SYNCFUSION_LICENSE_KEY");
builder.Services.AddSyncfusionBlazor();
// Register MSAL authentication so IAccessTokenProvider is available to components.
// Replace or configure the 'AzureAd' section in appsettings or provide ProviderOptions as needed.
builder.Services.AddMsalAuthentication(options =>
{
	builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
	// Example: options.ProviderOptions.DefaultAccessTokenScopes.Add("api://<your-api-client-id>/access_as_user");
});

// Use local Functions host during development so the client can call the API directly.
if (builder.HostEnvironment.IsDevelopment())
{
	builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7298/") });
}
else
{
	builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
}

await builder.Build().RunAsync();

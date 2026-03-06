using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using System.Net.Http.Headers;
using Client.Services;
using Client;
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.Services.AddSyncfusionBlazor();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddTransient<AuthMessageHandler>();

// Must match your Server base URL
var apiBase = new Uri("http://localhost:5005/");

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = apiBase;
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
})
.AddHttpMessageHandler<AuthMessageHandler>();

await builder.Build().RunAsync();
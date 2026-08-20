using BlazorRedisServer.Components;
using BlazorRedisServer.Services;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Surface real exception messages in the browser during development so
// connection / cache errors don't show up as opaque "circuit terminated" logs.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddServerSideBlazor()
        .AddCircuitOptions(o => o.DetailedErrors = true);
}

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSyncfusionBlazor();

// Register the Redis-backed IDistributedCache so EmployeeService can resolve it.
builder.Services.AddStackExchangeRedisCache(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Redis");
    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException(
            "Missing connection string 'Redis'. Add it under ConnectionStrings:Redis in appsettings.json.");
    }
    options.Configuration = connectionString;
    options.InstanceName = "BlazorRedisServer:";
    // Increase timeouts for Azure Redis to avoid connection issues
    var configOptions = StackExchange.Redis.ConfigurationOptions.Parse(connectionString);
    configOptions.ConnectTimeout = 10000;  // 10 seconds
    configOptions.SyncTimeout = 10000;     // 10 seconds
    options.ConfigurationOptions = configOptions;
});

// Sample domain service that uses the distributed (Redis) cache.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

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

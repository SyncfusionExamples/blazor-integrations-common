using BlazorIdentitySyncfusion.Components;
using BlazorIdentitySyncfusion.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// EF Core + SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ASP.NET Core Identity (cookie auth + default UI)
builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        // WARNING: Email confirmation is disabled for demo purposes only.
        // In production, set SignIn.RequireConfirmedAccount = true and configure
        // an email sender to prevent unauthorized account access.
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Razor Pages (Identity UI lives here)
builder.Services.AddRazorPages();

// Blazor auth state for <CascadingAuthenticationState>/<AuthorizeRouteView>
builder.Services.AddCascadingAuthenticationState();

// Register Syncfusion Blazor services
builder.Services.AddSyncfusionBlazor();

// Blazor components (interactive server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Authentication & Authorization middleware (order matters!)
app.UseAuthentication();
app.UseAuthorization();

// Antiforgery middleware (required for Identity Razor Pages login/logout forms in .NET 8+)
app.UseAntiforgery();

// Map endpoints
app.MapRazorPages(); // Identity UI endpoints
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
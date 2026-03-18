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
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Razor Pages (Identity UI lives here)
builder.Services.AddRazorPages();

// Blazor auth state for <CascadingAuthenticationState>/<AuthorizeRouteView>
builder.Services.AddCascadingAuthenticationState();

// Syncfusion
builder.Services.AddSyncfusionBlazor();

// Blazor components (interactive server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Identity + Authorization
app.UseAuthentication();
app.UseAuthorization();

// .NET 8 antiforgery middleware (required when endpoints have antiforgery metadata)
app.UseAntiforgery();

// Map endpoints
app.MapRazorPages(); // Identity UI endpoints
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
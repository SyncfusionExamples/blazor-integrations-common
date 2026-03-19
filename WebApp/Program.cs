using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<WeatherForecastService>();
var cognitoDomain = builder.Configuration["Cognito:Authority"];
var clientId = builder.Configuration["Cognito:ClientId"];

bool TryGetAuthorityUri(string? authority, out Uri? uri)
{
    uri = null;
    if (string.IsNullOrWhiteSpace(authority)) return false;
    if (authority.Contains("your-domain")) return false;
    // Require a valid absolute URI (e.g. https://your-domain.auth.region.amazoncognito.com)
    if (!Uri.TryCreate(authority, UriKind.Absolute, out var parsed)) return false;
    // only http or https are acceptable here for dev detection; production will require https
    if (parsed.Scheme != Uri.UriSchemeHttp && parsed.Scheme != Uri.UriSchemeHttps) return false;
    uri = parsed;
    return true;
}

// Decide whether to enable OIDC (Cognito) or fall back to cookie-only auth in Development.
bool useOidc = false;
if (!string.IsNullOrWhiteSpace(cognitoDomain)
    && !cognitoDomain.Contains("your-domain")
    && !string.Equals(cognitoDomain, "Test user", StringComparison.OrdinalIgnoreCase)
    && !string.IsNullOrWhiteSpace(clientId)
    && !clientId.Contains("YOUR_APP_CLIENT_ID"))
{
    if (Uri.TryCreate(cognitoDomain, UriKind.Absolute, out var u))
    {
        // Allow only HTTPS authority generally
        if (string.Equals(u.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
        {
            useOidc = true;
        }
        else if (string.Equals(u.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase)
                 && !u.IsLoopback
                 && builder.Environment.IsDevelopment())
        {
            // allow non-local http authority in development (rare)
            useOidc = true;
        }
    }
}

if (useOidc)
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        options.Authority = cognitoDomain!;          // Cognito Hosted UI domain
        options.ClientId = clientId!;                // App client ID
        options.ResponseType = "code";             // Authorization Code + PKCE
        options.SaveTokens = true;                  // persist tokens in auth session

        // If the authority is http and we're in Development, allow non-https metadata.
        options.RequireHttpsMetadata = !(builder.Environment.IsDevelopment()
            && Uri.TryCreate(cognitoDomain, UriKind.Absolute, out var uu)
            && string.Equals(uu.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase));

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("email");
        options.Scope.Add("profile");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "cognito:username",
            RoleClaimType = "cognito:groups"
        };

        options.CallbackPath = "/signin-oidc";
        options.SignedOutCallbackPath = "/signout-callback-oidc";
    });
}
else
{
    // Cookie-only auth for development/test when Cognito isn't configured.
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie();

    builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
}

// Register AuthenticationStateProvider for Blazor Server so AuthorizeView / CascadingAuthenticationState work
builder.Services.AddScoped<AuthenticationStateProvider, WebApp.Services.HttpContextAuthenticationStateProvider>();

// In Production require a valid HTTPS Cognito authority.
if (!builder.Environment.IsDevelopment())
{
    if (!TryGetAuthorityUri(cognitoDomain, out var prodUri) || prodUri!.Scheme != Uri.UriSchemeHttps || string.IsNullOrWhiteSpace(clientId) || clientId.Contains("YOUR_APP_CLIENT_ID"))
    {
        throw new InvalidOperationException(
            "Cognito configuration is invalid. Set 'Cognito:Authority' to your Cognito Hosted UI domain (https://<your-domain>.auth.<region>.amazoncognito.com) and 'Cognito:ClientId' to your app client id.");
    }

    // If we reach here, OIDC is already registered above when appropriate.
}

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
// Dev + auth endpoints for sign-in/sign-out to avoid performing SignIn/Challenge from Blazor components
app.MapGet("/signin", async (HttpContext ctx) =>
{
    if (useOidc)
    {
        await ctx.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
        return Results.Empty;
    }

    // Dev fallback: create a local cookie user and redirect home
    var claims = new[] {
        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, "devuser"),
        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "Developer User"),
        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, "dev@example.local")
    };
    var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var user = new System.Security.Claims.ClaimsPrincipal(identity);
    await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
    ctx.Response.Redirect("/");
    return Results.Empty;
});

app.MapGet("/signout", async (HttpContext ctx) =>
{
    if (useOidc)
    {
        await ctx.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
        return Results.Empty;
    }

    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    ctx.Response.Redirect("/");
    return Results.Empty;
});
app.MapFallbackToPage("/_Host");

app.Run();

using OAuth.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
// OpenID Connect removed; using GitHub OAuth only
using Microsoft.AspNetCore.Authentication;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
  .AddInteractiveServerComponents();

// Register Syncfusion Blazor services
builder.Services.AddSyncfusionBlazor();

// Configure authentication (Cookies + GitHub OAuth). GitHub is the default challenge.
builder.Services.AddAuthentication(options =>
{
  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = "GitHub";
})
  .AddCookie()
  .AddOAuth("GitHub", options =>
  {
    options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"] ?? "your-github-client-id";
    options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"] ?? "your-github-client-secret";
    options.CallbackPath = "/signin-github";
    options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
    options.TokenEndpoint = "https://github.com/login/oauth/access_token";
    options.UserInformationEndpoint = "https://api.github.com/user";
    options.Scope.Add("user:email");
    options.SaveTokens = true;

    options.ClaimActions.MapJsonKey("urn:github:login", "login");
    options.ClaimActions.MapJsonKey("urn:github:id", "id");
    options.ClaimActions.MapJsonKey("urn:github:url", "html_url");
    options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

    options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
    {
      OnCreatingTicket = async context =>
      {
        // GitHub requires a user-agent header
        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
        request.Headers.Accept.ParseAdd("application/json");
        request.Headers.UserAgent.ParseAdd("OAuthApp");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);

        var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
        response.EnsureSuccessStatusCode();
        var payload = System.Text.Json.JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        context.RunClaimActions(payload.RootElement);
      }
    };
  });

builder.Services.AddAuthorization();
// Add support for API controllers (used to proxy calls to protected APIs)
builder.Services.AddControllers();
// Register IHttpClientFactory for outbound HTTP calls
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

// Map API controllers
app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
  .AddInteractiveServerRenderMode();

app.Run();


# Authentication and Authorization with AWS Cognito in Blazor Server 

This guide explains how to authenticate a **Blazor Server** app using **Amazon Cognito User Pools** (and optionally **Identity Pools**) with the **Hosted UI** via **OpenID Connect (OIDC)**. You'll configure the User Pool & App client, wire OIDC in ASP.NET Core, handle tokens, protect API calls, enable MFA/password policies, and enforce **role-based authorization using Cognito Groups**. Code snippets are minimal and buildable on `.NET 10`.

## What is AWS?
Amazon Web Services (AWS) is a cloud platform offering compute, storage, databases, security, and many managed services. For identity, AWS provides Amazon Cognito to handle sign-up/sign-in, tokens, MFA, and issuing temporary AWS credentials via IAM. Cognito exposes managed login (Hosted UI) and standard OpenID Connect (OIDC) endpoints for modern auth flows.

## Why Amazon Cognito for Blazor?

* Standards-based OIDC: Works with ASP.NET Core's built-in OpenID Connect middleware for Blazor Server; no third-party libraries are necessary. This is the Microsoft-recommended pattern to connect non-Microsoft OIDC providers. 
* Hosted UI & MFA: Prebuilt, brandable login with MFA and password policies, reducing custom auth UI work. 
* Groups/roles: Emit cognito:groups in tokens for role-based authorization in your app and API. 
* Temporary AWS credentials (optional): Identity Pools can exchange a user's ID token for time-limited AWS credentials to call S3, DynamoDB, etc.

## Cognito building blocks

* **User Pools:** Managed user directory + OIDC authorization server (tokens, Hosted UI, MFA, groups). Use this to authenticate users and obtain ID/Access tokens for your app/APIs. 
* **Identity Pools (Federated Identities):** Optional service that exchanges a trusted identity (e.g., User Pool ID token) for temporary AWS credentials through IAM roles; use when your app (typically the server) must call AWS services on behalf of the user. 

## Prerequisites

* .NET 10  
*  Visual Studio 2022/2025 (latest) or VS Code + C# Dev Kit  
* AWS Account with permission to manage Cognito  

## Integrating Cognito with Blazor

Configure OpenID Connect with the Cognito Hosted UI (Authorization Code + PKCE), which Microsoft's docs show for any OIDC provider in Blazor Web Apps.

### Create project & add packages

```bash
# Create solution + Blazor Server project (net10.0)
mkdir BlazorCognitoServer && cd BlazorCognitoServer

dotnet new blazorserver -n WebApp -f net10.0

dotnet new sln -n BlazorCognitoServer

dotnet sln add WebApp/WebApp.csproj

# Add Syncfusion Blazor
cd WebApp

dotnet add package Syncfusion.Blazor
dotnet add package Microsoft.AspNetCore.Authentication.OpenIdConnect --version 10.*
```

### Update `appsettings.json`
```json
{
  "Cognito": {
    "Authority": "https://your-domain.auth.ap-south-1.amazoncognito.com",
    "ClientId": "YOUR_APP_CLIENT_ID"
  },
  "AllowedHosts": "*"
}
```

### `Program.cs` (OIDC + Cookies)
```csharp
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddHttpContextAccessor();

// Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR_LICENSE_KEY");

var cognitoDomain = builder.Configuration["Cognito:Authority"]!;
var clientId      = builder.Configuration["Cognito:ClientId"]!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.Authority = cognitoDomain;          // Cognito Hosted UI domain
    options.ClientId = clientId;                // App client ID
    options.ResponseType = "code";             // Authorization Code + PKCE
    options.SaveTokens = true;                  // persist tokens in auth session

    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("email");
    options.Scope.Add("profile");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "cognito:username",    // or "email"
        RoleClaimType = "cognito:groups"
    };

    options.CallbackPath = "/signin-oidc";
    options.SignedOutCallbackPath = "/signout-callback-oidc";
});

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
```

> Microsoft's **OpenID Connect** guidance for Blazor Web Apps shows this exact manual-configuration approach (authority, client id, code flow, cookie scheme) for non-Microsoft providers.

---

## Authentication flow

**Authorization Code + PKCE** (server-initiated):
1. App issues an OIDC **challenge** → browser redirects to Cognito's Hosted UI.  
2. User authenticates (password / MFA).  
3. Cognito redirects to `/signin-oidc` with an **authorization code**.  
4. Server exchanges code for **ID/Access** tokens and stores them (cookie session).  
5. Blazor Server uses `[Authorize]`, policies, and `AuthorizeView` to protect UI and endpoints.  

> AWS explains OIDC code flows and Hosted UI behavior; Microsoft docs cover Blazor's OIDC integration details.

## Using Hosted UI

- Configure a **domain** for the Hosted UI and set **callback** & **logout** URLs that exactly match your app.  
- Prefer **OIDC discovery** rather than hard-coding token/authorize endpoints.  
- Cognito's login/consent pages can be branded in the console.  

> Managed login/Hosted UI configuration and best practices are documented in Cognito's guides.

## Handling Tokens

- **ID Token**: identity & profile claims (e.g., `email`, `cognito:username`, `cognito:groups`) for display/authorization decisions.  
- **Access Token**: present to protected APIs as `Authorization: Bearer <token>`.  
- **Refresh Token**: available per App client settings; in Blazor Server, typical sessions rely on the auth cookie lifespan.  

> Blazor's security model and OIDC integration describe how tokens participate in auth flows; Cognito's OIDC/user-info scopes control issued claims. 

> **Note (Groups claim):** Ensure **Group membership** is enabled in token configuration. If `cognito:groups` seems missing, verify `openid` is requested and review your App client scope settings. 

## Calling Secured APIs

## Enabling MFA / Password Policies

In **User Pool → Sign-in experience**:
- Configure **Password policy** (length, complexity, expiration).  
- Turn **MFA** Off/Optional/Required; choose **SMS** or **TOTP** enrollment.  
- Hosted UI prompts users according to your policy.  

> Cognito's managed login and authentication-method docs cover the exact behavior and options. 

## Role-Based Authorization with Cognito Groups

1) Create groups (e.g., `Admin`) in **User Pool → Groups** and add users.  
2) Ensure **Group membership** is included in tokens.  
3) Map roles using `RoleClaimType = "cognito:groups"` and protect pages/endpoints with `[Authorize(Roles="Admin")]`.  

```razor
@page "/admin"
@attribute [Authorize(Roles = "Admin")]
<h3>Admin Dashboard</h3>
<p>Only users in the Admin group can access this page.</p>
```

> Group creation and behavior are documented in AWS; using roles in Blazor/ASP.NET Core follows Microsoft's standard policy system. 

## Troubleshooting

**Redirect loop**  
- Callback/Logout URLs must **exactly** match app configuration, and the **Authorization code** flow must be enabled. citeturn5search5

**403 from API**  
- Verify JWT `iss` (User Pool issuer) and `aud` (App client ID). Send `Authorization: Bearer <access_token>`. citeturn5search27

**Roles not applied**  
- Confirm `cognito:groups` in the token and set `RoleClaimType = "cognito:groups"`. citeturn5search10

**CORS** (if calling API cross-origin)  
- Configure API CORS for your Blazor origin. citeturn5search27

**Token lifetime/refresh**  
- Adjust token lifetimes in the App client; Blazor Server typically relies on cookie session. citeturn5search27

> **Advanced**: For complex token lifetimes/refresh and downstream calls from Blazor Server, see specialist guidance on **access-token management** in Blazor Server apps. (Third-party reference)

---

## Code Examples

### 1) Syncfusion DataGrid on an authenticated page
```razor
@page "/"
@attribute [Authorize]
@using Syncfusion.Blazor.Grids

<h3>Welcome</h3>
<p>You are signed in via AWS Cognito.</p>

<SfGrid DataSource="@_orders" AllowPaging="true" AllowSorting="true">
    <GridColumns>
        <GridColumn Field="Id" HeaderText="ID" Width="120"></GridColumn>
        <GridColumn Field="Item" HeaderText="Item"></GridColumn>
        <GridColumn Field="Qty" HeaderText="Qty"></GridColumn>
        <GridColumn Field="Price" HeaderText="Price" Format="C2" TextAlign="TextAlign.Right"></GridColumn>
    </GridColumns>
</SfGrid>

@code {
    private record Order(int Id, string Item, int Qty, decimal Price);
    private readonly List<Order> _orders =
    [
        new(1, "Laptop", 1, 1299.00m),
        new(2, "Mouse", 2, 49.99m)
    ];
}
```

### 2) Admin-only page (groups-based)
```razor
@page "/admin"
@attribute [Authorize(Roles = "Admin")]
<h3>Admin Dashboard</h3>
<p>Only users in the Cognito group <strong>Admin</strong> can access this page.</p>
```

### 3) Login/Logout endpoints
```razor
// Pages/Login.razor
@page "/login"
@using Microsoft.AspNetCore.Authentication
@using Microsoft.AspNetCore.Authentication.OpenIdConnect
@inject IHttpContextAccessor Ctx
<button class="btn btn-primary" @onclick="LoginAsync">Sign in</button>
@code {
    private Task LoginAsync() => Ctx.HttpContext!.ChallengeAsync(
        OpenIdConnectDefaults.AuthenticationScheme,
        new AuthenticationProperties { RedirectUri = "/" });
}
```

```razor
// Pages/Logout.razor
@page "/logout"
@using Microsoft.AspNetCore.Authentication
@using Microsoft.AspNetCore.Authentication.Cookies
@inject IHttpContextAccessor Ctx
<button class="btn btn-outline-secondary" @onclick="LogoutAsync">Sign out</button>
@code {
    private async Task LogoutAsync()
    {
        await Ctx.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await Ctx.HttpContext!.SignOutAsync("OpenIdConnect", new AuthenticationProperties { RedirectUri = "/" });
    }
}
```

---

## Quick start (Try it)

```bash
# 1) Create project (if not created yet)
mkdir BlazorCognitoServer && cd BlazorCognitoServer

dotnet new blazorserver -n WebApp -f net10.0

dotnet new sln -n BlazorCognitoServer

dotnet sln add WebApp/WebApp.csproj

# 2) Add Syncfusion
cd WebApp

dotnet add package Syncfusion.Blazor

# 3) Configure Cognito in appsettings.json (Authority, ClientId)
# 4) Run

dotnet run --urls https://localhost:5001
```

Open `https://localhost:5001`, navigate to `/login`, authenticate in the **Hosted UI**, and return to the app.

## Optional: Identity Pools (AWS credentials)

If your app needs **temporary AWS credentials** (e.g., S3/DynamoDB), create an **Identity Pool**, link the **User Pool** as an auth provider, and use IAM to grant access. For Blazor **Server**, perform AWS calls **server-side** and (if needed) exchange the **ID token** for credentials on the server. Refer to Cognito documentation for endpoints and managed login details.
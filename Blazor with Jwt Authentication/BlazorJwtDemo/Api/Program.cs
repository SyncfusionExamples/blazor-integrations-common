using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

// CORS: allow the client dev server
const string CorsPolicy = "DevClient";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy, policy =>
        policy.WithOrigins("http://localhost:5003")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// JWT setup (demo secret only!)
var jwtKey = builder.Configuration["Jwt:Key"] ?? "dev-secret-key-change-me-please-123456"; // use secrets in real apps
var issuer = builder.Configuration["Jwt:Issuer"] ?? "SyncGridJwtDemo";
var audience = builder.Configuration["Jwt:Audience"] ?? "SyncGridJwtDemoAudience";
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // dev only
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = signingKey,
        ClockSkew = TimeSpan.FromSeconds(5)
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseCors(CorsPolicy);
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/api/auth/login", (LoginRequest req) =>
{
    // Minimal demo check: in real life, validate against DB/Identity
    if (req is null || string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
        return Results.BadRequest("Username/password required");

    if (req.Username == "admin" && req.Password == "admin123")
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, req.Username),
            new Claim(ClaimTypes.Name, req.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return Results.Ok(new { token = jwt });
    }
    return Results.Unauthorized();
});



app.MapPost("/api/orders", ([FromBody] object _) =>
{
    var data = Order.Seed();
    return Results.Ok(new { result = data, count = data.Count });
}).RequireAuthorization();



app.Run();

record LoginRequest(string Username, string Password);

public record Order(int OrderID, string CustomerID, string ShipCountry)
{
    public static List<Order> Seed()
    {
        return new List<Order>
        {
            new Order(10001, "ALFKI", "Germany"),
            new Order(10002, "ANATR", "Brazil"),
            new Order(10003, "ANTON", "Mexico"),
            new Order(10004, "BERGS", "Sweden"),
            new Order(10005, "BLONP", "France"),
        };
    }
}
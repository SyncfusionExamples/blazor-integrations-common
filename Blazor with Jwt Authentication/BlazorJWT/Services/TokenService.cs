using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
namespace BlazorJWT.Services
{
  public class TokenService
  {
    private readonly IConfiguration _config;
    public TokenService(IConfiguration config) => _config = config;

    public string IssueToken(string subjectUserId, string? name = null)
    {
      var issuer = _config["Jwt:Issuer"]!;
      var audience = _config["Jwt:Audience"]!;
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, subjectUserId),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
      if (!string.IsNullOrWhiteSpace(name))
        claims.Add(new Claim(ClaimTypes.Name, name));

      var token = new JwtSecurityToken(
          issuer: issuer,
          audience: audience,
          claims: claims,
          notBefore: DateTime.UtcNow.AddMinutes(-1),
          expires: DateTime.UtcNow.AddMinutes(30),
          signingCredentials: creds);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}

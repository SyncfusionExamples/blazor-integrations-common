using BlazorJWT.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorJWT.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly TokenService _tokenService;
  public AuthController(TokenService tokenService) => _tokenService = tokenService;

  [HttpPost("token")]
  public IActionResult Token([FromQuery] string user = "user123")
  {
    var jwt = _tokenService.IssueToken(user, name: user);
    return Ok(new { token = jwt });
  }
}

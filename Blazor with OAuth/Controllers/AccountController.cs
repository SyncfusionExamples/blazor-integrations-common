using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace OAuth.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login(string? returnUrl = "/")
        {
            var props = new AuthenticationProperties { RedirectUri = returnUrl };
            // Challenge the GitHub scheme
            return Challenge(props, "GitHub");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // Sign out of the cookie and the OpenID Connect provider (if used)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}

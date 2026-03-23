using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorIdentitySyncfusion.Data;

/// <summary>
/// Database context for ASP.NET Core Identity. Inherits IdentityDbContext to include
/// tables for users, roles, claims, etc.
/// </summary>

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
}
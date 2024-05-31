using EFCoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIdentity.Context;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>,
    AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AppUserRole>().HasKey(x => new { x.RoleId, x.UserId });
        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
    }
}

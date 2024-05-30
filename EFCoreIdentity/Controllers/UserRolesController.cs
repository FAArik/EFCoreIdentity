using EFCoreIdentity.Context;
using EFCoreIdentity.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreIdentity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class UserRolesController(ApplicationDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Create(Guid userId, Guid roleId, CancellationToken cancellationToken)
        {
            AppUserRole appUserRole = new AppUserRole
            {
                UserId = userId,
                RoleId = roleId
            };
            await context.UserRoles.AddAsync(appUserRole);
            await context.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

    }
}

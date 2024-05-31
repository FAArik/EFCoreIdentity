using EFCoreIdentity.Context;
using EFCoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreIdentity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class UserRolesController(ApplicationDbContext context, UserManager<AppUser> userManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Create(Guid userId, string roleName, Guid roleID, CancellationToken cancellationToken)
        {
            AppUser? user = await userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                return BadRequest(new { Message = "Kullanıcı Bulunamadı" });
            }
            IdentityResult res = await userManager.AddToRoleAsync(user, roleName);
            if (!res.Succeeded)
            {
                return BadRequest(res.Errors.Select(x => x.Description));
            }
            return NoContent();
        }

    }
}

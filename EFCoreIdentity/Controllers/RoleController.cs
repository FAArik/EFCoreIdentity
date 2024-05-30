using EFCoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIdentity.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class RoleController(RoleManager<AppRole> _roleManager) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(string name, CancellationToken cancellationToken)
    {
        AppRole role = new AppRole
        {
            Name = name
        };
        IdentityResult res = await _roleManager.CreateAsync(role);
        if (!res.Succeeded)
        {
            return BadRequest(res.Errors.Select(x => x.Description));
        }
        return NoContent();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
        return Ok(roles);
    }
}



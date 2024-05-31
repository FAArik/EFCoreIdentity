using EFCoreIdentity.Dtos;
using EFCoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace EFCoreIdentity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class AuthController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto request, CancellationToken cancellationToken)
        {
            AppUser user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));
            }


            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user is null)
            {
                return BadRequest(new { Message = "Kullanıcı Bulunamadı" });
            }
            IdentityResult res = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!res.Succeeded)
            {
                return BadRequest(res.Errors.Select(x => x.Description));
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> ForgetPassword(string email, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return BadRequest(new { Message = "Kullanıcı Bulunamadı" });
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Ok(new { Token = token });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordUsingToken(ChangePasswordUsingTokenDto request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return BadRequest(new { Message = "Kullanıcı Bulunamadı" });
            }
            IdentityResult res = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            if (!res.Succeeded)
            {
                return BadRequest(res.Errors.Select(x => x.Description));
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.Users
                .FirstOrDefaultAsync(x =>
                    x.UserName == request.UserNameOrEmail ||
                    x.Email == request.UserNameOrEmail);
            if (user is null)
            {
                return BadRequest(new { Message = "Kullanıcı Bulunamadı" });
            }
            bool res = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!res)
            {
                return BadRequest(new { Message = "Kullanıcı adı veya şifre yanlış" });
            }

            return Ok(new { Token = "Token" });
        }
        [HttpPost]
        public async Task<IActionResult> LoginWithSigninManager(LoginDto request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.Users
                .FirstOrDefaultAsync(x =>
                    x.UserName == request.UserNameOrEmail ||
                    x.Email == request.UserNameOrEmail);
            if (user is null)
            {
                return BadRequest(new { Message = "Kullanıcı Bulunamadı" });
            }

            SignInResult res = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (res.IsLockedOut)
            {
                TimeSpan? timeSpan = user.LockoutEnd - DateTime.Now;
                if (timeSpan is not null)
                {
                    return StatusCode(500, new { Message = $"şifrenizi 3 kere yanlış girdiğiniz için kullanıcınız {Math.Round(timeSpan.Value.TotalSeconds, MidpointRounding.AwayFromZero)} saniye girişe yasaklanmıştır. Süre bitiminde tekrar giriş yapabilirsiniz" });
                }
                else
                {
                    return StatusCode(500, new { Message = $"şifrenizi 3 kere yanlış girdiğiniz için kullanıcınız 30 saniye girişe yasaklanmıştır. Süre bitiminde tekrar giriş yapabilirsiniz" });
                }
            }
            if (res.IsNotAllowed)
            {
                return StatusCode(500, new { Message = "Mail adresiniz onaylı değil" });
            }

            if (!res.Succeeded)
            {
                return BadRequest(new { Message = "Kullanıcı adı veya şifre yanlış" });
            }

            return Ok(new { Token = "Token" });
        }

    }
}

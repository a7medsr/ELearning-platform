using ELearning_Platforms.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ELearning_Platforms.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly IEmailService _emailService;
        public AuthController(IAuthServices authServices, IEmailService emailService)
        {
            _authServices = authServices;
            _emailService = emailService;
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _authServices.ForgotPassword(email);
            return Ok(new { message = result });
        }
        [HttpPost("change-password")]
        public async Task<IActionResult>Changepassword(string email, string oldpass, string newpass)
        {
            var result = await _authServices.ChangePassword(email, oldpass, newpass);
            if (result.Succeeded)
            {
                return Ok("Password changed successfully");
            }
            return BadRequest(result.Errors.Select(s => s.Description));
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("auto-reset-password")]
        public async Task<IActionResult> AutoResetPassword(string userId, string token)
        {
            var result = await _emailService.AutoResetPassword(userId, token);
            return Ok(result);
        }


    }
}

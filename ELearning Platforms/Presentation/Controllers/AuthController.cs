using ELearning_Platforms.Application.DTOs.Student;
using ELearning_Platforms.Application.DTOs.Teacher;
using ELearning_Platforms.Application.Services;
using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Models;
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
        private readonly UserManager<BaseUser> _userManager;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        public AuthController(IAuthServices authServices, IEmailService emailService, UserManager<BaseUser> userManager, IStudentService studentService, ITeacherService teacherService)
        {
            _authServices = authServices;
            _emailService = emailService;
            _userManager = userManager;
            _studentService = studentService;
            _teacherService = teacherService;
        }
      
        [HttpGet("Sign-In")]
        public async Task<string> SignIn(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return "User not found.";
            }
            if (!user.EmailConfirmed)
            {
                return "Email not confirmed.";
            }
            var result = await _userManager.CheckPasswordAsync(user, password);
            if (result)
            {
                var token = await _authServices.GenerateToken(user);
                return token;
            }
            return "Invalid password.";
        }
        [HttpPost("Student")]
        public async Task<IActionResult> RegisterStudent([FromBody] StudentRegistrationDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.RegisterStudentAsync(userDto);

            if (result.Succeeded)
                return Ok("Student created successfully");

            return BadRequest(result.Errors.Select(s => s.Description));
        }
        [HttpPost("Teacher")]
        public async Task<IActionResult> RegisterTeacher([FromBody] TeacherRegistrationDTO teacherDto)
        {
            if (teacherDto == null)
            {
                return BadRequest("Invalid teacher data");
            }
            var result = await _teacherService.RegisterTeacherAsync(teacherDto);
            if (result.Succeeded)
            {
                return Ok("Teacher registered successfully");
            }
            return BadRequest(result.Errors.Select(s => s.Description));
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> Changepassword(string email, string oldpass, string newpass)
        {
            var result = await _authServices.ChangePassword(email, oldpass, newpass);
            if (result.Succeeded)
            {
                return Ok("Password changed successfully");
            }
            return BadRequest(result.Errors.Select(s => s.Description));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _authServices.ForgotPassword(email);
            return Ok(new { message = result });
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("auto-reset-password")]
        public async Task<IActionResult> AutoResetPassword(string userId, string token)
        {
            var result = await _emailService.AutoResetPassword(userId, token);
            return Ok(result);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid user." });
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Invalid or expired token." });
            }

            return Ok(new { message = "Email verified successfully!" });
        }

    }
}

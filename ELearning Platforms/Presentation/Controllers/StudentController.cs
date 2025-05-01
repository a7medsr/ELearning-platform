using AutoMapper;
using ELearning_Platforms.Application.DTOs.Student;
using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Domain.Interfaces;
using ELearning_Platforms.Models;
using ELearning_Platforms.Models.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly UserManager<BaseUser> _userManager;
        private readonly ELearningDbContext _context;
        
        private readonly IStudentRepository _studentRepository;


        public StudentController(ELearningDbContext context,UserManager<BaseUser> userManager, IStudentService studentService, IStudentRepository studentRepository)
        {
            _studentService = studentService;
            _studentRepository = studentRepository;
            _userManager = userManager;
            _context = context;

        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] StudentRegistrationDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.RegisterStudentAsync(userDto);

            if (result.Succeeded)
                return Ok("User created successfully");

            return BadRequest(result.Errors.Select(s => s.Description));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] StudentUpdateDTO studentUpdateDTO)
        {
            var result = await _studentService.UpdateStudentAsync(id, studentUpdateDTO);
            if (result.Succeeded)
                return Ok("User created successfully");

            return BadRequest(result.Errors.Select(s => s.Description));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            var result = await _studentService.DeleteStudentAsync(id);
            if (result.Succeeded)
                return Ok("User Deleted successfully");

            return BadRequest(result.Errors.Select(s => s.Description));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var studentDto = await _studentService.GetStudentByIdAsync(id);
            if (studentDto == null)
            {
                return NotFound();
            }

            return Ok(studentDto);
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

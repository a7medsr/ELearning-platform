using AutoMapper;
using ELearning_Platforms.Application.DTOs.Student;
using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELearning_Platforms.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentService studentService, IStudentRepository studentRepository)
        {
            _studentService = studentService;
            _studentRepository = studentRepository;

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

    }
}

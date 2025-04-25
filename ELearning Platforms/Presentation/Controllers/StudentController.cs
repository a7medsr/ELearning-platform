using AutoMapper;
using ELearning_Platforms.Application.DTOs.Student;
using ELearning_Platforms.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearning_Platforms.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController (IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] StudentRegistrationDTO studentRegistrationDTO)
        {
            var result = await _studentService.RegisterStudentAsync(studentRegistrationDTO);
            return Ok(result); //TODO: this is not a correct logic because whatever was the result i will return OK
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] StudentUpdateDTO studentUpdateDTO)
        {
            var result = await _studentService.UpdateStudentFirstNameLastNameAsync(id, studentUpdateDTO);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _studentService.DeleteStudentAsync(id);
            if (!success)
                return NotFound("Student not found");
            return Ok("Student deleted");
        }

    }
}

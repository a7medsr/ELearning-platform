using ELearning_Platforms.Application.DTOs.Teacher;
using ELearning_Platforms.Application.Services;
using ELearning_Platforms.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearning_Platforms.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }


        [HttpPost]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] TeacherUpdateDTO teacherUpdateDTO)
        {
            var result = await _teacherService.UpdateTeacherAsync(id, teacherUpdateDTO);
            if (result.Succeeded)
                return Ok("User Updated successfully");

            return BadRequest(result.Errors.Select(s => s.Description));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _teacherService.DeleteTeacherAsync(id);
            if (result.Succeeded)
                return Ok("User Deleted successfully");

            return BadRequest(result.Errors.Select(s => s.Description));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var teacherDto = await _teacherService.GetTeacherByIdAsync(id);
            if (teacherDto == null)
            {
                return NotFound();
            }

            return Ok(teacherDto);
        }
    }
}

using ELearning_Platforms.Application.DTOs.Courses;
using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ELearning_Platforms.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseService courseService, ICourseRepository courseRepository)
        {
            _courseService = courseService;
            _courseRepository = courseRepository;
        }


        [HttpPost]
        [Authorize(Roles = "Teacher", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateCourse([FromBody] AddCourseDTO courseCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(teacherId))
            {
                return BadRequest("Teacher ID is missing or invalid.");
            }

            var result = await _courseService.AddCourseAsync(courseCreateDTO, teacherId);

            if (result.StartsWith("User not found") || result.StartsWith("Only teachers") || result.StartsWith("A course"))
            {
                return BadRequest(result); 
            }

            return Ok(new { Message = "Course created successfully", CourseId = result });
        }

        [HttpDelete]
        [Authorize(Roles = "Teacher", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            string? teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(teacherId))
            {
                return BadRequest("Teacher ID is missing or invalid.");
            }

            var result = await _courseService.DeleteCourseAsync(id, teacherId);

            if (result == "Course not found.")
            {
                return NotFound(result);
            }
            else if (result == "You are not authorized to delete this course.")
            {
                return Forbid(result); 
            }
            else if (result.StartsWith("Failed"))
            {
                return StatusCode(500, result); 
            }

            return Ok(new { Message = result });
        }


        [HttpPost("StudentEnrolmnt")]
        [Authorize(Roles = "Student", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> EnrollInCourse(string courseId)
        {
            string? studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(studentId))
            {
                return BadRequest("Student ID is missing or invalid.");
            }

            var result = await _courseService.EnrollStudentAsync(courseId, studentId);

            if (result == "Course not found.")
            {
                return NotFound(result);
            }
            else if (result == "You are already enrolled in this course.")
            {
                return BadRequest(result);
            }
            else if (result.StartsWith("Failed"))
            {
                return StatusCode(500, result);
            }

            return Ok(new { Message = "Enrollment successful", CourseId = courseId });
        }



    }
}

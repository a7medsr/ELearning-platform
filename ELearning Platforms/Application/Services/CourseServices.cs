using ELearning_Platforms.Application.DTOs.Courses;
using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Domain.Interfaces;
using ELearning_Platforms.Models;
using Microsoft.AspNetCore.Identity;

namespace ELearning_Platforms.Application.Services
{
    public class CourseServices : ICourseService
    {
        private readonly ICourseRepository _coursRepository;
        private readonly UserManager<BaseUser> _userManager;
        public CourseServices(ICourseRepository coursRepository,UserManager<BaseUser> userManager)
        {
            _coursRepository = coursRepository;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _coursRepository.GetAllCoursesAsync();
        }
        public async Task<Course> GetCourseByIdAsync(string id)
        {
            return await _coursRepository.GetCourseByIdAsync(id);
        }

        public async Task<string> AddCourseAsync(AddCourseDTO course, string teacherId)
        {
            var user = await _userManager.FindByIdAsync(teacherId);
            if (user == null)
            {
                return "User not found.";
            }

            if (!await _userManager.IsInRoleAsync(user, "Teacher"))
            {
                return "Only teachers can create courses.";
            }

            bool exists = await _coursRepository.CourseTitleExistsAsync(course.Title, teacherId);
            if (exists)
            {
                return "A course with this title already exists.";
            }

            var courseEntity = new Course
            {
                Id = Guid.NewGuid().ToString(),
                Title = course.Title.Trim(),
                Description = course.Description.Trim(),
                Price = course.Price,
                TeacherId = user.Id
            };

            await _coursRepository.AddCourseAsync(courseEntity);
            return courseEntity.Id;
        }



        public async Task UpdateCourseAsync(Course course)
        {
            await _coursRepository.UpdateCourseAsync(course);
        }
        public async Task<string> DeleteCourseAsync(string id, string teacherId)
        {
            var course = await _coursRepository.GetCourseByIdAsync(id);
            if (course == null)
            {
                return "Course not found.";
            }

            if (course.TeacherId != teacherId)
            {
                return "You are not authorized to delete this course.";
            }

            if (course.CourseEnrollments != null && course.CourseEnrollments.Any())
            {
                return "Cannot delete course. Students are currently enrolled.";
            }
            try
            {
                await _coursRepository.DeleteCourseAsync(id);
                return "Course deleted successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to delete the course: {ex.Message}";
            }
        }
        //dont work yet
        public async Task<string> EnrollStudentAsync(string studentId, string courseId)
        {
            //var student = await _userManager.FindByIdAsync(studentId);
            //if (student == null || !await _userManager.IsInRoleAsync(student, "Student"))
            //    return "Invalid student.";

            var course = await _coursRepository.GetCourseByIdAsync(courseId);
            if (course == null)
                return "Course not found.";

            var isAlreadyEnrolled = await _coursRepository.IsStudentEnrolledAsync(studentId, courseId);
            if (isAlreadyEnrolled)
                return "Student already enrolled in this course.";

            var enrollment = new CourseEnrollment
            {
                StudentId = studentId,
                CourseId = courseId,
            };

            await _coursRepository.EnrollStudentAsync(enrollment);
            return "Enrolled successfully.";
        }

    }
}

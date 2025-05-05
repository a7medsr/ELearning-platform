using AutoMapper;
using ELearning_Platforms.Application.DTOs.Courses;
using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Domain.Interfaces;
using ELearning_Platforms.Models;
using ELearning_Platforms.Models.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Application.Services
{
    public class CourseServices : ICourseService
    {
        private readonly ICourseRepository _coursRepository;
        private readonly UserManager<BaseUser> _userManager;
        private readonly ELearningDbContext _context;
        private readonly IMapper _mapper;
        public CourseServices( IMapper mapper,ICourseRepository coursRepository,UserManager<BaseUser> userManager,ELearningDbContext context)
        {
            _coursRepository = coursRepository;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
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
                Title = course.Title,
                Description = course.Description,
                Price = course.Price,
                TeacherId = user.Id,
                thumbnailUrl = course.ThumbnailUrl,
                Category = course.Category
            };

            await _coursRepository.AddCourseAsync(courseEntity);
            return courseEntity.Id;
        }
        public async Task<string> UpdateCourseAsync(UpdateCourseDTO course,string courseId, string teacherId)
        {
            var existingCourse = await _coursRepository.GetCourseByIdAsync(courseId);
            if (existingCourse == null)
            {
                return "Course not found.";
            }
            if (existingCourse.TeacherId != teacherId)
            {
                return "You are not authorized to update this course.";
            }
            if (await _coursRepository.CourseTitleExistsAsync(course.Title, teacherId))
            {
                return "A course with this title already exists.";
            }
            if (course.Price < 0)
            {
                return "Price cannot be negative.";
            }


            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            existingCourse.Price = course.Price;
            existingCourse.thumbnailUrl = course.ThumbnailUrl;
            existingCourse.Category = course.Category;

            if (await _coursRepository.UpdateCourseAsync(existingCourse))
            {
                return "Course updated successfully.";
            }
            else
            {
                return "Failed to update the course.";
            }
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


            bool result = await _coursRepository.IsThereAnyEnrollmentInCourseAsync(id);
            if (result)
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
        //need to integrate payment 
        public async Task<string> EnrollStudentAsync(string studentId, string courseId)
        {
            

            var course = await _coursRepository.GetCourseByIdAsync(courseId);
            if (course == null)
                return "Course not found.";

            var isAlreadyEnrolled = await _coursRepository.IsStudentEnrolledAsync(studentId, courseId);
            if (isAlreadyEnrolled)
            {
                return "Student already enrolled in this course.";
            }

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

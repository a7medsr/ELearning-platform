using ELearning_Platforms.Domain.Interfaces;
using ELearning_Platforms.Models;
using ELearning_Platforms.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Infrastructure.Repositories
{
    public class CourseRepository: ICourseRepository
    {
        private readonly ELearningDbContext _context;
        public CourseRepository(ELearningDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }
        public async Task<Course> GetCourseByIdAsync(string id)
        {
            return await _context.Courses.FindAsync(id);
        }
        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
             
        }
        public async Task<bool> CourseTitleExistsAsync(string title, string teacherId)
        {
            return await _context.Courses.AnyAsync(c => c.Title == title && c.TeacherId.ToString() == teacherId);
        }

        public async Task<bool> UpdateCourseAsync(Course updatedCourse)
        {
            _context.Courses.Update(updatedCourse);
            var result=  _context.SaveChanges();
            return result > 0;
        }
        public async Task DeleteCourseAsync(string id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> IsStudentEnrolledAsync(string studentId, string courseId)
        {
            return await _context.CourseEnrollment.AnyAsync(e =>
                e.StudentId == studentId && e.CourseId == courseId);
        }
        public async Task<bool> IsThereAnyEnrollmentInCourseAsync(string courseId)
        {
            var course = await _context.Courses
                .Include(c => c.CourseEnrollments)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course != null && course.CourseEnrollments != null && course.CourseEnrollments.Any())
            {
                return true;
            }

            return false;
        }


        public async Task EnrollStudentAsync(CourseEnrollment enrollment)
        {
            await _context.CourseEnrollment.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

    }
}

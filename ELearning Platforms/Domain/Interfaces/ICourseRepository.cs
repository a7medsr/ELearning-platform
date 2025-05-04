using ELearning_Platforms.Models;
using ELearning_Platforms.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Domain.Interfaces
{
    public interface ICourseRepository
    {

       Task<IEnumerable<Course>> GetAllCoursesAsync();

        Task<Course> GetCourseByIdAsync(string id);


        Task<bool> CourseTitleExistsAsync(string title, string teacherId);


       Task UpdateCourseAsync(Course course);
        Task AddCourseAsync(Course course);
        Task DeleteCourseAsync(string id);

        Task EnrollStudentAsync(CourseEnrollment enrollment);
        Task<bool> IsStudentEnrolledAsync(string studentId, string courseId);

    }
}

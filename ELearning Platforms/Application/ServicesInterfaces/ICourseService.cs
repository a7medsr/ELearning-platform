using ELearning_Platforms.Application.DTOs.Courses;
using ELearning_Platforms.Models;

namespace ELearning_Platforms.Application.ServicesInterfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(string id);
        Task<string> AddCourseAsync(AddCourseDTO course, string Id);
        Task UpdateCourseAsync(Course course);
        Task<string> DeleteCourseAsync(string id, string teacherId);

        Task<string> EnrollStudentAsync(string studentId, string courseId);
    }
}

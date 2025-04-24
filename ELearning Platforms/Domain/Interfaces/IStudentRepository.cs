using ELearning_Platforms.Models;

namespace ELearning_Platforms.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task CreateStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task<Student> GetStudentByIdAsync(string Id);
        Task<Student> GetStudentByEmailAsync(string Email);
        Task<Student> GetStudentByPhoneNumberAsync(string PhoneNumber);
        Task<IEnumerable<Student>> GetAllStudentsAsync(); 
    }
}

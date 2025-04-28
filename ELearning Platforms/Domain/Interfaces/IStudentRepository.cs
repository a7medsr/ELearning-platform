using ELearning_Platforms.Models;
using Microsoft.AspNetCore.Identity;

namespace ELearning_Platforms.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<IdentityResult> CreateStudentAsync(Student user, string password);
        Task<IdentityResult> DeleteStudentAsync(Student student);
        Task<IdentityResult> UpdateStudentAsync(Student student);
        Task<Student> GetStudentByIdAsync(string Id);
        Task<Student> GetStudentByEmailAsync(string Email);
        Task<Student> GetStudentByPhoneNumberAsync(string PhoneNumber);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
    }
}

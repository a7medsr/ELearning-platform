using ELearning_Platforms.Models;

namespace ELearning_Platforms.Domain.Interfaces
{
    public interface ITeacherRepository
    {
        Task CreateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(Teacher teacher);
        Task UpdateTeacherAsync(Teacher teacher);
        Task<Teacher> GetTeacherByIdAsync(string Id);
        Task<Teacher> GetTeacherByEmailAsync(string Email);
        Task<Teacher> GetTeacherByPhoneNumberAsync(string PhoneNumber);
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
    }
}

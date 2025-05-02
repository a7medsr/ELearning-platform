using ELearning_Platforms.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Domain.Interfaces
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        

        Task<Teacher> GetTeacherByEmailAsync(string Email);
       

        Task<Teacher> GetTeacherByIdAsync(string Id);
    

        Task<Teacher> GetTeacherByPhoneNumberAsync(string PhoneNumber);
        

        Task<IdentityResult> CreateTeacherAsync(Teacher teacher, string password);
        Task<IdentityResult> DeleteTeacherAsync(Teacher teacher);
        Task<IdentityResult> UpdateTeacherAsync(Teacher teacher);
    }
}

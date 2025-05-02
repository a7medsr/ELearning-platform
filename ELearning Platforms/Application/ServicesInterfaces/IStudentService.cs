using ELearning_Platforms.Application.DTOs.Student;
using Microsoft.AspNetCore.Identity;

namespace ELearning_Platforms.Application.ServicesInterfaces
{
    public interface IStudentService
    {
        Task<IdentityResult> RegisterStudentAsync(StudentRegistrationDTO studentDto);
        Task<StudentResponseDTO> LoginAsync(StudentRegistrationDTO loginDto);
        Task<IdentityResult> UpdateStudentAsync(string studentId, StudentUpdateDTO updateDto);
        Task<IdentityResult> DeleteStudentAsync(string studentId);
        Task<StudentResponseDTO> GetStudentByIdAsync(string Id);
        Task<StudentResponseDTO> GetStudentByPhoneNumberAsync(string phoneNumber);


    }
}

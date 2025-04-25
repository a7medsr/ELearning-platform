using ELearning_Platforms.Application.DTOs.Student;

namespace ELearning_Platforms.Application.ServicesInterfaces
{
    public interface IStudentService
    {
        Task<string> RegisterStudentAsync(StudentRegistrationDTO studentDto);
        Task<StudentResponseDTO> LoginAsync(StudentRegistrationDTO loginDto);
        Task<string> UpdateStudentFirstNameLastNameAsync(string studentId, StudentUpdateDTO updateDto);
        Task<bool> DeleteStudentAsync(string studentId);
        Task<StudentResponseDTO> GetStudentByIdAsync(string Id);
        Task<StudentResponseDTO> GetStudentByPhoneNumberAsync(string phoneNumber);

    }
}

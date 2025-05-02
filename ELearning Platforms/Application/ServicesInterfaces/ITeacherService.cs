using ELearning_Platforms.Application.DTOs.Teacher;
using ELearning_Platforms.Models;
using Microsoft.AspNetCore.Identity;

namespace ELearning_Platforms.Application.ServicesInterfaces
{
    public interface ITeacherService
    {
        Task<IdentityResult> RegisterTeacherAsync(TeacherRegistrationDTO teacherDto);

        Task<IdentityResult> DeleteTeacherAsync(string teacherId);

        Task<TeacherResponseDTO> GetTeacherByIdAsync(string Id);

        Task<TeacherResponseDTO> GetTeacherByPhoneNumberAsync(string phoneNumber);


        //need auth
        Task<TeacherResponseDTO> LoginAsync(TeacherRegistrationDTO loginDto);




        Task<IdentityResult> UpdateTeacherAsync(string teacherId, TeacherUpdateDTO updateDto);

    }
}

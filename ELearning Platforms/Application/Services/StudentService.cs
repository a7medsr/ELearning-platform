using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ELearning_Platforms.Models;
using ELearning_Platforms.Application.DTOs.Student;

namespace ELearning_Platforms.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<Student> _userManager;

        public StudentService(IStudentRepository repo, IMapper mapper, UserManager<Student> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> DeleteStudentAsync(string studentId)
        {
            var student = await _repo.GetStudentByIdAsync(studentId);

            if (student == null)
            {
                return false;
            }
            await _repo.DeleteStudentAsync(student);
            return true;
        }

        public async Task<StudentResponseDTO> GetStudentByIdAsync(string Id)
        {
            var student = await _repo.GetStudentByIdAsync(Id);

            if (student == null)
            {
                throw new Exception("Student not found");
            }

            var studentResponseDTO = _mapper.Map<StudentResponseDTO>(student);

            return studentResponseDTO;
        }

        public async Task<StudentResponseDTO> GetStudentByPhoneNumberAsync(string phoneNumber)
        {
            var student = await _repo.GetStudentByPhoneNumberAsync(phoneNumber);
            
            if (student == null)
            {
                throw new Exception("Student not found");
            }

            return _mapper.Map<StudentResponseDTO>(student);
        }

        public async Task<StudentResponseDTO> LoginAsync(StudentRegistrationDTO loginDto)
        {
            var currentStuent = await _userManager.FindByEmailAsync(loginDto.Email);
            if (currentStuent == null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(currentStuent, loginDto.Password);

            return _mapper.Map<StudentResponseDTO>(currentStuent);
        }

        public async Task<string> RegisterStudentAsync(StudentRegistrationDTO studentDto)
        {
            if(studentDto == null)
            {
                return "invalid student data";
            }

            var existingUserEmail = await _userManager.FindByEmailAsync(studentDto.Email);
            if (existingUserEmail != null)
                return "A user with this email already exist";

            var existingUserName = await _userManager.FindByNameAsync(studentDto.UserName);
            if (existingUserName != null)
                return "This username already exist";

            var newStudent = _mapper.Map<Student>(studentDto);

            var result = await _userManager.CreateAsync(newStudent, studentDto.Password);
            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            var roleResult = await _userManager.AddToRoleAsync(newStudent, "Student");
            if(!roleResult.Succeeded)
                return "Student created but failed to assign role: " + string.Join(", ", result.Errors.Select(e => e.Description));

            return "Student registered successfully";
        }

        public async Task<string> UpdateStudentFirstNameLastNameAsync(string studentId, StudentUpdateDTO updateDto)
        {
            var currentStudent = await _userManager.FindByIdAsync(studentId);
            if (currentStudent == null)
                return "Student not found";

            if(!string.IsNullOrWhiteSpace(updateDto.FirstName))
            {
                currentStudent.FirstName = updateDto.FirstName;
            }

            if(!string.IsNullOrWhiteSpace(updateDto.LastName))
            {
                currentStudent.LastName = updateDto.LastName;
            }

            var result = await _userManager.UpdateAsync(currentStudent);

            if (result.Succeeded)
                return "Student updated successfully";
            else
                return string.Join(", ", result.Errors.Select(e => e.Description));
        }
    }
}

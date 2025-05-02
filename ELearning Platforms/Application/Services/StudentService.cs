using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ELearning_Platforms.Models;
using ELearning_Platforms.Application.DTOs.Student;
using System.Net;

namespace ELearning_Platforms.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<BaseUser> _userManager;
        private readonly IEmailService _emailService;


        public StudentService(IEmailService emailService, IStudentRepository repo, IMapper mapper, UserManager<BaseUser> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<IdentityResult> DeleteStudentAsync(string studentId)
        {
            var student = await _repo.GetStudentByIdAsync(studentId);

            if (student == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid student data" });
            }

            return await _repo.DeleteStudentAsync(student);
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


        //need auth
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
        //need auth
        public async Task<IdentityResult> RegisterStudentAsync(StudentRegistrationDTO studentDto)
        {
            if (studentDto == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid student data" });
            }

            var existingUserEmail = await _userManager.FindByEmailAsync(studentDto.Email);
            if (existingUserEmail != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "A user with this email already exists" });
            }

            var existingUserName = await _userManager.FindByNameAsync(studentDto.UserName);
            if (existingUserName != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "This username already exists" });
            }

            var newStudent = _mapper.Map<Student>(studentDto);

            var result = await _repo.CreateStudentAsync(newStudent, studentDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newStudent, "Student");
               
                _emailService.verifyAsync(newStudent);

            }

            return result;
        }


        public async Task<IdentityResult> UpdateStudentAsync(string studentId, StudentUpdateDTO updateDto)
        {
            var currentStudent = await _userManager.FindByIdAsync(studentId);

            if (currentStudent == null) return IdentityResult.Failed(new IdentityError { Description = "Student not found" });


            if (!string.IsNullOrWhiteSpace(updateDto.FirstName))
            {
                currentStudent.FirstName = updateDto.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.LastName))
            {
                currentStudent.LastName = updateDto.LastName;
            }
            if (!string.IsNullOrWhiteSpace(updateDto.UserName))
            {
                var existingUserName = await _userManager.FindByNameAsync(updateDto.UserName);
                if (existingUserName != null)
                    return IdentityResult.Failed(new IdentityError { Description = "This username already exist" });
                currentStudent.UserName = updateDto.UserName;
            }

            return await _userManager.UpdateAsync(currentStudent);


        }
    }
}

using AutoMapper;
using ELearning_Platforms.Application.DTOs.Teacher;
using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Domain.Interfaces;
using ELearning_Platforms.Models;
using Microsoft.AspNetCore.Identity;

namespace ELearning_Platforms.Application.Services
{
    public class TeacherService:ITeacherService
    {
        private readonly UserManager<BaseUser> _userManager;
        private readonly IMapper _mapper;  
        private readonly ITeacherRepository _teacherRepository;
        private readonly IEmailService _emailService;
        public TeacherService(IEmailService emailService, UserManager<BaseUser> userManager, IMapper mapper,ITeacherRepository teacherRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _teacherRepository = teacherRepository;
            _emailService = emailService;
        }
        
        public async Task<IdentityResult> RegisterTeacherAsync(TeacherRegistrationDTO teacherDto)
        {
            if (teacherDto == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid Teacher data" });
            }

            var existingUserEmail = await _userManager.FindByEmailAsync(teacherDto.Email);
            if (existingUserEmail != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "A user with this email already exists" });
            }

            var existingUserName = await _userManager.FindByNameAsync(teacherDto.UserName);
            if (existingUserName != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "This username already exists" });
            }

            var newTeacher = _mapper.Map<Teacher>(teacherDto);

            var result = await _teacherRepository.CreateTeacherAsync(newTeacher, teacherDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newTeacher, "Teacher");
                _emailService.verifyAsync(newTeacher);
            }

            return result;
        }

        public async Task<IdentityResult> DeleteTeacherAsync(string teacherId)
        {
            var teacher = await _teacherRepository.GetTeacherByIdAsync(teacherId);

            if (teacher == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid Teacher data" });
            }

            return await _teacherRepository.DeleteTeacherAsync(teacher);
        }

        public async Task<TeacherResponseDTO> GetTeacherByIdAsync(string Id)
        {
            var teacher = await _teacherRepository.GetTeacherByIdAsync(Id);

            if (teacher == null)
            {
                throw new Exception("Teacher not found");
            }

            var teacherResponseDTO = _mapper.Map<TeacherResponseDTO>(teacher);

            return teacherResponseDTO;
        }

        public async Task<TeacherResponseDTO> GetTeacherByPhoneNumberAsync(string phoneNumber)
        {
            var teacher = await _teacherRepository.GetTeacherByPhoneNumberAsync(phoneNumber);

            if (teacher == null)
            {
                throw new Exception("Teacher not found");
            }

            return _mapper.Map<TeacherResponseDTO>(teacher);
        }


        //need auth
        public async Task<TeacherResponseDTO> LoginAsync(TeacherRegistrationDTO loginDto)
        {
            var currentTeacher = await _userManager.FindByEmailAsync(loginDto.Email);
            if (currentTeacher == null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(currentTeacher, loginDto.Password);

            return _mapper.Map<TeacherResponseDTO>(currentTeacher);
        }
      


        public async Task<IdentityResult> UpdateTeacherAsync(string teacherId, TeacherUpdateDTO updateDto)
        {
            var currentTeacher = await _teacherRepository.GetTeacherByIdAsync(teacherId);

            if (currentTeacher == null) return IdentityResult.Failed(new IdentityError { Description = "Teacher not found" });


            if (!string.IsNullOrWhiteSpace(updateDto.FirstName))
            {
                currentTeacher.FirstName = updateDto.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.LastName))
            {
                currentTeacher.LastName = updateDto.LastName;
            }
            if(!string.IsNullOrWhiteSpace(updateDto.Bio))
            {
                currentTeacher.Bio = updateDto.Bio;
            }
            if (!string.IsNullOrWhiteSpace(updateDto.UserName))
            {
                var existingUserName = await _userManager.FindByNameAsync(updateDto.UserName);
                if (existingUserName != null)
                    return IdentityResult.Failed(new IdentityError { Description = "This username already exist" });
                currentTeacher.UserName = updateDto.UserName;
            }

            return await _userManager.UpdateAsync(currentTeacher);


        }

        
    }
}

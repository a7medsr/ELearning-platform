using ELearning_Platforms.Application.DTOs;
using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Domain.Interfaces;
using AutoMapper;

namespace ELearning_Platforms.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task DeleteStudentAsync(string studentId)
        {
            var student = await _repo.GetStudentByIdAsync(studentId);

            if (student == null)
            {
                throw new Exception("Student not found");
            }
            await _repo.DeleteStudentAsync(student);
        }

        public async Task<StudentResponseDTO> GetStudentByIdAsync(string Id)
        {
            var student = await _repo.GetStudentByIdAsync(Id);

            if (student == null)
            {
                throw new Exception("Student not found");
            }
            return _mapper.Map<StudentResponseDTO>(student);
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

        public Task<StudentResponseDTO> LoginAsync(StudentRegistrationDTO loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterStudentAsync(StudentRegistrationDTO studentDto)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateStudentAsync(string studentId, StudentUpdateDTO updateDto)
        {
            throw new NotImplementedException();
        }
    }
}

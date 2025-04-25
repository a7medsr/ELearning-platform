using AutoMapper;
using ELearning_Platforms.Models;
using ELearning_Platforms.Application.DTOs.Student;
namespace ELearning_Platforms.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Student, StudentResponseDTO>();
            CreateMap<StudentRegistrationDTO, Student>();
            CreateMap<StudentUpdateDTO, Student>();
        }
    }
}

using AutoMapper;
using ELearning_Platforms.Models;
using ELearning_Platforms.Application.DTOs.Student;
using ELearning_Platforms.Application.DTOs.Teacher;
using ELearning_Platforms.Application.DTOs.Courses;
namespace ELearning_Platforms.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Student Mapping
            CreateMap<Student, StudentResponseDTO>();
            CreateMap<StudentRegistrationDTO, Student>();
            CreateMap<StudentUpdateDTO, Student>();

            // Teacher Mapping 
            CreateMap<Teacher, TeacherResponseDTO>();
            CreateMap<TeacherRegistrationDTO, Teacher>();
            CreateMap<TeacherUpdateDTO, Teacher>();

            CreateMap<Course, UpdateCourseDTO>();
            CreateMap<UpdateCourseDTO, Course>();

        }
    }
}

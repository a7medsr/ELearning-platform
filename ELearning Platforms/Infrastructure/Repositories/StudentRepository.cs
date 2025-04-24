using ELearning_Platforms.Domain.Interfaces;
using ELearning_Platforms.Models;
using ELearning_Platforms.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ELearningDbContext _context;

        public StudentRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public async Task<Student> GetStudentByIdAsync(string Id)
        {
            return await _context.Students.FindAsync(Id);
        }

        public async Task<Student> GetStudentByEmailAsync(string Email)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Email == Email);
        }

        public async Task<Student> GetStudentByPhoneNumberAsync(string PhoneNumber)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.PhoneNumber == PhoneNumber);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task CreateStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

    }
}

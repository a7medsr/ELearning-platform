using ELearning_Platforms.Domain.Interfaces;
using ELearning_Platforms.Models;
using ELearning_Platforms.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ELearningDbContext _context;

        public TeacherRepository(ELearningDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetTeacherByEmailAsync(string Email)
        {
            return await _context.Teachers.FirstOrDefaultAsync(t => t.Email == Email);
        }

        public async Task<Teacher> GetTeacherByIdAsync(string Id)
        {
            return await _context.Teachers.FindAsync(Id);
        }

        public async Task<Teacher> GetTeacherByPhoneNumberAsync(string PhoneNumber)
        {
            return await _context.Teachers.FirstOrDefaultAsync(x => x.PhoneNumber == PhoneNumber);
        }

        public async Task CreateTeacherAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }
    }
}

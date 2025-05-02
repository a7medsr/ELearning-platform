using ELearning_Platforms.Domain.Interfaces;
using ELearning_Platforms.Models;
using ELearning_Platforms.Models.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
            private readonly ELearningDbContext _context;
            private readonly UserManager<BaseUser> _userManager;
  

           public TeacherRepository(ELearningDbContext context, UserManager<BaseUser> userManager)
            {
               _context = context;
               _userManager = userManager;
           }
          public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
          {
             return await _context.Teachers.ToListAsync();
           }

        public async Task<Teacher> GetTeacherByEmailAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user is Teacher teacher)
            {
                return teacher;
            }
            return null;
        }

        public async Task<Teacher> GetTeacherByIdAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (user is Teacher teacher)
            {
                return teacher;
            }
            return null;

        }

        public async Task<Teacher> GetTeacherByPhoneNumberAsync(string PhoneNumber)
        {
            return await _context.Teachers.FirstOrDefaultAsync(x => x.PhoneNumber == PhoneNumber);
        }


        public async Task<IdentityResult> CreateTeacherAsync(Teacher teacher, string password)
        {
            return await _userManager.CreateAsync(teacher, password);
        }
        public async Task<IdentityResult> DeleteTeacherAsync(Teacher teacher)
        {
            return await _userManager.DeleteAsync(teacher);
        }
        public async Task<IdentityResult> UpdateTeacherAsync(Teacher teacher)
        {
            return await _userManager.UpdateAsync(teacher);
        }
        
    }

}

using ELearning_Platforms.Domain.Interfaces;
using ELearning_Platforms.Models;
using ELearning_Platforms.Models.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ELearningDbContext _context;
        private readonly UserManager<BaseUser> _userManager;

        public StudentRepository(ELearningDbContext context, UserManager<BaseUser> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        public async Task<Student> GetStudentByIdAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (user is Student student)
            {
                return student;
            }
            return null;

        }

        public async Task<Student> GetStudentByEmailAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user is Student student)
            {
                return student;
            }
            return null;
        }

        public async Task<Student> GetStudentByPhoneNumberAsync(string PhoneNumber)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.PhoneNumber == PhoneNumber);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();

        }


        public async Task<IdentityResult> CreateStudentAsync(Student user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> DeleteStudentAsync(Student student)
        {
            return await _userManager.DeleteAsync(student);
        }
        //did not use
        public async Task<IdentityResult> UpdateStudentAsync(Student student)
        {
            return await _userManager.UpdateAsync(student);
        }

    }
}

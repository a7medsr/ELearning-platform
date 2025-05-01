using ELearning_Platforms.Application.DTOs.Student;
using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELearning_Platforms.Application.Services
{
    public class AuthServices: IAuthServices
    {
        private readonly UserManager<BaseUser> _userManager;
        private readonly IEmailService _emailService;
        public AuthServices(UserManager<BaseUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }
        public async Task<string> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return "User not found.";
            }
            return await _emailService.ForgotPasswordEmail(user);
        }

        public async Task<IdentityResult> ChangePassword(string email,string oldpass,string newpass)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }
            var result = await _userManager.ChangePasswordAsync(user, oldpass, newpass);
           
                return IdentityResult.Success;
           
        }
        

        

    }
}

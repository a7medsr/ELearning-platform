using Microsoft.AspNetCore.Identity;

namespace ELearning_Platforms.Application.ServicesInterfaces
{
    public interface IAuthServices
    {
        Task<string> ForgotPassword(string email);
        Task<IdentityResult> ChangePassword(string email, string oldpass, string newpass);
    }
}

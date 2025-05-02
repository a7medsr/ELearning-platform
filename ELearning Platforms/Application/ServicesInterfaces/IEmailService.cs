using ELearning_Platforms.Models;

namespace ELearning_Platforms.Application.ServicesInterfaces
{
    public interface IEmailService
    {
        string EmailBody(string url);
        Task SendEmailAsync(string toEmail, string subject, string body);
        string ForgotPasswordBody(string url);
        string PasswordRestBody(string newPassword);
        void verifyAsync(BaseUser user);
        Task<string> ForgotPasswordEmail(BaseUser user);
        Task<string> AutoResetPassword(string userId, string token);
    }
}

using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using ELearning_Platforms.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Identity;
using ELearning_Platforms.Models;
using System.Net;
namespace ELearning_Platforms.Application.Services
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<BaseUser> _userManager;

        public EmailService(IConfiguration configuration, UserManager<BaseUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;

            email.Body = new TextPart("html") { Text = body };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(emailSettings["SenderEmail"], emailSettings["SenderPassword"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
        public async Task<string> SendEmail(string to, string subject = "Hello To our project", string body = "Is this really you?")
        {
            if (string.IsNullOrWhiteSpace(to) ||
                string.IsNullOrWhiteSpace(subject) ||
                string.IsNullOrWhiteSpace(body))
            {
                return "Failed: All fields (To, Subject, Body) are required!";
            }
            try
            {
                await SendEmailAsync(to, subject, body);
                return "Email sent successfully!";
            }
            catch (Exception ex)
            {
                return $"Failed to send email. Error: {ex.Message}";
            }
        }
        
        public async void verifyAsync(BaseUser user)
        {

            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(emailToken);
            var verificationUrl = $"verify-email?userId={user.Id}&token={encodedToken}";
            string emailBody = EmailBody(verificationUrl);
            await SendEmailAsync(user .Email, $"Hello {user.FirstName} {user.LastName}", emailBody);

        }
        public async Task<string> ForgotPasswordEmail(BaseUser user)
        {
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetUrl = $"auto-reset-password?userId={user.Id}&token={Uri.EscapeDataString(token)}";
            string emailBody = ForgotPasswordBody(resetUrl);
            await SendEmailAsync(user.Email, "Reset Your Password", emailBody);
            return "Password reset link sent to your email.";
        }

        public async Task<string> AutoResetPassword(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "Invalid user.";
            }
            var isValidToken = await _userManager.VerifyUserTokenAsync(user,
                _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);

            if (!isValidToken)
            {
                return "Invalid or expired token.";
            }
            string newPassword = GenerateRandomPassword();

            var resetResult =  _userManager.ResetPasswordAsync(user, token, newPassword);
         

            string emailBody = PasswordRestBody(newPassword);

            await SendEmailAsync(user.Email, "Your Password Has Been Reset", emailBody);

            return "Password has been reset. Check your email for the new password.";
        }
        public string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$!";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }




        public string EmailBody(string url)
        {
            string emailBody = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
        .container {{ max-width: 600px; margin: 20px auto; background: #ffffff; padding: 20px; border-radius: 10px; 
                      box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); text-align: center; }}
        .header {{ font-size: 24px; font-weight: bold; color: #333; margin-bottom: 10px; }}
        .content {{ font-size: 16px; color: #555; margin-bottom: 20px; }}
        .btn {{ display: inline-block; padding: 12px 20px; font-size: 18px; color: #fff; background-color: #28a745; 
                text-decoration: none; border-radius: 5px; }}
        .footer {{ margin-top: 20px; font-size: 14px; color: #777; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>Welcome to Our Project!</div>
        <div class='content'>
            Thank you for signing up. Please click the button below to verify your email address.
        </div>
        <a href='https://localhost:7225/api/Auth/{url}' class='btn'>Verify Email</a>
        <div class='footer'>
            If you did not create an account, please ignore this email.
        </div>
    </div>
</body>
</html>";
            return emailBody;

        }
        public string ForgotPasswordBody(string url)
        {
            string emailBody = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 20px;
            }}
            .container {{
                max-width: 500px;
                margin: auto;
                background: white;
                padding: 20px;
                border-radius: 10px;
                box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                text-align: center;
            }}
            h2 {{
                color: #333;
            }}
            p {{
                color: #555;
                font-size: 16px;
            }}
            .button {{
                display: inline-block;
                padding: 12px 24px;
                background-color: #28a745;
                color: white;
                font-size: 16px;
                text-decoration: none;
                border-radius: 5px;
                margin-top: 20px;
            }}
            .footer {{
                margin-top: 20px;
                font-size: 12px;
                color: #888;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h2>Password Reset Request</h2>
            <p>We received a request to reset your password. Click the button below to proceed:</p>
            <a href='https://localhost:7225/api/Auth/{url}' class='button'>Reset Password</a>
            <p>If you did not request this, please ignore this email.</p>
            <p class='footer'>This link will expire soon. Please reset your password promptly.</p>
        </div>
    </body>
    </html>";

            return emailBody;
        }

        public string PasswordRestBody(string newPassword)
        {
            string emailBody = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 0;
            }}
            .container {{
                max-width: 600px;
                margin: 20px auto;
                background: #ffffff;
                padding: 20px;
                border-radius: 10px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                text-align: center;
            }}
            h2 {{
                color: #333;
            }}
            p {{
                color: #555;
                font-size: 16px;
            }}
            .password-box {{
                display: inline-block;
                padding: 10px;
                background-color: #f8f9fa;
                border: 1px solid #ddd;
                border-radius: 5px;
                font-size: 18px;
                font-weight: bold;
                margin-top: 10px;
            }}
            .footer {{
                margin-top: 20px;
                font-size: 12px;
                color: #888;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h2>Password Reset Successful</h2>
            <p>Your password has been reset successfully. Here is your new temporary password:</p>
            <div class='password-box'>{newPassword}</div>
            <p>Please log in and change your password immediately to keep your account secure.</p>
            <p class='footer'>If you did not request this change, please contact support immediately.</p>
        </div>
    </body>
    </html>";

            return emailBody;
        }
    }
}

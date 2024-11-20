using BlazorHybridBackend.Interfaces.Services;
using BlazorHybridBackend.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BlazorHybridBackend.Services
{
    public class EmailService(IOptions<SmtpConfiguration> smtpConfig) : IEmailService
    {
        private readonly SmtpConfiguration _smtpConfig = smtpConfig.Value;

        public async Task<bool> SendConfirmationEmail(string email, string confirmationToken)
        {
            return await SendEmail(
                email,
                "Please Confirm Your Email",
                $"Please click the following link to confirm your email: <a href='{_smtpConfig.AppBaseUrl}/confirm-email?token={confirmationToken}'>Confirm Email</a>"
            );
        }

        public async Task<bool> SendPasswordResetEmail(string email, string resetToken)
        {
            return await SendEmail(
                email,
                "Reset Your Password",
                $"Please click the following link to reset your password: <a href='{_smtpConfig.AppBaseUrl}/recover-password?token={resetToken}'>Reset Password</a>"
            );
        }

        private async Task<bool> SendEmail(string email, string subject, string htmlBody)
        {
            try
            {
                var message = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_smtpConfig.FromAddress),
                };
                message.From.Add(new MailboxAddress(_smtpConfig.FromName, _smtpConfig.FromAddress));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(
                    _smtpConfig.SmtpServer,
                    _smtpConfig.Port,
                    SecureSocketOptions.StartTls
                );
                await client.AuthenticateAsync(_smtpConfig.Username, _smtpConfig.AppPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                // Log the error as needed
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine($"Sending email to {to}: {subject}\n{body}");
            await Task.CompletedTask;
        }
    }
}

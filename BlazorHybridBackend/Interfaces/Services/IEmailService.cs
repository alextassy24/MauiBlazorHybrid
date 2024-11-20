namespace BlazorHybridBackend.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendConfirmationEmail(string email, string confirmationToken);
        Task<bool> SendPasswordResetEmail(string email, string resetToken);
         Task SendEmailAsync(string to, string subject, string body);
    }
}

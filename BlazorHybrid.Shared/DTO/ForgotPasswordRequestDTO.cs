
using System.ComponentModel.DataAnnotations;

namespace BlazorHybrid.Shared.DTO
{
    public class ForgotPasswordRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
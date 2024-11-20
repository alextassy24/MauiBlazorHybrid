using System.ComponentModel.DataAnnotations;

namespace BlazorHybridBackend.Models
{
    public class VerifyRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(6)]
        public string VerificationCode { get; set; } = string.Empty;
    }

    public class VerifyResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}

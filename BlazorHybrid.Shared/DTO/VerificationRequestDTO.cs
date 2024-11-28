using System.ComponentModel.DataAnnotations;

namespace BlazorHybrid.Shared.DTO
{
    public class VerificationRequestDto
    {
        [Required(ErrorMessage = "Email is required."), EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Verification code is required.")]
        [StringLength(6, ErrorMessage = "Verification code must be 6 digits.", MinimumLength = 6)]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Verification code must be numeric.")]
        public string Code { get; set; } = string.Empty;
    }
}
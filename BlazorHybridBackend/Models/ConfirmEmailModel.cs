namespace BlazorHybridBackend.Models
{
    public class ConfirmEmailModel
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
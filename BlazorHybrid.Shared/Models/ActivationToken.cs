using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorHybrid.Shared.Models
{
    public class ActivationToken
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsConfirmed { get; set; } = false;

    }
}
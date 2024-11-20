using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorHybrid.Shared.Models
{
    public class Weight
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal Value { get; set; }
    }
}
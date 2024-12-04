using System;
using System.Collections.Generic;

namespace BlazorHybrid.Shared.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = "Uncategorized";
        public decimal TotalCalories { get; set; } = 0;
        public decimal TotalProteins { get; set; } = 0;
        public decimal TotalCarbohydrates { get; set; } = 0;
        public decimal TotalFats { get; set; } = 0;
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public List<MealItem> MealItems { get; set; } = [];
    }
}

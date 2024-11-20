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
        public decimal TotalCalories { get; set; }
        public decimal TotalProteins { get; set; }
        public decimal TotalCarbohydrates { get; set; }
        public decimal TotalFats { get; set; }
        public DateTime DateAdded { get; set; }
        public List<MealItem> MealItems { get; set; }

        public Meal()
        {
            MealItems = [];
            DateAdded = DateTime.UtcNow;
        }
    }
}

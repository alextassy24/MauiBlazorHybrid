using System.ComponentModel.DataAnnotations;
using BlazorHybrid.Shared.Models.Enums;

namespace BlazorHybrid.Shared.Models
{
    public class MealItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }
        public decimal Proteins { get; set; } = 0;
        public decimal Carbohydrates { get; set; } = 0;
        public decimal Fats { get; set; } = 0;
        public decimal Calories { get; set; } = 0;
        public UnitMeasure UnitMeasure { get; set; }

        public MealItem()
        {
            Calories = 4 * Proteins + 4 * Carbohydrates + 9 * Fats;
        }
    }
}

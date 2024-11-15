using BlazorHybrid.Models.Enums;

namespace BlazorHybrid.Models
{
    public class MealItem
    {
        public int Id { get; set; }
        public int? Weight { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fats { get; set; }
        public int Calories { get; set; }
        public UnitMeasure UnitMeasure { get; set; }
    }
}

using BlazorHybrid.Shared.Models;
using BlazorHybrid.Shared.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace BlazorHybridBackend.Models
{
    public class User : IdentityUser
    {
        // Custom properties
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int Height { get; set; } = 0;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;
        public bool IsClient { get; set; }
        public bool IsTrainer { get; set; }
        public Guid? TrainerId { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public int DaysStreak { get; set; } = 0;

        // Relationships
        public ActivationToken? ActivationToken { get; set;}
        public List<Workout> Workouts { get; set; } = [];
        public List<Meal> Meals { get; set; } = [];
        public List<Guid> ClientIds { get; set; } = [];
        public List<Weight> Weights { get; set; } = [];
        public List<string> MealCategories { get; set; } = ["Breakfast", "Lunch", "Dinner"];
    }
}

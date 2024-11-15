using BlazorHybrid.Models.Enums;

namespace BlazorHybrid.Models
{
    public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool IsClient { get; set; }
    public Guid TrainerId { get; set; }
    public bool IsTrainer { get; set; }
    public DateTime LastLogin { get; set; }
    public int DaysStreak { get; set; }
    public List<Workout> Workouts { get; set; }
    public List<Meal> Meals { get; set; }
    public List<User> Clients { get; set; }

    public User()
    {
        Workouts = [];
        Meals = [];
        Clients = [];
    }
}

}

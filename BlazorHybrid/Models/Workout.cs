namespace BlazorHybrid.Models
{
    public class Workout
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateCompleted { get; set; }
    public List<Exercise> Exercises { get; set; }

    public Workout()
    {
        Exercises = [];
    }
}

}
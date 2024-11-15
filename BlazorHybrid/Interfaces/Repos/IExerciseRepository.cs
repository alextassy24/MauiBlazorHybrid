using BlazorHybrid.Models;

namespace BlazorHybrid.Interfaces.Repos
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        List<Exercise> GetExercisesByWorkoutId(int workoutId);
    }
}

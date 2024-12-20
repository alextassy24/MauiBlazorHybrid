using BlazorHybrid.Shared.Models;

namespace BlazorHybrid.Interfaces.Repos
{
    public interface IWorkoutRepository : IRepository<Workout> 
    {
        List<Workout> GetWorkoutsByUserId(Guid userId);
    }
}

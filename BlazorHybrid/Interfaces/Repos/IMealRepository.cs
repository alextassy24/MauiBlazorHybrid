using BlazorHybrid.Models;

namespace BlazorHybrid.Interfaces.Repos
{
    public interface IMealRepository : IRepository<Meal>
    {
        List<Meal> GetMealsByUserId(Guid userId);
    }
}

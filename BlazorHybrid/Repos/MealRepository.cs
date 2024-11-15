using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Models;

namespace BlazorHybrid.Repos
{
    public class MealRepository : IMealRepository
    {
        private readonly List<Meal> _meals = [];

        public Meal GetById(object id) => _meals.FirstOrDefault(m => m.Id == (int)id) ?? new Meal();

        public List<Meal> GetAll() => _meals;

        public void Add(Meal meal) => _meals.Add(meal);

        public void Update(Meal meal)
        {
            var index = _meals.FindIndex(m => m.Id == meal.Id);
            if (index != -1) _meals[index] = meal;
        }

        public void Delete(object id) => _meals.RemoveAll(m => m.Id == (int)id);

        public List<Meal> GetMealsByUserId(Guid userId)
        {
            return _meals.Where(m => m.UserId == userId).ToList();
        }
    }
}

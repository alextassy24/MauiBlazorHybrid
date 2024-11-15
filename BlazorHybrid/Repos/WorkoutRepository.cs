using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Models;

namespace BlazorHybrid.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly List<Workout> _workouts = [];

        public Workout GetById(object id) => _workouts.FirstOrDefault(w => w.Id == (int)id) ?? new Workout();

        public List<Workout> GetAll() => _workouts;

        public void Add(Workout workout) => _workouts.Add(workout);

        public void Update(Workout workout)
        {
            var index = _workouts.FindIndex(w => w.Id == workout.Id);
            if (index != -1) _workouts[index] = workout;
        }

        public void Delete(object id) => _workouts.RemoveAll(w => w.Id == (int)id);

        public List<Workout> GetWorkoutsByUserId(Guid userId)
        {
            return _workouts.Where(w => w.UserId == userId).ToList();
        }
    }
}

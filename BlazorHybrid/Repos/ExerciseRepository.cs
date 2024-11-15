using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Models;

namespace BlazorHybrid.Repos
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly List<Exercise> _exercises = [];

        public Exercise GetById(object id) => _exercises.FirstOrDefault(e => e.Id == (int)id) ?? new Exercise();

        public List<Exercise> GetAll() => _exercises;

        public void Add(Exercise exercise) => _exercises.Add(exercise);

        public void Update(Exercise exercise)
        {
            var index = _exercises.FindIndex(e => e.Id == exercise.Id);
            if (index != -1) _exercises[index] = exercise;
        }

        public void Delete(object id) => _exercises.RemoveAll(e => e.Id == (int)id);

        public List<Exercise> GetExercisesByWorkoutId(int workoutId)
        {
            return _exercises.Where(e => e.WorkoutId == workoutId).ToList();
        }
    }
}

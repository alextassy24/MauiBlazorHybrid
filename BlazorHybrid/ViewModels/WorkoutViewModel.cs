using System.Collections.ObjectModel;
using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Models;

namespace BlazorHybrid.ViewModels
{
    public class WorkoutViewModel
    {
        private readonly IWorkoutRepository _workoutRepository;

        public ObservableCollection<Workout> Workouts { get; private set; } = new();

        public WorkoutViewModel(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
            LoadWorkouts();
        }

        private void LoadWorkouts()
        {
            var workouts = _workoutRepository.GetAll();
            Workouts = [.. workouts];
        }
    }
}

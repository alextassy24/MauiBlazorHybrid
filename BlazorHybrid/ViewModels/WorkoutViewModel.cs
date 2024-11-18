using System.Collections.ObjectModel;
using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorHybrid.ViewModels
{
    public class WorkoutViewModel
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly NavigationManager _navigationManager;

        public ObservableCollection<Workout> Workouts { get; private set; } = [];

        public WorkoutViewModel(IWorkoutRepository workoutRepository, NavigationManager navigationManager)
        {
            _workoutRepository = workoutRepository;
            _navigationManager = navigationManager;
            LoadWorkouts();
        }

        public void LoadWorkouts()
        {
            var workouts = _workoutRepository.GetAll();
            Workouts = [.. workouts];
        }

        public void NavigateToAddWorkout()
        {
            _navigationManager.NavigateTo("/add-workout");
        }

        public void NavigateToDetails(int id)
        {
            _navigationManager.NavigateTo($"/workout/{id}");
        }

        public void NavigateToEdit(int id)
        {
            _navigationManager.NavigateTo($"/edit-workout/{id}");
        }

        public void DeleteWorkout(int id)
        {
            _workoutRepository.Delete(id);
            var workoutToRemove = Workouts.FirstOrDefault(w => w.Id == id);
            if (workoutToRemove != null)
            {
                Workouts.Remove(workoutToRemove);
            }
        }
    }
}

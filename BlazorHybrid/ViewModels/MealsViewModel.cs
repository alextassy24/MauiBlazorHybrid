using System.Collections.ObjectModel;
using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorHybrid.ViewModels
{
    public class MealsViewModel
    {
        private readonly IMealRepository _mealRepository;
        private readonly NavigationManager _navigationManager;

        public ObservableCollection<Meal> Meals { get; private set; } = [];
        public Meal? SelectedMeal { get; private set; }

        public MealsViewModel(IMealRepository mealRepository, NavigationManager navigationManager)
        {
            _mealRepository = mealRepository;
            _navigationManager = navigationManager;
            LoadMeals();
        }

        private void LoadMeals()
        {
            var meals = _mealRepository.GetAll();
            Meals = [.. meals];
        }

        public void AddMeal()
        {
            var newMeal = new Meal
            {
                Id = Meals.Count + 1,
                Name = "New Meal",
                Description = "Description for new meal",
                UserId = Guid.NewGuid()
            };
            _mealRepository.Add(newMeal);
            Meals.Add(newMeal);
        }

        public void RemoveMeal(int mealId)
        {
            _mealRepository.Delete(mealId);
            var mealToRemove = Meals.FirstOrDefault(m => m.Id == mealId);
            if (mealToRemove != null)
            {
                Meals.Remove(mealToRemove);
            }
        }

        public void LoadMeal(int mealId)
        {
            SelectedMeal = _mealRepository.GetById(mealId);
        }

        public void EditMeal(int mealId)
        {
            _navigationManager.NavigateTo($"/edit-meal/{mealId}");
        }

        public void ViewMeal(int mealId)
        {
            _navigationManager.NavigateTo($"/meal/{mealId}");
        }

        public void NavigateBackToMeals()
        {
            _navigationManager.NavigateTo("/meals");
        }
    }
}

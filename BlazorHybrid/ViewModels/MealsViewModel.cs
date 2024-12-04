using System.Collections.ObjectModel;
using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Shared.DTO;
using BlazorHybrid.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorHybrid.ViewModels
{
    public class MealsViewModel
    {
        private readonly IMealRepository _mealRepository;
        private readonly NavigationManager _navigationManager;
        private readonly UserState _userState;
        private readonly string UserId;
        public ObservableCollection<Meal> Meals { get; private set; } = [];
        public Meal? SelectedMeal { get; private set; }

        public MealsViewModel(IMealRepository mealRepository, NavigationManager navigationManager, UserState UserState)
        {
            _mealRepository = mealRepository;
            _navigationManager = navigationManager;
            _userState = UserState;
            UserId = _userState.LoggedInUser.Id;
            LoadMeals();
        }

        public void LoadMeals()
        {
            var meals = _mealRepository.GetMealsByUserId(UserId);
            Meals = [.. meals];
        }
        
        public void AddMeal(string category)
        {
            var newMeal = new Meal
            {
                Id = Meals.Count + 1,
                Name = "New Meal",
                Description = "Description for new meal",
                Category = category,
                UserId = Guid.NewGuid(),
            };
            _mealRepository.Add(newMeal);
            Meals.Add(newMeal);
            SelectedMeal = newMeal;
        }

        public void AddMealItem(MealItem? item)
        {
            if (SelectedMeal == null)
                return;

            item.Id = SelectedMeal.MealItems.Any() ? SelectedMeal.MealItems.Max(i => i.Id) + 1 : 1;
            SelectedMeal.MealItems.Add(item);
            SelectedMeal.TotalCarbohydrates += item.Carbohydrates;
            SelectedMeal.TotalFats += item.Fats;
            SelectedMeal.TotalProteins += item.Proteins;
            SelectedMeal.TotalCalories += item.Calories;
            UpdateMealNutrition();
            _mealRepository.Update(SelectedMeal);
        }

        public void UpdateMealItem(MealItem? item)
        {
            if (SelectedMeal == null)
                return;

            var existingItem = SelectedMeal.MealItems.Any() ? SelectedMeal.MealItems.FirstOrDefault(i => i.Id == item.Id) : null;
            if (existingItem != null)
            {
                var index = SelectedMeal.MealItems.IndexOf(existingItem);
                SelectedMeal.MealItems[index] = item;
                UpdateMealNutrition();
                _mealRepository.Update(SelectedMeal);
            }
        }

        public void RemoveMealItem(int itemId)
        {
            if (SelectedMeal == null)
                return;

            var item = SelectedMeal.MealItems.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                SelectedMeal.MealItems.Remove(item);
                UpdateMealNutrition();
                _mealRepository.Update(SelectedMeal);
            }
        }

        private void UpdateMealNutrition()
        {
            if (SelectedMeal == null)
                return;

            SelectedMeal.TotalCalories = SelectedMeal.MealItems.Sum(i => i.Calories);
            SelectedMeal.TotalProteins = SelectedMeal.MealItems.Sum(i => i.Proteins);
            SelectedMeal.TotalCarbohydrates = SelectedMeal.MealItems.Sum(i => i.Carbohydrates);
            SelectedMeal.TotalFats = SelectedMeal.MealItems.Sum(i => i.Fats);
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

        public IEnumerable<Meal> GetMealsByCategory(string category)
        {
            return Meals.Where(m => m.Category == category);
        }

        public void NavigateBackToMeals()
        {
            _navigationManager.NavigateTo("/meals");
        }
    }
}

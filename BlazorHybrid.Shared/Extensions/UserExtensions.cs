using System;
using System.Linq;
using BlazorHybrid.Shared.DTO;
using BlazorHybrid.Shared.Models;

namespace BlazorHybrid.Shared.Extensions
{
    public static class UserExtensions
    {
        public static decimal GetLastWeight(this UserDto? userDto)
        {
            if (userDto?.Weights == null || userDto.Weights.Count == 0)
                return 0;

            return userDto.Weights.Last().Value;
        }

        public static int DaysSinceLastWeight(this UserDto? userDto)
        {
            if (userDto?.Weights == null || userDto.Weights.Count == 0)
                return 0;

            return (DateTime.UtcNow.Date - userDto.Weights.Last().DateAdded.Date).Days;
        }

        public static Workout? GetLastWorkout(this UserDto? userDto)
        {
            if (userDto?.Workouts == null || userDto.Workouts.Count == 0)
                return null;

            return userDto.Workouts.Last();
        }

        public static int DaysSinceLastWorkout(this UserDto? userDto)
        {
            if (userDto?.Workouts == null || userDto.Workouts.Count == 0)
                return 0;

            return (DateTime.UtcNow.Date - userDto.Workouts.Last().DateCreated.Date).Days;
        }

        public static int GetDaysStreak(this UserDto? userDto)
        {
            return userDto?.DaysStreak ?? 0;
        }
    }
}
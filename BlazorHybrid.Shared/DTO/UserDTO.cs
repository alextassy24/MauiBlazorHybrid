using System;
using System.Collections.Generic;
using BlazorHybrid.Shared.Models;
using BlazorHybrid.Shared.Models.Enums;

namespace BlazorHybrid.Shared.DTO
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;// Unique identifier for the user
        public string FirstName { get; set; } = string.Empty; // User's first name
        public string LastName { get; set; } = string.Empty; // User's last name
        public string Email { get; set; } = string.Empty; // User's email address
        public string PhoneNumber { get; set; } = string.Empty; // User's email address
        public DateTime DateOfBirth { get; set; } // User's date of birth
        public Gender Gender { get; set; } // Gender as a string (e.g., "Male", "Female")
        public string Address { get; set; } = string.Empty; // User's address
        public string City { get; set; } = string.Empty; // User's city
        public string Country { get; set; } = string.Empty; // User's country
        public bool IsClient { get; set; } // Indicates if the user is a client
        public bool IsTrainer { get; set; } // Indicates if the user is a trainer
        public string? TrainerId { get; set; } // Trainer's ID, if applicable
        public DateTime LastLogin { get; set; } // Timestamp of the user's last login
        public int DaysStreak { get; set; } // Number of consecutive days logged in or active

        // Relationships
        public ActivationToken? ActivationToken { get; set;}
        public List<Guid> ClientIds { get; set; } = []; // List of client IDs if the user is a trainer
        public List<Workout> Workouts { get; set; } = []; // Workouts associated with the user
        public List<Meal> Meals { get; set; } = []; // Meals associated with the user
    }
}

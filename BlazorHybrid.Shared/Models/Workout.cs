using System;
using System.Collections.Generic;
using BlazorHybrid.Shared.Models.Enums;

namespace BlazorHybrid.Shared.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateCompleted { get; set; }
        public Level Level { get; set; }
        public Category Category { get; set; }
        public List<Exercise> Exercises { get; set; } = [];
        public bool IsCompleted { get; set; } = false;
    }
}

using System.Collections.Generic;
using BlazorHybrid.Shared.Models.Enums;

namespace BlazorHybrid.Shared.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<WorkingSet> WorkingSets { get; set; }
        public int? RestTime { get; set; }
        public int? Duration { get; set; }

        public Exercise()
        {
            WorkingSets = [];
        }
    }
}

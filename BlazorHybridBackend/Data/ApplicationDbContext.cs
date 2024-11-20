#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlazorHybrid.Shared.Models;
using BlazorHybridBackend.Models;

namespace BlazorHybridBackend.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        public DbSet<ActivationToken> ActivationTokens { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkingSet> WorkingSets { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<MealItem> MealItems { get; set; }
    }
}

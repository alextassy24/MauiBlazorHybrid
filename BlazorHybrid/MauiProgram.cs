using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Interfaces.Services;
using BlazorHybrid.Repos;
using BlazorHybrid.Repositories;
using BlazorHybrid.Services;
using BlazorHybrid.Shared.DTO;
using BlazorHybrid.ViewModels;
using Microsoft.Extensions.Logging;

namespace BlazorHybrid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // Add base URL for API

        builder.Services.AddTransient<UsersViewModel>();
        builder.Services.AddTransient<UserViewModel>();
        builder.Services.AddTransient<EditUserViewModel>();
        builder.Services.AddTransient<WorkoutViewModel>();
        builder.Services.AddTransient<MealsViewModel>();
        // builder.Services.AddTransient<MealModel>();
        // builder.Services.AddTransient<EditMealViewModel>();
        builder.Services.AddSingleton<IUserRepository, UserRepository>();
        builder.Services.AddSingleton<IWorkoutRepository, WorkoutRepository>();
        builder.Services.AddSingleton<IExerciseRepository, ExerciseRepository>();
        builder.Services.AddSingleton<IMealRepository, MealRepository>();
        builder.Services.AddSingleton<UserState>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IHttpService, HttpService>();
        builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri("http://10.0.2.2:5159/") });

        builder.Services.AddScoped<IAuthService, AuthService>();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();

        return builder.Build();
    }
}

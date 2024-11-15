using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Interfaces.Services;
using BlazorHybrid.Repos;
using BlazorHybrid.Repositories;
using BlazorHybrid.Services;
using BlazorHybrid.ViewModels;
using Microsoft.Extensions.Logging;

namespace BlazorHybrid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.Services.AddTransient<UserViewModel>();
        builder.Services.AddTransient<WorkoutViewModel>();

        builder.Services.AddSingleton<IUserRepository, UserRepository>();
        builder.Services.AddSingleton<IWorkoutRepository, WorkoutRepository>();
        builder.Services.AddSingleton<IExerciseRepository, ExerciseRepository>();
        builder.Services.AddSingleton<IMealRepository, MealRepository>();
        builder.Services.AddSingleton<IUserService, UserService>();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

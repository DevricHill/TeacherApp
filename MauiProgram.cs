using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeacherApp.Data;
using TeacherApp.Services;
using TeacherApp.ViewModels;

namespace TeacherApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<StudentServices>();
            builder.Services.AddSingleton<StudentViewModel>();
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddDbContext<StudentContext>();
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<StudentContext>();
                try
                {
                    db.Database.EnsureCreated();
                    System.Diagnostics.Debug.WriteLine($"DB PATH: {db.Database.GetDbConnection().DataSource}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"DB INIT FAILED:\n{ex}");
                    throw;
                }
            }

            return app;
        }
    }
}

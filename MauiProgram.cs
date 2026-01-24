using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using JournalApp.Data;
using JournalApp.Services;

namespace JournalApp
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
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<AuthState>();
            builder.Services.AddSingleton<ThemeState>();
            builder.Services.AddScoped<TagService>();



            var dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "journal.db");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            return app;
        }
    }
}

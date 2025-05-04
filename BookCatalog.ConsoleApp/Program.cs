using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BookCatalog.Data;
using BookCatalog.Data.Models;
using BookCatalog.Services.Implementations;
using BookCatalog.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using BookCatalog.ConsoleApp;

var configuration = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory) // Ensure Microsoft.Extensions.Hosting is referenced
               .AddJsonFile("appsettings.json", optional: false)
               .Build();

var services = new ServiceCollection();

services.AddLogging(config =>
{
    config.ClearProviders(); // Премахва всички лог системи
});

services.AddDbContext<BookCatalogDbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<IBookService, BookService>();
services.AddScoped<IGenreService, GenreService>();
services.AddScoped<IAuthorService, AuthorService>();

var serviceProvider = services.BuildServiceProvider();

services.AddScoped<IGenreService, GenreService>();

//  SEED Жанрове при първо стартиране
using (var scope = serviceProvider.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookCatalogDbContext>();

    if (!context.Genres.Any())
    {
        context.Genres.AddRange(
            new Genre { Name = "Action" },
            new Genre { Name = "Comedy" },
            new Genre { Name = "Drama" }

        );
        context.SaveChanges();
        Console.WriteLine("Genres seeded successfully!");
    }
}

// Старт на приложението
using var runScope = serviceProvider.CreateScope();
var app = new App(
    runScope.ServiceProvider.GetRequiredService<IBookService>(),
    runScope.ServiceProvider.GetRequiredService<IGenreService>(),
    runScope.ServiceProvider.GetRequiredService<IAuthorService>()

);
app.Run();

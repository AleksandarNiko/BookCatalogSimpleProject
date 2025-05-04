using BookCatalog.Data.Models;
using BookCatalog.Services.Interfaces;
using BookCatalog.Data.Models;
using static BookCatalog.Common.EntityValidationConstants;

namespace BookCatalog.ConsoleApp
{
    public class App
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;

        public App(IBookService movieService, IGenreService genreService, IAuthorService actorService)
        {
            _bookService = movieService;
            _genreService = genreService;
            _authorService = actorService;
        }

        public void Run()
        {
            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.Clear();
                Console.WriteLine("=== Book Catalog ===");
                Console.WriteLine("1. List Books");
                Console.WriteLine("2. Add Book");
                Console.WriteLine("3. Edit Book");
                Console.WriteLine("4. Delete Book");
                Console.WriteLine("5. Manage Genres");
                Console.WriteLine("6. Manage Authors");
                Console.WriteLine("7. Search Books");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1": ListBooksAsync().Wait(); break;
                    case "2": AddBookAsync().Wait(); break;
                    case "3": EditBookAsync().Wait(); break;
                    case "4": DeleteBookAsync().Wait(); break;
                    case "5": GenreMenuAsync().Wait(); break;
                    case "6": AuthorMenuAsync().Wait(); break;
                    case "7": SearchBookAsync().Wait(); break;

                    case "0": exitRequested = true; break;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }

        private async Task ListBooksAsync()
        {
            Console.Clear();
            var books = await _bookService.GetAllAsync();

            if (books.Count == 0)
                Console.WriteLine("No books found.");
            else
            {
                Console.WriteLine("=== Books ===");
                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Id}. {book.Title} ({book.Year}) - Rating: {book.Rating}/10 - Genre: {book.Genre?.Name ?? "N/A"}");
                }
            }

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }

        private async Task AddBookAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Add New Book ===");

            Console.Write("Title: ");
            string? title = Console.ReadLine();

            Console.Write("Year: ");
            if (!int.TryParse(Console.ReadLine(), out int year)) { Console.WriteLine("Invalid year!"); Console.ReadLine(); return; }

            Console.Write("Rating (0-10): ");
            if (!double.TryParse(Console.ReadLine(), out double rating)) { Console.WriteLine("Invalid rating!"); Console.ReadLine(); return; }

            Console.Write("Genre Id: ");
            if (!int.TryParse(Console.ReadLine(), out int genreId)) { Console.WriteLine("Invalid genre!"); Console.ReadLine(); return; }

            var book = new Data.Models.Book { Title = title!, Year = year, Rating = rating, GenreId = genreId };
            await _bookService.AddAsync(book);

            Console.WriteLine("Book added successfully!");
            Console.ReadLine();
        }

        private async Task EditBookAsync()
        {
            Console.Clear();
            Console.Clear();
            Console.Write("Enter book ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); Console.ReadLine(); return; }

            var book = await _bookService.GetByIdAsync(id);
            if (book == null) { Console.WriteLine("Movie not found."); Console.ReadLine(); return; }

            Console.Write($"New title ({book.Title}): ");
            string? title = Console.ReadLine();
            book.Title = string.IsNullOrWhiteSpace(title) ? book.Title : title;

            Console.Write($"New year ({book.Year}): ");
            if (int.TryParse(Console.ReadLine(), out int year)) book.Year = year;

            Console.Write($"New rating ({book.Rating}): ");
            if (double.TryParse(Console.ReadLine(), out double rating)) book.Rating = rating;

            Console.Write($"New genre ID ({book.GenreId}): ");
            if (int.TryParse(Console.ReadLine(), out int genreId)) book.GenreId = genreId;

            await _bookService.UpdateAsync(book);
            Console.WriteLine("Movie updated!");
            Console.ReadLine();
        }

        private async Task DeleteBookAsync()
        {
            Console.Clear();
            Console.Write("Enter book ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); Console.ReadLine(); return; }

            var book = await _bookService.GetByIdAsync(id);
            if (book == null) { Console.WriteLine("Book not found."); Console.ReadLine(); return; }

            Console.Write($"Are you sure you want to delete '{book.Title}'? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                await _bookService.DeleteAsync(id);
                Console.WriteLine("Book deleted.");
            }
            else
            {
                Console.WriteLine("Cancelled.");
            }
            Console.ReadLine();
        }

        private async Task GenreMenuAsync()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Genres ===");
                Console.WriteLine("1. List Genres");
                Console.WriteLine("2. Add Genre");
                Console.WriteLine("3. Edit Genre");
                Console.WriteLine("4. Delete Genre");
                Console.WriteLine("0. Back");
                Console.Write("Choose: ");

                switch (Console.ReadLine())
                {
                    case "1": await ListGenresAsync(); break;
                    case "2": await AddGenreAsync(); break;
                    case "3": await EditGenreAsync(); break;
                    case "4": await DeleteGenreAsync(); break;
                    case "0": back = true; break;
                    default: Console.WriteLine("Invalid."); Console.ReadLine(); break;
                }
            }
        }

        private async Task ListGenresAsync()
        {
            Console.Clear();
            var genres = await _genreService.GetAllAsync();
            foreach (var genre in genres)
            {
                Console.WriteLine($"{genre.Id}. {genre.Name}");
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        private async Task AddGenreAsync()
        {
            Console.Clear();
            Console.Write("New genre name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) { Console.WriteLine("Invalid name."); Console.ReadLine(); return; }
            await _genreService.AddAsync(new Data.Models.Genre { Name = name });
            Console.WriteLine("Genre added.");
            Console.ReadLine();
        }

        private async Task EditGenreAsync()
        {
            Console.Clear();
            Console.Write("Enter genre ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); Console.ReadLine(); return; }

            var genre = await _genreService.GetByIdAsync(id);
            if (genre == null) { Console.WriteLine("Genre not found."); Console.ReadLine(); return; }

            Console.Write($"New name ({genre.Name}): ");
            string? name = Console.ReadLine();
            genre.Name = string.IsNullOrWhiteSpace(name) ? genre.Name : name;

            await _genreService.UpdateAsync(genre);
            Console.WriteLine("Genre updated.");
            Console.ReadLine();
        }

        private async Task DeleteGenreAsync()
        {
            Console.Clear();
            Console.Write("Enter genre ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); Console.ReadLine(); return; }

            var genre = await _genreService.GetByIdAsync(id);
            if (genre == null) { Console.WriteLine("Genre not found."); Console.ReadLine(); return; }

            Console.Write($"Delete '{genre.Name}'? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                await _genreService.DeleteAsync(id);
                Console.WriteLine("Genre deleted.");
            }
            else
            {
                Console.WriteLine("Cancelled.");
            }
            Console.ReadLine();
        }

        private async Task AuthorMenuAsync()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Authors ===");
                Console.WriteLine("1. List Authors");
                Console.WriteLine("2. Add Author");
                Console.WriteLine("3. Edit Author");
                Console.WriteLine("4. Delete Author");
                Console.WriteLine("0. Back");
                Console.Write("Choose: ");

                switch (Console.ReadLine())
                {
                    case "1": await ListAuthorsAsync(); break;
                    case "2": await AddAuthorAsync(); break;
                    case "3": await EditAuthorAsync(); break;
                    case "4": await DeleteAuthorAsync(); break;
                    case "0": back = true; break;
                    default: Console.WriteLine("Invalid."); Console.ReadLine(); break;
                }
            }
        }

        private async Task ListAuthorsAsync()
        {
            Console.Clear();
            var authors = await _authorService.GetAllAsync();
            foreach (var actor in authors)
            {
                Console.WriteLine($"{actor.Id}. {actor.Name} (Born: {actor.DateOfBirth:yyyy-MM-dd})");
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        private async Task AddAuthorAsync()
        {
            Console.Clear();
            Console.Write("Author name: ");
            var name = Console.ReadLine();
            Console.Write("Birth date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
            {
                Console.WriteLine("Invalid date.");
                Console.ReadLine();
                return;
            }
            await _authorService.AddAsync(new Data.Models.Author { Name = name!, DateOfBirth = birthDate });
            Console.WriteLine("Author added.");
            Console.ReadLine();
        }

        private async Task EditAuthorAsync()
        {
            Console.Clear();
            Console.Write("Enter Author ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); Console.ReadLine(); return; }

            var author = await _authorService.GetByIdAsync(id);
            if (author == null) { Console.WriteLine("Author not found."); Console.ReadLine(); return; }

            Console.Write($"New name ({author.Name}): ");
            string? name = Console.ReadLine();
            author.Name = string.IsNullOrWhiteSpace(name) ? author.Name : name;

            Console.Write($"New birth date ({author.DateOfBirth:yyyy-MM-dd}): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime birthDate)) author.DateOfBirth = birthDate;

            await _authorService.UpdateAsync(author);
            Console.WriteLine("Author updated.");
            Console.ReadLine();
        }

        private async Task SearchBookAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Search Books ===");

            Console.Write("Title contains (leave empty to skip): ");
            var titlePart = Console.ReadLine();

            Console.Write("Year (leave empty to skip): ");
            var yearInput = Console.ReadLine();
            int? year = int.TryParse(yearInput, out int y) ? y : null;

            Console.Write("Genre (leave empty to skip): ");
            var genreInput = Console.ReadLine();
            int? genreId = int.TryParse(genreInput, out int g) ? g : null;

            var allMovies = await _bookService.GetAllAsync();

            var filtered = allMovies
                 .Where(m =>
                     (string.IsNullOrWhiteSpace(titlePart) || m.Title.Contains(titlePart, StringComparison.OrdinalIgnoreCase)) &&
                 (!year.HasValue || m.Year == year) &&
                 (!genreId.HasValue || m.GenreId == genreId)
                 )
                 .ToList();

            Console.WriteLine("\nSort by:");
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Year");
            Console.WriteLine("3. Rating");
            Console.Write("Choose (or leave empty for no sorting): ");
            var sortOption = Console.ReadLine();

            filtered = sortOption switch
            {
                "1" => filtered.OrderBy(m => m.Title).ToList(),
                "2" => filtered.OrderBy(m => m.Year).ToList(),
                "3" => filtered.OrderByDescending(m => m.Rating).ToList(),
                _ => filtered
            };

            Console.WriteLine($"\nFound {filtered.Count} book(s):");

            foreach (var book in filtered)
            {
                Console.WriteLine($"{book.Id}. {book.Title} ({book.Year}) - {book.Rating}/10 - Genre: {book.Genre.Name ?? "N/A"}");
            }

            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
        }


        private async Task DeleteAuthorAsync()
        {
            Console.Clear();
            Console.Write("Enter author ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); Console.ReadLine(); return; }

            var author = await _authorService.GetByIdAsync(id);
            if (author == null) { Console.WriteLine("Author not found."); Console.ReadLine(); return; }

            Console.Write($"Delete '{author.Name}'? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                await _authorService.DeleteAsync(id);
                Console.WriteLine("Author deleted.");
            }
            else
            {
                Console.WriteLine("Cancelled.");
            }
            Console.ReadLine();
        }
    }
}

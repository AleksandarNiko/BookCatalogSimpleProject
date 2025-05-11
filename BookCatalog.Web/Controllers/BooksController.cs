using BookCatalog.Data;
using BookCatalog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using BookCatalog.Web.Models;
using System.Security.Claims;

namespace BookCatalog.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookCatalogDbContext _context;

        public BooksController(BookCatalogDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(m => m.Genre)
                .ToListAsync();
            return View(books);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new BookFormViewModel();
            model.Genres = await GetGenres();

            return View(model);
        }

        private async Task<IEnumerable<GenreViewModel>> GetGenres()
        {
            return await _context.Genres
                 .AsNoTracking()
                 .Select(data => new GenreViewModel
                 {

                     Id = data.Id,
                     Name = data.Name

                 })
                 .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                model.Genres = await GetGenres();
                return View(model);
            }

            var entity = new Book()
            {
                Title = model.Title,
                Year = model.Year,
                Rating = model.Rating,
                GenreId = model.GenreId,
            };

            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books
               .Select(p => new BookEditViewModel
               {
                   Id = p.Id,
                   Title = p.Title,
                   Year = p.Year,
                   Rating = p.Rating,
                   GenreId = p.GenreId,
               }).AsNoTracking().FirstOrDefaultAsync();

            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            book.Genres = await GetGenres();

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookEditViewModel model, int Id)
        {
           
            Book? productt = await _context.Books
                .Where(p => p.Id == Id)
                .FirstOrDefaultAsync();

            if (productt != null)
            {
                productt.Title = model.Title;
                productt.Year = model.Year;

                productt.Rating = model.Rating;
                productt.GenreId = model.GenreId;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { Id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _context.Books
                .Where(e => e.Id == id)
                .AsNoTracking()
                .Select(e => new BookDetailsViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    Year  = e.Year,
                    Rating = e.Rating,
                    GenreName = e.Genre.Name
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

    }
}

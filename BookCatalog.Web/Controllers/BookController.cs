using BookCatalog.Data;
using BookCatalog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly BookCatalogDbContext _context;

        public BookController(BookCatalogDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Books
                .Include(m => m.Genre)
                .ToListAsync();
            return View(movies);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Title", "Author","Language");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Genre"] = new SelectList(_context.Genres, "Id", "Name", book.Genre);
            return View(book);
        }
    }
}

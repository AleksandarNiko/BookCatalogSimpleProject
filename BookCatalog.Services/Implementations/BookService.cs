using Microsoft.EntityFrameworkCore;
using BookCatalog.Data;
using BookCatalog.Data.Models;
using BookCatalog.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookCatalog.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly BookCatalogDbContext _context;

        public BookService(BookCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(m => m.Genre)
                .Include(m => m.BookAuthors)
                .ThenInclude(ma => ma.Author)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(m => m.Genre)
                .Include(m => m.BookAuthors)
                .ThenInclude(ma => ma.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Book movie)
        {
            _context.Books.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book movie)
        {
            _context.Books.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}

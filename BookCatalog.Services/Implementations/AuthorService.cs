using BookCatalog.Data;
using BookCatalog.Data.Models;
using Microsoft.EntityFrameworkCore;
using BookCatalog.Data;
using BookCatalog.Data.Models;
using BookCatalog.Services.Interfaces;

namespace BookCatalog.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly BookCatalogDbContext _context;

        public AuthorService(BookCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAsync()
            => await _context.Authors.ToListAsync();

        public async Task<Author?> GetByIdAsync(int id)
            => await _context.Authors.FindAsync(id);

        public async Task AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }
    }
}

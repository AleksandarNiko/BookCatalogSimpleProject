using BookCatalog.Data;
using BookCatalog.Data.Models;
using Microsoft.EntityFrameworkCore;
using BookCatalog.Data;
using BookCatalog.Data.Models;
using BookCatalog.Services.Interfaces;

namespace BookCatalog.Services.Implementations
{
    public class AuthorService : IActorService
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

        public async Task AddAsync(Author actor)
        {
            _context.Authors.Add(actor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author actor)
        {
            _context.Authors.Update(actor);
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

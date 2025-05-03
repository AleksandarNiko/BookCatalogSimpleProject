using Microsoft.EntityFrameworkCore;
using BookCatalog.Data;
using BookCatalog.Data.Models;
using BookCatalog.Services.Interfaces;

namespace BookCatalog.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly BookCatalogDbContext _context;

        public GenreService(BookCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetAllAsync()
            => await _context.Genres.ToListAsync();

        public async Task<Genre?> GetByIdAsync(int id)
            => await _context.Genres.FindAsync(id);

        public async Task AddAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using BookCatalog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookCatalog.Services.Interfaces
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAllAsync();
        Task<Genre?> GetByIdAsync(int id);
        Task AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(int id);
    }
}

using BookCatalog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookCatalog.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book movie);
        Task UpdateAsync(Book movie);
        Task DeleteAsync(int id);
    }
}

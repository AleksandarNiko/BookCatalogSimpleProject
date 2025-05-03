using BookCatalog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookCatalog.Services.Interfaces
{
    public interface IActorService
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task AddAsync(Author actor);
        Task UpdateAsync(Author actor);
        Task DeleteAsync(int id);
    }
}


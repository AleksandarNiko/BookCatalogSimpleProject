using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BookCatalog.Web.Models
{
    public class BookFormViewModel
    {
        
        public  string Title { get; set; }

        public  int Year { get; set; }

        public  int Rating { get; set; }

        public  int GenreId { get; set; }
        public IEnumerable<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
    }
}

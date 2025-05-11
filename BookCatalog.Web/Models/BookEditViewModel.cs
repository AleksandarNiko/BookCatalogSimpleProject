namespace BookCatalog.Web.Models
{
    public class BookEditViewModel
    {
        public int Id { get; set; }
        public  string Title { get; set; }
        public  int Year { get; set; }

        public  double Rating { get; set; }

        public  int GenreId { get; set; }

        public  IEnumerable<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
    }
}

namespace BookCatalog.Web.Models
{
    public class BookDetailsViewModel
    {
        public  int Id { get; set; }
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rating { get; set; }

        public string GenreName { get; set; }
    }
}

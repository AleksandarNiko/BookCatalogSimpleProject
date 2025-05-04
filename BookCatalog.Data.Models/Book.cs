using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCatalog.Data.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Common.EntityValidationConstants.Book.TitleMaxLength)]
        public string Title { get; set; } = null!;

        public int Year { get; set; }

        public double Rating { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; } = null!;


        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}

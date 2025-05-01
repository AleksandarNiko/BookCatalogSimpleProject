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

        [Required]
        [MaxLength(Common.EntityValidationConstants.Author.NameMaxLength)]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(Common.EntityValidationConstants.Genre.NameMaxLength)]
        public string Genre { get; set; } = null!;

        public DateTime PublishedDate { get; set; }

        [Required]
        [MaxLength(Common.EntityValidationConstants.Book.ISBNMaxLength)]
        public string ISBN { get; set; } = null!;

        [Required]
        [MaxLength(Common.EntityValidationConstants.Publisher.NameMaxLength)]
        public string Publisher { get; set; } = null!;
        public int PageCount { get; set; }

        [Required]
        [MaxLength(Common.EntityValidationConstants.Book.LanguageMaxLength)]
        public string Language { get; set; } = null!;

        [Required]
        [MaxLength(Common.EntityValidationConstants.Book.DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        // Navigation properties
        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Data.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Common.EntityValidationConstants.Author.NameMaxLength)]
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }

        // Navigation property
        public virtual ICollection<BookAuthor> Books { get; set; } = new List<BookAuthor>();


    }
}

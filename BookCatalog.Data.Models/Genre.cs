using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Data.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Common.EntityValidationConstants.Genre.NameMaxLength)]
        public string Name { get; set; } = null!;



        // Navigation property
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

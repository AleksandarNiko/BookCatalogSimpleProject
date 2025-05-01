using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Data.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Common.EntityValidationConstants.Publisher.NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(Common.EntityValidationConstants.Publisher.AddressMaxLength)]
        public string Address { get; set; }= null!;

        [Required]
        [MaxLength(Common.EntityValidationConstants.Publisher.PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(Common.EntityValidationConstants.Publisher.EmailMaxLength)]
        public string Email { get; set; } = null!;

        // Navigation property
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(15, ErrorMessage = "First Name must between 6 to 15 characters ", MinimumLength = 6)]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, ErrorMessage = "Last Name must between 6 to 15 characters ", MinimumLength = 6)]
        public string LName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}

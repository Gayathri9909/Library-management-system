using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace library.Models
{
    public class BookModel
    {
        [Required]
        [Display(Name = "Book ID")]
        public int BookId { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Book title cannot exceed 255 characters.")]
        [Display(Name = "Book Title")]
        public string BookTitle { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Author name cannot exceed 255 characters.")]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number of books must be at least 1.")]
        [Display(Name = "Number of Books")]
        public int NumberOfBooks { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        public bool IsChecked {  get; set; }
    }
    public class RequestModel
    {
        public List<BookModel> Books { get; set; }
    }
}
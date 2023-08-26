using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {

        // primary key property
        public int Id { get; set; }

        [Required]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a title.")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter author name.")]
        public string Author { get; set; } = string.Empty;

        [Range(1.0, 1000.0, ErrorMessage = "Price must be between [1-1000].")]
        public double Price { get; set; }

        // foreign key property
        [Required(ErrorMessage = "Please select a category.")]
        public int BookCategoryId { get; set; }

        // navigation properties
        [ValidateNever]
        public BookCategory BookCategory { get; set; } = null!;

        public string Description { get; set; } = string.Empty;
    }
}

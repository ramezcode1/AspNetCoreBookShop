using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookCategory
    {
        // initialize navigation property collection in constructor
        public BookCategory() => Books = new HashSet<Book>();

        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter category name.")]
        public string Name { get; set; } = string.Empty;

        // navigation property
        public ICollection<Book> Books { get; set; }
    }
}

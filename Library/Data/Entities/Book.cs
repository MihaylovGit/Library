using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Library.Data.DataConstants.Book;

namespace Library.Data.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BookTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(BookAuthorMaxLength)]
        public string Author { get; set; }

        [Required]
        [MaxLength(BookDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Range(typeof(decimal), "0.00", "10.00")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rating { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        public Category? Category { get; set; }

        public List<ApplicationUserBook> UsersMovies { get; set; } = new List<ApplicationUserBook>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Common;

namespace Library.Data.Models
{
	public class Book
	{
		public Book()
		{
			UserBooks = new HashSet<IdentityUserBook>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(ValidationConstants.Book.TitleMaxLength)]
		public string Title { get; set; } = null!;

		[Required]
		[MaxLength(ValidationConstants.Author.AuthorMaxLength)]
		public string Author { get; set; } = null!;

		[Required]
		[MaxLength(ValidationConstants.Description.DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required] 
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Range(ValidationConstants.Rating.RatingMinValue,ValidationConstants.Rating.RatingMaxValue)]
		public decimal Rating { get; set; }

		[Required]
		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }

		[Required]
		public Category Category { get; set; }

		public ICollection<IdentityUserBook> UserBooks { get; set; }
	}
}

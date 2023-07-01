using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Library.Data.ValidationConstants;

namespace Library.Data.Entities
{
	public class Book
	{
		public Book()
		{
			UsersBooks = new HashSet<IdentityUserBook>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(BookValidationConstants.TitleMaxLength)]
		public string Title { get; set; } = null!;

		[Required]
		[MaxLength(BookValidationConstants.AuthorMaxLength)]
		public string Author { get; set; } = null!;

		[Required]
		[MaxLength(BookValidationConstants.DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required] 
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Range(BookValidationConstants.RatingMinValue,BookValidationConstants.RatingMaxValue)]
		public decimal Rating { get; set; }

		[Required]
		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }

		[Required]
		public Category Category { get; set; }

		public ICollection<IdentityUserBook> UsersBooks { get; set; }
	}
}

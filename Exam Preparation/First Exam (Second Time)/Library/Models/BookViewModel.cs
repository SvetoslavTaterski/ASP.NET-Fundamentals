using Library.Data.Entities;
using static Library.Data.ValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
	public class BookViewModel
	{
		public int Id { get; set; }

		[StringLength(BookValidationConstants.TitleMaxLength,MinimumLength = BookValidationConstants.TitleMinLength)]
		public string Title { get; set; } = null!;

		[StringLength(BookValidationConstants.AuthorMaxLength,MinimumLength = BookValidationConstants.AuthorMinLength)]
		public string Author { get; set; } = null!;

		[StringLength(BookValidationConstants.DescriptionMaxLength,MinimumLength = BookValidationConstants.DescriptionMinLength)]
		public string Description { get; set; } = null!;

		public string ImageUrl { get; set; } = null!;

		[Range(BookValidationConstants.RatingMinValue, BookValidationConstants.RatingMaxValue)]
		public decimal Rating { get; set; }

		public int CategoryId { get; set; }

		public string Category { get; set; } = null!;

		public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
	}
}

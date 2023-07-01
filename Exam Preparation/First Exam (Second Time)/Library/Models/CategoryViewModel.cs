using Library.Data.Entities;
using static Library.Data.ValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
	public class CategoryViewModel
	{
		public int Id { get; set; }

		[StringLength(CategoryValidationConstants.NameMaxLength,MinimumLength = CategoryValidationConstants.NameMinLength)]
		public string Name { get; set; } = null!;

		public ICollection<Book> Books { get; set; } = new HashSet<Book>();
	}
}

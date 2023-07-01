using System.ComponentModel.DataAnnotations;
using static Library.Data.ValidationConstants;

namespace Library.Data.Entities
{
	public class Category
	{
		public Category()
		{
			Books = new HashSet<Book>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(CategoryValidationConstants.NameMaxLength)]
		public string Name { get; set; } = null!;

		public ICollection<Book> Books { get; set; }
	}
}

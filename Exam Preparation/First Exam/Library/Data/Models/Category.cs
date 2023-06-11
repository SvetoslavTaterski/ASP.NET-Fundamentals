using System.ComponentModel.DataAnnotations;
using Library.Common;

namespace Library.Data.Models
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
		[MaxLength(ValidationConstants.CategoryName.NameMaxLength)]
		public string Name { get; set; } = null!;

		public ICollection<Book> Books { get; set; }
	}
}

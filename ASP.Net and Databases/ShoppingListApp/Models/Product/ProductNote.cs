using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListApp.Models.Product
{
	public class ProductNote
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Note { get; set; } = null!;

		[Required]
		public DateTime NoteDate { get; set; }

		[Required]
		public int ProductId { get; set; }

		[Required]
		[ForeignKey (nameof(ProductId))]
		public ProductViewModel ProductViewModel { get; set; } = null!;
	}
}

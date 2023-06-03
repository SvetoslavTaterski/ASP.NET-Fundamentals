using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Models.Product
{
	public class ProductViewModel
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(50)]
		public string Name { get; set; } = null!;

		public List<ProductNote> ProductNotes { get; set; } = new List<ProductNote>();
	}
}

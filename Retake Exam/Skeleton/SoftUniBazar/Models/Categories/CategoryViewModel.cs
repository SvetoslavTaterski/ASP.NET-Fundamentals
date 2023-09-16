using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Models.Categories
{
	public class CategoryViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required] 
		public string Name { get; set; } = null!;
	}
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SoftUniBazar.Data.Entities
{
	public class Ad
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(25)]
		public string Name { get; set; } = null!;

		[Required] 
		[MaxLength(250)] 
		public string Description { get; set; } = null!;

		[Required]
		public decimal Price { get; set; }

		[Required] 
		public string OwnerId { get; set; } = null!;

		[Required] 
		public IdentityUser Owner { get; set; } = null!;

		[Required] 
		public string ImageUrl { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }

		[Required] 
		public Category Category { get; set; } = null!;
	}
}

using System.ComponentModel.DataAnnotations;
using SoftUniBazar.Models.Categories;

namespace SoftUniBazar.Models.Ads
{
	public class AddAdsViewModel
	{
		[Required]
		[StringLength(25,MinimumLength = 5)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(250, MinimumLength = 15)]
		public string Description { get; set; } = null!;

		[Required] 
		public string ImageUrl { get; set; } = null!;

		public string CreatedOn { get; set; } = null!;

		public string OwnerId { get; set; } = null!;

		[Required]
		public decimal Price { get; set; }

		public int CategoryId { get; set; }

		public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
	}
}

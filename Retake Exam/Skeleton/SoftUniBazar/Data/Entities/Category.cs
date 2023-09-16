using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Data.Entities
{
	public class Category
	{
		public Category()
		{
			Ads = new HashSet<Ad>();
		}

		[Key]
		public int Id { get; set; }

		[Required] 
		[MaxLength(15)] 
		public string Name { get; set; } = null!;

		public ICollection<Ad> Ads { get; set; }
	}
}

using System.ComponentModel.DataAnnotations;
using static Homies.Data.ValidationConstants;

namespace Homies.Models.Types
{
	public class TypeViewModel
	{
		public int Id { get; set; }

		[StringLength(TypeValidationConstants.NameMaxLength,MinimumLength = TypeValidationConstants.NameMinLength)]
		public string Name { get; set; } = null!;
	}
}
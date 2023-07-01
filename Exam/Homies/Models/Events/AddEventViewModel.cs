using System.ComponentModel.DataAnnotations;
using Homies.Models.Types;
using Microsoft.AspNetCore.Identity;
using static Homies.Data.ValidationConstants;

namespace Homies.Models.Events
{
	public class AddEventViewModel
	{
		public AddEventViewModel()
		{
			Types = new HashSet<TypeViewModel>();
		}

		public int Id { get; set; }

		[StringLength(EventValidationConstants.NameMaxLength,MinimumLength = EventValidationConstants.NameMinLength)]
		public string Name { get; set; } = null!;

		[StringLength(EventValidationConstants.DescriptionMaxLength,MinimumLength = EventValidationConstants.DescriptionMinLength)]
		public string Description { get; set; } = null!;

		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		public int TypeId { get; set; }

        public string OrganiserId { get; set; } = null!;

		public IdentityUser Organiser { get; set; } = null!;

        public IEnumerable<TypeViewModel> Types { get; set; }
	}
}
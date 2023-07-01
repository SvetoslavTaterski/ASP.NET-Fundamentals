using Homies.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Type = Homies.Data.Entities.Type;

namespace Homies.Models.Events
{
	public class AllEventsViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public DateTime Start { get; set; }

        public IdentityUser Organiser { get; set; } = null!;

        public string Type { get; set; } = null!;

		public int TypeId { get; set; }
	}
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static Homies.Data.ValidationConstants;

namespace Homies.Data.Entities
{
	public class Event
	{
		public Event()
		{
			EventsParticipants = new HashSet<EventParticipant>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(EventValidationConstants.NameMaxLength)]
		public string Name { get; set; } = null!;

		[Required]
		[MaxLength(EventValidationConstants.DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required] 
		public string OrganiserId { get; set; } = null!;

		[Required] 
		public IdentityUser Organiser { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public DateTime Start { get; set; }

		[Required]
		public DateTime End { get; set; }

		[Required]
		[ForeignKey(nameof(Type))]
		public int TypeId { get; set; }

		[Required] 
		public Type Type { get; set; } = null!;

		public ICollection<EventParticipant> EventsParticipants { get; set; }
	}
}

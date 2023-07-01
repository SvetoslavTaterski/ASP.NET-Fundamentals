using Microsoft.AspNetCore.Identity;
using Homies.Data.Entities;
using Type = Homies.Data.Entities.Type;

namespace Homies.Models.Events
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Organiser { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int TypeId { get; set; }

        public Type Type { get; set; } = null!;
    }
}

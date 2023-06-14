using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Contacts.Data.Models;

namespace Contacts.Data.Models
{
    public class ApplicationUserContact
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; } = null!;

        [ForeignKey(nameof(Contact))]
        public int ContactId { get; set; }

        public Contact Contact { get; set; } = null!;
    }
}

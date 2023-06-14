using System.ComponentModel.DataAnnotations;

namespace Contacts.Data.Models
{
    public class Contact
    {
        public Contact()
        {
            ApplicationUsersContacts = new HashSet<ApplicationUserContact>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ContactValidations.FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.ContactValidations.LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.ContactValidations.EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.ContactValidations.PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }

        [Required]
        public string Website { get; set; } = null!;

        public ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; }
    }
}

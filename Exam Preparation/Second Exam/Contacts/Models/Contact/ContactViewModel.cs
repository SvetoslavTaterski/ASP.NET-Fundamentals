using Contacts.Data;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Models.Contact
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.ContactValidations.FirstNameMaxLength,MinimumLength = ValidationConstants.ContactValidations.FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.ContactValidations.LastNameMaxLength,MinimumLength = ValidationConstants.ContactValidations.LastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.ContactValidations.EmailMaxLength,MinimumLength = ValidationConstants.ContactValidations.EmailMinLength)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.ContactValidations.PhoneNumberMaxLength,MinimumLength = ValidationConstants.ContactValidations.PhoneNumberMinLength)]
        [RegularExpression(ValidationConstants.ContactValidations.PhoneRegex)]
        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }

        [Required]
        [RegularExpression(ValidationConstants.ContactValidations.WebSiteRegex)]
        public string Website { get; set; } = null!;
    }
}

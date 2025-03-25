using System.ComponentModel.DataAnnotations;

namespace EStoreAPI.Models
{
    public class CustomerForCreationDto
    {
        [Required(ErrorMessage = "Förnamn är obligatoriskt")]
        [MaxLength(50, ErrorMessage = "Förnamn får inte vara längre än 50 tecken")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn är obligatoriskt")]
        [MaxLength(50, ErrorMessage = "Efternamn får inte vara längre än 50 tecken")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email är obligatoriskt")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
        [MaxLength(100, ErrorMessage = "E-postadress får inte vara längre än 100 tecken")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobilnummer är obligatoriskt")]
        [Phone(ErrorMessage = "Ogiltigt mobilnummer")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Gatuadress är obligatoriskt")]
        [MaxLength(200, ErrorMessage = "Gatuadress får inte vara längre än 200 tecken")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Postort är obligatoriskt")]
        [MaxLength(100, ErrorMessage = "Postort får inte vara längre än 100 tecken")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postnummer är obligatoriskt")]
        [MaxLength(50, ErrorMessage = "Postnummer får inte vara längre än 50 tecken")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Land är obligatoriskt")]
        [MaxLength(50, ErrorMessage = "Land får inte vara längre än 50 tecken")]
        public string Country { get; set; }
    }
}

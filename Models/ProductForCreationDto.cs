using System.ComponentModel.DataAnnotations;

namespace EStoreAPI.Models
{
    public class ProductForCreationDto
    {
        [Required(ErrorMessage = "Produktnummer är obligatoriskt")]
        [MaxLength(50, ErrorMessage = "Produktnummer får inte vara längre än 50 tecken")]
        public string ProductNumber { get; set; }

        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [MaxLength(100, ErrorMessage = "Namn får inte vara längre än 100 tecken")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Beskrivning är obligatoriskt")]
        [MaxLength(500, ErrorMessage = "Beskrivning får inte vara längre än 500 tecken")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pris är obligatoriskt")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Pris måste vara större än 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Produktkategori är obligatoriskt")]
        [MaxLength(100, ErrorMessage = "Produktkategori får inte vara längre än 100 tecken")]
        public string Category { get; set; }

        public bool IsDiscontinued { get; set; } 
    }
}

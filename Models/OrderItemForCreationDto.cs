using EStoreAPI.Validation;
using System.ComponentModel.DataAnnotations;

namespace EStoreAPI.Models
{
    public class OrderItemForCreationDto
    {
        [Required(ErrorMessage = "Produkt-ID är obligatoriskt")]
        [MaxLength(50, ErrorMessage = "Produkt-ID får inte vara längre än 50 tecken")]
        [ObjectIdValidation(ErrorMessage = "Ogiltigt ProductId. Måste vara ett giltigt ObjectId")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Antal är obligatoriskt")]
        [Range(1, int.MaxValue, ErrorMessage = "Antal måste vara större än 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Pris är obligatoriskt")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Pris måste vara större än 0")]
        public decimal Price { get; set; }
    }
}

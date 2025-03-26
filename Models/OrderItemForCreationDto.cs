using System.ComponentModel.DataAnnotations;

namespace EStoreAPI.Models
{
    public class OrderItemForCreationDto
    {
        [Required(ErrorMessage = "Produkt är obligatorisk")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Antal är obligatoriskt")]
        [Range(1, int.MaxValue, ErrorMessage = "Antalet måste vara större än 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Pris är obligatoriskt")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Pris måste vara större än 0")]
        public decimal Price { get; set; }
    }
}

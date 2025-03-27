using System.ComponentModel.DataAnnotations;

using EStoreAPI.Validation;

namespace EStoreAPI.Models
{
    public class OrderForCreationDto
    {
        [Required(ErrorMessage = "Du måste välja en kund")]
        [MaxLength(50, ErrorMessage = "Kund-ID får inte vara längre än 50 tecken")]
        [ObjectIdValidation(ErrorMessage = "Ogiltigt CustomerId. Måste vara ett giltigt ObjectId")]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Ordern måste innehålla minst en produkt")]
        public List<OrderItemForCreationDto> Items { get; set; }

        [Required(ErrorMessage = "Orderdatum är obligatoriskt")]
        public DateTime OrderDate { get; set; }
    }
}

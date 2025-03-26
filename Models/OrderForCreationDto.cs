using System.ComponentModel.DataAnnotations;

namespace EStoreAPI.Models
{
    public class OrderForCreationDto
    {
        [Required(ErrorMessage = "Du måste välja en kund")]
        [MaxLength(50, ErrorMessage = "Kund-ID får inte vara längre än 50 tecken")]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Ordern måste innehålla minst en produkt")]
        public List<OrderItemForCreationDto> Items { get; set; }

        [Required(ErrorMessage = "Orderdatum är obligatoriskt")]
        public DateTime OrderDate { get; set; }
    }
}

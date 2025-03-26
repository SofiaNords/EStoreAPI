using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EStoreAPI.Models
{
    public class OrderForCreationDto
    {
        [Required(ErrorMessage = "Kund är obligatorisk")]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Ordern måste innehålla minst en produkt")]
        public List<OrderItemForCreationDto> Items { get; set; }

        [Required(ErrorMessage = "Orderdatum är obligatoriskt")]
        public DateTime OrderDate { get; set; }
    }
}

namespace EStoreAPI.Models
{
    public class OrderItemForCreationDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

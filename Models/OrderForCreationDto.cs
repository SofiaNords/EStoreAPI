namespace EStoreAPI.Models
{
    public class OrderForCreationDto
    {
        public string CustomerId { get; set; }
        public List<OrderItemForCreationDto> Items { get; set; }
    }
}

namespace EStoreAPI.Models
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

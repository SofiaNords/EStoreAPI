namespace EStoreAPI.Entities
{
    public class OrderItem
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}

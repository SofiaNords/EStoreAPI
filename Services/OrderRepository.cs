using EStoreAPI.Entities;
using MongoDB.Driver;

namespace EStoreAPI.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderRepository(IMongoDatabase database)
        {
            _orderCollection = database.GetCollection<Order>("orders");
        }

        public async Task<Order?> GetOrderAsync(string orderId)
        {
            return await _orderCollection
                .Find(o => o.Id == orderId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId)
        {
            return await _orderCollection
                .Find(o => o.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderCollection.InsertOneAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, order.Id);
            await _orderCollection.ReplaceOneAsync(filter, order);
        }

        public async Task DeleteOrderAsync(string orderId)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, orderId);
            await _orderCollection.DeleteOneAsync(filter);
        }

    }
}

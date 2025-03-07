using EStoreAPI.Entities;

namespace EStoreAPI.Services
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderAsync(string orderId);
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(string orderId);

    }
}

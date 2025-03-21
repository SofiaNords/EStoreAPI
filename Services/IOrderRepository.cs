using EStoreAPI.Entities;

namespace EStoreAPI.Services
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderAsync(string orderId);
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(string orderId);

    }
}

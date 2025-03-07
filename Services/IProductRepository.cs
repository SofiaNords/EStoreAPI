using EStoreAPI.Entities;

namespace EStoreAPI.Services
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllCustomersAsync();

        Task<Product?> GetCustomerAsync(string productId);
        Task AddCustomerAsync(Product product);

        Task DeleteCustomerAsync(string productId);

        Task UpdateCustomerAsync(Product product);
    }
}

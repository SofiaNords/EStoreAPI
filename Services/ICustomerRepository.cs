using EStoreAPI.Entities;

namespace EStoreAPI.Services
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        Task<Customer?> GetCustomerAsync(string customerId);

        Task<Customer?> GetCustomerEmailAsync(string email);
        Task AddCustomerAsync(Customer customer);

        Task DeleteCustomerAsync(string customerId);

        Task UpdateCustomerAsync(Customer customer);

    }
}

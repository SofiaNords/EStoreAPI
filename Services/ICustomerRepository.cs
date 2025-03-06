using EStoreAPI.Entities;

namespace EStoreAPI.Services
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        //Task<Customer?> GetCustomerAsync(int customerId);

        //Task AddCustomerAsync(Customer customer);

        //Task DeleteCustomerAsync(Customer customer);

        //Task UpdateCustomerAsync(Customer customer);

    }
}

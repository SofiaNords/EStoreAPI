using EStoreAPI.Data;
using EStoreAPI.Entities;
using MongoDB.Driver;

namespace EStoreAPI.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerRepository(IMongoDatabase database)
        {
            _customerCollection = database.GetCollection<Customer>("customer");
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerCollection.Find(c => true).ToListAsync();
        }

        public async Task<Customer?> GetCustomerAsync(string customerId)
        {
            var customer = await _customerCollection
                .Find(c => c.Id == customerId)
                .FirstOrDefaultAsync();

            return customer;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _customerCollection.InsertOneAsync(customer);
        }

        

        //Task ICustomerRepository.DeleteCustomerAsync(Customer customer)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Customer?> ICustomerRepository.GetCustomerAsync(int customerId)
        //{
        //    throw new NotImplementedException();
        //}

      

        //Task ICustomerRepository.UpdateCustomerAsync(Customer customer)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

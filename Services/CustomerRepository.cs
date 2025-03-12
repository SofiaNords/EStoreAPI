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

        public async Task DeleteCustomerAsync(string customerId)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, customerId);
            var result = await _customerCollection.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                throw new KeyNotFoundException("Customer not found");
            }
        }
  
        public async Task UpdateCustomerAsync(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, customer.Id);
            var result = await _customerCollection.ReplaceOneAsync(filter, customer);
        }

        public async Task<Customer?> GetCustomerEmailAsync(string email)
        {
            return await _customerCollection
                .Find(c => c.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}

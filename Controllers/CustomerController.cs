using EStoreAPI.Data;
using EStoreAPI.Entities;
using EStoreAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return Ok(customers);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Customer>> GetById(string id)
        //{
        //    var filter = Builders<Customer>.Filter.Eq("_id", new ObjectId(id));
        //    var customer = await _customers.Find(filter).FirstOrDefaultAsync();
        //    return customer != null ? Ok(customer) : NotFound();
        //}

        //[HttpPost]
        //public async Task<ActionResult> Create(Customer customer)
        //{
        //    await _customers.InsertOneAsync(customer);
        //    return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        //}

        //[HttpPut]
        //public async Task<ActionResult> Update(Customer customer)
        //{
        //    var filter = Builders<Customer>.Filter.Eq("_id", new ObjectId(customer.Id));

        //    await _customers.ReplaceOneAsync(filter, customer);
        //    return Ok();
        //}

        //[HttpDelete]
        //public async Task<ActionResult> Delete(string id)
        //{
        //    var filter = Builders<Customer>.Filter.Eq("_id", new ObjectId(id));
        //    await _customers.DeleteOneAsync(filter);
        //    return Ok();
        //}
    }
}

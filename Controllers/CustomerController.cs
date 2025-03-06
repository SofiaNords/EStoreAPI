using AutoMapper;
using EStoreAPI.Models;
using EStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EStoreAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            var customerDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customerDto);
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

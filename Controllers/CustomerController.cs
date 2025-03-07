using AutoMapper;
using EStoreAPI.Entities;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(string id)
        {
            var customer = await _customerRepository.GetCustomerAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerForCreationDto customerForCreationDto)
        {
            var customer = _mapper.Map<Customer>(customerForCreationDto);

            await _customerRepository.AddCustomerAsync(customer);

            var customerDto = _mapper.Map<CustomerDto>(customer);
          
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var customer = await _customerRepository.GetCustomerAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            await _customerRepository.DeleteCustomerAsync(id);

            return NoContent();
        }


        [HttpPut]
        public async Task<ActionResult> Update(string customerId, CustomerForUpdateDto customerForUpdateDto)
        {
            var customer = await _customerRepository.GetCustomerAsync(customerId);

            if (customerId != customer.Id)
            {
                return NotFound();
            }

            _mapper.Map(customerForUpdateDto, customer);

            await _customerRepository.UpdateCustomerAsync(customer);

            var updatedCustomerDto = _mapper.Map<CustomerDto>(customer);

            return Ok(updatedCustomerDto);
        }

  
    }
}

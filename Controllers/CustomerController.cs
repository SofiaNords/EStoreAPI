using AutoMapper;
using EStoreAPI.Entities;
using EStoreAPI.Models;
using EStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <response code="200">Returns a list of customers. If no customers are found, returns an empty list.</response>
        /// <response code="404">No customers found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers([FromQuery] string? searchQuery)
        {
            var customers = await _customerRepository.GetAllCustomersAsync(searchQuery); 

            if (customers == null || !customers.Any()){

                return NotFound();
            }

            var customerDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customerDto);
        }

        /// <summary>
        /// Get a specific customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the customer details if found</response>
        /// <response code="404">If no customer with the specified id is found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="customerForCreationDto">The customer information to create</param>
        /// <response code="201">Returns the created customer with a location header to access the newly created customer</response>
        /// <response code="409">Conflict, if a customer with the provided email already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), 201)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerForCreationDto customerForCreationDto)
        {
            var existingCustomerEmail = await _customerRepository.GetCustomerEmailAsync(customerForCreationDto.Email);
            if (existingCustomerEmail != null)
            {
                return Conflict("Customer with this email address already exists.");
            }

            var customer = _mapper.Map<Customer>(customerForCreationDto);

            await _customerRepository.AddCustomerAsync(customer);

            var customerDto = _mapper.Map<CustomerDto>(customer);
          
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customerDto);
        }


        /// <summary>
        /// Delete a customer by id
        /// </summary>
        /// <param name="id">The unique identifier of the customer to delete</param>
        /// <response code="204">The customer was successfully deleted</response>
        /// <response code="400">If the id is invalid or the format is incorrect</response>
        /// <response code="404">If no customer with the specified id was found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteCustomer(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid customer id");
            }

            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid customer id format");
            }

            var customer = await _customerRepository.GetCustomerAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            await _customerRepository.DeleteCustomerAsync(id);

            return NoContent();
        }


        /// <summary>
        /// Update a customer by id
        /// </summary>
        /// <param name="id">The unique identifier of the customer to update</param>
        /// <param name="customerForUpdateDto">The customer information to update</param>
        /// <response code="200">If the customer was successfully updated</response>
        /// <response code="400">If the id is invalid or the update data is incomplete</response>
        /// <response code="404">If no customer with the specified id was found</response>
        /// <response code="500">If an internal server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomerForUpdateDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Update(string id, CustomerForUpdateDto customerForUpdateDto)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid customer id format");
            }

            var customer = await _customerRepository.GetCustomerAsync(id);

            if (customer == null)
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

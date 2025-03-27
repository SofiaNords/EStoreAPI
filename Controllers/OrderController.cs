using AutoMapper;
using EStoreAPI.Entities;
using EStoreAPI.Models;
using EStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EStoreAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <response code="200">
        /// Returns a list of orders. If no orders are found, returns an empty list.
        /// </response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            if (orders == null || !orders.Any())
            {
                return Ok(new List<OrderDto>());
            }

            var orderDto = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(orderDto);
        }


        /// <summary>
        /// Get a specific order by id
        /// </summary>
        /// <param name="id">The unique identifier of the order</param>
        /// <response code="200">Returns the order details</response>
        /// <response code="404">If no order with the specified id is found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<OrderDto>> GetOrderById(string id)
        {
            var order = await _orderRepository.GetOrderAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }


        /// <summary>
        /// Add a new order
        /// </summary>
        /// <param name="orderForCreationDto">The order information to create</param>
        /// <response code="201">Returns the created order with a location header to access the newly created order</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), 201)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderForCreationDto orderForCreationDto)
        {
            var order = _mapper.Map<Order>(orderForCreationDto);

            try
            {
                await _orderRepository.AddOrderAsync(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the order.");
            }

            var orderDto = _mapper.Map<OrderDto>(order);

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, orderDto);
        }
    }
}

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
        /// Returns a list with orders
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(orderDto);
        }


        /// <summary>
        /// Get a specifik order by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a specifik order by id</response>
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
        /// <response code="201">Returns the created order</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), 201)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderForCreationDto orderForCreationDto)
        {
            var order = _mapper.Map<Order>(orderForCreationDto);

            await _orderRepository.AddOrderAsync(order);

            var orderDto = _mapper.Map<OrderDto>(order);

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, orderDto);
        }
    }
}

using AutoMapper;
using EStoreAPI.Entities;
using EStoreAPI.Models;
using EStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EStoreAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentException(nameof(productRepository));
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <response code="200">
        /// Returns a list with products
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(
            [FromQuery] string? name, string? searchQuery, string? productNumber)
        {
            var products = await _productRepository.GetAllProductsAsync(name, searchQuery, productNumber);

            if (products == null || !products.Any())
            {
                return NotFound();
            }

            var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productDto);
        }

        /// <summary>
        /// Get a specifik product by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">
        /// Returns a specific product by id
        /// </response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProductDto>> GetProductById(string id)
        {
            var product = await _productRepository.GetProductAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="productForCreationDto">
        /// The product information to create
        /// </param>
        /// <response code="201">
        /// Returns the created product
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), 201)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductForCreationDto productForCreationDto)
        {
            var existingProduct = await _productRepository.GetProductByProductNumberAsync(productForCreationDto.ProductNumber);
            if (existingProduct != null)
            {
                return Conflict("Product with this product number already exists.");
            }

            var product = _mapper.Map<Product>(productForCreationDto);

            await _productRepository.AddProductAsync(product);

            var productDto = _mapper.Map<ProductDto>(product); 

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }


        /// <summary>
        /// Delete a product by id
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the product to delete
        /// </param>
        /// <response code="204">The product was successfully deleted</response>
        /// <response code="400">If the id is invalid</response>
        /// <response code="404">If the product was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]  
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid product id");
            }

            var product = await _productRepository.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteProductAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Update a product by id
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the product to update</param>
        /// <param name="productForUpdateDto">
        /// The product information to update
        /// </param>
        /// <response code="200">If the product was successfully updated</response>
        /// <response code="400">If the id is invalid or the update data is invalid</response>
        /// <response code="404">If the product was not found</response>
        /// <response code="409">If a product with the same product number already exists</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)] 
        [ProducesResponseType(400)]  
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> Update(string id, ProductForUpdateDto productForUpdateDto)
        {
            var product = await _productRepository.GetProductAsync(id);

            if (id != product.Id)
            {
                return NotFound();
            }

            var existingProduct = await _productRepository.GetProductByProductNumberAsync(productForUpdateDto.ProductNumber);
            if (existingProduct != null && existingProduct.Id != product.Id)
            {
                return Conflict("Product with this product number already exists.");
            }

            _mapper.Map(productForUpdateDto, product);

            await _productRepository.UpdateProductAsync(product);

            var updatedProductDto = _mapper.Map<ProductDto>(product);

            return Ok(updatedProductDto);
        }
    }
}

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();

            var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productDto);
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductForCreationDto productForCreationDto)
        {
            var product = _mapper.Map<Product>(productForCreationDto);

            await _productRepository.AddProductAsync(product);

            var productDto = _mapper.Map<ProductDto>(product); 

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var product = await _productRepository.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteProductAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> Update(string id, ProductForUpdateDto productForUpdateDto)
        {
            var product = await _productRepository.GetProductAsync(id);

            if (id != product.Id)
            {
                return NotFound();
            }

            _mapper.Map(productForUpdateDto, product);

            await _productRepository.UpdateProductAsync(product);

            var updatedProductDto = _mapper.Map<ProductDto>(product);

            return Ok(updatedProductDto);
        }
    }
}

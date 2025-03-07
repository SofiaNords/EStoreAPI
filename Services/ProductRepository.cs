using EStoreAPI.Entities;
using MongoDB.Driver;

namespace EStoreAPI.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductRepository(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("product");
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productCollection.Find(p => true).ToListAsync();
        }

        public async Task<Product?> GetProductAsync(string productId)
        {
            var product = await _productCollection
                .Find(p => p.Id == productId)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task AddProductAsync(Product product)
        {
            _productCollection.InsertOneAsync(product);
        }

        public async Task DeleteProductAsync(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            var result = await _productCollection.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                throw new KeyNotFoundException("Product not found");
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            var result = await _productCollection.ReplaceOneAsync(filter, product);
        }

    }
}

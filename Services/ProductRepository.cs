using EStoreAPI.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EStoreAPI.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductRepository(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("products");
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productCollection.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(string? name, string? searchQuery)
        {
            var filter = Builders<Product>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                filter = filter & Builders<Product>.Filter.Eq(p => p.Name, name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                var searchFilter = Builders<Product>.Filter.Or(
                    Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(searchQuery, "i")),
                    Builders<Product>.Filter.Regex(p => p.Description, new MongoDB.Bson.BsonRegularExpression(searchQuery, "i"))
                );
                filter = filter & searchFilter;
            }

            var products = await _productCollection.Find(filter)
                .Sort(Builders<Product>.Sort.Ascending(p => p.Name))
                .ToListAsync();

            return products;
        }

        public async Task<Product?> GetProductAsync(string productId)
        {
            var product = await _productCollection
                .Find(p => p.Id == productId)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<Product> GetProductByProductNumberAsync(string productNumber)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.ProductNumber, productNumber);
            var product = await _productCollection.Find(filter).FirstOrDefaultAsync();
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

﻿using EStoreAPI.Entities;

namespace EStoreAPI.Services
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<IEnumerable<Product>> GetAllProductsAsync(string? name, string? searchQuery, string? productNumber);

        Task<Product?> GetProductAsync(string productId);

        Task<Product?> GetProductByProductNumberAsync(string productNumber);

        Task AddProductAsync(Product product);

        Task DeleteProductAsync(string productId);

        Task UpdateProductAsync(Product product);
    }
}

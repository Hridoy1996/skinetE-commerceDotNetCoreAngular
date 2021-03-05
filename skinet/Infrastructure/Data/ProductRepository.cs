using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoClient _client;
        private IMongoCollection<Product> Product;
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase repository;

        public ProductRepository(IMongoClient client)
        {
            _client = client;
            repository = client.GetDatabase("skinet_db");
            Product = repository.GetCollection<Product>("Product");
        }
       
        public async Task<Product> GetProductByIdAsync(string id)
        {

            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            var product = await Product.FindAsync(filter);
            return product.Single();
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            var filter = Builders<Product>.Filter.Empty;
            var products = await Product.Find(filter).ToListAsync();
            return products;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            var filter = Builders<ProductBrand>.Filter.Empty;
            var productBrands = await repository.GetCollection<ProductBrand>("ProductBrand").Find(filter).ToListAsync();
            return productBrands;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            var filter = Builders<ProductType>.Filter.Empty;
            var productTypes = await repository.GetCollection<ProductType>("ProductType").Find(filter).ToListAsync();
            return productTypes;
        }
    }

}
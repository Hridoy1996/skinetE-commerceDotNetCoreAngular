using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Core.Entities;
using Core.Interfaces;
using API.Dtos;
using AutoMapper;
using MongoDB.Bson;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TestController
    {

        private readonly ILogger<ProductsController> _logger;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        public readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
        private readonly IMongoDatabase database;
        public TestController(IMongoClient client,
                                 IGenericRepository<Product> productRepo,
                                 IGenericRepository<ProductType> productTypeRepo,
                                 IGenericRepository<ProductBrand> productBrandRepo,
                                 IMapper mapper)
        {
            database = client.GetDatabase("skinet_db");
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            database = client.GetDatabase("skinet_db");
        }

        [HttpPost("[action]")]
        public async Task AddEmbedded()
        {    
           
            var productBrands = await _productBrandRepo.GetAllAsync();
            var builder = Builders<Product>.Update;

            foreach(ProductBrand productBrand in productBrands)
            {
                var filter = Builders<Product>.Filter.Eq(x => x.ProductBrandId, productBrand.Id);
                var update = new BsonDocument("$set", new BsonDocument("ProductBrand", productBrand.ToBsonDocument()));
                var result = database.GetCollection<Product>("Product").UpdateManyAsync(filter,update);
            }
            return ;

        }

      
    }
}

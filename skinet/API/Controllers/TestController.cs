﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Core.Entities;
using Core.Interfaces;
using API.Dtos;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

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
        }

        [HttpPost("[action]")]
        public async Task AddEmbeddedBrand()
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
        [HttpPost("[action]")]
        public async Task AddEmbeddedType()
        {

            var producTypes = await _productTypeRepo.GetAllAsync();
            var builder = Builders<Product>.Update;

            foreach (ProductType productType in producTypes)
            {
                var filter = Builders<Product>.Filter.Eq(x => x.ProductBrandId, productType.Id);
                var update = new BsonDocument("$set", new BsonDocument("ProductType", productType.ToBsonDocument()));
                var result = database.GetCollection<Product>("Product").UpdateManyAsync(filter, update);
            }
            return;

        }
        [HttpPost("[action]")]
        public async Task AddSeedData()
        {


            string text = System.IO.File.ReadAllText("C:/Users/TM Hridoy/e-commerce/skinet/Infrastructure/Data/SeedData/products.json");
            var products = BsonSerializer.Deserialize<List<Product>>(text);
            var Product = database.GetCollection<Product>("Product");
            await Product.InsertManyAsync(products);

             text = System.IO.File.ReadAllText("C:/Users/TM Hridoy/e-commerce/skinet/Infrastructure/Data/SeedData/types.json");
             var  types = BsonSerializer.Deserialize<List<ProductType>>(text);
             var Type  = database.GetCollection<ProductType>("ProductType");
             await Type.InsertManyAsync(types);


            text = System.IO.File.ReadAllText("C:/Users/TM Hridoy/e-commerce/skinet/Infrastructure/Data/SeedData/brands.json");
            var brands = BsonSerializer.Deserialize<List<ProductBrand>>(text);
            var Brand = database.GetCollection<ProductBrand>("ProductBrand");
            await Brand.InsertManyAsync(brands);
        }


    }
}

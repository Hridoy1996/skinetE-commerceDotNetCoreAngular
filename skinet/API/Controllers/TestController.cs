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
using MongoDB.Bson.Serialization;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Core.Entities.AccountModels;
using Microsoft.AspNetCore.Authorization;

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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TestController(IMongoClient client,
                                 IGenericRepository<Product> productRepo,
                                 IGenericRepository<ProductType> productTypeRepo,
                                 IGenericRepository<ProductBrand> productBrandRepo,
                                 IMapper mapper,
                                 UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager
                                 )
        {
            database = client.GetDatabase("skinet_db");
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public ActionResult<string> GetSecretText()
        {
            return "hridoy";
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

            string text = System.IO.File.ReadAllText(@"F:\ecommerce\skinetE-commerceDotNetCoreAngular\skinet\Infrastructure\Data\SeedData\products.json");
            var products = BsonSerializer.Deserialize<List<Product>>(text);
            var Product = database.GetCollection<Product>("Product");
            await Product.InsertManyAsync(products);

             text = System.IO.File.ReadAllText(@"F:\ecommerce\skinetE-commerceDotNetCoreAngular\skinet\Infrastructure\Data\SeedData\types.json");
             var  types = BsonSerializer.Deserialize<List<ProductType>>(text);
             var Type  = database.GetCollection<ProductType>("ProductType");
             await Type.InsertManyAsync(types);


            text = System.IO.File.ReadAllText(@"F:\ecommerce\skinetE-commerceDotNetCoreAngular\skinet\Infrastructure\Data\\SeedData\brands.json");
            var brands = BsonSerializer.Deserialize<List<ProductBrand>>(text);
            var Brand = database.GetCollection<ProductBrand>("ProductBrand");
            await Brand.InsertManyAsync(brands);
        }
        [HttpPost("[action]")]
        public async Task<Product> TestUpdate()
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, "1");

            var product = database.GetCollection<Product>("Product");
            var data = await  product.FindAsync(filter);
            var item = data.FirstOrDefault();
            item.Name = "Hridoy";
            return item;
        }
    }
}

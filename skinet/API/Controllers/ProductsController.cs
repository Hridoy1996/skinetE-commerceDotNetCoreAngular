using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Core.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
    
        private readonly ILogger<ProductsController> _logger;
        private readonly IConfiguration _configuration;
        private IMongoCollection<Product> Product;
       
        public ProductsController(ILogger<ProductsController> logger, IMongoClient client, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            var repository = client.GetDatabase(_configuration.GetValue<string>("Database"));
            Product = repository.GetCollection<Product>("Product");
        }
         [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var filter = Builders<Product>.Filter.Empty;
            var products = await Product.Find(filter).ToListAsync();
            return Ok(products);

        }
        
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<List<Product>>> GetProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id , id);
            var product = await Product.Find(filter).ToListAsync();
            return Ok(product);
        }
 
 
    }
}

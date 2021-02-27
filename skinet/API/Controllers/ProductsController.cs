using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
    
        private readonly ILogger<ProductsController> _logger;
        private readonly IConfiguration configuration;
        private IMongoCollection<Product> product;
       
        public ProductsController(ILogger<ProductsController> logger, IMongoClient client)
        {
            _logger = logger;
            var repository = client.GetDatabase(configuration["Database"]);
            product = repository.GetCollection<Product>("Product");
        }
        [HttpGet]
        public ActionResult<List<Product>> GetProducts(){

            var filter = Builders<Product>.Filter.Empty;
            var products = product.Find(filter).ToList();
            return Ok(products);

        }
 
    }
}

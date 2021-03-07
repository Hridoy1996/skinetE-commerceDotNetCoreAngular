using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        public readonly IGenericRepository<ProductType> _productTypeRepo;

        public ProductsController(IGenericRepository<Product> productRepo,
                                 IGenericRepository<ProductType> productTypeRepo,
                                 IGenericRepository<ProductBrand> productBrandRepo)
        {
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _productRepo = productRepo;

        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {

            var products = await _productRepo.GetAllAsync();
            return Ok(products);

        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {

            var product = await _productRepo.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {

            var productTypes = await _productTypeRepo.GetAllAsync();
            return Ok(productTypes);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {

            var productBrands = await  _productBrandRepo.GetAllAsync();
            return Ok(productBrands);
        }


    }
}

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
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {

            var products = await _productRepository.GetProductsAsync();
            return Ok(products);

        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {

            var product = await _productRepository.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {

            var productTypes = await _productRepository.GetProductTypesAsync();
            return Ok(productTypes);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {

            var productBrands = await _productRepository.GetProductBrandsAsync();
            return Ok(productBrands);
        }


    }
}

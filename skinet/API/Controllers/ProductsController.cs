using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Core.Entities;
using Core.Interfaces;
using API.Dtos;
using AutoMapper;
using Core.Specifications;
using Core.Entities.Params;
using API.Helpers;

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
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepo,
                                 IGenericRepository<ProductType> productTypeRepo,
                                 IGenericRepository<ProductBrand> productBrandRepo,
                                 IMapper mapper)
        {
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ProductToReturnDto>>>  GetProducts()
        {

            var products = await _productRepo.GetAllAsync();

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(data);
        } 
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Product>>> GetOrderedProducts(string sort)
        {
            IReadOnlyList<Product> products = null;
            if(sort == "asc")
              products = await _productRepo.ListDescAsync(u => u.Name);
            else
               products = await _productRepo.ListDescAsync(u => u.Name);
            
            return Ok(products);

        }
        [HttpGet("[action]")]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProductsWithPaggination([FromQuery] ProductParams productParams)
        {
            
            IReadOnlyList<Product> products = null;
            int count = 0;
            if (productParams.Sort == "desc")
            {
                products = await _productRepo.ListAscAsync(u => u.Name
                , u => ((u.ProductType.Id == productParams.TypeId) || (u.ProductBrand.Id == productParams.BrandId))
                , productParams.PageIndex, productParams.pageSize);

                count = products.Count;
            }
            else
            {
                products = await _productRepo.ListDescAsync(u => u.Name);
                count = products.Count; 
            }
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.pageSize,
                count, data));

        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(string id)
        {

            var product = await _productRepo.GetByIdAsync(id);
            return _mapper.Map<Product, ProductToReturnDto>(product);
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

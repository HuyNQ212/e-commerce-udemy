using API.Dtos;
using API.Repositories.Interfaces;
using API.Services.CommonServices.Interfaces;
using API.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProductService productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            this.mapper = mapper;
            this.productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await productService.GetProductsIncludeBrandAndType();

            var productsReturn = mapper.Map<List<ProductReturnDto>>(products);

            return Ok(productsReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound($"Cannot found product with id = {id}");
            }

            ProductReturnDto returnDto = mapper.Map<ProductReturnDto>(product);

            return Ok(returnDto);
        }

    }
}

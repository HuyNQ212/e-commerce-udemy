using API.Commons;
using API.Dtos;
using API.Helpers;
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

        [HttpGet("")]
        public async Task<IActionResult> GetProducts(int pageIndex, int pageSize , int? brandId, int? typeId, string? sortOrder)
        {
            try
            {
                var products =
                    await productService.GetProductsIncludeBrandAndType(pageIndex, pageSize, brandId, typeId,
                        sortOrder);

                var productsReturn = products.MappingToProductReturnDtos();
                productsReturn.Ok();

                return Ok(productsReturn);
            }
            catch (Exception e)
            {
                var result = new ResponseData<BaseDto>();
                result.InternalServerError(e.Message);
                return Ok(result);
            }
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

        [HttpGet("Brands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await productService.GetBrands();

            return Ok(brands);
        }

        [HttpGet("Types")]
        public async Task<IActionResult> GetTypes()
        {
            var types = await productService.GetTypes();

            return Ok(types);
        }
    }
}

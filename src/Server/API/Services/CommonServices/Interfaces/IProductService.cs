using API.Commons;
using API.Dtos;
using API.Entities;
using API.Services.BaseService;

namespace API.Services.CommonServices.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        Task<IEnumerable<ProductBrand>> GetBrands();
        Task<List<Product>> GetProductsIncludeBrandAndType();

        Task<ResponseData<Product>> GetProductsIncludeBrandAndType(int pageIndex, int pageSize, int? brandId,
            int? typeId, string? filter = "name");

        Task<IEnumerable<ProductType>> GetTypes();
    }
}
